using Flandre.Core.Attributes;
using Flandre.Core.Common;
using Flandre.Core.Messaging;
using Genbox.WolframAlpha;

namespace Flandre.Plugins.WolframAlpha;

[Plugin("WolframAlpha")]
public class WolframAlphaPlugin : Plugin
{
    private readonly WolframAlphaConfig _config;

    public WolframAlphaPlugin(WolframAlphaConfig config)
    {
        _config = config;
    }

    [Command("wa <query:string>")]
    public async Task<MessageContent> OnWolframAlpha(MessageContext _, ParsedArgs args)
    {
        var client = new WolframAlphaClient(_config.AppId);

        var results = await client.FullResultAsync(args.GetArgument<string>("query"));

        Logger.Debug("Searching Timing: " + results.Timing);

        var image = await WolframAlphaImageGenerator.Generate(results, _config.FontPath, Logger);

        return new MessageBuilder().Image(image);
    }
}

public class WolframAlphaConfig
{
    public string AppId { get; set; } = string.Empty;

    public string FontPath { get; set; } = string.Empty;
}