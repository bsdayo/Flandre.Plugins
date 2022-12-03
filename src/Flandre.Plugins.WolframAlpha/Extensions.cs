using Flandre.Framework;

namespace Flandre.Plugins.WolframAlpha;

public static class WolframAlphaPluginExtensions
{
    public static FlandreAppBuilder UseWolframAlphaPlugin(this FlandreAppBuilder builder,
        WolframAlphaPluginConfig? config = null)
    {
        return builder.UsePlugin<WolframAlphaPlugin, WolframAlphaPluginConfig>(
            config ?? new WolframAlphaPluginConfig());
    }
}