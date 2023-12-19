using FluentScheduler;
using HtmlAgilityPack;
using Nito.AsyncEx;
using ScrapySharp.Network;
using Sdk.Articles;

namespace Sdk.Base
{
    public abstract class IDllMain : IJob
    {
        private Action<IArticle> _updateCallback;
        private readonly ScrapingBrowser _browser;

        protected IDllMain()
        {
            this._browser = new ScrapingBrowser();
        }

        protected string GetPageHtmlContent(string pageUrl)
        {
            return AsyncContext.Run(async () => await this._browser.AjaxDownloadStringAsync(new Uri(pageUrl)));
        }

        public abstract void Execute();

        public void SetUpdateCallback(Action<IArticle> updateCallback)
        {
            this._updateCallback = updateCallback;
        }

        protected void RaiseUpdateEvent(IArticle article)
        {
            this._updateCallback(article);
        }

        protected HtmlDocument GetPageDom(string pageUrl)
        {
            var content = this.GetPageHtmlContent(pageUrl);
            var document = new HtmlDocument();

            document.LoadHtml(content);

            return document;
        }
    }
}
