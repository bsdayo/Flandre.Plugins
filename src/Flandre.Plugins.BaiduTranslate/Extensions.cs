using Flandre.Framework;

namespace Flandre.Plugins.BaiduTranslate;

public static class BaiduTranslatePluginExtensions
{
    public static FlandreAppBuilder UseBaiduTranslatePlugin(this FlandreAppBuilder builder,
        BaiduTranslatePluginConfig? config = null)
    {
        return builder.UsePlugin<BaiduTranslatePlugin, BaiduTranslatePluginConfig>(
            config ?? new BaiduTranslatePluginConfig());
    }
}