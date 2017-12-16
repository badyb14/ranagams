using System.Collections.Generic;

namespace AnCore
{
  /// <summary>
  /// Contract that a word source must implement
  /// </summary>
  public interface IWordList
  {
    /// <summary>
    /// Get the language
    /// </summary>
    string Language { get; }

    /// <summary>
    /// Get all words that it has.
    /// </summary>
    IEnumerable<string> WordList { get; }

    /// <summary>
    /// Check if word exist
    /// </summary>
    /// <param name="word"></param>
    /// <returns></returns>
    bool Contains(string word);

    /// <summary>
    /// Load the source
    /// </summary>
    void Load();
  }
}