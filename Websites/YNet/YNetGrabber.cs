using HtmlAgilityPack;
using Sdk.Articles;
using Sdk.Base;
using YNet.Models;

namespace YNet
{
    public class YNetGrabber : IGrabber
    {
        private readonly HtmlDocument _document;

        public YNetGrabber(HtmlDocument document)
        {
            this._document = document;
        }

        public IArticle GrabArticleFirstOrDefault()
        {
            try
            {
                return new ArticleBuilder(new YNetArticle())
                    .SetHeadline(this.GetHeadline())
                    .SetTitle(this.GetTitle())
                    .SetBody(this.GetBody())
                    .SetLink(this.GetHref())
                    .SetImgSrc(this.GetSrcAttr())
                    .Build();
            }
            catch (Exception ex)
            {
                return new ArticleBuilder(new ExceptionArticle())
                    .SetOptionalInfo(ex.ToString())
                    .Build();
            }
        }

        private string GetHeadline()
        {
            var img = this._document.DocumentNode.SelectSingleNode("//img[@class='SiteImageMedia']");
            var raw = img.GetAttributeValue<string>("alt", "");

            return raw;
        }

        private string GetTitle()
        {
            var span = this._document.DocumentNode.SelectSingleNode("//h1[@class='slotTitle']/span");
            var raw = span.GetDirectInnerText();

            return raw;
        }

        private string GetBody()
        {
            var span = this._document.DocumentNode.SelectSingleNode("//div[@class='slotSubTitle']/span");
            var raw = span.GetDirectInnerText();

            return raw;
        }

        private string GetHref()
        {
            var a = this._document.DocumentNode.SelectSingleNode("//div[@class='slotView']//a");
            var href = a.GetAttributeValue<string>("href", "");

            return href;
        }

        private string GetSrcAttr()
        {
            var img = this._document.DocumentNode.SelectSingleNode("//div[@class='slotView']//img");
            var src = img.GetAttributeValue<string>("src", "");

            return src;
        }
    }
}
