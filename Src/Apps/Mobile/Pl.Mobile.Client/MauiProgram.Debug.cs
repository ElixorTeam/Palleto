#if DEBUG

using BarcodeScanning;
using Microsoft.Extensions.Logging;

namespace Pl.Mobile.Client;

public static partial class MauiProgram
{
    static partial void ConfigureDebugServices(MauiAppBuilder builder)
    {
        builder.UseBarcodeScanning();
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
    }
}

#endif