using Hanssens.Net;
using HtmlAgilityPack;
using ImageMagick;
using Nito.AsyncEx;
using Sdk.Articles;
using Sdk.Base;
using Sdk.ImageShack;
using Sdk.ImgBB;
using Walla_.Models;

namespace Walla_
{
    public class DllMain : DllBase
    {
        private readonly LocalStorage cache;
        private readonly ImageShackClient imageShackClient;

        public DllMain()
        {
            this.cache = new LocalStorage();
            this.imageShackClient = new ImageShackClient();
        }

        public override void Execute()
        {
            try
            {
                var htmlDoc = this.GetHtmlDocument("https://www.walla.co.il/");
                var headline = this.GetHeadline(htmlDoc);
                var title = this.GetTitle(htmlDoc);
                var body = this.GetBody(htmlDoc);
                var href = this.GetHref(htmlDoc);
                var srcset = this.GetSrcAttr(htmlDoc);

                // since the image is in .webp format,
                // we read the bytes from the remote url
                // convert to base64, and upload to imgbb,
                // and then display in .jpg format so it will 
                // successfully render in ntfy.sh
                //var imgSrcBytes = this.GetImageBytesAsJpg(srcset);
                //var imgBase64Encoded = Convert.ToBase64String(imgSrcBytes);

                var sessionHash = srcset.GetHashCode();
                var cachedHash = new ImgHash();

                if (cache.Exists(nameof(cachedHash)))
                {
                    cachedHash = cache.Get<ImgHash>(nameof(cachedHash));
                }

                if (cachedHash.hash == sessionHash)
                {
                    this.RaiseUpdate(new WallaArticle(headline, title, body, cachedHash.url, href));
                    return;
                }

                var baseDir = AppDomain.CurrentDomain.BaseDirectory;
                var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
                var filePath = Path.Combine(baseDir, fileName);

                this.SaveImageAsJpg(srcset, filePath);
                var apiResult = this.imageShackClient.UploadImageByFilename(fileName, filePath);

                // delete afterwards
                System.IO.File.Delete(filePath);

                cachedHash.hash = sessionHash;
                cachedHash.url = apiResult.links.image_link;

                this.RaiseUpdate(new WallaArticle(headline, title, body, cachedHash.url, href));

                // save in the cache last imghash
                this.cache.Store(nameof(cachedHash), cachedHash);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.RaiseUpdate(new NullArticle());
            }
        }

        private string GetHeadline(HtmlDocument html)
        {
            // short headline below article
            var div = html.DocumentNode.SelectSingleNode("//div[@class='roof']");

            if (div != null)
            {
                return div.GetDirectInnerText();
            }

            // long headline below media div (outside article)
            var img = html.DocumentNode.SelectSingleNode("//picture[contains(@class, 'main-media')]/img");
            var raw = img.GetAttributeValue<string>("title", "");

            return raw;
        }

        private string GetTitle(HtmlDocument html)
        {
            var h2 = html.DocumentNode.SelectSingleNode("//body//article/a/h2");
            var raw = h2.GetDirectInnerText();
            return raw;
        }

        private string GetBody(HtmlDocument html)
        {
            var p = html.DocumentNode.SelectSingleNode("//body//article/a/p");
            var raw = p.GetDirectInnerText();
            return raw;
        }

        private string GetHref(HtmlDocument html)
        {
            var a = html.DocumentNode.SelectSingleNode("//body//article/a");
            var href = a.GetAttributeValue<string>("href", "");
            return href;
        }

        private string GetSrcAttr(HtmlDocument html)
        {
            var imgNode = html.DocumentNode.SelectSingleNode("//picture//img");
            var imgSrc = imgNode.GetAttributeValue<string>("srcset", "");

            var breakIdx = imgSrc.IndexOf("969w") - 1; // -1 for empty space
            var srcset = imgSrc.Substring(0, breakIdx);

            return srcset;
        }

        private byte[] GetImageBytesAsJpg(string imgUrl)
        {
            using HttpClient client = new HttpClient();

            var bytes = AsyncContext.Run(async () => await client.GetByteArrayAsync(imgUrl));
            using MagickImage image = new MagickImage(bytes)
            {
                Format = MagickFormat.Jpeg
            };

            using MemoryStream buffer = new MemoryStream();
            image.Write(buffer);

            return buffer.ToArray();
        }

        private void SaveImageAsJpg(string remotePath, string localPath)
        {
            using HttpClient client = new HttpClient();

            var bytes = AsyncContext.Run(async () => await client.GetByteArrayAsync(remotePath));
            using MagickImage image = new MagickImage(bytes)
            {
                Format = MagickFormat.Jpeg
            };

            image.Write(localPath);
        }
    }
}
