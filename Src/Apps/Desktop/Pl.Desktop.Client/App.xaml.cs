namespace Pl.Desktop.Client;

public partial class App : Application
{
    private static readonly Mutex Mutex = new(true, Assembly.GetEntryAssembly()?.GetName().Name);

    public App()
    {
        if (!Mutex.WaitOne(TimeSpan.Zero, true))
        {
            Current?.Quit();
            Environment.Exit(0);
        }
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState) =>
        new(new MainPage()) { Title = "Palleto Desktop" };
}