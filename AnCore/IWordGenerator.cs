using System.Collections.Generic;

namespace AnCore
{
  public interface IWordGenerator
  {
    IEnumerable<char[]> GetPermutations();
  }
}