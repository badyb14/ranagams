
using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AnCoreUnitTests
{
  public partial class HashSetWordListSourceUnitTest
  {
    [TestMethod]
    [TestCategory("Load")]
    public void Load_LoadsAllSources()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = "extrapath";
      string exclusionPath = "excludepath";

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {extraPath,false },
        {exclusionPath,false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path)=> 
      {
        pathCalls[path] = true;
        return new string[] { };
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      objectUnderTest.Load();

      //Assert
      foreach (var item in pathCalls)
      {
        Assert.AreEqual(true, item.Value, item.Key +" wat not called");
      }
    }


    [TestMethod]
    [TestCategory("Load")]
    [ExpectedException(typeof(ArgumentException), noExceptionMessage: "null sources should except. is a sign of missconfig ")]
    public void Load_ThrowsIfWordListRetrieverReturnsNull()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = "extrapath";
      string exclusionPath = "excludepath";

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {extraPath,false },
        {exclusionPath,false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path) =>
      {
        pathCalls[path] = true;
        return null;
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      //Assert
      objectUnderTest.Load();
    }


    [TestMethod]
    [TestCategory("Load")]
    public void Load_MissingExtraAndExclusion()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = null;
      string exclusionPath = null;

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {"extraPath", false },
        {"exclusionPath",false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path) =>
      {
        pathCalls[path] = true;
        return new string[] { };
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      objectUnderTest.Load();

      //Assert
      Assert.AreEqual(true, pathCalls[filePath]);
      Assert.AreEqual(false, pathCalls["extraPath"]); // do not load if emtpy or null
      Assert.AreEqual(false, pathCalls["exclusionPath"]);// do not load if emtpy or null
    }

    [TestMethod]
    [TestCategory("Load")]
    public void Contains_BaseTest()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = null;
      string exclusionPath = null;

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {"extraPath", false },
        {"exclusionPath",false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path) =>
      {
        pathCalls[path] = true;
        if (path == filePath)
        {
          return new string[] {"abc","Abc","bcd" };
        }
        return null;
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      objectUnderTest.Load();

      //Assert
      Assert.AreEqual(true, pathCalls[filePath]);

      Assert.IsFalse(objectUnderTest.Contains(null)); // bad value
      Assert.IsFalse(objectUnderTest.Contains(string.Empty)); // bad value
      Assert.IsFalse(objectUnderTest.Contains("peiopwei")); // not a word value

      Assert.IsTrue(objectUnderTest.Contains("abc"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("bcd"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("ABC"));//case invariant match
    }

    [TestMethod]
    [TestCategory("Load")]
    public void Contains_ExtraTest()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = "extraPath";
      string exclusionPath = null;

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {extraPath, false },
        {"exclusionPath",false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path) =>
      {
        pathCalls[path] = true;
        if (path == filePath)
        {
          return new string[] { "abc", "Abc" }; // duplicates are allowed
        }
        if (path == extraPath)
        {
          return new string[] { "1243,", "bcd" };
        }
        return null;
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      objectUnderTest.Load();

      //Assert
      Assert.AreEqual(true, pathCalls[filePath]);

      Assert.IsTrue(objectUnderTest.Contains("abc"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("bcd"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("ABC"));//case invariant match
      Assert.IsTrue(objectUnderTest.Contains("1243,"));//extra
      Assert.IsTrue(objectUnderTest.Contains("bcd"));//extra
      Assert.IsTrue(objectUnderTest.Contains("bCd"));//extra case invariant match
    }


    [TestMethod]
    [TestCategory("Load")]
    public void Contains_ExclusionTest()
    {
      //Arrange
      string filePath = "somepath";
      string language = "en";
      string extraPath = "extraPath";
      string exclusionPath = "exclusionPath";

      Dictionary<string, bool> pathCalls = new Dictionary<string, bool>()
      {
        {filePath,false },
        {extraPath, false },
        {exclusionPath,false },
      };

      Func<string, IEnumerable<string>> wordListRetriever = (path) =>
      {
        pathCalls[path] = true;
        if (path == filePath)
        {
          return new string[] { "abc", "Abc" ,"badword" }; // duplicates are allowed
        }
        else if (path == extraPath)
        {
          return new string[] { "1243,", "bcd" };
        }
        else if (path == exclusionPath)
        {
          return new string[] { "1243," , "badword" };
        }

        return null;
      };
      var objectUnderTest = new HashSetWordListSource(filePath, language, extraPath, exclusionPath, wordListRetriever);

      //Act
      objectUnderTest.Load();

      //Assert
      Assert.AreEqual(true, pathCalls[filePath]);

      Assert.IsFalse(objectUnderTest.Contains("1243,"));//extra but not allowed
      Assert.IsFalse(objectUnderTest.Contains("badword,"));// not allowed

      Assert.IsTrue(objectUnderTest.Contains("abc"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("bcd"));// exact match
      Assert.IsTrue(objectUnderTest.Contains("ABC"));//case invariant match
      Assert.IsTrue(objectUnderTest.Contains("bcd"));//extra
      Assert.IsTrue(objectUnderTest.Contains("bCd"));//extra case invariant match
    }

  }
}