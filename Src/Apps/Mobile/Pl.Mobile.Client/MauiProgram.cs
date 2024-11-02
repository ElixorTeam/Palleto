using CommunityToolkit.Maui;
using Fluxor;
using Microsoft.FluentUI.AspNetCore.Components;
using Pl.Shared.Web.Extensions;
using Pl.Mobile.Client.Source.Shared.Api;

namespace Pl.Mobile.Client;

public static partial class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Configuration.LoadAppSettings<IScalesMobileAssembly>();

        builder.RegisterRefitClients();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .SetupMauiLocalizer(builder.Configuration)
            .AddRefitEndpoints<IScalesMobileAssembly>()
            .AddDelegatingHandlers<IScalesMobileAssembly>();

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IScalesMobileAssembly).Assembly);
        });

        ConfigureDebugServices(builder);

        return builder.Build();
    }

    static partial void ConfigureDebugServices(MauiAppBuilder builder);
}