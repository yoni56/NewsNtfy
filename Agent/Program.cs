using DotNetEnv;
using FluentScheduler;
using Hanssens.Net;
using Sdk.Articles;
using NewsNotify.Services;
using NewsNotify.Registries;
using Sdk.Base;
using System.Reflection;

Console.Title = "NewsNotfy";

Env.Load();

var ntfy = new NtfySh();
var cache = new LocalStorage();

var minutes = Env.GetInt("minutes");

var Registries = GetJobs()
    .Select(x => new SiteRegistry(x, minutes))
    .ToArray();

JobManager.Initialize(Registries);
Console.WriteLine("registries initialized.");

Console.ReadKey();

List<IJob?> GetJobs()
{
    var modules = Directory.EnumerateFiles(
        "..\\Websites\\", 
        "*.Site.dll", 
        SearchOption.AllDirectories
    ).ToList();

    var items = modules.Select(path =>
    {
        var assembly = Assembly.LoadFrom(path);
        var typeName = assembly.ExportedTypes
            .Single(x => x.Name.Equals("DllMain")).FullName;
        var instance = assembly.CreateInstance(typeName);

        ((DllBase)instance).SetUpdate(Update);
        return (IJob)instance;
    }).ToList();

    return items!;
}

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