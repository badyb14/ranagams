using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AnCore
{
  public static class WordListFileSourceFactory
  {
    public static IWordList[] GetWordListFromPath(string folder, string fileName, string extra, string exclusionList)
    {
      //TODO trace...
      var current = Directory.GetCurrentDirectory();
      var path = Path.Combine(current, folder);

      var baseNames = Directory.GetFiles(path, fileName);
      var list = new List<IWordList>(1);
      foreach (var item in baseNames)
      {
        var parts = item.Split(new char[] { '_','.' });
        if (parts != null && parts.Length > 2)
        {
          var lang = parts[1];
          var extraL = $"{extra}_{lang}.txt";
          var exclusionListL = $"{exclusionList}_{lang}.txt";
          list.Add(new WordListFileSource(item, lang, extraL, exclusionListL));
        }
      }

      return list.ToArray();
    }
  }
}
