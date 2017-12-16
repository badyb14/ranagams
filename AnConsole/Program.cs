using AnCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnConsole
{
  class Program
  {
    static void Main(string[] args)
    {
      var filePath = Directory.GetCurrentDirectory();
      var source1 = WordListFileSourceFactory.GetWordListFromPath("Dictionaries", "words*", null, null)[0];
      source1.Load();
      var source2 = WordListFileSourceFactory.GetWordListFromPathV2("Dictionaries", "words*", null, null)[0];
      source2.Load();

      var generator = new WordGenerator("elvis");
     
      Stopwatch sw = new Stopwatch();
      sw.Start();
      int t = 0;
      foreach (var w in generator.GetPermutations())
      {
        var s = new string(w);
        t++;
        //Debug.WriteLine(t);
        //Console.Clear();
        //Console.Write(t);
        //Console.WriteLine(s);
        
        if (source1.Contains(s))
        {
          Debug.WriteLine($"Found in source1: {s}");
        }
      }

      Debug.WriteLine($"Time {sw.Elapsed}");

      generator = new WordGenerator("elvis");
      sw.Restart();
      foreach (var w in generator.GetPermutations())
      {
        var s = new string(w);
        t++;
        //Debug.WriteLine(t);
        //Console.Clear();
        //Console.Write(t);
        //Console.WriteLine(s);


        if (source2.Contains(s))
        {
          Debug.WriteLine($"Found in source2: {s}");
        }
      }

      Debug.WriteLine($"Time {sw.Elapsed}");

      Console.ReadKey();
    }
  }
}
