using Flandre.Core.Attributes;
using Flandre.Core.Common;
using Flandre.Core.Messaging;
using Flandre.Core.Messaging.Segments;

namespace Flandre.Plugins.HttpCat;

[Plugin("HttpCat")]
public class HttpCatPlugin : Plugin
{
    private readonly HttpClient _httpClient = new();

    private readonly HttpCatPluginConfig _config;

    public HttpCatPlugin(HttpCatPluginConfig? config = null)
    {
        _config = config ?? new HttpCatPluginConfig();
    }

    [Command("httpcat <code:int>")]
    public async Task<MessageContent> OnHttpCat(MessageContext ctx, ParsedArgs args)
    {
        try
        {
            var code = args.GetArgument<int>("code");

            byte[] image;

            if (_config.EnableCache)
            {
                var path = $"{_config.CachePath}/{code}.jpg";
                if (File.Exists(path))
                    image = await File.ReadAllBytesAsync(path);
                else
                {
                    image = await GetImageFromApi(code);
                    await File.WriteAllBytesAsync(path, image);
                }
            }
            else
            {
                image = await GetImageFromApi(code);
            }

            return new MessageBuilder()
                .Image(ImageSegment.FromData(image));
        }
        catch (Exception e)
        {
            Logger.Error(e);
            return $"获取图片时发生错误：{e.Message}";
        }
    }

    private async Task<byte[]> GetImageFromApi(int code)
    {
        try
        {
            return await _httpClient.GetByteArrayAsync($"https://http.cat/{code}");
        }
        catch
        {
            return await _httpClient.GetByteArrayAsync("https://http.cat/404");
        }
    }
}

public class HttpCatPluginConfig
{
    public bool EnableCache { get; set; } = false;

    public string CachePath { get; set; } = ".";
}