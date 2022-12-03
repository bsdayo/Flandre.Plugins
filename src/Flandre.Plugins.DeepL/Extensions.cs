using Flandre.Framework;

namespace Flandre.Plugins.DeepL;

public static class DeepLPluginExtensions
{
    public static FlandreAppBuilder UseDeepLPlugin(this FlandreAppBuilder builder,
        DeepLPluginConfig? config = null)
    {
        return builder.UsePlugin<DeepLPlugin, DeepLPluginConfig>(
            config ?? new DeepLPluginConfig());
    }
}