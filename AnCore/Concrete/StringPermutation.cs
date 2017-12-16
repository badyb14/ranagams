using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AnCore
{
  /// <summary>
  /// A permutations generator of a single string.
  /// Only permutations different from the original string are generated.
  /// The results are converted to lower invariant.
  /// Optimizations have been made to use minimal memory allocations.
  /// If you need to keep the all permutations, clone the results.
  /// This class is not safe to be used by concurrent threads.
  /// </summary>
  public sealed class StringPermutation : IStringPermutation
  {
    #region Fields
    private readonly string _startConfiguration;

    public string StartConfiguration => _startConfiguration;
    #endregion

    #region Constructor
    public StringPermutation(string word)
    {
      if (word == null)
      {
        throw new ArgumentNullException(nameof(word));
      }

      if (string.IsNullOrWhiteSpace(word))
      {
        throw new ArgumentException("contains only white space", nameof(word));
      }

      _startConfiguration = word.ToLowerInvariant();
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Get all string permutations.
    /// Consider cloning the results if necessary.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<char[]> GetPermutations()
    {
      var stringLength = _startConfiguration.Length;
      if (stringLength == 1)
      {
        yield break;
      }
      else if (IsTrivialWord(_startConfiguration))
      {
        //var p = _startConfiguration.Reverse().ToArray();
        //if (IsValidPermutation(p, _startConfiguration))
        //{
        //  yield return p;
        //}
        yield break;
      }

      var perm = new int[stringLength];
      var orderedChars = _startConfiguration.ToCharArray().OrderBy(x => (int)x).ToArray();
      var currentConfig = new char[stringLength];
      for (int i = 0; i < stringLength; i++)
      {
        perm[i] = i;
      }

      for (int i = 0; i < stringLength; i++)
      {
        for (int j = i + 1; j < stringLength; j++)
        {
          if (orderedChars[j] == orderedChars[i])
          {
            perm[j] = i;
          }
          else
          {
            perm[j] = j;
          }
        }
        currentConfig[i] = orderedChars[perm[i]];
      }

      if (IsValidPermutation(currentConfig, _startConfiguration))
      {
        yield return currentConfig;
      }
      var prev = currentConfig.Clone() as char[];
      while (NextPermutation(currentConfig))
      {
        //for (int i = 0; i < stringLength; i++)
        //{
        //  currentConfig[i] = orderedChars[perm[i]];
        //}

        if (IsValidPermutation(currentConfig, _startConfiguration) && IsValidPermutation(currentConfig, prev))
        {
          prev = currentConfig.Clone() as char[];
          yield return currentConfig;
        }
      }
    }

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

    public static bool NextPermutation(int[] currentConfiguration)
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
    #endregion

    #region Private methods

    private static bool IsValidPermutation(char[] current, string startWord)
    {
      for (int i = 0; i < current.Length; i++)
      {
        if (current[i] != startWord[i])
        {
          return true; // found a difference 
        }
      }
      return false; //found no difference
    }

    private static bool IsValidPermutation(char[] current, char[] prev)
    {
      for (int i = 0; i < current.Length; i++)
      {
        if (current[i] != prev[i])
        {
          return true; // found a difference 
        }
      }
      return false; //found no difference
    }

    private static bool IsTrivialWord(string startWord)
    {
      var first = startWord[0];
      for (int i = 1; i < startWord.Length; i++)
      {
        if (first != startWord[i])
        {
          return false; // found a difference 
        }
      }
      return true; //all other chars match first char
    }

    private static void Print(int[] perm, string word)
    {
      for (int i = 0; i < perm.Length; i++)
      {
        Debug.Write(word[perm[i]]);
      }
      Debug.Write(Environment.NewLine);
    }
    #endregion
  }
}
