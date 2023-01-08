using Flandre.Core.Messaging;
using Flandre.Core.Messaging.Segments;
using Flandre.Framework.Attributes;
using Flandre.Framework.Common;
using Microsoft.Extensions.Logging;

namespace Flandre.Plugins.HttpCat;

public sealed class HttpCatPlugin : Plugin
{
    private readonly HttpClient _httpClient = new();

    private readonly HttpCatPluginOptions _options;

    private readonly ILogger<HttpCatPlugin> _logger;

    public HttpCatPlugin(HttpCatPluginOptions options, ILogger<HttpCatPlugin> logger)
    {
        _options = options;
        _logger = logger;
    }

    [Command("httpcat <code:int>")]
    public async Task<MessageContent> OnHttpCat(MessageContext _, ParsedArgs args)
    {
        try
        {
            var code = args.GetArgument<int>("code");

            byte[] image;

            if (_options.EnableCache)
            {
                var path = $"{_options.CachePath}/{code}.jpg";
                if (File.Exists(path))
                {
                    image = await File.ReadAllBytesAsync(path);
                }
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
            _logger.LogError(e, "发生错误。");
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

public sealed class HttpCatPluginOptions
{
    public bool EnableCache { get; set; } = false;

    public string CachePath { get; set; } = ".";
}