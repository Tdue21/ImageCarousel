namespace ImageCarousel.Interfaces;

public interface IFileSystem
{
    string[]? GetFiles(string path, string? searchPattern = null);
}