using AnagramApi.Controllers;
using AnagramApi.Telemetry;
using AnCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramApiTests
{
  [TestClass]
  public class AnagramControllerUnitTest
  {
    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_WhenWordIsNull_DoesNotLog()
    {
      //Arrange 
      IAnagramResolverService resolver = null;
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = null;
      string language = null;

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(false, logger.LogCalled);
      Assert.AreEqual(1, (actual.Value as string[]).Length);
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_WhenWordIsEmpty_DoesNotLog()
    {
      //Arrange 
      IAnagramResolverService resolver = null;
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = "";
      string language = null;

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(false, logger.LogCalled);
      Assert.AreEqual(1, (actual.Value as string[]).Length);
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_WhenEmptyLanguage_DoesNotLog()
    {
      //Arrange 
      IAnagramResolverService resolver = null;
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = "s";
      string language = null;

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(false, logger.LogCalled);
      Assert.AreEqual(1, (actual.Value as string[]).Length);
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_WhenLongWords_DoesLog()
    {
      //Arrange 
      IAnagramResolverService resolver = null;
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = "0123456789AB";
      string language = "en";

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(true, logger.LogCalled);
      Assert.AreEqual(1, (actual.Value as string[]).Length);
    }


    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_WhenOtherLanguages_DoesLog()
    {
      //Arrange 
      IAnagramResolverService resolver = null;
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = "ab";
      string language = "gr";

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(true, logger.LogCalled);
      Assert.AreEqual(1, (actual.Value as string[]).Length);
    }

    [TestMethod]
    [TestCategory("GetAnagrams")]
    public void GetAnagrams_DoesLogExceptions_ButItDoesNotThrow()
    {
      //Arrange 
      IAnagramResolverService resolver = new TestResolver();
      TestLogger logger = new TestLogger();
      IAnagramResolverMetric metric = null;
      var objectUnderTest = new AnagramController(resolver, logger, metric);

      string word = "ab";
      string language = "en";

      //Act
      var actual = objectUnderTest.GetAnagrams(word, language).GetAwaiter().GetResult() as BadRequestObjectResult;

      //Assert
      Assert.AreEqual(true, logger.LogCalled);
      Assert.AreEqual("error", (actual.Value as string[]) [0]);
    }

    private class TestResolver : IAnagramResolverService
    {
      public void AddResolver(IAnagramResolver resolver)
      {
        throw new NotImplementedException();
      }

      public void DisableResolver(IAnagramResolver resolver)
      {
        throw new NotImplementedException();
      }

      public void EnableResolver(IAnagramResolver resolver)
      {
        throw new NotImplementedException();
      }

      public IEnumerable<string> GetAnagrams(string word, string language)
      {
        throw new NotImplementedException();
      }

      public Task<IEnumerable<string>> GetAnagramsAsync(string word, string language)
      {
        throw new NotImplementedException();
      }

      public Task<IEnumerable<string>> GetAnagramsAsync(AnagramOptions options)
      {
        throw new NotImplementedException();
      }

      public void RemoveResolver(IAnagramResolver resolver)
      {
        throw new NotImplementedException();
      }
    }

    private class TestLogger : ILogger<AnagramController>
    {
      public bool LogCalled = false;

      public IDisposable BeginScope<TState>(TState state)
      {
        return null;
      }

      public bool IsEnabled(LogLevel logLevel)
      {
        return true;
      }

      public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
      {
        LogCalled = true;
      }
    }
  }
}
