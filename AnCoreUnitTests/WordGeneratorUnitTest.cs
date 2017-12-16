using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnCoreUnitTests
{
  [TestClass]
  public partial class WordGeneratorUnitTest
  {
    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null word values are not accepted")]
    public void Constructor_Throws_WhenNullWord()
    {
      //Arrange
      string word = null;
      //Act
      //Assert
      var objectUnderTest = new WordGenerator(word);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "whitespace word values are not accepted")]
    public void Constructor_Throws_WhenWhiteSpaceWord1()
    {
      //Arrange
      string word = string.Empty;
      //Act
      //Assert
      var objectUnderTest = new WordGenerator(word);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "whitespace word values are not accepted")]
    public void Constructor_Throws_WhenWhiteSpaceWord2()
    {
      //Arrange
      string word = "   ";
      //Act
      //Assert
      var objectUnderTest = new WordGenerator(word);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "whitespace word values are not accepted")]
    public void Constructor_Throws_WhenWhiteSpaceWord3()
    {
      //Arrange
      string word = " \t  ";
      //Act
      //Assert
      var objectUnderTest = new WordGenerator(word);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentOutOfRangeException), noExceptionMessage: "word is too long")]
    public void Constructor_Throws_WordIsTooLong()
    {
      //Arrange
      string word = new string('a',12);
      //Act
      //Assert
      var objectUnderTest = new WordGenerator(word);
    }
  }
}
