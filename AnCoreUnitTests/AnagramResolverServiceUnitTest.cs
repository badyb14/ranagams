using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnCoreUnitTests
{
  [TestClass]
  public partial class AnagramResolverServiceUnitTest
  {

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null factory is not accepted")]
    public void Constructor_Throws_WhenNullFactory()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList { Language="en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = null;
      //Act
      //Assert
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null source is not accepted")]
    public void Constructor_Throws_WhenNullSource()
    {
      //Arrange
      IWordList[] sources = null;
      Func<string, IWordGenerator> wordGeneratorFactory = (a)=> { return null; };
      //Act
      //Assert
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null source refernce is not accepted")]
    public void Constructor_Throws_WhenInvalidReferenceInSourcesCase1()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { null };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      //Act
      //Assert
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null source refernce is not accepted")]
    public void Constructor_Throws_WhenInvalidReferenceInSourcesCase2()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList()  };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      //Act
      //Assert
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "null source refernce is not accepted")]
    public void Constructor_Throws_WhenInvalidReferenceInSourcesCase3()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language=string.Empty} };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      //Act
      //Assert
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);
    }

    private class TestWordList : IWordList
    {

      public string Language { get; set; }

      public IEnumerable<string> WordList { get; set; }

      public bool Contains(string word)
      {
        if (WordList == null) return false;

        return WordList.Contains(word);
      }

      public void Load()
      {

      }
    }
  }
}