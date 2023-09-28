using DotNetEnv;
using FluentScheduler;
using Hanssens.Net;
using NewsNotify.Registries;
using NewsNotify.Services;
using Sdk.Articles;
using Sdk.Base;
using System.Diagnostics;
using System.Reflection;

Console.Title = "NewsNtfy";

Env.Load();

var ntfy = new NtfySh();
var cache = new LocalStorage();

object updatelock = new object();
var minutes = Env.GetInt("minutes");

var Registries = GetJobs()
    .Select(x => new SiteRegistry(x, minutes))
    .ToArray();


JobManager.Initialize(Registries);

var instance = Process.GetCurrentProcess();

Console.WriteLine($"{Registries.Count()} Registries initialized, Press any key to exit... PID is {instance.Id}");
Console.ReadKey();

List<IJob?> GetJobs()
{
#if DEBUG
    var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Websites");
#else
      var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Websites");
#endif
    var modules = Directory.EnumerateFiles(path, "*.Site.dll", SearchOption.AllDirectories).ToList();

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
    lock (updatelock)
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

        ntfy.sendMessage(article.SiteName, $"{article.Headline}\n{article.Title}", article.ImgSrc, article.Link);

        cache.Store(article.Key, article);
        cache.Persist();
    }
}