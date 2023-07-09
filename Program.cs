using DotNetEnv;
using FluentScheduler;
using Hanssens.Net;
using NewsNotify.Jobs;
using NewsNotify.Models;
using NewsNotify.Services;
using NewsNotify.Registries;

Console.Title = "NewsNotfy";

Env.Load();

var ntfy = new NtfySh();
var cache = new LocalStorage();

var minutes = Env.GetInt("minutes");

var registries = new SiteRegistry[]
{
    new SiteRegistry(new YNetJob(Update), minutes),
    new SiteRegistry(new WallaJob(Update), minutes)
};

JobManager.Initialize(registries);
Console.WriteLine("registries initialized.");

Console.ReadKey();

void Update(IArticle article)
{
    if (article is NullArticle)
    {
        return;
    }

    var hashCode = article.GetHashCode();

    if (cache.Exists(article.Key))
    {
        var cachedArticle = article.ReadCached(cache);
        var cachedHashCode = cachedArticle.GetHashCode();

        if (hashCode == cachedHashCode)
        {
            // same article, we exit
            Console.WriteLine($"{hashCode} same article for {article.Key}, exit.");
            return;
        }
    }

    Console.WriteLine($"{hashCode} new article for {article.Key}, publish.");

    ntfy.sendMessage(article.SiteName, $"{article.Headline}\n{article.Title}", article.Link);

    cache.Store(article.Key, article);
    cache.Persist();
}