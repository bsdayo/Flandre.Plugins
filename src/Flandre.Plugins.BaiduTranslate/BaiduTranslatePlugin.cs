using Flandre.Core.Attributes;
using Flandre.Core.Common;

namespace Flandre.Plugins.BaiduTranslate;

[Plugin("BaiduTranslate")]
public partial class BaiduTranslatePlugin : Plugin
{
    private readonly BaiduTranslatePluginConfig _config;
    
    public BaiduTranslatePlugin(BaiduTranslatePluginConfig config)
    {
        _config = config;
    }
}

public class BaiduTranslatePluginConfig
{
    public string AppId { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}