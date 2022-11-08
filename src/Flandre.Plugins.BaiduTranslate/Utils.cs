namespace Flandre.Plugins.BaiduTranslate;

internal static class BaiduTranslateUtils
{
    private static readonly string[][] LanguageMap =
    {
        // langCode, langName, ...langAlias
        new[] { "auto", "自动", "自动检测" },
        new[] { "zh", "中文", "简中" },
        new[] { "en", "英语", "英文" },
        new[] { "yue", "粤语" },
        new[] { "wyw", "文言文" },
        new[] { "jp", "日语", "日文" },
        new[] { "kor", "韩语", "韩文" },
        new[] { "fra", "法语", "法文" },
        new[] { "spa", "西班牙语" },
        new[] { "th", "泰语" },
        new[] { "ara", "阿拉伯语" },
        new[] { "ru", "俄语", "俄文" },
        new[] { "pt", "葡萄牙语" },
        new[] { "de", "德语", "德文" },
        new[] { "it", "意大利语" },
        new[] { "el", "希腊语" },
        new[] { "nl", "荷兰语" },
        new[] { "pl", "波兰语" },
        new[] { "bul", "保加利亚语" },
        new[] { "est", "爱沙尼亚语" },
        new[] { "dan", "丹麦语" },
        new[] { "fin", "芬兰语" },
        new[] { "cs", "捷克语" },
        new[] { "rom", "罗马尼亚语" },
        new[] { "slo", "斯洛文尼亚语" },
        new[] { "swe", "瑞典语" },
        new[] { "hu", "匈牙利语" },
        new[] { "cht", "繁体中文", "繁中" },
        new[] { "vie", "越南语" }
    };

    internal static string GetLangCode(string lang)
        => LanguageMap.FirstOrDefault(l => l.Contains(lang))?[0] ?? lang;


    internal static string GetLangName(string lang)
        => LanguageMap.FirstOrDefault(l => l.Contains(lang))?[1] ?? lang;
}