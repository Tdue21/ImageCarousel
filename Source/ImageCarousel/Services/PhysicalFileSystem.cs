using System.IO;
using ImageCarousel.Interfaces;

namespace ImageCarousel.Services;

/// <summary>
/// Concrete implementation of the <see cref="IFileSystem"/> interface.
/// </summary>
public class PhysicalFileSystem : IFileSystem
{
    /// <inheritdoc />
    public string[] GetFiles(string path, string? searchPattern = null) => Directory.GetFiles(path, searchPattern ?? "*");

    /// <inheritdoc />
    public string ResolveAbsolutePath(string path, string basePath = null!)
    {
        return Path.IsPathFullyQualified(path) && string.IsNullOrEmpty(basePath)
                   ? Path.GetFullPath(path) 
                   : Path.GetFullPath(path, basePath!);
    }
}