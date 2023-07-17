using FluentScheduler;
using HtmlAgilityPack;
using Walla_.Models;
using Sdk.Articles;
using Sdk.Base;

namespace Walla_
{
    public class DllMain : DllBase, IJob
    {
        public override void Execute()
        {
            try
            {
                var doc = this.GetHtmlDocument("https://www.walla.co.il/");

                var articleNode = doc.DocumentNode.SelectSingleNode("//body//article");

                var linkNode = articleNode.SelectSingleNode("./a");
                var headlineNode = linkNode.SelectSingleNode("./div");
                var titleNode = linkNode.SelectSingleNode("./h2");
                var bodyNode = linkNode.SelectSingleNode("./p");

                var headline = headlineNode?.GetDirectInnerText() ?? "";
                var title = titleNode.GetDirectInnerText();
                var body = bodyNode.GetDirectInnerText();
                var href = linkNode.GetAttributeValue<string>("href", "");

                this.RaiseUpdate(new WallaArticle(headline, title, body, href));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.RaiseUpdate(new NullArticle());
            }
        }
    }
}
