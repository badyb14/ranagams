using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnCore;
using AngramApi.Models;
using Microsoft.Extensions.Logging;

namespace anagramApi.Controllers
{
  [ApiVersion("1.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  [Produces("application/json")]
  public class AnagramController : Controller
  {
    private readonly ILogger<AnagramController> _logger;
    private readonly IAnagramResolverService _resolver;

    public AnagramController(IAnagramResolverService resolver,
      ILogger<AnagramController> logger)
    {
      _resolver = resolver;
      _logger = logger;
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
      IEnumerable<string> anagrams;
      try
      {
        ValidateParameters(word, language);
        anagrams = await _resolver.GetAnagramsAsync(word, language);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "{0},{1}", word, language);
        return new BadRequestObjectResult(new string[] { "error", ex.Message });
       }
      return Ok(anagrams);
      //return new string[] { "a1", "a2",word };
    }

    #region CustomParameterValidation
    private void ValidateParameters(string word, string language)
    {
      if (string.IsNullOrWhiteSpace(word) || string.IsNullOrWhiteSpace(language))
      {
        throw new ArgumentException("null or emtpy strings");
      }
      if (word.Length > 11)
      {
        throw new ArgumentException("too long max word length is 11");
      }
      if (string.Compare("en", language, StringComparison.OrdinalIgnoreCase) != 0)
      {
        throw new ArgumentException("invalid language");
      }
    }
    #endregion
  }
}
