using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ImageCarousel;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder ConfigureApplication(this WebApplicationBuilder builder, Action<IWebHostEnvironment, ConfigurationManager> configuration)
    {
        configuration.Invoke(builder.Environment, builder.Configuration);
        return builder;
    }

    public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder, Action<IServiceCollection> servicesHandler)
    {
        servicesHandler.Invoke(builder.Services);
        return builder;
    }
}