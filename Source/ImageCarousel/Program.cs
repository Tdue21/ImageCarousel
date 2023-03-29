using System;
using ImageCarousel;
using ImageCarousel.Interfaces;
using ImageCarousel.Models;
using ImageCarousel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var builder     = WebApplication.CreateBuilder(args);
var environment = builder.Environment;
var configuration = builder.Configuration
                           .AddJsonFile("appsettings.json", false, true)
                           .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true).Build();

builder.Services
       .Configure<CarouselOptions>(configuration.GetSection(CarouselOptions.Carousel))
       .AddTransient<IFileSystem, PhysicalFileSystem>()
       .AddSingleton(Random.Shared)
       .AddSingleton<NextImageController>()
       .AddHttpsRedirection(options => { });

var app = builder.Build();

app.MapGet("/image.png", (NextImageController controller) => controller.GetImage());
app.Run();