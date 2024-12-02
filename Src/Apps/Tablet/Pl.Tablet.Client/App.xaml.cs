using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace Pl.Tablet.Client;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new MainPage();
        Current?.On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
    }
}