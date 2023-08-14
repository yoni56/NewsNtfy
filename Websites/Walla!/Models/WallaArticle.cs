using Hanssens.Net;
using System.Web;
using Sdk.Articles;

namespace Walla_.Models
{
    public class WallaArticle : IArticle
    {
        public WallaArticle(string headline, string title, string body, string imgSrc, string link)
        {
            this.Key = "WALLA";
            this.SiteName = "Walla! News";
            this.Headline = HttpUtility.HtmlDecode(headline);
            this.Title = HttpUtility.HtmlDecode(title);
            this.Body = HttpUtility.HtmlDecode(body);
            this.Link = link;
            this.ImgSrc = imgSrc;
        }

        public override int GetHashCode()
        {
            var lastIndex = this.Link.LastIndexOf('/');
            var subString = this.Link.Substring(lastIndex + 1);

            return Convert.ToInt32(subString);
        }

        public override IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<WallaArticle>(this.Key);
        }
    }
}
