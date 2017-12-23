using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AnCore
{
  /// <summary>
  /// A permutations generator of a single word.
  /// Only permutations different from the original string are generated.
  /// The results are converted to lower invariant.
  /// Optimizations have been made for minimal memory allocations.
  /// If you need to keep the all permutations, clone the results.
  /// This class is not safe to be used by concurrent threads.
  /// </summary>
  public sealed class WordGenerator : IWordGenerator
  {
    #region Fields
    private readonly string _startConfiguration;
    private const byte MaxWordLength = 11; // about 10 seconds of brute generation

    public string StartConfiguration => _startConfiguration;
    #endregion

    #region Constructor
    public WordGenerator(string word)
    {
      if (word == null)
      {
        throw new ArgumentNullException(nameof(word));
      }

      if (string.IsNullOrWhiteSpace(word))
      {
        throw new ArgumentException("contains only white space", nameof(word));
      }

      if (word.Length > MaxWordLength)
      {
        throw new ArgumentOutOfRangeException(nameof(word), "Too long, max accepted word length is 11 char");
      }

      _startConfiguration = word.ToLowerInvariant();
    }
    #endregion

    #region Public Methods

    /// <summary>
    /// Get all string permutations in form of char arrays.Consider cloning the results if necessary.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<char[]> GetPermutations()
    {
      if (IsTrivialWord(_startConfiguration))
      {
        yield break;
      }

      var stringLength = _startConfiguration.Length;
      var currentConfig = new char[stringLength];
     
      //sort the chars so that currentConfig is alphabetical sorted
      var orderedChars = _startConfiguration.ToCharArray().OrderBy(x => x).ToArray();
      BuildFirstConfiguration(orderedChars, currentConfig);

      if (IsValidPermutation(currentConfig, _startConfiguration))//make sure we do not lose the first perm
      {
        yield return currentConfig;
      }

      
      var prev = currentConfig.Clone() as char[];
      while (PermutationHelper.NextPermutation(currentConfig))
      {
        if (IsValidPermutation(currentConfig, prev, _startConfiguration))
        {
          Array.Copy(currentConfig, prev, stringLength);
          yield return currentConfig; // must be different than previous permutation and different than original string
        }
      }
    }
   
    #endregion

    #region Private methods
    private void BuildFirstConfiguration(char[] orderedChars, char[] firstConfig)
    {
      var stringLength = orderedChars.Length;
      var perm = new int[stringLength];
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
        firstConfig[i] = orderedChars[perm[i]];
      }
    }

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

    private static bool IsValidPermutation(char[] current, char[] prev, string startWord)
    {
      var prevDiff = false;
      var orginalDiff = false; 
      for (int i = 0; i < current.Length; i++)
      {
        if (current[i] != prev[i])
        {
          prevDiff = true;
        }
        if (current[i] != startWord[i])
        {
          orginalDiff =true;
        }
        if (prevDiff && orginalDiff)
        {
          return true;
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
