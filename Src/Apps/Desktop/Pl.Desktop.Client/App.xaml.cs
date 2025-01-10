using Microsoft.Extensions.Configuration;
using Pl.Desktop.Client.Source.Shared.Services.System;
using Velopack;
using Pl.Shared.Web.Extensions;

namespace Pl.Desktop.Client;

public partial class App : Application
{
    private readonly bool _fullScreen;

    private static readonly Mutex Mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);

    public App(AppService app, IConfiguration configuration)
    {
        VelopackApp.Build().Run();

        _fullScreen = configuration.GetSection("System").GetValueOrDefault("FullScreenMode", true);
        InitializeComponent();

        Task.Run(async () => await app.Update()).ConfigureAwait(false);

        if (!Mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }
    }

    protected override Window CreateWindow(IActivationState? activationState) =>
        new(new MainPage(_fullScreen)) { Title = "Palleto" };
}
