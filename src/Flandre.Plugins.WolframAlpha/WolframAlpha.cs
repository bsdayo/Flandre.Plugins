using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;
using Genbox.WolframAlpha;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.WolframAlpha;

public sealed class WolframAlphaPlugin : Plugin
{
    private readonly WolframAlphaPluginConfig _config;

    private readonly ILogger<WolframAlphaPlugin> _logger;

    public WolframAlphaPlugin(WolframAlphaPluginConfig config, ILogger<WolframAlphaPlugin> logger)
    {
        _config = config;
        _logger = logger;
    }

    [Command("wa <query:text>")]
    public async Task<MessageContent> OnWolframAlpha(MessageContext _, ParsedArgs args)
    {
        var client = new WolframAlphaClient(_config.AppId);

        var results = await client.FullResultAsync(args.GetArgument<string>("query"));

        _logger.LogDebug("Searching Time: {SearchingTime}", results.Timing);

        var image = await WolframAlphaImageGenerator.Generate(results, _config.FontPath, _logger);

        return new MessageBuilder().Image(image);
    }
}

public sealed class WolframAlphaPluginConfig
{
    public string AppId { get; set; } = string.Empty;

    public string FontPath { get; set; } = string.Empty;
}
