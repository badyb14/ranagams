using System;
using System.Collections.Generic;
using System.IO;

namespace AnCore
{
  /// <summary>
  /// Factory of word list instances.
  /// Contains the parsing naming convention.
  /// </summary>
  public sealed class WordListFileSourceFactory
  {
    #region Fields
    private readonly string _path;
    private readonly string[] _baseNames;
    private readonly string _extraPrefix;
    private readonly string _exclusionPrefix;
    #endregion

    #region Properties

    #endregion

    #region Constructors
    /// <summary>
    /// Create a word list factory with a specific configuration.
    /// </summary>
    /// <param name="baseNames">full path for all base word files.</param>
    /// <param name="path">folder where extra and exclusion word files are located.</param>
    /// <param name="extraPrefix">the file name prefix for extra word list </param>
    /// <param name="exclusionPrefix">the file name prefix for the exclusion word list</param>
    public WordListFileSourceFactory(string[] baseNames, string path,  string extraPrefix, string exclusionPrefix)
    {
      _baseNames = baseNames ?? throw new ArgumentNullException(nameof(baseNames));
      foreach (var p in baseNames)
      {
        if (string.IsNullOrEmpty(p))
        {
          throw new ArgumentException("found invalid base path");
        }
      }
      _path = path;
      _extraPrefix = extraPrefix;
      _exclusionPrefix = exclusionPrefix;
    }
    #endregion

    #region Public Methods

    public IWordList[] GetWordList(bool isHashSet)
    {
      var list = ProcessFileList(_path, _baseNames, _extraPrefix, _exclusionPrefix, isHashSet);
      return list.ToArray();
    }

    public static string BuildWordListFullPath(string path, string name, string lang)
    {
    
      var longPath = Path.Combine(path, $"{name}_{lang}.txt");
      return longPath;
    }

    public static bool TryGetWordListLanguage(string filePath, out string language)
    {
      language = null;
      if (string.IsNullOrEmpty(filePath))
      {
        return false;
      }
      var fName = Path.GetFileName(filePath);

      var parts = fName.Split(new char[] { '_', '.' });
      if (parts != null && parts.Length ==3 && !string.IsNullOrWhiteSpace(parts[1]))
      {
        language = parts[1];
        return true;
      }
      return false;
    }
    #endregion

    #region Private methods

    private static List<IWordList> ProcessFileList(string path, string[] baseNames,string extraPath, string exclusionPath, bool isHashSet)
    {
      var list = new List<IWordList>(1);
      foreach (var filePath in baseNames)
      {
        string lang;
        string extraPathLong = null;
        string exclusionPathLong = null;

        if (TryGetWordListLanguage(filePath, out lang))
        {
          if (!string.IsNullOrEmpty(extraPath))
          {
            extraPathLong = BuildWordListFullPath(path, extraPath, lang); ;
          }

          if (!string.IsNullOrEmpty(exclusionPath))
          {
            exclusionPathLong = BuildWordListFullPath(path, exclusionPath, lang); ;
          }

          if (isHashSet)
          {
            list.Add(new HashSetWordListSource(filePath, lang, extraPathLong, exclusionPathLong, (fpath) => { return File.ReadAllLines(fpath); }));
          }
          else
          {
            list.Add(new HashtableWordListSource(filePath, lang, extraPathLong, exclusionPathLong, (fpath) => { return File.ReadAllLines(fpath); }));
          }
        }
        else
        {
          continue;
        }
      }

      return list;
    }
    #endregion

  }
}
