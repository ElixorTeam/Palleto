using Microsoft.Extensions.Configuration;
using Pl.Shared.Web.Extensions;

namespace Pl.Desktop.Client;

public partial class App : Application
{
    private readonly bool _fullScreen;

    private static readonly Mutex Mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);

    public App(IConfiguration configuration)
    {
        if (!Mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }

        _fullScreen = configuration.GetSection("System").GetValueOrDefault("FullScreenMode", true);
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState) =>
        new(new MainPage(_fullScreen)) { Title = "Palleto Desktop" };
}
