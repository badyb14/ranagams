using AnCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AnCoreUnitTests
{
  public partial class StringPermutationUnitTest
  {
    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_SingleLetterWord()
    {
      //Arrange
      var word = "a";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(0, indexCount);
      Assert.IsNull(actual, "there is no permutation for one letter word");
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_TwoLetterWord()
    {
      //Arrange
      var word = "ab";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(1, indexCount, "there is only one two letter word permutation");
      Assert.AreEqual(word.Length, actual.Length, "the length of the permutation must have same lenght as the original word.");
      Assert.AreEqual(word[1], actual[0]);
      Assert.AreEqual(word[0], actual[1]);
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_ThreeLetterWord()
    {
      //Arrange
      var word = "abc";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(Util.Factorial(word.Length) - 1, indexCount, "the number of permutations must be equal with Factorial(n)-1");
      Assert.AreEqual(word.Length, actual.Length, "the length of the permutation must have same lenght as the original word.");
    }

    /// <summary>
    /// Average english word length
    /// </summary>
    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_SixLetterWord()
    {
      //Arrange
      var word = "1234567";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(Util.Factorial(word.Length) - 1, indexCount, "the number of permutations must be equal with Factorial(n)-1");
      Assert.AreEqual(word.Length, actual.Length, "the length of the permutation must have same lenght as the original word.");
    }

    /// <summary>
    /// Double the average lenght
    /// </summary>
    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_12LetterWord()
    {
      //Arrange
      var word = "0123456789";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(Util.Factorial(word.Length) - 1, indexCount, "the number of permutations must be equal with Factorial(n)-1");
      Assert.AreEqual(word.Length, actual.Length, "the length of the permutation must have same lenght as the original word.");
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_TrivialWord1()
    {
      //Arrange
      var word = "aa";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(0, indexCount, "the number of permutations must be 0");
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_TrivialWord2()
    {
      //Arrange
      var word = "bbbbbbbbBBbbbbbbbbbbb";
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(0, indexCount, "the number of permutations must be 0");
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_NonTrivialWord1()
    {
      //Arrange
      var word = "aBa"; // test case sensitivity
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(word.Length - 1, indexCount, "the number of permutations must be word.Length - 1");
    }

    [TestMethod]
    [TestCategory("Count Correctness")]
    public void GetPermutations_Count_NonTrivialWord2()
    {
      //Arrange
      var word = "aaab"; // test case sensitivity
      var objectUnderTest = new StringPermutation(word);

      //Act
      var permutations = objectUnderTest.GetPermutations();
      char[] actual = null;
      var indexCount = 0;
      foreach (var perm in permutations)
      {
        actual = perm;
        indexCount++;
      }

      //Assert
      Assert.AreEqual(word.Length - 1, indexCount, "the number of permutations must be word.Length - 1");
    }

  }
}