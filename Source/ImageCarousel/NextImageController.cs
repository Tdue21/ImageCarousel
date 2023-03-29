using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ImageCarousel.Interfaces;
using ImageCarousel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

[assembly:InternalsVisibleTo("ImageCarousel.Tests")]

namespace ImageCarousel;

/// <summary>
/// The controller determining and responding with a random image from the configured stores.
/// </summary>
public class NextImageController
{
    private readonly IWebHostEnvironment _environment;
    private readonly IFileSystem _fileSystem;
    private readonly CarouselOptions _settings;
    private readonly Random _random;

    /// <summary>
    /// Constructor for the <see cref="NextImageController"/> class. 
    /// </summary>
    /// <param name="options">An instance object of the settings for the controller</param>
    /// <param name="environment">A reference to the web host environment</param>
    /// <param name="fileSystem">A reference to the file system</param>
    /// <param name="random">A <see cref="Random"/> object reference.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public NextImageController(IOptions<CarouselOptions> options, IWebHostEnvironment environment, IFileSystem fileSystem, Random random)
    {
        _fileSystem  = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        _settings    = options?.Value ?? throw new ArgumentNullException(nameof(options));
        _random      = random ?? throw new ArgumentNullException(nameof(random));
        _environment = environment ?? throw new ArgumentNullException(nameof(environment));
    }

    /// <summary>
    /// Determines a random image image based on the configuration and returns it to the caller.
    /// </summary>
    /// <returns>A <see cref="IResult"/> object containing the image.</returns>
    public async Task<IResult> GetImage()
    {
        var images = new List<string>();

        if(_settings.ImagePaths != null && _settings.ImagePaths.Length != 0)
        {
            foreach (var path in _settings.ImagePaths)
            {
                if (_settings.Extensions != null)
                {
                    var fullPath = _fileSystem.ResolveAbsolutePath(path, _environment.ContentRootPath);
                    foreach (var ext in _settings.Extensions)
                    {
                        var files = _fileSystem.GetFiles(fullPath, ext);
                        images.AddRange((files ?? Array.Empty<string>()).Where(x => !string.IsNullOrEmpty(x)));
                    }
                }
            }
        }
        var nextImage = GetNextString(images);
        var result    = Results.File(nextImage, "image/png");
        return await Task.FromResult(result);
    }

    /// <summary>
    /// Selects a random image from the supplied list.
    /// </summary>
    /// <param name="images">A list of image paths.</param>
    /// <returns>One fully qualified path of an image file.</returns>
    internal string GetNextString(IEnumerable<string> images)
    {
        var imageList = images.ToArray();
        var count     = imageList.Length;
        var index     = _random.Next(0, count);

        return imageList[index];
    }
}