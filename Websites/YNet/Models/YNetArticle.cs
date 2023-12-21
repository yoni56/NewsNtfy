using Hanssens.Net;
using Sdk.Articles;

namespace YNet.Models
{
    public class YNetArticle : IArticle
    {
        public YNetArticle()
        {
            this.Key = "YNET";
            this.SiteName = "ynet";
        }

        public override IArticle GetCached(ILocalStorage storage)
        {
            return storage.Get<YNetArticle>(this.Key);
        }
    }
}
