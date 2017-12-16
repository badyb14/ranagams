using System;
using System.Collections.Generic;

namespace AnCore
{
  /// <summary>
  /// Word list that uses HashSet<string> as internal storage.
  /// Must be thread safe: should allow multiple threads to call load and access its property concurrently.
  /// 
  /// The word list is compiled by using the base path, additional path and excluded word.
  /// For now excluded words are potentially duplicated (once in base list once in excluded list), 
  /// this is done for a fast load.
  /// 
  /// TODO: Use the language to decide the string comparer and if the lower case conversion is needed.
  /// </summary>
  public sealed class HashSetWordListSource : IWordList
  {
    #region Fields
    private readonly HashSet<string> _wordList = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    private readonly HashSet<string> _exclusion = null;
    private readonly string _language;
    private readonly string _wordListFilePath;
    private readonly string _extraPath;
    private readonly string _exclusionPath;
    private readonly object _loadGate = new object();
    private readonly Func<string, IEnumerable<string>> _wordListRetriever;
    #endregion

    #region Properties
    /// <summary>
    /// Get the list of words
    /// </summary>
    public IEnumerable<string> WordList
    {
      get
      {
        EnsureLoadInternal();
        return _wordList;
      }
    }

    /// <summary>
    /// Get and indicator if the list was loaded.
    /// </summary>
    public bool IsLoaded { get; private set; }

    /// <summary>
    /// Get the language of the word list.
    /// </summary>
    public string Language { get { return _language; } }


    #endregion
    #region Constructors
    public HashSetWordListSource(string filePath, string language, string extraPath, string exclusionPath, Func<string, IEnumerable<string>> wordListRetriever)
    {
      if (filePath == null)
      {
        throw new ArgumentNullException(nameof(filePath));
      }

      if (string.IsNullOrEmpty(filePath))
      {
        throw new ArgumentException("invalid", nameof(filePath));
      }

      if (language == null)
      {
        throw new ArgumentNullException(nameof(language));
      }
      if (string.IsNullOrEmpty(language))
      {
        throw new ArgumentException("invalid", nameof(language));
      }

      _wordListRetriever = wordListRetriever ?? throw new ArgumentNullException(nameof(wordListRetriever));
      _wordListFilePath = filePath;
      _language = language;
      _extraPath = extraPath;
      _exclusionPath = exclusionPath;

      if (!string.IsNullOrWhiteSpace(exclusionPath))
      {
        _exclusion = new HashSet<string>();
      }
    }
    #endregion

    #region Public methods
    /// <summary>
    /// Load the list of words.Use this if you want to load the list before "WordList" is accesed.
    /// </summary>
    public void Load()
    {
      EnsureLoadInternal();
    }

    public bool Contains(string word)
    {
      if (string.IsNullOrWhiteSpace(word))
      {
        return false;
      }
      EnsureLoadInternal();
      word = word.ToLowerInvariant();
      if (_exclusion == null) // no exlusion list provided
      {
        return _wordList.Contains(word);
      }

      // assume the exlusion list lookup is faster and the word list lookup is slower
      if (!_exclusion.Contains(word))
      {
        return _wordList.Contains(word);
      }
      return false;
    }
    #endregion

    #region Private methods
    private void EnsureLoadInternal()
    {
      if (!IsLoaded)
      {
        lock (_loadGate)
        {
          if (!IsLoaded)
          {
            LoadList(_wordListFilePath, _wordList);
            LoadList(_extraPath, _wordList);
            LoadList(_exclusionPath, _exclusion);
            IsLoaded = true;
          }
        }
      }
    }

    private void LoadList(string wordListFilePath, HashSet<string> list)
    {
      if (string.IsNullOrWhiteSpace(wordListFilePath))
      {
        return;
      }
      var source = _wordListRetriever(wordListFilePath);
      if (source == null)
      {
        throw new  ArgumentException("invalid list source");
      }
        
      foreach (var item in source)
      {
        var w = item.ToLowerInvariant();
        list.Add(w);
      }
    }

    #endregion
  }
}
