using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.DeepL;

public static class DeepLPluginExtensions
{
    public static FlandreAppBuilder AddDeepLPlugin(this FlandreAppBuilder builder,
        IConfiguration? configuration = null)
    {
        return configuration is null
            ? builder.AddPlugin<DeepLPlugin>()
            : builder.AddPlugin<DeepLPlugin, DeepLPluginOptions>(configuration);
    }

    public static FlandreAppBuilder AddDeepLPlugin(this FlandreAppBuilder builder,
        Action<DeepLPluginOptions> action)
    {
        return builder.AddPlugin<DeepLPlugin, DeepLPluginOptions>(action);
    }
}