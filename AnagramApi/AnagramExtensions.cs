using AnCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
      var sources =WordListFileSourceFactory.GetWordListFromPath(folder, fileName, extra, exclusionList);

      var an = new AnagramResolverService(sources, (w) => new StringPermutation(w));



      collection.AddSingleton<IAnagramResolverService, AnagramResolverService>((s) => { return an; });
      return null;
    }

  }
}
