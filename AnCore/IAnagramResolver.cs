using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnCore
{
  public enum AnagramResolverType
  {
    File,
    Database,
    WebService
  }

  public interface IAnagramResolver
  {
    string Language { get; }

    AnagramResolverType Type { get; }

    Task<IEnumerable<string>> GetAnagramsAsync(string word);

    Task<IEnumerable<string>> GetAnagramsAsync(AnagramOptions options);
    
  }
}
