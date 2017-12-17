using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnCore
{
  /// <summary>
  /// Word list that uses Hashtable as internal storage.
  /// Must be thread safe: should allow multiple threads to call load and access its property concurrently.
  /// 
  /// The word list is compiled by using the base path, additional path and excluded word.
  /// For now excluded words are potentially duplicated (once in base list once in excluded list), 
  /// this is done for a fast load.
  /// 
  /// TODO: Use the language to decide the string comparer and if the lower case conversion is needed.
  /// </summary>
  public sealed class HashtableWordListSource : IWordList
  {
    #region Fields
    private readonly Hashtable _wordList = new Hashtable();
    private readonly Hashtable _exclusion = null;
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
        return _wordList.Values.Cast<string>();
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

    public HashtableWordListSource(string filePath, string language, string extraPath, string exclusionPath, Func<string, IEnumerable<string>> wordListRetriever)
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
        _exclusion = new Hashtable();
      }
    }

    #region Public methods

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

    public void Load()
    {
      EnsureLoadInternal();
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

    private void LoadList(string wordListFilePath, Hashtable list)
    {
      if (string.IsNullOrWhiteSpace(wordListFilePath))
      {
        return;
      }
      var source = _wordListRetriever(wordListFilePath);
      if (source == null)
      {
        throw new ArgumentException("invalid list source");
      }

      foreach (var item in source)
      {
        try
        {
          var w = item.ToLowerInvariant();
          list.Add(w, w);
        }
        catch (ArgumentNullException)
        {
          //null; just igore
        }
        catch (ArgumentException)
        {
          //duplicate just ignore
        }
        //all other execption are critical and bubble up...
      }
    }
    
    #endregion
  }
}
