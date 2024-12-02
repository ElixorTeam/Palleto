using Append.Blazor.Printing;
using MauiPageFullScreen;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Pl.Desktop.Client.Source.Shared.Api;
using Pl.Desktop.Client.Source.Shared.Extensions;
using Pl.Desktop.Client.Source.Shared.Services.Devices;
using Pl.Shared.Web.Extensions;
using TailwindMerge.Extensions;

namespace Pl.Desktop.Client;

public static class MauiProgram
{
    public static MauiAppBuilder CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder.Configuration.LoadAppSettings<IDesktopAssembly>();

        builder
            .UseMauiApp<App>()
            .UseFullScreen()
            .RegisterRefitClients();

        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .SetupMauiLocalizer(builder.Configuration)
            .AddRefitEndpoints<IDesktopAssembly>()
            .AddDelegatingHandlers<IDesktopAssembly>()
            .AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .AddScoped<HtmlRenderer>()
            .AddScoped<IPrintingService, PrintingService>();

        builder.Services.AddTailwindMerge();

        #if DEBUG

        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();

        #endif

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IDesktopAssembly).Assembly);
        });

        IConfigurationSection systemSection = builder.Configuration.GetSection("System");
        bool isScalesMock = systemSection.GetValueOrDefault("MockScales", false);
        bool isPrinterMock = systemSection.GetValueOrDefault("MockPrinter", false);

        builder.Services
            .AddServiceOrMock<IScalesService, ScalesService, MockScalesService>(isScalesMock, ServiceLifetime.Singleton)
            .AddServiceOrMock<IPrinterService, PrinterService, MockPrinterService>(isPrinterMock, ServiceLifetime.Singleton);

        return builder;
    }
}