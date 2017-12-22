using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnCoreUnitTests
{
  public partial class AnagramResolverServiceUnitTest
  {
    [TestMethod]
    [TestCategory("Resolver Management")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null resolvers are not accepted")]
    public void AddResolverThrows_WhenNullResolver()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = null;
      //Act
      //Assert
      objectUnderTest.AddResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    public void AddResolver_Basic()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = new TestResolver();
      //Act
      //Assert
      objectUnderTest.AddResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null resolvers are not accepted")]
    public void RemoveResolverThrows_WhenNullResolver()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = null;
      //Act
      //Assert
      objectUnderTest.RemoveResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    public void RemoveResolver_Basic()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = new TestResolver();
      //Act
      //Assert
      objectUnderTest.RemoveResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null resolvers are not accepted")]
    public void EnableResolverThrows_WhenNullResolver()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = null;
      //Act
      //Assert
      objectUnderTest.EnableResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    public void EnableResolver_Basic()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = new TestResolver();
      //Act
      //Assert
      objectUnderTest.EnableResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    [ExpectedException(exceptionType: typeof(ArgumentNullException), noExceptionMessage: "null resolvers are not accepted")]
    public void DisableResolverThrows_WhenNullResolver()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = null;
      //Act
      //Assert
      objectUnderTest.DisableResolver(resolver);
    }

    [TestMethod]
    [TestCategory("Resolver Management")]
    public void DisableResolver_Basic()
    {
      //Arrange
      IWordList[] sources = new IWordList[] { new TestWordList() { Language = "en" } };
      Func<string, IWordGenerator> wordGeneratorFactory = (a) => { return null; };
      var objectUnderTest = new AnagramResolverService(sources, wordGeneratorFactory);

      IAnagramResolver resolver = new TestResolver();
      //Act
      //Assert
      objectUnderTest.DisableResolver(resolver);
    }

    private class TestResolver : IAnagramResolver
    {
      public string Language => throw new NotImplementedException();

      public AnagramResolverType Type => throw new NotImplementedException();

      public Task<IEnumerable<string>> GetAnagramsAsync(string word)
      {
        throw new NotImplementedException();
      }

      public Task<IEnumerable<string>> GetAnagramsAsync(AnagramOptions options)
      {
        throw new NotImplementedException();
      }
    }
  }
}