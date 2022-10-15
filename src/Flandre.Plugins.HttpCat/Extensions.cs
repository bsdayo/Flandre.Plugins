using Flandre.Core;

namespace Flandre.Plugins.HttpCat;

public static class FlandreAppExtensions
{
    public static FlandreApp UseHttpCatPlugin(this FlandreApp app, HttpCatPluginConfig? config = null)
    {
        return app.Use(new HttpCatPlugin(config));
    }
}