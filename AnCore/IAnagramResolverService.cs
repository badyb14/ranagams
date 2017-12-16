using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnCore
{
  public interface IAnagramResolverService
  {
    IEnumerable<string> GetAnagrams(string word, string language);

    Task<IEnumerable<string>> GetAnagramsAsync(string word, string language);
  }
}
