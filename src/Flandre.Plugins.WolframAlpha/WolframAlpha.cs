using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;
using Genbox.WolframAlpha;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Flandre.Plugins.WolframAlpha;

public sealed class WolframAlphaPlugin : Plugin
{
    private readonly WolframAlphaPluginOptions _options;

    private readonly ILogger<WolframAlphaPlugin> _logger;

    public WolframAlphaPlugin(IOptionsSnapshot<WolframAlphaPluginOptions> options, ILogger<WolframAlphaPlugin> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    [Command("wa")]
    public async Task<MessageContent> OnWolframAlpha(params string[] query)
    {
        var client = new WolframAlphaClient(_options.AppId);
        var queryText = string.Join(' ', query);

        var results = await client.FullResultAsync(queryText);

        _logger.LogDebug("Searching Time: {SearchingTime}", results.Timing);

        if (!results.Pods.Any())
            return "没有找到相关的内容。";

        var image = await WolframAlphaImageGenerator.Generate(results, _options.FontPath, _logger);

        return new MessageBuilder().Image(image);
    }
}

public sealed class WolframAlphaPluginOptions
{
    public string AppId { get; set; } = string.Empty;

    public string FontPath { get; set; } = string.Empty;
}