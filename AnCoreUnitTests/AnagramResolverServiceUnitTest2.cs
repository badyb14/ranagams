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
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenInvalidWord_Case1()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word=null;
      string language="some language";

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }

    [TestMethod]
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenInvalidWord_Case2()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "";
      string language = "some language";

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }

    [TestMethod]
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenInvalidLanguage_Case1()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "af";
      string language = null;

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }


    [TestMethod]
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenInvalidLanguage_Case2()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "af";
      string language = "";

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }

    [TestMethod]
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenInvalidLanguage_Case3()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "af";
      string language = "only en is supported in phase 1";

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }

    [TestMethod]
    [TestCategory("GetAnagrams Safety")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null or empty values are not accepted")]
    public void GetAnagramsThrows_WhenWordTooLong_Case1()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      string word = "0123456789AB";
      string language = "en";

      //Act
      //Assert
      objectUnderTest.GetAnagrams(word, language);
    }
   
  }
}