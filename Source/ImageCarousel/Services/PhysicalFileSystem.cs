using System.IO;
using ImageCarousel.Interfaces;

namespace ImageCarousel.Services;

public class PhysicalFileSystem : IFileSystem
{
    public string[]? GetFiles(string path, string? searchPattern = null) => Directory.GetFiles(path, searchPattern ?? "*");
}