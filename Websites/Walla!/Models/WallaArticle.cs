using Hanssens.Net;
using System.Web;
using Sdk.Articles;

namespace Walla_.Models
{
    public class WallaArticle : IArticle
    {
        public string Key => "WALLA";
        public string SiteName => "Walla! News";
        public string Headline { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Link { get; private set; }

        public WallaArticle(string headline, string title, string body, string link)
        {
            this.Headline = HttpUtility.HtmlDecode(headline);
            this.Title = HttpUtility.HtmlDecode(title);
            this.Body = HttpUtility.HtmlDecode(body);
            this.Link = link;
        }

        public override int GetHashCode()
        {
            var lastIndex = Link.LastIndexOf('/');
            var subString = Link.Substring(lastIndex + 1);

            return Convert.ToInt32(subString);
        }

        public IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<WallaArticle>(Key);
        }
    }
}
