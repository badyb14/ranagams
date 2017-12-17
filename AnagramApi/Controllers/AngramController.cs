using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AnCore;
using AngramApi.Models;

namespace anagramApi.Controllers
{
  [Produces("application/json")]
  [Route("api/anagram")]
  public class AnagramController : Controller
  {

    private readonly AnagramContext _context;
    private readonly IAnagramResolverService _resolver;

    public AnagramController(AnagramContext context, IAnagramResolverService resolver)
    {
      _resolver = resolver;
      _context = context;
    }

    // GET: api/anagram
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }


    // GET: api/anagram/mouse?language=en
    [HttpGet("{word}", Name ="Get")]
    public IEnumerable<string> GetAnagrams(string word,[FromQuery]string language)
    {
      //TO to validate parameters.
      //TODO execute this method as task
      //var ar =_context.anagramItems.FirstOrDefault(a => string.Compare(a.Word, word, StringComparison.OrdinalIgnoreCase) == 0);
      //if (ar == null)
      //{
      //  _context.Add(new anagramItem() { Word = word, Language = "en", Requested = 1 });
      //}
      //else
      //{
      //  ar.Requested++;
      //}
      //_context.SaveChangesAsync();

      var r = _resolver.GetAnagrams(word,language) ;
      return r;
      //return new string[] { "a1", "a2",word };
    }

    
    

    // POST: api/anagram
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT: api/anagram/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE: api/ApiWithActions/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
  }
}
