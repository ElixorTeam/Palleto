using Android.App;
using Android.Runtime;

namespace Pl.Mobile.Client;
[Application]
public class MainApplication : MauiApplication
{
    public MainApplication(IntPtr handle, JniHandleOwnership ownership)
        : base(handle, ownership)
    {
    }

    protected override MauiApp CreateMauiApp() => Pl.Mobile.Client.MauiProgram.CreateMauiApp();
}