using Hanssens.Net;

namespace NewsNotify.Models
{
    public interface IArticle
    {
        string Key { get; }
        string SiteName { get; }
        string Headline { get; }
        string Title { get; }
        string Body { get; }
        string Link { get; }
        int GetHashCode();
        IArticle ReadCached(LocalStorage storage);
    }
}
