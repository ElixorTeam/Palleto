using Microsoft.AspNetCore.Components.WebView;

namespace Pl.Desktop.Client;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Bwv_BlazorWebViewInitialized(object sender, BlazorWebViewInitializedEventArgs e)
    {
        e.WebView.CoreWebView2.Settings.IsPinchZoomEnabled = false;
        e.WebView.CoreWebView2.Settings.IsZoomControlEnabled = false;
    }
}