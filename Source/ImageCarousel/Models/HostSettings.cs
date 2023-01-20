using System;
using System.Collections.Generic;
using System.Linq;

namespace ImageCarousel.Models;

public class HostSettings
{
    private readonly Random _random;

    public HostSettings()
    {
        _random = Random.Shared;
    }

    public string? WebRootPath { get; set; }
    public string? ContentRootPath { get; set; }


    public string GetNextString(IEnumerable<string> images)
    {
        var imageList = images.ToArray();
        var count = imageList.Length;
        var index = _random.Next(0, count);

        return imageList[index];
    }
}