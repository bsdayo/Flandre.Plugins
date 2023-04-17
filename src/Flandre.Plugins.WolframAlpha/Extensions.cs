using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.WolframAlpha;

public static class WolframAlphaPluginExtensions
{
    public static IPluginCollection AddWolframAlpha(this IPluginCollection plugins,
        IConfiguration? configuration)
    {
        return configuration is null
            ? plugins.Add<WolframAlphaPlugin>()
            : plugins.Add<WolframAlphaPlugin, WolframAlphaPluginOptions>(configuration);
    }

    public static IPluginCollection AddWolframAlpha(this IPluginCollection plugins,
        Action<WolframAlphaPluginOptions> action)
    {
        return plugins.Add<WolframAlphaPlugin, WolframAlphaPluginOptions>(action);
    }
}