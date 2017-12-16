
using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnCoreUnitTests
{
  [TestClass]
  public partial class HashSetWordListSourceUnitTest
  {
    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null filePath values are not accepted")]
    public void Constructor_Throws_WhenNullPath()
    {
      //Arrange
      string filePath = null;
      string language = null;
      string extraPath = null;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = null;
      //Act
      //Assert
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "empty filePath values are not accepted")]
    public void Constructor_Throws_WhenEmptyPath()
    {
      //Arrange
      string filePath = string.Empty;
      string language = null;
      string extraPath = null;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = null;
      //Act
      //Assert
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null language values are not accepted")]
    public void Constructor_Throws_WhenNullLanguage()
    {
      //Arrange
      string filePath = "some path";
      string language = null;
      string extraPath = null;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = null;
      //Act
      //Assert
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "empty language values are not accepted")]
    public void Constructor_Throws_WhenEmptyLanguage()
    {
      //Arrange
      string filePath = "some path";
      string language = string.Empty;
      string extraPath = null;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = null;
      //Act
      //Assert
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    public void Constructor_DoesNotThrow_WhenNullextraPath()
    {
      //Arrange
      string filePath = "some path";
      string language = "en";
      string extraPath = null;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = (s)=> { return null; };
     
      //Act
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Assert
      Assert.IsNotNull(objectUnderTest);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    public void Constructor_DoesNotThrow_WhenEmptyextraPath()
    {
      //Arrange
      string filePath = "some path";
      string language = "en";
      string extraPath = string.Empty;
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = (s) => { return null; };

      //Act
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Assert
      Assert.IsNotNull(objectUnderTest);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    public void Constructor_DoesNotThrow_WhenNullexclusionPath()
    {
      //Arrange
      string filePath = "some path";
      string language = "en";
      string extraPath = "other";
      string exclusionPath = null;
      Func<string, IEnumerable<string>> wordListRetriever = (s) => { return null; };

      //Act
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Assert
      Assert.IsNotNull(objectUnderTest);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    public void Constructor_DoesNotThrow_WhenEmptyexclusionPath()
    {
      //Arrange
      string filePath = "some path";
      string language = "en";
      string extraPath = "other";
      string exclusionPath = string.Empty;
      Func<string, IEnumerable<string>> wordListRetriever = (s) => { return null; };

      //Act
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Assert
      Assert.IsNotNull(objectUnderTest);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null wordListRetriever values are not accepted")]
    public void Constructor_Throws_WhenNullWordListRetriever()
    {
      //Arrange
      string filePath = "some path";
      string language = "en";
      string extraPath = "other";
      string exclusionPath = "exc";
      Func<string, IEnumerable<string>> wordListRetriever = null;
     
      //Act
      //Assert
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);
    }
  }
}