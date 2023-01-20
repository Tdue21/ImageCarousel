using ImageCarousel;
using ImageCarousel.Interfaces;
using ImageCarousel.Models;
using ImageCarousel.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders.Internal;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApplication((environment, configuration) =>
                     {
                         configuration.AddJsonFile("appsettings.json", false, true);
                         if (environment.IsDevelopment())
                         {
                             configuration.AddJsonFile($"appsettings.{environment.EnvironmentName}.json", false, true);
                         }
                     });

builder.ConfigureServices(services =>
        {
            var configuration = builder.Configuration;
            var environment = builder.Environment;

            var hostSetting = new HostSettings
                              {
                                  WebRootPath = environment.WebRootPath,
                                  ContentRootPath = environment.ContentRootPath
                              };

            services.Configure<CarouselOptions>(configuration.GetSection(CarouselOptions.Carousel));
            services.AddTransient<IFileSystem, PhysicalFileSystem>();
            services.AddSingleton(hostSetting);
            services.AddSingleton<NextImageController>();
        });

var app = builder.Build();

app.MapGet("/", (NextImageController controller) => controller.GetImage());
app.Run();
