using CommunityToolkit.Maui;
using Fluxor;
using Microsoft.FluentUI.AspNetCore.Components;
using Pl.Shared.Web.Extensions;
using Pl.Mobile.Client.Source.Shared.Api;
using TailwindMerge.Extensions;

namespace Pl.Mobile.Client;

public static partial class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();

        builder.UseMauiApp<App>().UseMauiCommunityToolkit();
        builder.Configuration.LoadAppSettings<IMobileAssembly>();

        builder.RegisterRefitClients();

        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddFluentUIComponents(c => c.ValidateClassNames = false);

        builder.Services
            .SetupMauiLocalizer(builder.Configuration)
            .AddRefitEndpoints<IMobileAssembly>()
            .AddDelegatingHandlers<IMobileAssembly>();

        builder.Services.AddFluxor(options =>
        {
            options.WithLifetime(StoreLifetime.Singleton);
            options.ScanAssemblies(typeof(IMobileAssembly).Assembly);
        });

        builder.Services.AddTailwindMerge();

        ConfigureDebugServices(builder);

        return builder.Build();
    }

    static partial void ConfigureDebugServices(MauiAppBuilder builder);
}