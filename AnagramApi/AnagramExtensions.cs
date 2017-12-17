using AnCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AngramApi
{
  public static class AnagramExtensions
  {
    public static IAnagramResolverService AddFileBasedAnagramService(this IServiceCollection collection, IConfiguration config)
    {
      var folder = config["WordListConfig:FileProvider:Folder"];
      var fileName = config["WordListConfig:FileProvider:BaseListName"];
      var extra = config["WordListConfig:FileProvider:SupplementalName"];
      var exclusionList = config["WordListConfig:FileProvider:BadWords"];

      if (string.IsNullOrWhiteSpace(folder))
      {
        throw new ArgumentException("invalid folder configuration");
      }

      if (string.IsNullOrWhiteSpace(fileName))
      {
        throw new ArgumentException("invalid file name configuration");
      }

      var sources =WordListFileSourceFactory.GetWordListFromPath(folder, fileName, extra, exclusionList);
      var resolverService = new AnagramResolverService(sources, (w) => new WordGenerator(w));



      collection.AddSingleton<IAnagramResolverService, AnagramResolverService>((s) => { return resolverService; });
      return null;
    }

  }
}
