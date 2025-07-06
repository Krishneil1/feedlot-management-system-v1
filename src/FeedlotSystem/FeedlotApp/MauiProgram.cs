// -------------------------------------------------------------------------------------------------
// MauiProgram.cs -- Configures and builds the MAUI application.
// -------------------------------------------------------------------------------------------------

using Microsoft.Extensions.Logging;

namespace FeedlotApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("bootstrap-icons.ttf", "BootstrapIcons");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
