using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnCore
{
  public sealed class AnagramResolverService : IAnagramResolverService
  {
    #region Fields
    private HashtableWordListSource _englishWordList;

    private readonly Func<string,IWordGenerator> _permutationFactory;
    //this is thread safe for reading
    private readonly Hashtable _wordListByLanguage = new Hashtable(StringComparer.OrdinalIgnoreCase);
    #endregion

    public AnagramResolverService(IWordList[] sources, Func<string,IWordGenerator> permutationFactory)
    {

      _permutationFactory = permutationFactory;

      //TODO trace
      foreach (var source in sources)
      {
        if (source == null || source.Language == null || string.IsNullOrEmpty(source.Language))
        {
          continue;
        }
        _wordListByLanguage.Add(source.Language, source);
      }
      //var filePath = @"C:\Users\adi\Source\Repos\Anagram\AnConsoleApp\AnCore\Dictionaries\words_en.txt";
      //_englishWordList = new WordListFileSource(filePath, "en",null,null);
    }

    public IEnumerable<string> GetAnagrams(string word, string language)
    {
      return GetAnagramsInternal(word, language);
    }

    private void Load()
    {
      _englishWordList.Load();
    }

    private IEnumerable<string> GetAnagramsInternal(string word, string language)
    {
      var wordList = _wordListByLanguage[language] as IWordList;
      if (wordList == null)
      {
        throw new Exception("Languge is not supported");
      }
      var generator = _permutationFactory(word);
      var result = new List<string>();
      foreach (var w in generator.GetPermutations())
      {
        //if(w !=) TODO  check for the origial
        var s = new string(w);
        if (wordList.Contains(s))
        {
          result.Add(s);
          Debug.WriteLine($"Found {s}");
        }
      }
      return result;
    }

    public async Task<IEnumerable<string>> GetAnagramsAsync(string word, string language)
    {
      var result = await Task.Run<IEnumerable<string>>(() => GetAnagramsInternal(word, language));
      return result;
    }
  }
}
