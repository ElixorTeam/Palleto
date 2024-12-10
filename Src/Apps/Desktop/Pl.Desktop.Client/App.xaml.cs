using Microsoft.Extensions.Configuration;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace Pl.Desktop.Client;

public partial class App : Application
{
    private static readonly Mutex Mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);

    public App(IConfiguration configuration)
    {
        Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);

        if (!Mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }

        InitializeComponent();
        MainPage = new MainPage(configuration);
    }
}