using AnCore;
using System;
using System.Diagnostics;
using System.Linq;

namespace AnConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      var filePath = @"C:\Users\adi\Source\Repos\Anagram\AnConsoleApp\AnConsole\Dictionaries\words_en.txt";
      var dic = new WordListFileSource(filePath, "en", null, null);
      dic.Load();

      var generator = new StringPermutation("permuta");

      var tr = dic.WordList1["d"];
      var ju = dic.WordList2;
      //perm.Compute();
      Stopwatch sw = new Stopwatch();
      sw.Start();
      int t = 0;
      foreach (var w in generator.GetPermutations())
      {
        //if(w !=) TODO  check for the origial
        var s = new string(w);
        t++;
        //Debug.WriteLine(t);
        //Console.Clear();
        //Console.Write(t);
        //Console.WriteLine(s);
        
        
        //if (dic.WordList2.Contains(s))
        //{
        //  Debug.WriteLine($"Found {s}");
        //}

        if (dic.WordList1[s] != null)
        {
          Debug.WriteLine($"Found {s}");
        }
      }

      Debug.WriteLine($"Time {sw.Elapsed}");
      Console.ReadKey();
    }
  }
}
