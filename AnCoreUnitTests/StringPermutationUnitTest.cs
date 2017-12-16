using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnCoreUnitTests
{
  [TestClass]
  public partial class StringPermutationUnitTest
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
      var objectUnderTest = new StringPermutation(word);
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
      var objectUnderTest = new StringPermutation(word);
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
      var objectUnderTest = new StringPermutation(word);
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
      var objectUnderTest = new StringPermutation(word);
    }
  }
}
