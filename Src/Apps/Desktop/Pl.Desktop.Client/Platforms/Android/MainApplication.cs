using Android.App;
using Android.Runtime;

namespace Pl.Desktop.Client;

[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => Pl.Desktop.Client.MauiProgram.CreateMauiApp();
}