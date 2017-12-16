using System;
using System.Collections.Generic;
using System.IO;

namespace AnCore
{
  public static class WordListFileSourceFactory
  {
    public static IWordList[] GetWordListFromPath(string folder, string fileName, string extraPath, string exclusionPath)
    {
      return GetWordListFromPathInternal(folder, fileName, extraPath, exclusionPath, true);
    }

    public static IWordList[] GetWordListFromPathV2(string folder, string fileName, string extraPath, string exclusionPath)
    {
      return GetWordListFromPathInternal(folder, fileName, extraPath, exclusionPath, false);
    }

    private static IWordList[] GetWordListFromPathInternal(string folder, string fileName, string extraPath, string exclusionPath, bool isHashSet)
    {
      //TODO trace...
      var current = Directory.GetCurrentDirectory();
      var path = Path.Combine(current, folder);

      var baseNames = Directory.GetFiles(path, fileName);
      var list = new List<IWordList>(1);
      foreach (var filePath in baseNames)
      {
        var parts = filePath.Split(new char[] { '_', '.' });
        if (parts != null && parts.Length > 2)
        {
          var lang = parts[1];
          var extraPathLong = Path.Combine(path, $"{extraPath}_{lang}.txt");
          var exclusionPathLong = Path.Combine(path, $"{exclusionPath}_{lang}.txt");

          if (string.IsNullOrEmpty(extraPath))
          {
            extraPathLong = null;
          }

          if (string.IsNullOrEmpty(exclusionPath))
          {
            exclusionPathLong = null;
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
      }

      return list.ToArray();
    }
  }
}
