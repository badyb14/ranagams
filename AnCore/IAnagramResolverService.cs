using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnCore
{

  /// <summary>
  /// Model that describes options to find anagrams.
  /// </summary>
  public sealed class AnagramOptions
  {
    #region Properties
    public string Word { get; set; }

    public string Language { get; set; }

    public string BeginsWith { get; set; }

    public string EndWith { get; set; }

    public int MinLenght { get; set; }

    public int MaxLenght { get; set; }
    #endregion
  }

  /// <summary>
  /// Contract that for an Anagram resolver service
  /// </summary>
  public interface IAnagramResolverService
  {
    void AddResolver(IAnagramResolver resolver);

    void RemoveResolver(IAnagramResolver resolver);

    void EnableResolver(IAnagramResolver resolver);

    void DisableResolver(IAnagramResolver resolver);

    IEnumerable<string> GetAnagrams(string word, string language);

    Task<IEnumerable<string>> GetAnagramsAsync(string word, string language);

    Task<IEnumerable<string>> GetAnagramsAsync(AnagramOptions options);
  }
}
