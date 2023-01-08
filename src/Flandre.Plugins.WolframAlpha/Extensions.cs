using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.WolframAlpha;

public static class WolframAlphaPluginExtensions
{
    public static FlandreAppBuilder AddWolframAlphaPlugin(this FlandreAppBuilder builder,
        IConfiguration? configuration)
    {
        return configuration is null
            ? builder.AddPlugin<WolframAlphaPlugin>()
            : builder.AddPlugin<WolframAlphaPlugin, WolframAlphaPluginOptions>(configuration);
    }

    public static FlandreAppBuilder AddWolframAlphaPlugin(this FlandreAppBuilder builder,
        Action<WolframAlphaPluginOptions> action)
    {
        return builder.AddPlugin<WolframAlphaPlugin, WolframAlphaPluginOptions>(action);
    }
}