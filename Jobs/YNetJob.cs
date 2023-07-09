using FluentScheduler;
using HtmlAgilityPack;
using NewsNotify.Models;

namespace NewsNotify.Jobs
{
    public class YNetJob : BaseJob, IJob
    {
        private readonly Action<IArticle> _update;

        public YNetJob(Action<IArticle> update)
        {
            this._update = update;
        }

        public void Execute()
        {
            try
            {
                var uri = new Uri("https://www.ynet.co.il/home/0,7340,L-8,00.html");
                var html = this.GetBrowser().AjaxDownloadString(uri);
                var doc = new HtmlDocument();

                doc.LoadHtml(html);

                var articleNode = doc.DocumentNode.SelectSingleNode("//div[@class='slotView']");

                var linkNode = articleNode.SelectSingleNode("./div[1]/a");
                var headlineNode = articleNode.SelectSingleNode("./div[2]//img");
                var titleNode = linkNode.SelectSingleNode("./h1/span");
                var bodyNode = linkNode.SelectSingleNode("./div/span");

                var headline = headlineNode.GetAttributeValue<string>("alt", "");
                var title = titleNode.GetDirectInnerText();
                var body = bodyNode.GetDirectInnerText();
                var href = linkNode.GetAttributeValue<string>("href", "");

                this._update(new YNetArticle(headline, title, body, href));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this._update(new NullArticle());
            }
        }
    }
}
