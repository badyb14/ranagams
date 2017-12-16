using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AngramApi.Models;
using AnCore;

namespace AngramApi.Controllers
{
  [Produces("application/json")]
  [Route("api/Angram")]
  public class AngramController : Controller
  {

    private readonly AngramContext _context;
    private readonly IAnagramResolverService _resolver;

    public AngramController(AngramContext context, IAnagramResolverService resolver)
    {
      _resolver = resolver;
      _context = context;
    }

    // GET: api/Angram
    [HttpGet]
    public IEnumerable<string> Get()
    {
      return new string[] { "value1", "value2" };
    }


    // GET: api/Angram/5
    [HttpGet("{word}", Name ="Get")]
    public IEnumerable<string> GetAnagrams(string word,[FromQuery]string language)
    {
      //TO to validate parameters.
      //TODO execute this method as task
      //var ar =_context.AngramItems.FirstOrDefault(a => string.Compare(a.Word, word, StringComparison.OrdinalIgnoreCase) == 0);
      //if (ar == null)
      //{
      //  _context.Add(new AngramItem() { Word = word, Language = "en", Requested = 1 });
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

    
    

    // POST: api/Angram
    [HttpPost]
    public void Post([FromBody]string value)
    {
    }

    // PUT: api/Angram/5
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
