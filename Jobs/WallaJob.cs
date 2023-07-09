using FluentScheduler;
using HtmlAgilityPack;
using NewsNotify.Models;

namespace NewsNotify.Jobs
{
    public class WallaJob : BaseJob, IJob
    {
        private readonly Action<IArticle> _update;

        public WallaJob(Action<IArticle> update)
        {
            _update = update;
        }

        public void Execute()
        {
            try
            {
                var uri = new Uri("https://www.walla.co.il/");
                var html = this.GetBrowser().AjaxDownloadString(uri);
                var doc = new HtmlDocument();

                doc.LoadHtml(html);

                var articleNode = doc.DocumentNode.SelectSingleNode("//body//article");

                var linkNode = articleNode.SelectSingleNode("./a");
                var headlineNode = linkNode.SelectSingleNode("./div");
                var titleNode = linkNode.SelectSingleNode("./h2");
                var bodyNode = linkNode.SelectSingleNode("./p");

                var headline = headlineNode?.GetDirectInnerText() ?? "";
                var title = titleNode.GetDirectInnerText();
                var body = bodyNode.GetDirectInnerText();
                var href = linkNode.GetAttributeValue<string>("href", "");

                this._update(new WallaArticle(headline, title, body, href));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                this._update(new NullArticle());
            }
        }
    }
}
