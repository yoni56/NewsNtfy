using Hanssens.Net;
using HtmlAgilityPack;

namespace Sdk.Articles
{
    public class NullArticle : IArticle
    {
        public string Key => throw new NotImplementedException();

        public string SiteName => throw new NotImplementedException();

        public string Headline => throw new NotImplementedException();

        public string Title => throw new NotImplementedException();

        public string Body => throw new NotImplementedException();

        public string Link => throw new NotImplementedException();

        public IArticle CreateArticle(HtmlNode article)
        {
            throw new NotImplementedException();
        }

        public IArticle ReadCached(ILocalStorage storage)
        {
            throw new NotImplementedException();
        }
    }
}
