using MauiPageFullScreen;
using Microsoft.AspNetCore.Components.WebView;
using Microsoft.Extensions.Configuration;
using Pl.Shared.Web.Extensions;

namespace Pl.Desktop.Client;

public partial class MainPage : ContentPage
{
    private readonly bool _fullScreen;

    public MainPage(IConfiguration configuration)
    {
        InitializeComponent();
        _fullScreen = configuration.GetSection("System").GetValueOrDefault("FullScreenMode", true);
    }

    private void Bwv_BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
        if (_fullScreen) Controls.FullScreen();
    }
}