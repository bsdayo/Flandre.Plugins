using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Flandre.Core.Messaging;
using Flandre.Framework.Attributes;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.BaiduTranslate;

public partial class BaiduTranslatePlugin
{
    [Command("trans")]
    [StringShortcut("翻译", AllowArguments = true)]
    public async Task<MessageContent> OnTrans(
        [Option(ShortName = 's')] string source = "auto",
        [Option(ShortName = 't')] string target = "zh",
        params string[] text)
    {
        var textToTranslate = string.Join(' ', text);
        var sourceLang = BaiduTranslateUtils.GetLangCode(source);
        var targetLang = BaiduTranslateUtils.GetLangCode(target);

        try
        {
            var salt = new Random()
                .NextInt64(10000000000)
                .ToString()
                .PadLeft(10, '0');
            var sign = BitConverter.ToString(
                    MD5.Create().ComputeHash(Encoding.Default.GetBytes(
                        _options.AppId + textToTranslate + salt + _options.Token)))
                .Replace("-", "")
                .ToLower();

            var client = new HttpClient();
            var query = new StringBuilder()
                .Append($"?q={textToTranslate}")
                .Append($"&from={sourceLang}")
                .Append($"&to={targetLang}")
                .Append($"&appid={_options.AppId}")
                .Append($"&salt={salt}")
                .Append($"&sign={sign}")
                .ToString();

            var resp = await client.GetStringAsync(
                "https://fanyi-api.baidu.com/api/trans/vip/translate" + query);

            var data = JsonSerializer.Deserialize<BaiduTranslateResponse>(resp)!;

            if (data.ErrorCode is not null && data.ErrorCode != "52000")
            {
                var errorMessage = data.ErrorCode switch
                {
                    "52001" => "API 请求超时。",
                    "52002" => "系统错误。",
                    "52003" => "用户未授权，请联系开发者。",
                    "54000" => "参数错误，请联系开发者。",
                    "54001" => "签名错误，请联系开发者。",
                    "54003" => "API 调用频率达到限制，请稍后再试。",
                    "54004" => "API 账户余额不足，请联系开发者。",
                    "54005" => "短时间内翻译长文本的频率过高，请稍后再试。",
                    "58000" => "请求源 IP 未在白名单内，请联系开发者。",
                    "58001" => "不支持的翻译语言方向。",
                    "58002" => "服务已关闭，请联系开发者。",
                    "90107" => "API 认证失败，请联系开发者。",
                    _ => "未知错误。"
                };
                _logger.LogError("API 请求发生错误: {ErrorMessage}", errorMessage);
                return errorMessage;
            }

            return new MessageBuilder()
                .Text(
                    $"翻译结果 [{BaiduTranslateUtils.GetLangName(data.From)} > {BaiduTranslateUtils.GetLangName(data.To)}]\n")
                .Text(data.Result[0].Dst);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "发生错误。");
            return $"发生错误：{e.Message}";
        }
    }
}