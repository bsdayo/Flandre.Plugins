using DeepL;
using Flandre.Core.Attributes;
using Flandre.Core.Common;

namespace Flandre.Plugins.DeepL;

[Plugin("DeepL")]
public partial class DeepLPlugin : Plugin
{
    private readonly Translator _translator;

    public DeepLPlugin(DeepLPluginConfig config)
    {
        if (string.IsNullOrEmpty(config.AuthKey))
            Logger.Warning("DeepL 插件需要 Auth Key，请在插件配置中传入。");

        _translator = new Translator(config.AuthKey,
            new TranslatorOptions { MaximumNetworkRetries = config.MaximumNetworkRetries });
    }
}

public class DeepLPluginConfig
{
    public string AuthKey { get; set; } = string.Empty;

    public int MaximumNetworkRetries { get; set; } = 5;
}