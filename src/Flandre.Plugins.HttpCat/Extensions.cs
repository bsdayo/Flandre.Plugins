using Flandre.Framework;

namespace Flandre.Plugins.HttpCat;

public static class HttpCatPluginExtensions
{
    public static FlandreAppBuilder UseHttpCatPlugin(this FlandreAppBuilder builder,
        HttpCatPluginConfig? config = null)
    {
        return builder.UsePlugin<HttpCatPlugin, HttpCatPluginConfig>(
            config ?? new HttpCatPluginConfig());
    }
}