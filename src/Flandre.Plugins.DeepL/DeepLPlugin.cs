using DeepL;
using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.DeepL;

public sealed partial class DeepLPlugin : Plugin
{
    private readonly Translator _translator;

    private readonly ILogger<DeepLPlugin> _logger;

    public DeepLPlugin(DeepLPluginConfig config, ILogger<DeepLPlugin> logger)
    {
        if (string.IsNullOrEmpty(config.AuthKey))
            logger.LogWarning("DeepL 插件需要 Auth Key，请在插件配置中传入。");

        _translator = new Translator(config.AuthKey,
            new TranslatorOptions { MaximumNetworkRetries = config.MaximumNetworkRetries });
        _logger = logger;
    }
}

public sealed class DeepLPluginConfig
{
    public string AuthKey { get; set; } = string.Empty;

    public int MaximumNetworkRetries { get; set; } = 5;
}