using AnagramApi.Telemetry;
using AnCore;
using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace AnagramApi
{
  public static class AnagramExtensions
  {
    internal static ILoggerFactory LoggerFactory;

    public static IServiceCollection AddFileBasedAnagramService(this IServiceCollection collection, IConfiguration config)
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

      AnagramResolverService resolverService = null;
      //could take ILoggerFactory wrap it and make it a parameter for consumption.
      var logger = LoggerFactory.CreateLogger("AnagramApi.AnagramResolverService");
      var current = Directory.GetCurrentDirectory();
      var path = Path.Combine(current, folder);
      var baseNames = Directory.GetFiles(path, fileName);

      logger.LogWarning("Configuration path {0}. File pattern {1}. File Count {2}", path, fileName, baseNames.Length);

      var sourceFactory = new WordListFileSourceFactory(baseNames, path, extra, exclusionList);
      var sources = sourceFactory.GetWordList(true);
      resolverService = new AnagramResolverService(sources, (w) => new WordGenerator(w));

      collection.AddSingleton<IAnagramResolverService, AnagramResolverService>((s) => { return resolverService; });

      EnsureAnagramServiceTelemetry(collection);

      return collection;
    }

    private static void EnsureAnagramServiceTelemetry(IServiceCollection collection)
    {
      var telemetryClient = new TelemetryClient();
      var metricCollection = new AnagramResolverMetricCollection(telemetryClient);

      collection.AddSingleton<IAnagramResolverMetric, AnagramResolverMetricCollection>((s)=> { return metricCollection; });
    }

  }
}
