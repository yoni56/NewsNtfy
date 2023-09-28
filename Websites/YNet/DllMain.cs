using HtmlAgilityPack;
using Sdk.Articles;
using Sdk.Base;
using YNet.Models;

namespace YNet
{
    public class DllMain : DllBase
    {
        public override void Execute()
        {
            try
            {
                var doc = this.GetHtmlDocument("https://www.ynet.co.il/home/0,7340,L-8,00.html");
                var headline = this.GetHeadline(doc);
                var title = this.GetTitle(doc);
                var body = this.GetBody(doc);
                var href = this.GetHref(doc);
                var src = this.GetSrcAttr(doc);

                this.RaiseUpdate(new YNetArticle(headline, title, body, src, href));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.RaiseUpdate(new NullArticle());
            }
        }

        private string GetHeadline(HtmlDocument html)
        {
            var img = html.DocumentNode.SelectSingleNode("//img[@class='SiteImageMedia']");
            var raw = img.GetAttributeValue<string>("alt", "");

            return raw;
        }

        private string GetTitle(HtmlDocument html)
        {
            var span = html.DocumentNode.SelectSingleNode("//h1[@class='slotTitle']/span");
            var raw = span.GetDirectInnerText();

            return raw;
        }

        private string GetBody(HtmlDocument html)
        {
            var span = html.DocumentNode.SelectSingleNode("//div[@class='slotSubTitle']/span");
            var raw = span.GetDirectInnerText();

            return raw;
        }

        private string GetHref(HtmlDocument html)
        {
            var a = html.DocumentNode.SelectSingleNode("//div[@class='slotView']//a");
            var href = a.GetAttributeValue<string>("href", "");

            return href;
        }

        private string GetSrcAttr(HtmlDocument html)
        {
            var img = html.DocumentNode.SelectSingleNode("//div[@class='slotView']//img");
            var src = img.GetAttributeValue<string>("src", "");

            return src;
        }
    }
}
