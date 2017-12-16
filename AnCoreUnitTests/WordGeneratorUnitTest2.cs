using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AnCoreUnitTests
{
  public partial class WordGeneratorUnitTest
  {

    [TestMethod]
    [TestCategory("Content Correctness")]
    public void GetPermutations_Content_Word1()
    {
      //Arrange
      var word = "aBa";
      var objectUnderTest = new WordGenerator(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      var indexCount = 0;
      var results = new List<string>();
      foreach (var perm in permutations)
      {
        results.Add(new string(perm));
        indexCount++;
      }
      
      //Assert
      Assert.AreEqual(2, indexCount, "the number of permutations must be 2");
      Assert.IsTrue(results.Contains("aab"));
      Assert.IsTrue(results.Contains("baa"));
      Assert.IsTrue(!results.Contains("Baa")); //lower case transformation
      Assert.IsTrue(!results.Contains("aaB")); //lower case transformation
    }

    [TestMethod]
    [TestCategory("Content Correctness")]
    public void GetPermutations_Content_Word2()
    {
      //Arrange
      var word = "aaab";
      var objectUnderTest = new WordGenerator(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      var results = new List<string>();
      foreach (var perm in permutations)
      {
        var newWord = new string(perm);
        results.Add(newWord);
        Trace.TraceInformation(newWord);
      }

      //Assert
      Assert.AreEqual(word.Length -1, results.Count, "the number of permutations must be word.Length -1");
      Assert.IsTrue(results.Contains("aaba"));
      Assert.IsTrue(results.Contains("abaa"));
      Assert.IsTrue(results.Contains("baaa"));
      Assert.IsTrue(!results.Contains("aaab"));
    }

    [TestMethod]
    [TestCategory("Content Correctness")]
    public void GetPermutations_Content_SWord()
    {
      //Arrange
      var word = "Silent"; // test case sensitivity
      var objectUnderTest = new WordGenerator(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      var results = new List<string>();
      foreach (var perm in permutations)
      {
        var newWord = new string(perm);
        results.Add(newWord);
        Trace.TraceInformation(newWord);
      }

      //Assert
      Assert.AreEqual(Util.Factorial(word.Length) -1, results.Count , "the number of permutations must be Factorial(word.Length)-1 because all word letters are distinct");
     
      // Random test lets  try to find up to 80% random strings in our result list.
      var rand = new Random(word.Length / 2);
      var config = Enumerable.Range(0, word.Length).ToArray();
      var randomChars = new char[word.Length];
      var lowerCaseWord = word.ToLowerInvariant();
      for (int i = 0; i < word.Length * 0.8; i++) // this does not get 80% coverage because we
      {
        Util.GeneratePermutation(config, rand); // asume that this is correct.
        var c = 0;
        foreach (var index in config)
        {
          randomChars[c] = lowerCaseWord[index];
          c++;
        }
        var newWord = new string(randomChars);
        if (newWord == word) continue;

        Assert.IsTrue(results.Contains(newWord), newWord);
        Trace.TraceInformation(newWord);
      }
    }
  }
}