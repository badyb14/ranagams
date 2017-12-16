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
  }
}
