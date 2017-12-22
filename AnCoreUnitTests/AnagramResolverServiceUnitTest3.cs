using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnCoreUnitTests
{
  public partial class AnagramResolverServiceUnitTest
  {
    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_DoesSomeUsefulWork()
    {
      //Arrange
      int factoryUseage = 0;
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };

      Func<string, IWordGenerator> wordGeneratorFactory = (a) =>
      {
        factoryUseage++;
        return new AbWordGenerator();
      };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "ab";
      string language = "en";

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language);

      //Assert
      Assert.IsNotNull(actual, "there should be some response..");
      Assert.AreEqual(1, factoryUseage);
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_DoesUseTheWordList()
    {
      //Arrange
      int factoryUseage = 0;
      var sources = new IWordList[] { new TestWordList() { Language = "en", WordList = new[] { "ab" } } };

      Func<string, IWordGenerator> wordGeneratorFactory = (a) =>
      {
        factoryUseage++;
        return new AbWordGenerator();
      };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "ab";
      string language = "en";

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language);

      //Assert
      Assert.AreEqual(1, factoryUseage);
      Assert.AreEqual("ab", actual.ToArray()[0]); //we put ab in word list and the generator is hard coded also to ab
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagramAsync_DoesUseTheWordList()
    {
      //Arrange
      int factoryUseage = 0;
      var sources = new IWordList[] { new TestWordList() { Language = "en", WordList = new[] { "ab" } } };

      Func<string, IWordGenerator> wordGeneratorFactory = (a) =>
      {
        factoryUseage++;
        return new AbWordGenerator();
      };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "ab";
      string language = "en";

      //Act
      var actual = objectUnderTest.GetAnagramsAsync(word, language).GetAwaiter().GetResult();

      //Assert
      Assert.AreEqual(1, factoryUseage);
      Assert.AreEqual("ab", actual.ToArray()[0]); //we put ab in word list and the generator is hard coded also to ab
    }


    [TestMethod]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "only in phase 2")]
    [TestCategory("GetAnagrams")]
    public void GetAnagramByOptionAsync_Throws()
    {
      //Arrange
      var sources = new IWordList[] { new TestWordList() { Language = "en", WordList = new[] { "ab" } } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) =>
      {
        return null;
      };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
      var options = new AnagramOptions() { Word = "ab", Language = "en" };

      //Act
      //Assert
      var actual = objectUnderTest.GetAnagramsAsync(options).GetAwaiter().GetResult();
     
    }

    private class AbWordGenerator : IWordGenerator
    {
      public IEnumerable<char[]> GetPermutations()
      {
        yield return new[] { 'a', 'b' };
      }
    }
  }
}