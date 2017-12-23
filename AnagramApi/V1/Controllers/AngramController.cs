using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnCore;
using AnagramApi.Telemetry;
using System.Diagnostics;

namespace AnagramApi.Controllers
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [Produces("application/json")]
  public class AnagramController : Controller
  {
    private readonly ILogger<AnagramController> _logger;
    private readonly IAnagramResolverService _resolver;
    private IAnagramResolverMetric _metric;

    public AnagramController(IAnagramResolverService resolver,
      ILogger<AnagramController> logger,
      IAnagramResolverMetric metric)
    {
      _resolver = resolver;
      _logger = logger;
      _metric = metric;
    }

    // GET: api/anagram
    [HttpGet]
    public string Get()
    {
      return "try /api/v1/anagram/silent?language=en";
    }

    // GET: api/anagram/mouse?language=en
    [HttpGet("{word}", Name ="Get")]
    public async Task<IActionResult> GetAnagrams(string word, [FromQuery]string language)
    {
      IEnumerable<string> anagrams = null;
      try
      {
        if (ValidParameters(word, language, _logger))
        {
          var stopWatch = Stopwatch.StartNew();
          anagrams = await _resolver.GetAnagramsAsync(word, language);
          TrackTime(stopWatch);
        }
        else
        {
          anagrams = new string[] {"invalid request" };
          return new BadRequestObjectResult(anagrams);
        }
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "{0},{1} {2}", word, language, ex);
        return new BadRequestObjectResult(new string[] { "error", ex.Message }); // this can be a concern on information disclosure.
       }
      return Ok(anagrams);
    }

    #region CustomParameterValidation
    private bool ValidParameters(string word, string language, ILogger<AnagramController> logger)
    {
      if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(language))
      {
        //logger.LogWarning("empty word or language");
        return false;
      }
      if (word.Length > 11)
      {
        logger.LogWarning("word too long");
        //throw new ArgumentException("too long max word length is 11");
        return false;
      }
      if (string.Compare("en", language, StringComparison.OrdinalIgnoreCase) != 0)
      {
        logger.LogInformation("{0} language request",language);
        return false;
      }
      return true;
    }

    private void TrackTime(Stopwatch watch)
    {
      watch.Stop();
      _metric.TrackComputeTime(watch.Elapsed);
    }
    #endregion
  }
}
