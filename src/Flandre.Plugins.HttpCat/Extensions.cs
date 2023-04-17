using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.HttpCat;

public static class HttpCatPluginExtensions
{
    public static IPluginCollection AddHttpCat(this IPluginCollection plugins,
        IConfiguration? configuration)
    {
        return configuration is null
            ? plugins.Add<HttpCatPlugin>()
            : plugins.Add<HttpCatPlugin, HttpCatPluginOptions>(configuration);
    }

    public static IPluginCollection AddHttpCat(this IPluginCollection plugins,
        Action<HttpCatPluginOptions> action)
    {
        return plugins.Add<HttpCatPlugin, HttpCatPluginOptions>(action);
    }
}