using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ImageCarousel.Interfaces;
using ImageCarousel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ImageCarousel;

public class NextImageController
{
    private readonly HostSettings _hostSettings;
    private readonly IFileSystem _fileSystem;
    private readonly CarouselOptions _settings;

    public NextImageController(HostSettings hostSettings, IOptions<CarouselOptions> options, IFileSystem fileSystem)
    {
        _hostSettings = hostSettings ?? throw new ArgumentNullException(nameof(hostSettings));
        _fileSystem = fileSystem ?? throw new ArgumentNullException(nameof(fileSystem));
        _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    public async Task<IResult> GetImage()
    {
        var images = new List<string>();

        if(_settings.ImagePaths != null && _settings.ImagePaths.Length != 0)
        {
            foreach (var path in _settings.ImagePaths)
            {
                if (_settings.Extensions != null)
                {
                    foreach (var ext in _settings.Extensions)
                    {
                        var files = _fileSystem.GetFiles(path, ext);
                        images.AddRange((files ?? Array.Empty<string>()).Where(x => !string.IsNullOrEmpty(x)));
                    }
                }
            }
        }

        var nextImage = _hostSettings.GetNextString(images);
        var extension = nextImage[nextImage.LastIndexOf(".", StringComparison.Ordinal)..];
        var content = extension switch
        {
            "png" => "image/png",
            "jpg" => "image/jpg",
            _     => ""
        };

        return await Task.FromResult(Results.File(nextImage, content));
    }
}