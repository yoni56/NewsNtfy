using Hanssens.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Sdk.Articles;

namespace YNet.Models
{
    public class YNetArticle : IArticle
    {
        public string Key => "YNET";
        public string SiteName => "ynet";
        public string Headline { get; private set; }
        public string Title { get; private set; }
        public string Body { get; private set; }
        public string Link { get; private set; }

        public YNetArticle(string headline, string title, string body, string link)
        {
            this.Headline = HttpUtility.HtmlDecode(headline);
            this.Title = HttpUtility.HtmlDecode(title);
            this.Body = HttpUtility.HtmlDecode(body);
            this.Link = link;
        }

        public override int GetHashCode()
        {
            var lastSlash = Link.LastIndexOf('/');
            var lastHash = Link.LastIndexOf('#');

            var subString = lastHash > -1 ?
                Link.Substring(lastSlash + 1, lastHash - lastSlash - 1) :
                Link.Substring(lastSlash + 1);

            return GetUniqueHashCode(subString);
        }

        private int GetUniqueHashCode(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                int hash = BitConverter.ToInt32(hashBytes.Take(4).ToArray(), 0);
                return hash;
            }
        }

        public IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<YNetArticle>(Key);
        }
    }
}
