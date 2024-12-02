using Android.Content;
using Android.Print;
using Pl.Tablet.Client;
using Pl.Tablet.Client.Source.Shared;
using WebView = Android.Webkit.WebView;

[assembly: Dependency(typeof(PrintService))]
namespace Pl.Tablet.Client;

public class PrintService : IPrintService
{
    public void Print(string htmlContent)
    {
        if (Platform.CurrentActivity == null) return;
        PrintManager printManager = (PrintManager)Platform.CurrentActivity.GetSystemService(Context.PrintService)!;
        WebView webView = new(Platform.CurrentActivity);

        webView.SetWebViewClient(new());
        webView.LoadDataWithBaseURL(null, htmlContent, "text/html", "utf-8", null);

        PrintDocumentAdapter printDocumentAdapter = webView.CreatePrintDocumentAdapter("MyPrintJob");
        printManager.Print("MyPrintJob", printDocumentAdapter, new PrintAttributes.Builder().Build());
    }
}
