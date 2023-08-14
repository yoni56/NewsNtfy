using Hanssens.Net;
using HtmlAgilityPack;

namespace Sdk.Articles
{
    public class NullArticle : IArticle
    {
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override IArticle ReadCached(ILocalStorage storage)
        {
            throw new NotImplementedException();
        }
    }
}
