using System.Text.Json.Serialization;

namespace Flandre.Plugins.BaiduTranslate;

#pragma warning disable CS8618
// ReSharper disable once UnusedAutoPropertyAccessor.Global
internal sealed class BaiduTranslateResponse
{
    [JsonPropertyName("from")] public string From { get; set; }

    [JsonPropertyName("to")] public string To { get; set; }

    [JsonPropertyName("trans_result")] public BaiduTranslateResult[] Result { get; set; }

    [JsonPropertyName("error_code")] public string? ErrorCode { get; set; }

    [JsonPropertyName("error_msg")] public string? ErrorMessage { get; set; }
}

internal sealed class BaiduTranslateResult
{
    [JsonPropertyName("src")] public string Src { get; set; }

    [JsonPropertyName("dst")] public string Dst { get; set; }
}