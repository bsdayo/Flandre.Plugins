using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.BaiduTranslate;

public sealed partial class BaiduTranslatePlugin : Plugin
{
    private readonly BaiduTranslatePluginConfig _config;

    private readonly ILogger<BaiduTranslatePlugin> _logger;

    public BaiduTranslatePlugin(BaiduTranslatePluginConfig config, ILogger<BaiduTranslatePlugin> logger)
    {
        _config = config;
        _logger = logger;
    }
}

public sealed class BaiduTranslatePluginConfig
{
    public string AppId { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}