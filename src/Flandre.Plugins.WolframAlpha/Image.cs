using Genbox.WolframAlpha.Responses;
using Microsoft.Extensions.Logging;
using SkiaSharp;

namespace Flandre.Plugins.WolframAlpha;

internal class WolframAlphaImageGenerator
{
    internal static async Task<byte[]> Generate(FullResultResponse result, string fontPath, ILogger logger)
    {
        return await Task.Run(() =>
        {
            var totalHeight = 0;
            var maxWidth = 500;
            var imageTasks = new List<Task>();
            var images = new Dictionary<int, byte[]>();

            foreach (var pod in result.Pods)
            {
                totalHeight += 30;
                foreach (var subPod in pod.SubPods)
                {
                    var y = totalHeight;
                    var task = Task.Run(() =>
                    {
                        var image = new HttpClient().GetByteArrayAsync(subPod.Image.Src).Result;
                        images[y] = image;
                        logger.LogDebug("Downloaded image: {ImageUrl}", subPod.Image.Src);
                    });
                    imageTasks.Add(task);

                    totalHeight += subPod.Image.Height + 10;
                    if (subPod.Image.Width > maxWidth)
                        maxWidth = subPod.Image.Width + 50;
                }
            }

            totalHeight += 30;

            Task.WaitAll(imageTasks.ToArray());

            var imageInfo = new SKImageInfo(maxWidth, totalHeight);
            using var surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            using var fontBold = SKTypeface.FromFile(fontPath);

            using var backgroundPaint = new SKPaint
            {
                Color = SKColors.White
            };
            using var titlePaint = new SKPaint
            {
                Color = SKColor.Parse("#f96932"),
                TextSize = 18,
                IsAntialias = true,
                Typeface = fontBold
            };
            using var rectPaint = new SKPaint
            {
                Color = SKColor.Parse("e2e2e2")
            };

            canvas.DrawRect(0, 0, imageInfo.Width, totalHeight, backgroundPaint);

            var yPosition = 0;

            foreach (var pod in result.Pods)
            {
                canvas.DrawRect(0, yPosition, imageInfo.Width, 30, rectPaint);
                canvas.DrawText(pod.Title, 8, yPosition + 20, titlePaint);
                yPosition += 30;

                foreach (var subPod in pod.SubPods)
                    yPosition += subPod.Image.Height + 10;
            }

            foreach (var (y, image) in images)
            {
                using var imageBitmap = SKBitmap.Decode(image);
                canvas.DrawBitmap(imageBitmap, 16, y + 5);
            }

            titlePaint.Color = SKColor.Parse("#dd0e00");
            canvas.DrawText("by Wolfram Alpha Non-Commercial API", 8, yPosition + 20, titlePaint);

            var data = surface
                .Snapshot()
                .Encode(SKEncodedImageFormat.Jpeg, 90)
                .ToArray();

            return data;
        });
    }
}