using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.HttpCat;

public static class HttpCatPluginExtensions
{
    public static FlandreAppBuilder AddHttpCatPlugin(this FlandreAppBuilder builder,
        IConfiguration? configuration)
    {
        return configuration is null
            ? builder.AddPlugin<HttpCatPlugin>()
            : builder.AddPlugin<HttpCatPlugin, HttpCatPluginOptions>(configuration);
    }

    public static FlandreAppBuilder AddHttpCatPlugin(this FlandreAppBuilder builder,
        Action<HttpCatPluginOptions> action)
    {
        return builder.AddPlugin<HttpCatPlugin, HttpCatPluginOptions>(action);
    }
}