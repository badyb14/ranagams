using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace AnCoreUnitTests
{
  public partial class StringPermutationUnitTest
  {

    [TestMethod]
    [TestCategory("Content Correctness")]
    public void GetPermutations_NonTrivialWord()
    {
      //Arrange
      var word = "anagram"; // test case sensitivity
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
        Trace.TraceInformation(new string(actual));
      }
      
      //Assert
      //Assert.AreEqual(Util.Factorial(word.Length) - 2, indexCount, "the number of permutations must be 4");
    }
  }
}