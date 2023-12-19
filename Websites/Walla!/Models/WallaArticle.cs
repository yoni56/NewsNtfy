using Hanssens.Net;
using Sdk.Articles;

namespace Walla_.Models
{
    public class WallaArticle : IArticle
    {
        public WallaArticle()
        {
            this.Key = "WALLA";
            this.SiteName = "Walla! News";
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeMd5(this.Link);
        }

        public override IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<WallaArticle>(this.Key);
        }
    }
}
