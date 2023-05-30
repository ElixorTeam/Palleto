// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.ServiceModel.Channels;
using System.Windows.Forms;

namespace WsLabelCore.Utils;
/// <summary>
/// Утилиты навигации по контролам.
/// </summary>
#nullable enable
public static class WsFormNavigationUtils
{
    #region Public and private fields, properties, constructor

    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private static WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public static WsFormWaitUserControl WaitUserControl { get; } = new();
    public static WsFormDialogUserControl DialogUserControl { get; } = new();
    public static WsFormLinesUserControl LinesUserControl { get; } = new();
    public static WsFormMoreUserControl KneadingUserControl { get; } = new();
    public static WsFormMoreUserControl MoreUserControl { get; } = new();
    public static WsFormNavigationUserControl NavigationUserControl { get; } = new();
    public static WsFormPlusLinesUserControl PlusLineUserControl { get; } = new();
    public static WsFormPlusNestingUserControl PlusNestingUserControl { get; } = new();

    #endregion

    #region Public and private methods

    /// <summary>
    /// Возврат назад из навигации.
    /// </summary>
    public static void ActionBackFromNavigation()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control control2 in tableLayoutPanel.Controls)
                {
                    if (control2 is WsFormBaseUserControl formUserControl)
                    {
                        formUserControl.Visible = false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Навигация в контрол замеса.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToKneadingUserControl(Action<WsFormBaseUserControl, string> showNavigation)
    {
        KneadingUserControl.Page.ViewModel.UpdateCommandsFromActions();
        KneadingUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(KneadingUserControl, LocaleCore.Scales.SwitchKneadingTitle);
        NavigationUserControl.SwitchUserControl(KneadingUserControl);
    }

    /// <summary>
    /// Навигация в контрол линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToLinesUserControl(Action<WsFormBaseUserControl, string> showNavigation)
    {
        LinesUserControl.Page.ViewModel.UpdateCommandsFromActions();
        LinesUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(LinesUserControl, LocaleCore.Scales.SwitchLineTitle);
        NavigationUserControl.SwitchUserControl(LinesUserControl);
    }

    /// <summary>
    /// Навигация в контрол диалога.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    public static void NavigateToMessageUserControlCancelYes(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        showNavigation(DialogUserControl, LocaleCore.Scales.OperationControl);
        DialogUserControl.Page.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в контрол диалога.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    public static void NavigateToMessageUserControlOk(Action<WsFormBaseUserControl, string> showNavigation, 
        string message, bool isLog, WsEnumLogType logType)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        showNavigation(DialogUserControl, LocaleCore.Scales.OperationControl);
        DialogUserControl.Page.ViewModel.SetupButtonsOk(message, ActionBackFromNavigation, NavigationUserControl.Width);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в контрол ожидания.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    public static void NavigateToWaitUserControl(Action<WsFormBaseUserControl, string> showNavigation, string message)
    {
        WaitUserControl.Page.ViewModel.Message = message;
        showNavigation(WaitUserControl, LocaleCore.Scales.AppWait);
        WaitUserControl.Page.ViewModel.SetupButtonsCustom(message, ActionBackFromNavigation, NavigationUserControl.Width);
        NavigationUserControl.SwitchUserControl(WaitUserControl);
    }

    /// <summary>
    /// Навигация в контрол ешё.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToMoreUserControl(Action<WsFormBaseUserControl, string> showNavigation)
    {
        MoreUserControl.Page.ViewModel.UpdateCommandsFromActions();
        MoreUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(MoreUserControl, LocaleCore.Scales.SwitchMoreTitle);
        NavigationUserControl.SwitchUserControl(MoreUserControl);
    }

    /// <summary>
    /// Навигация в контрол смены ПЛУ линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToPlusLineUserControl(Action<WsFormBaseUserControl, string> showNavigation)
    {
        PlusLineUserControl.Page.ViewModel.UpdateCommandsFromActions();
        PlusLineUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(PlusLineUserControl, LocaleCore.Scales.SwitchPluTitle);
        NavigationUserControl.SwitchUserControl(PlusLineUserControl);
    }

    /// <summary>
    /// Навигация в контрол смены вложенности ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToPlusNestingUserControl(Action<WsFormBaseUserControl, string> showNavigation)
    {
        PlusNestingUserControl.Page.ViewModel.UpdateCommandsFromActions();
        PlusNestingUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(PlusNestingUserControl, LocaleCore.Scales.SwitchPluNestingTitle);
        NavigationUserControl.SwitchUserControl(PlusNestingUserControl);
    }

    private static void ShowNewOperationControlLogType(string message, WsEnumLogType logType,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
    {
        switch (logType)
        {
            case WsEnumLogType.Error:
                ContextManager.ContextItem.SaveLogErrorWithInfo(message, filePath, lineNumber, memberName);
                break;
            case WsEnumLogType.Information:
            case WsEnumLogType.None:
                ContextManager.ContextItem.SaveLogInformation(message);
                break;
            case WsEnumLogType.Question:
                ContextManager.ContextItem.SaveLogQuestion(message);
                break;
            case WsEnumLogType.Warning:
                ContextManager.ContextItem.SaveLogWarning(message);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(logType), logType.ToString());
        
        }
    }

    public static WsSqlDeviceModel SetNewDeviceWithQuestion(Action<WsFormBaseUserControl, string> showNavigation,
        WsSqlDeviceModel device, string ip, string mac)
    {
        if (device.IsNew)
        {
            // Навигация в контрол сообщений.
            NavigateToMessageUserControlCancelYes(showNavigation,
                LocaleCore.Scales.HostNotFound(device.Name) + Environment.NewLine + LocaleCore.Scales.QuestionWriteToDb,
                false, WsEnumLogType.Information, () => { }, ActionYes);
            void ActionYes()
            {
                device = new()
                {
                    Name = device.Name,
                    PrettyName = device.Name,
                    Ipv4 = ip,
                    MacAddress = new(mac),
                    CreateDt = DateTime.Now,
                    ChangeDt = DateTime.Now,
                    LoginDt = DateTime.Now,
                    IsMarked = false,
                };
                AccessManager.AccessItem.Save(device);
            }
        }
        else
        {
            device.Ipv4 = ip;
            device.MacAddress = new(mac);
            device.ChangeDt = DateTime.Now;
            device.LoginDt = DateTime.Now;
            device.IsMarked = false;
            AccessManager.AccessItem.Update(device);
        }
        return device;
    }

    private static void CatchExceptionCore(Action<WsFormBaseUserControl, string> showNavigation, Exception ex, 
        string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

        string message = ex.InnerException is null
            ? ex.Message
            : ex.Message + Environment.NewLine + ex.InnerException.Message;
        // Навигация в контрол сообщений.
        NavigateToMessageUserControlOk(showNavigation, 
            $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
            $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message, true, WsEnumLogType.Error);
    }

    private static void CatchExceptionSimpleCore(Exception ex, string filePath, int lineNumber, string memberName)
    {
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);
    }

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchException(Action<WsFormBaseUserControl, string> showNavigation, Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionCore(showNavigation, ex, filePath, lineNumber, memberName);

    /// <summary>
    /// Show catch exception window.
    /// </summary>
    /// <param name="ex"></param>
    /// <param name="filePath"></param>
    /// <param name="lineNumber"></param>
    /// <param name="memberName"></param>
    /// <returns></returns>
    public static void CatchExceptionSimple(Exception ex,
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        CatchExceptionSimpleCore(ex, filePath, lineNumber, memberName);

    private static void MakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        if (win32Window is not Form form) return;
        using MemoryStream memoryStream = new();
        using Bitmap bitmap = new(form.Width, form.Height);
        using Graphics graphics = Graphics.FromImage(bitmap);
        graphics.CopyFromScreen(form.Location.X, form.Location.Y, 0, 0, form.Size);
        using Image img = bitmap;
        img.Save(memoryStream, ImageFormat.Png);
        WsSqlScaleScreenShotModel scaleScreenShot = new() { Scale = scale, ScreenShot = memoryStream.ToArray() };
        AccessManager.AccessItem.Save(scaleScreenShot);
    }

    public static void ActionTryCatch(Action action, Action<WsFormBaseUserControl, string> showNavigation)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            CatchException(showNavigation, ex);
        }
    }

    public static void ActionTryCatchSimple(Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            CatchExceptionSimple(ex);
        }
    }

    public static void ActionTryCatch(IWin32Window win32Window, Action<WsFormBaseUserControl, string> showNavigation, 
        Action action)
    {
        try
        {
            action();
        }
        catch (Exception ex)
        {
            ActionMakeScreenShot(win32Window, LabelSession.Line);
            CatchException(showNavigation, ex);
        }
    }

    public static void ActionMakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        try
        {
            MakeScreenShot(win32Window, scale);
            PluginMemory.MemorySize.Execute();
            ContextManager.ContextItem.SaveLogMemory(PluginMemory.GetMemorySizeAppMb(), PluginMemory.GetMemorySizeFreeMb());
            GC.Collect();
        }
        catch (Exception ex)
        {
            CatchExceptionSimple(ex);
        }
    }
    
    #endregion
}