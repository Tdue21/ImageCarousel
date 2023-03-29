using FluentAssertions;
using ImageCarousel.Interfaces;
using ImageCarousel.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace ImageCarousel.Tests;

public class NextImageControllerTests
{
    [Fact]
    public void GetNextString_Test()
    {
        var images = new[]
                     {
                         "Path1\\Image1.png",
                         "Path1\\Image2.png",
                         "Path2\\Image1.png",
                         "Path2\\Image2.png",
                         "Path3\\Image1.png",
                     };
        var controller = SetupNextImageController();
        
        controller.GetNextString(images).Should().BeEquivalentTo(images[1]);
        controller.GetNextString(images).Should().BeEquivalentTo(images[4]);
        controller.GetNextString(images).Should().BeEquivalentTo(images[2]);
    }

    [Theory]
    [InlineData(1, "image2.png")]
    [InlineData(4, "image5.png")]
    [InlineData(2, "image3.png")]
    public async Task GetImage_Test(int index, string expected)
    {
        var controller = SetupNextImageController(index);
        var result = await controller.GetImage();

        var actual = result.As<VirtualFileHttpResult>();
        actual.Should().NotBeNull();
        actual.FileName.Should().BeEquivalentTo(expected);
    }

    /// <summary>
    /// Setting up the testing environment. 
    /// </summary>
    /// <returns></returns>
    private NextImageController SetupNextImageController(int? index = null)
    {
        var settings = new CarouselOptions
                       {
                           ImagePaths = new[] {"Path1"},
                           Extensions = new[] {"*.png"}
                       };
        var options     = Mock.Of<IOptions<CarouselOptions>>(x => x.Value == settings);
        var environment = Mock.Of<IWebHostEnvironment>(x => x.ContentRootPath == "C:\\web\\root");
        var fileSystem  = new Mock<IFileSystem>();

        fileSystem.Setup(x => x.ResolveAbsolutePath(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns<string, string>((s1, s2) => s2 + "\\" + s1);

        fileSystem.SetupSequence(x => x.GetFiles(It.IsAny<string>(), It.IsAny<string>()))
                  .Returns(new[] {"image1.png", "image2.png", "image3.png", "image4.png", "image5.png"});

        var random = new Mock<Random>();
        if(index == null)
        {
            random.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                  .Returns(1)
                  .Returns(4)
                  .Returns(2);
        }
        else
        {
            random.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                  .Returns(index.Value);
        }

        var controller = new NextImageController(options, environment, fileSystem.Object, random.Object);
        return controller;
    }
}