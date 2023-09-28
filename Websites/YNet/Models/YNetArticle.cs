using Hanssens.Net;
using Sdk.Articles;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace YNet.Models
{
    public class YNetArticle : IArticle
    {
        public YNetArticle(string headline, string title, string body, string imgSrc, string link)
        {
            this.Key = "YNET";
            this.SiteName = "ynet";
            this.Headline = HttpUtility.HtmlDecode(headline);
            this.Title = HttpUtility.HtmlDecode(title);
            this.Body = HttpUtility.HtmlDecode(body);
            this.Link = link;
            this.ImgSrc = imgSrc;
        }

        public override int GetHashCode()
        {
            //var lastSlash = this.Link.LastIndexOf('/');
            //var lastHash = this.Link.LastIndexOf('#');

            //var subString = lastHash > -1 ?
            //    this.Link.Substring(lastSlash + 1, lastHash - lastSlash - 1) :
            //    this.Link.Substring(lastSlash + 1);

            //return GetUniqueHashCode(subString);

            return this.Link.GetHashCode();
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

        public override IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<YNetArticle>(this.Key);
        }
    }
}
