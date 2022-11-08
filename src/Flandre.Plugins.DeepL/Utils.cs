using DeepL;

namespace Flandre.Plugins.DeepL;

internal static class DeepLUtils
{
    private static readonly string[][] LanguageCodeMap =
    {
        new[] { "保加利亚语", "bg", LanguageCode.Bulgarian },
        new[] { "捷克语", "cs", LanguageCode.Czech },
        new[] { "丹麦语", "da", LanguageCode.Danish },
        new[] { "德语", "de", LanguageCode.German },
        new[] { "希腊语", "el", LanguageCode.Greek },
        new[] { "英语", "英文", "en", LanguageCode.English },
        new[] { "英式英语", "en-gb", LanguageCode.EnglishBritish },
        new[] { "美式英语", "en-us", LanguageCode.EnglishAmerican },
        new[] { "西班牙语", "es", LanguageCode.Spanish },
        new[] { "爱沙尼亚语", "et", LanguageCode.Estonian },
        new[] { "芬兰语", "fi", LanguageCode.Finnish },
        new[] { "法语", "fr", LanguageCode.French },
        new[] { "匈牙利语", "hu", LanguageCode.Hungarian },
        new[] { "印度尼西亚语", "id", LanguageCode.Indonesian },
        new[] { "意大利语", "it", LanguageCode.Italian },
        new[] { "日语", "日文", "ja", LanguageCode.Japanese },
        new[] { "立陶宛语", "lt", LanguageCode.Lithuanian },
        new[] { "拉脱维亚语", "lv", LanguageCode.Latvian },
        new[] { "荷兰语", "nl", LanguageCode.Dutch },
        new[] { "波兰语", "pl", LanguageCode.Polish },
        new[] { "葡萄牙语", "pt", LanguageCode.Portuguese },
        new[] { "巴西葡萄牙语", "pt-br", LanguageCode.PortugueseBrazilian },
        new[] { "欧洲葡萄牙语", "pt-pt", LanguageCode.PortugueseEuropean },
        new[] { "罗马尼亚语", "ro", LanguageCode.Romanian },
        new[] { "俄语", "俄文", "ru", LanguageCode.Russian },
        new[] { "斯洛伐克语", "sk", LanguageCode.Slovak },
        new[] { "斯洛文尼亚语", "sl", LanguageCode.Slovenian },
        new[] { "瑞典语", "sv", LanguageCode.Swedish },
        new[] { "土耳其语", "tr", LanguageCode.Turkish },
        new[] { "乌克兰语", "uk", LanguageCode.Ukrainian },
        new[] { "中文", "简体中文", "zh", "zh-cn", LanguageCode.Chinese },
    };

    internal static string GetLanguageName(string source)
        => LanguageCodeMap.FirstOrDefault(
            lang => lang.Contains(source.ToLower()))?[0] ?? source;

    internal static string? GetLanguageCode(string source)
        => LanguageCodeMap.FirstOrDefault(
            lang => lang.Contains(source.ToLower()))?[^1];
}