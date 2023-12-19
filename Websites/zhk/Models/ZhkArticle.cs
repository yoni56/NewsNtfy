using Hanssens.Net;
using Sdk.Articles;

namespace zhk.Models
{
    public class ZhkArticle : IArticle
    {
        public ZhkArticle()
        {
            this.Key = "ZHK_Karmiel";
            this.SiteName = "ZoharNet, Karmie'l";
        }

        public override int GetHashCode()
        {
            return this.GetHashCodeMd5(this.Link);
        }

        public override IArticle ReadCached(ILocalStorage storage)
        {
            return storage.Get<ZhkArticle>(this.Key);
        }
    }
}
