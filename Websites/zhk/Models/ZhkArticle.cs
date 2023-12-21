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

        public override IArticle GetCached(ILocalStorage storage)
        {
            return storage.Get<ZhkArticle>(this.Key);
        }
    }
}
