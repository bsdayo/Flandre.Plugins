using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Flandre.Plugins.BaiduTranslate;

public sealed partial class BaiduTranslatePlugin : Plugin
{
    private readonly BaiduTranslatePluginOptions _options;

    private readonly ILogger<BaiduTranslatePlugin> _logger;

    public BaiduTranslatePlugin(IOptionsSnapshot<BaiduTranslatePluginOptions> options,
        ILogger<BaiduTranslatePlugin> logger)
    {
        _options = options.Value;
        _logger = logger;
    }
}

public sealed class BaiduTranslatePluginOptions
{
    public string AppId { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}