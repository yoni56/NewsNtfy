using FluentScheduler;
using HtmlAgilityPack;
using YNet.Models;
using Sdk.Base;
using Sdk.Articles;

namespace YNet
{
    public class DllMain : DllBase, IJob
    {
        public override void Execute()
        {
            try
            {
                var doc = this.GetHtmlDocument("https://www.ynet.co.il/home/0,7340,L-8,00.html");

                var articleNode = doc.DocumentNode.SelectSingleNode("//div[@class='slotView']");

                var linkNode = articleNode.SelectSingleNode("./div[1]/a");
                var headlineNode = articleNode.SelectSingleNode("./div[2]//img");
                var titleNode = linkNode.SelectSingleNode("./h1/span");
                var bodyNode = linkNode.SelectSingleNode("./div/span");

                var headline = headlineNode.GetAttributeValue<string>("alt", "");
                var title = titleNode.GetDirectInnerText();
                var body = bodyNode.GetDirectInnerText();
                var href = linkNode.GetAttributeValue<string>("href", "");

                this.RaiseUpdate(new YNetArticle(headline, title, body, href));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this.RaiseUpdate(new NullArticle());
            }
        }
    }
}
