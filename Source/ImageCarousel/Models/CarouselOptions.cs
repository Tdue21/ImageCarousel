namespace ImageCarousel.Models;

/// <summary>
/// Represents carousel configuration read from appsettings.json.
/// </summary>
public class CarouselOptions
{
    /// <summary>
    /// Constant defining the configuration section name.
    /// </summary>
    public const string Carousel = "Carousel";

    /// <summary>
    /// Gets or sets an array of relative or absolute paths for where the application will look for images. 
    /// </summary>
    public string[]? ImagePaths { get; set; }

    /// <summary>
    /// Gets or sets an array of image file extensions, that are recognized by the application.
    /// Currently only *.png is supported.
    /// </summary>
    public string[]? Extensions { get; set; }
}