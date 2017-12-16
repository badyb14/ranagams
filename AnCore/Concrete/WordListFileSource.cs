using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace AnCore
{
  public sealed class WordListFileSource : IWordList
  {
    #region Fields
    private readonly HashSet<string> _wordList1 = new HashSet<string>();
    private readonly Hashtable _wordList = new Hashtable();
    private readonly string _wordListFilePath = null;
    private readonly string _language;
    private readonly object _loadGate = new object();
    #endregion

    #region Properties
    public Hashtable WordList1
    {
      get
      {
        LoadInternal();
        return _wordList;
      }
    }

    public IEnumerable<string> WordList2
    {
      get
      {
        
        LoadInternal1();
        return _wordList1;
      }
    }

    public bool IsLoaded { get; private set; }

    public string Language { get { return _language; } }

    IEnumerable<string> IWordList.WordList => throw new NotImplementedException();
    #endregion

    public WordListFileSource(string filePath, string language, string extra, string exclusionList)
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
      _wordListFilePath = filePath;
      _language = language;
    }

    #region Public methods
    public void Load()
    {
      //LoadInternal1();
      LoadInternal();
    }
    #endregion

    #region Private methods
    private void LoadInternal1()
    {
      if (!IsLoaded)
      {
        lock (_loadGate)
        {
          if (!IsLoaded)
          {
            foreach (var item in File.ReadLines(_wordListFilePath))
            {
              try
              {
                var w = item.ToLowerInvariant();
                _wordList1.Add(w);
              }
              catch (Exception)
              {
              }
            }
            IsLoaded = true;
          }
        }
      }
    }

    private void LoadInternal()
    {
      if (!IsLoaded)
      {
        lock (_loadGate)
        {
          if (!IsLoaded)
          {
            foreach (var item in File.ReadLines(_wordListFilePath))
            {
              try
              {
                var w = item.ToLowerInvariant();
                _wordList.Add(w, w);
              }
              catch (Exception)
              {
              }
            }
            IsLoaded = true;
          }
        }
      }
    }
    #endregion
  }
}
