using DeepL;
using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Flandre.Plugins.DeepL;

public sealed partial class DeepLPlugin : Plugin
{
    private readonly Translator _translator;

    private readonly ILogger<DeepLPlugin> _logger;

    public DeepLPlugin(IOptions<DeepLPluginOptions> options, ILogger<DeepLPlugin> logger)
    {
        if (string.IsNullOrEmpty(options.Value.AuthKey))
            logger.LogWarning("DeepL 插件需要 Auth Key，请在插件配置中传入。");

        _translator = new Translator(options.Value.AuthKey,
            new TranslatorOptions { MaximumNetworkRetries = options.Value.MaximumNetworkRetries });
        _logger = logger;
    }
}

public sealed class DeepLPluginOptions
{
    public string AuthKey { get; set; } = string.Empty;

    public int MaximumNetworkRetries { get; set; } = 5;
}