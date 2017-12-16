using System.Collections.Generic;

namespace AnCore
{
  public interface IWordList
  {
    string Language { get; }
    IEnumerable<string> WordList { get; }
  }
}