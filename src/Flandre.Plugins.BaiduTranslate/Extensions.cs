using Flandre.Framework;
using Microsoft.Extensions.Configuration;

namespace Flandre.Plugins.BaiduTranslate;

public static class BaiduTranslatePluginExtensions
{
    public static FlandreAppBuilder AddBaiduTranslatePlugin(this FlandreAppBuilder builder,
        IConfiguration? configuration)
    {
        return configuration is null
            ? builder.AddPlugin<BaiduTranslatePlugin>()
            : builder.AddPlugin<BaiduTranslatePlugin, BaiduTranslatePluginOptions>(configuration);
    }

    public static FlandreAppBuilder AddBaiduTranslatePlugin(this FlandreAppBuilder builder,
        Action<BaiduTranslatePluginOptions> action)
    {
        return builder.AddPlugin<BaiduTranslatePlugin, BaiduTranslatePluginOptions>(action);
    }
}