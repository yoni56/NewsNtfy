using HtmlAgilityPack;
using Sdk.Articles;
using Sdk.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using zhk.Models;

namespace zhk
{
    public class ZhkGrabber : IGrabber
    {
        private readonly HtmlDocument _document;
        private const string root_xpath = "//div[@id='main-content']//article";

        public object HttpUtilities { get; private set; }

        public ZhkGrabber(HtmlDocument document)
        {
            this._document = document;
        }

        public IArticle GrabArticleFirstOrDefault()
        {
            try
            {
                return new ArticleBuilder(new ZhkArticle())
                    .SetHeadline(this.GetHeadline())
                    .SetBody(this.GetBody())
                    .SetLink(this.GetHref())
                    .SetImgSrc(HttpUtility.UrlPathEncode(this.GetSrcAttr()))
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
            var a = this._document.DocumentNode.SelectSingleNode($"{root_xpath}/h2/a");
            var raw = a.GetDirectInnerText();

            return raw;
        }

        private string GetBody()
        {
            var p = this._document.DocumentNode.SelectSingleNode($"{root_xpath}/div[@class='entry']/p");
            var raw = p.GetDirectInnerText();

            return raw;
        }

        private string GetHref()
        {
            var a = this._document.DocumentNode.SelectSingleNode($"{root_xpath}/h2/a");
            var href = a.GetAttributeValue<string>("href", "");

            return href;
        }

        private string GetSrcAttr()
        {
            var a = this._document.DocumentNode.SelectSingleNode($"{root_xpath}//img");
            var src = a.GetAttributeValue<string>("src", "");

            return src;
        }
    }
}
