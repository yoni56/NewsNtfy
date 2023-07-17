using FluentScheduler;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
using Nito.AsyncEx;
using ScrapySharp.Network;
using Sdk.Articles;
using System.ComponentModel;

namespace Sdk.Base
{
    public delegate void OnArticle(IArticle e);

    public abstract class DllBase : IJob
    {
        private OnArticle _update;
        private readonly ScrapingBrowser _browser;

        protected DllBase()
        {
            this._browser = new ScrapingBrowser();
        }

        protected string GetHtmlString(string url)
        {
            return AsyncContext.Run(async () => await this._browser.AjaxDownloadStringAsync(new Uri(url)));
        }

        public abstract void Execute();

        public void SetUpdate(OnArticle update)
        {
            this._update = update;
        }

        protected void RaiseUpdate(IArticle article)
        {
            this._update(article);
        }

        protected HtmlDocument GetHtmlDocument(string url)
        {
            var html = this.GetHtmlString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            return doc;
        }
    }
}
