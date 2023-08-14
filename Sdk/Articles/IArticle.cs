using Hanssens.Net;

namespace Sdk.Articles
{
    public abstract class IArticle
    {
        public string Key { get; protected set; }
        public string SiteName { get; protected set; }
        public string Headline { get; protected set; }
        public string Title { get; protected set; }
        public string Body { get; protected set; }
        public string Link { get; protected set; }
        public string ImgSrc { get; protected set; }
        public override abstract int GetHashCode();
        public abstract IArticle ReadCached(ILocalStorage storage);
    }
}
