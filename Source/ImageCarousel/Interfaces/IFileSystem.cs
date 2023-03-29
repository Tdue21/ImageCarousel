namespace ImageCarousel.Interfaces;

/// <summary>
/// Abstraction interface for file system operations. 
/// </summary>
public interface IFileSystem
{
    /// <summary>
    /// Returns an array of fully qualified strings representing all files matching the search pattern on the specified path.
    /// </summary>
    ///
    /// <param name="path">The relative or absolute path to the directory to search. This string is not case-sensitive.</param>
    ///
    /// <param name="searchPattern">The search string to match against the names of files in path.
    /// This parameter can contain a combination of valid literal path and wildcard (* and ?)
    /// characters, but it doesn't support regular expressions.</param>
    ///
    /// <returns>An array of the full names (including paths) for the files in the specified directory that match
    /// the specified search pattern, or an empty array if no files are found.</returns>
    string[]? GetFiles(string path, string? searchPattern = null);

    /// <summary>
    /// Returns the absolute path for the specified path, with an optional base path used for relative paths.
    /// </summary>
    /// <param name="path">The file or directory for which to obtain absolute path information.</param>
    /// <param name="basePath">The beginning of a fully qualified path.</param>
    /// <returns>The absolute path.</returns>
    string ResolveAbsolutePath(string path, string basePath = null!);
}