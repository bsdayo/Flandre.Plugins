using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.BaiduTranslate;

public static class BaiduTranslatePluginExtensions
{
    public static IPluginCollection AddBaiduTranslate(this IPluginCollection plugins,
        IConfiguration? configuration)
    {
        return configuration is null
            ? plugins.Add<BaiduTranslatePlugin>()
            : plugins.Add<BaiduTranslatePlugin, BaiduTranslatePluginOptions>(configuration);
    }

    public static IPluginCollection AddBaiduTranslate(this IPluginCollection plugins,
        Action<BaiduTranslatePluginOptions> action)
    {
        return plugins.Add<BaiduTranslatePlugin, BaiduTranslatePluginOptions>(action);
    }
}