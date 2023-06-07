// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLocalizationCore.Utils;

namespace ScalesUI;

internal static class Program
{
    private static WsAppVersionHelper AppVersion => WsAppVersionHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;

    [STAThread]
    internal static void Main()
    {
        // В первую очередь.
        System.Windows.Forms.Application.EnableVisualStyles();
        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
        // Настройка.
        AppVersion.Setup(Assembly.GetExecutingAssembly(), WsLocaleCore.LabelPrint.AppTitle);
        ContextManager.SetupJsonScales(Directory.GetCurrentDirectory(), typeof(Program).Assembly.GetName().Name);
        // Запуск.
        ContextManager.ContextItem.SaveLogInformation(
            WsLocaleCore.LabelPrint.RegistrationSuccess(LabelSession.DeviceName, LabelSession.DeviceScaleFk.Scale.Description));
        // Режив работы.
        WsDebugHelper.Instance.IsSkipDialogs = false;
        System.Windows.Forms.Application.Run(new WsMainForm());
    }
}