using AnCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace AnConsole
{
  class Program
  {
    #region Fields
    private static IWordList source1;
    private static IWordList source2;
    #endregion

    static void Main(string[] args)
    {
      var filePath = Directory.GetCurrentDirectory();
      source1 = WordListFileSourceFactory.GetWordListFromPath("Dictionaries", "words*", null, null)[0];
      source1.Load();
      source2 = WordListFileSourceFactory.GetWordListFromPathV2("Dictionaries", "words*", null, null)[0];
      source2.Load();
      string word = "trainers"; //silent,elvis,samples,calipers,trainers, salesman, auctioned,mastering, discounted,reductions,percussion

      for (int i = 0; i < 1; i++)
      {
        RunTests(word);
      }

      Console.ReadKey();
    }

    private static void RunTests(string word)
    {
      var generator = new WordGenerator(word);
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

      var t1 = sw.Elapsed;
      //Debug.WriteLine($"Time {t1}");

      generator = new WordGenerator(word);
      t = 0;
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

      var t2 = sw.Elapsed;
      //Debug.WriteLine($"Time {t2}");
      Debug.WriteLine($"Diff {(t2 - t1)}");
    }
  }
}
