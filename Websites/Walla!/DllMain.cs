using Hanssens.Net;
using HtmlAgilityPack;
using ImageMagick;
using Nito.AsyncEx;
using Sdk.Articles;
using Sdk.Base;
using Sdk.ImageShack;
using Sdk.ImgBB;
using System.Reflection;
using Walla_.Models;

namespace Walla_
{
    public class DllMain : IDllMain
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
                var pageDom = this.GetPageDom("https://www.walla.co.il/");
                var article = new WallaGrabber(pageDom).GrabArticleFirstOrDefault();
                
                var imgSrcHash = article.ImgSrc.GetHashCode();
                var cachedImgSrc = new ImgHash();

                // check for existing hash in cache
                if (this.cache.Exists(nameof(cachedImgSrc)))
                {
                    cachedImgSrc = this.cache.Get<ImgHash>(nameof(cachedImgSrc));

                    // set the article's imgsrc to point to the cached version
                    article.ImgSrc = cachedImgSrc.url;
                }

                // we compare the current hash to the cached copy to prevent duplicate of the article's image!
                if (cachedImgSrc.hash != imgSrcHash)
                {
                    var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                    var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ".jpg";
                    var filePath = Path.Combine(baseDir, fileName);

                    this.MagickDownloadImageAsJpg(article.ImgSrc, filePath);
                    var uploadResult = this.imageShackClient.UploadImageByFile(fileName, filePath);

                    // delete afterwards
                    System.IO.File.Delete(filePath);

                    cachedImgSrc.hash = imgSrcHash;
                    cachedImgSrc.url = uploadResult.links.image_link;

                    // save in the cache last imgHash
                    this.cache.Store(nameof(cachedImgSrc), cachedImgSrc);

                    // update img link in the article
                    article.ImgSrc = uploadResult.links.image_link;
                }

                this.RaiseUpdateEvent(article);
            }
            catch (Exception ex)
            {
                this.RaiseUpdateEvent(new ExceptionArticle()
                {
                    OptionalInfo = ex.ToString()
                });
            }
        }

        [Obsolete]
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

        private void MagickDownloadImageAsJpg(string imageUrl, string filePath)
        {
            using HttpClient client = new HttpClient();

            var bytes = AsyncContext.Run(async () => await client.GetByteArrayAsync(imageUrl));

            using MagickImage image = new MagickImage(bytes)
            {
                Format = MagickFormat.Jpeg
            };

            image.Write(filePath);
        }
    }
}
