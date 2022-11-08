using Flandre.Core.Attributes;
using Flandre.Core.Common;
using Flandre.Core.Messaging;

namespace Flandre.Plugins.DeepL;

public partial class DeepLPlugin
{
    [Command("trans <text:text>")]
    [Option("source", "-s [source: string = auto]")]
    [Option("target", "-t [target: string = zh]")]
    public async Task<MessageContent> OnTrans(MessageContext ctx, ParsedArgs args)
    {
        var text = args.GetArgument<string>("text");
        var target = DeepLUtils.GetLanguageCode(args.GetOption<string>("source"));
        var source = DeepLUtils.GetLanguageCode(args.GetOption<string>("source"));
        if (target is null)
            return "未知目标语言。";

        var translation = await _translator.TranslateTextAsync(text, source, target);
        var sourceLangName = DeepLUtils.GetLanguageName(translation.DetectedSourceLanguageCode);
        var targetLangName = DeepLUtils.GetLanguageName(target);

        return new MessageBuilder()
            .Text($"翻译结果 [{sourceLangName} > {targetLangName}]\n")
            .Text(translation.Text);
    }
}