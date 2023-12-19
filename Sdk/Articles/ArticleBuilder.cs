using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Sdk.Articles
{
    public class ArticleBuilder
    {
        private readonly IArticle _article;

        public ArticleBuilder(IArticle article)
        {
            this._article = article;
        }

        public ArticleBuilder SetHeadline(string headline)
        {
            this._article.Headline = HttpUtility.HtmlDecode(headline);
            return this;
        }

        public ArticleBuilder SetTitle(string title)
        {
            this._article.Title = HttpUtility.HtmlDecode(title);
            return this;
        }

        public ArticleBuilder SetBody(string body)
        {
            this._article.Body = HttpUtility.HtmlDecode(body);
            return this;
        }

        public ArticleBuilder SetLink(string link)
        {
            this._article.Link = link;
            return this;
        }

        public ArticleBuilder SetImgSrc(string src)
        {
            this._article.ImgSrc = src;
            return this;
        }

        public ArticleBuilder SetOptionalInfo(string info)
        {
            this._article.OptionalInfo = info;
            return this;
        }

        public IArticle Build()
        {
            return this._article;
        }
    }
}
