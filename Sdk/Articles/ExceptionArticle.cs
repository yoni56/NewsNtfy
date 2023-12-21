using Hanssens.Net;

namespace Sdk.Articles
{
    public class ExceptionArticle : IArticle
    {
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override IArticle GetCached(ILocalStorage storage)
        {
            throw new NotImplementedException();
        }
    }
}
