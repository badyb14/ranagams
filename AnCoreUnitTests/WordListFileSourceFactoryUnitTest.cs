using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AnCoreUnitTests
{
  [TestClass]
  public partial class WordListFileSourceFactoryUnitTest
  {
    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null baseNames is not accepted")]
    public void Constructor_Throws_WhenNullBaseName()
    {
      //Arrange
      string[] baseNames = null;
      string path = "";
      string extraPrefix = "";
      string exclusionPrefix = "";
      //Act
      //Assert
      var objectUnderTest = new WordListFileSourceFactory(baseNames, path, extraPrefix, exclusionPrefix);
    }

    [TestMethod]
    [TestCategory("Constructors")]
    [ExpectedException(exceptionType: typeof(ArgumentException), noExceptionMessage: "invalid baseNames is not accepted")]
    public void Constructor_Throws_WhenInvalidBaseName()
    {
      //Arrange
      string[] baseNames = new[] { null, string.Empty };
      string path = "";
      string extraPrefix = "";
      string exclusionPrefix = "";
      //Act
      //Assert
      var objectUnderTest = new WordListFileSourceFactory(baseNames, path, extraPrefix, exclusionPrefix);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: " null path is not accepted")]
    public void BuildWordListFullPath_Throws_WhenPathIsNull()
    {
      //Arrange
      string path = null;
      string name = "s";
      string lang = "eb";

      //Act
      //Assert
      var actual = WordListFileSourceFactory.BuildWordListFullPath(path, name, lang);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void BuildWordListFullPath_WhenPathIsEmpty()
    {
      //Arrange
      string path = "";
      string name = "s";
      string lang = "eb";

      //Act
      var actual = WordListFileSourceFactory.BuildWordListFullPath(path, name, lang);

      //Assert
      Assert.AreEqual("s_eb.txt", actual);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void BuildWordListFullPath_Case1()
    {
      //Arrange
      string path = "";
      string name = null;
      string lang = null;

      //Act
      var actual = WordListFileSourceFactory.BuildWordListFullPath(path, name, lang);

      //Assert
      Assert.AreEqual("_.txt", actual);
    }


    [TestMethod]
    [TestCategory("Behaviour")]
    public void BuildWordListFullPath_Case2()
    {
      //Arrange
      string path = @"c:\somefolder";
      string name = "words";
      string lang = "en";

      //Act
      var actual = WordListFileSourceFactory.BuildWordListFullPath(path, name, lang);

      //Assert
      Assert.AreEqual(@"c:\somefolder\words_en.txt", actual);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_NullPath_RetursFalse()
    {
      //Arrange
      string filePath = null;

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_EmptyPath_RetursFalse()
    {
      //Arrange
      string filePath = string.Empty;

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_MissingFileName_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_Case1_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder\w.txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_Case2_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder\wtxt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_Case3_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder\w_txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_Case4_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder\w._.txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_Case5_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\somefolder\w..txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_case6_RetursFalse()
    {
      //Arrange
      string filePath = @"c:\some_fol.der\words_en.txt.txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(false, actual);
      Assert.AreEqual(null, language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_RetursTrue()
    {
      //Arrange
      string filePath = @"c:\somefolder\words_en.txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(true, actual);
      Assert.AreEqual("en", language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void TryGetWordListLanguage_case1_RetursTrue()
    {
      //Arrange
      string filePath = @"c:\some_fol.der\words_en.txt";

      //Act
      string language = null;
      var actual = WordListFileSourceFactory.TryGetWordListLanguage(filePath, out language);

      //Assert
      Assert.AreEqual(true, actual);
      Assert.AreEqual("en", language);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void GetWordList_Case1()
    {
      //Arrange
      string[] baseNames = new string[] {@"c:\d\w_en.txt" };
      string path = @"c:\d";
      string extraPrefix = "";
      string exclusionPrefix = "";
      var objectUnderTest = new WordListFileSourceFactory(baseNames, path, extraPrefix, exclusionPrefix);

      //Act
      var actual = objectUnderTest.GetWordList(true);

      //Assert
      Assert.AreEqual(1, actual.Length);
    }

    [TestMethod]
    [TestCategory("Behaviour")]
    public void GetWordList_Case2()
    {
      //Arrange
      string[] baseNames = new string[] { @"c:\d\w_en.txt","badentry" };
      string path = @"c:\d";
      string extraPrefix = "";
      string exclusionPrefix = "";
      var objectUnderTest = new WordListFileSourceFactory(baseNames, path, extraPrefix, exclusionPrefix);

      //Act
      var actual = objectUnderTest.GetWordList(true);

      //Assert
      Assert.AreEqual(1, actual.Length);
    }
  }
}