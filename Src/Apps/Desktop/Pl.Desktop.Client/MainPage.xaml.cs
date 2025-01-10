using MauiPageFullScreen;
using Microsoft.AspNetCore.Components.WebView;

namespace Pl.Desktop.Client;

public partial class MainPage : ContentPage
{
    private readonly bool _fullScreen;
    public MainPage(bool fullScreen)
    {
        InitializeComponent();
        _fullScreen = fullScreen;
    }

    private void Bwv_BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
        if (_fullScreen) Controls.FullScreen();
    }
}
