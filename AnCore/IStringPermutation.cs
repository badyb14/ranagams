using System.Collections.Generic;

namespace AnCore
{
  public interface IStringPermutation
  {
    IEnumerable<char[]> GetPermutations();
  }
}