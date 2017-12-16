using System;
using System.Collections.Generic;

namespace AnCore
{
   /// <summary>
  /// Stateless utility class that can generate permutation of arrays of comparable types.
  /// </summary>
  public static class PermutationHelper
  {
    /// <summary>
    /// Generate the next permutation using alpha comparations from a given char array
    /// </summary>
    /// <param name="currentConfiguration">the char array to use as current configuration</param>
    /// <returns></returns>
    public static bool NextPermutation(char[] currentConfiguration)
    {
      /*
       D. Knuth
       1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
       2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
       3. Swap a[j] with a[l].
       4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

       */
      var largestIndex = -1;
      for (var i = currentConfiguration.Length - 2; i >= 0; i--)
      {
        if (currentConfiguration[i] < currentConfiguration[i + 1])
        {
          largestIndex = i;
          break;
        }
      }

      if (largestIndex < 0) return false;

      var largestIndex2 = -1;
      for (var i = currentConfiguration.Length - 1; i >= 0; i--)
      {
        if (currentConfiguration[largestIndex] < currentConfiguration[i])
        {
          largestIndex2 = i;
          break;
        }
      }

      var tmp = currentConfiguration[largestIndex];
      currentConfiguration[largestIndex] = currentConfiguration[largestIndex2];
      currentConfiguration[largestIndex2] = tmp;

      for (int i = largestIndex + 1, j = currentConfiguration.Length - 1; i < j; i++, j--)
      {
        tmp = currentConfiguration[i];
        currentConfiguration[i] = currentConfiguration[j];
        currentConfiguration[j] = tmp;
      }

      return true;
    }

    /// <summary>
    /// Generate the next permutation using comparations from a given array of T
    /// </summary>
    /// <typeparam name="T"> the type of the permutation item</typeparam>
    /// <param name="currentConfiguration">the array to use as current configuration</param>
    /// <param name="comparer">a non trivial comparer for type T object instances</param>
    /// <returns></returns>
    public static bool NextPermutation<T>(T[] currentConfiguration, IComparer<T> comparer)
    {
      /*
       D. Knuth
       1. Find the largest index j such that a[j] < a[j + 1]. If no such index exists, the permutation is the last permutation.
       2. Find the largest index l such that a[j] < a[l]. Since j + 1 is such an index, l is well defined and satisfies j < l.
       3. Swap a[j] with a[l].
       4. Reverse the sequence from a[j + 1] up to and including the final element a[n].

       */
      var largestIndex = -1;
      for (var i = currentConfiguration.Length - 2; i >= 0; i--)
      {
        if (comparer.Compare(currentConfiguration[i], currentConfiguration[i + 1]) < 0)
        {
          largestIndex = i;
          break;
        }
      }

      if (largestIndex < 0) return false;

      var largestIndex2 = -1;
      for (var i = currentConfiguration.Length - 1; i >= 0; i--)
      {
        if (comparer.Compare(currentConfiguration[largestIndex], currentConfiguration[i]) < 0)
        {
          largestIndex2 = i;
          break;
        }
      }

      var tmp = currentConfiguration[largestIndex];
      currentConfiguration[largestIndex] = currentConfiguration[largestIndex2];
      currentConfiguration[largestIndex2] = tmp;

      for (int i = largestIndex + 1, j = currentConfiguration.Length - 1; i < j; i++, j--)
      {
        tmp = currentConfiguration[i];
        currentConfiguration[i] = currentConfiguration[j];
        currentConfiguration[j] = tmp;
      }

      return true;
    }
  }
}
