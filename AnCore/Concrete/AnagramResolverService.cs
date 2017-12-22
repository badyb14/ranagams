using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AnCore
{
  /// <summary>
  /// Aggregates all IAnagramResolver in system.
  /// Has builtin anagram finding using word lists aka default resolvers.
  /// TODO complete Resolver Management 
  /// </summary>
  public sealed class AnagramResolverService : IAnagramResolverService
  {
    #region Fields
    private const string SupportedLanguage = "en";
    private readonly Func<string, IWordGenerator> _wordGeneratorFactory;
    //this is thread safe for reading
    private readonly Hashtable _wordListByLanguage = new Hashtable(StringComparer.OrdinalIgnoreCase);
    private readonly List<IAnagramResolver> _otherResolvers = new List<IAnagramResolver>();
    private readonly List<IAnagramResolver> _disabledResolver = new List<IAnagramResolver>();

    private readonly object _resolverGate = new object();
    #endregion

    #region Constuctors
    public AnagramResolverService(IWordList[] sources, Func<string, IWordGenerator> wordGeneratorFactory)
    {
      if (sources == null)
      {
        throw new ArgumentNullException(nameof(sources));
      }

      _wordGeneratorFactory = wordGeneratorFactory ?? throw new ArgumentNullException(nameof(wordGeneratorFactory));
      
      foreach (var source in sources)
      {
        if (source == null || source.Language == null || string.IsNullOrEmpty(source.Language))
        {
          throw new ArgumentException("found invalid word list reference", nameof(source));
        }
        _wordListByLanguage.Add(source.Language, source);
      }
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Get anagrams using default resolvers
    /// </summary>
    /// <param name="word"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public IEnumerable<string> GetAnagrams(string word, string language)
    {
      return GetAnagramsInternal(word, language);
    }
    
    /// <summary>
    /// Get anagrams using default resolvers
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetAnagramsAsync(string word, string language)
    {
      var result = await Task.Run(() => GetAnagramsInternal(word, language));
      return result;
    }

    /// <summary>
    /// Get anagrams using 'advanced' resolvers
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public Task<IEnumerable<string>> GetAnagramsAsync(AnagramOptions options)
    {
      if (options == null)
      {
        throw new ArgumentNullException(nameof(options));
      }
      //find an advanced resolver
      var resolver = FindBestResolver(options);
      if (resolver == null)
      {
        throw new ArgumentException("Langauge is not supported");
      }
      return resolver.GetAnagramsAsync(options);
    }

    public void AddResolver(IAnagramResolver resolver)
    {
      if (resolver == null)
      {
        throw new ArgumentNullException(nameof(resolver));
      }
      lock (_resolverGate)
      {
        _otherResolvers.Add(resolver);
      }
    }

    public void RemoveResolver(IAnagramResolver resolver)
    {
      if (resolver == null)
      {
        throw new ArgumentNullException(nameof(resolver));
      }
      lock (_resolverGate)
      {
        _otherResolvers.Remove(resolver);
      }
    }

    public void EnableResolver(IAnagramResolver resolver)
    {
      if (resolver == null)
      {
        throw new ArgumentNullException(nameof(resolver));
      }
      lock (_resolverGate)
      {
        _disabledResolver.Remove(resolver);
        _otherResolvers.Add(resolver);
      }
    }

    public void DisableResolver(IAnagramResolver resolver)
    {
      if (resolver == null)
      {
        throw new ArgumentNullException(nameof(resolver));
      }
      lock (_resolverGate)
      {
        _otherResolvers.Remove(resolver);
        _disabledResolver.Add(resolver);
      }
    }

    #endregion

    #region Private methods
    private IEnumerable<string> GetAnagramsInternal(string word, string language)
    {
      ValidateParameters(word, language);
      var wordList = _wordListByLanguage[language] as IWordList;
      if (wordList == null) // this  looks redundat but is needed; the word list may change; ie disable a language...
      {
        throw new Exception("Language is not supported");
      }
      var generator = _wordGeneratorFactory(word);
      var result = new List<string>();

      foreach (var w in generator.GetPermutations())
      {
        var s = new string(w);
        if (wordList.Contains(s))
        {
          result.Add(s);
          Debug.WriteLine($"Found {s}");
        }
      }
      return result;
    }

    private IAnagramResolver FindBestResolver(AnagramOptions optins)
    {
      IAnagramResolver resolver = null;
      lock (_resolverGate)
      {
        var languageMatchSortedByType = _otherResolvers.FirstOrDefault(r => r.Language == optins.Language && r.Type != AnagramResolverType.File);
        resolver= languageMatchSortedByType;
      }
      return resolver;
    }

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
      if (string.Compare(SupportedLanguage, language, StringComparison.OrdinalIgnoreCase) != 0)
      {
        throw new ArgumentException("invalid language");
      }
    }

    #endregion

  }
}