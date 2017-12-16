using System;
using System.Collections.Generic;
using System.Text;

namespace AnCoreUnitTests
{
  /// <summary>
  /// Containts utility methods for unit test
  /// </summary>
  internal static class Util
  {
    /// <summary>
    /// Compute the factorial in recursive way.
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public static int Factorial(int n)
    {
      if (n < 0)
      {
        throw new ArgumentException("must be positive", nameof(n));
      }
      if (n == 0)
      {
        return 1;
      }
      return n * Factorial(n - 1);
    }

    public static void GeneratePermutation(int[] config, Random rand)
    {
      if (config == null)
      {
        throw new ArgumentNullException(nameof(config));
      }

      if (rand == null)
      {
        throw new ArgumentNullException(nameof(rand));
      }

      if (config.Length == 1)
      {
        return; //trivial
      }

      // swap n random positions
      for (int i = 0; i < config.Length; i++)
      {
        var j = rand.Next() % config.Length; // this is good enough but is its distribution may be skewed. 
        var temp = config[i];
        config[i] = config[j];
        config[j] = temp;
      }
    }
  }

  
}
