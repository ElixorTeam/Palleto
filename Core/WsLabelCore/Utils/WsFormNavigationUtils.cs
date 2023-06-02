// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Forms;

namespace WsLabelCore.Utils;

/// <summary>
/// WinForms навигация по контролам.
/// </summary>
#nullable enable
public static class WsFormNavigationUtils
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Пользовательская сессия.
    /// </summary>
    private static WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    /// <summary>
    /// Плагин замеров памяти.
    /// </summary>
    private static WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    /// <summary>
    /// SQL-менеджер прямого доступа к данным БД (используется ядром фреймворка).
    /// </summary>
    private static WsSqlAccessManagerHelper AccessManager => WsSqlAccessManagerHelper.Instance;
    /// <summary>
    /// SQL-менеджер доступа к данным БД (используется клиентами).
    /// </summary>
    private static WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    /// <summary>
    /// WinForms-контрол ожидания.
    /// </summary>
    public static WsXamlWaitUserControl WaitUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол диалога.
    /// </summary>
    public static WsXamlDialogUserControl DialogUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол ввода цифр.
    /// </summary>
    public static WsXamlDigitsUserControl DigitsUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены линии.
    /// </summary>
    public static WsXamlLinesUserControl LinesUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол замеса.
    /// </summary>
    public static WsXamlKneadingUserControl KneadingUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол навигации.
    /// </summary>
    public static WsFormNavigationUserControl NavigationUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены плу линии.
    /// </summary>
    public static WsXamlPlusLinesUserControl PlusLineUserControl { get; } = new();
    /// <summary>
    /// WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    public static WsXamlPlusNestingUserControl PlusNestingUserControl { get; } = new();
    /// <summary>
    /// Тег временных диалогов.
    /// </summary>
    private const string DialogTempTag = "DIALOG_TEMP";

    #endregion

    #region Public and private methods

    /// <summary>
    /// Возврат из навигации.
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
                        MdInvokeControl.SetVisible(formUserControl, false);
                    }
                }
            }
        }
        MdInvokeControl.SetVisible(NavigationUserControl, false);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsLines(Action<WsFormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сесси пользователя.
        ((WsXamlLinesViewModel)LinesUserControl.Page.ViewModel).Areas = LabelSession.ContextCache.Areas;
        ((WsXamlLinesViewModel)LinesUserControl.Page.ViewModel).Lines = LabelSession.ContextCache.Lines;
        ((WsXamlLinesViewModel)LinesUserControl.Page.ViewModel).Area = LabelSession.Area;
        ((WsXamlLinesViewModel)LinesUserControl.Page.ViewModel).Line = LabelSession.Line;

        LinesUserControl.Page.ViewModel.UpdateCommandsFromActions();
        LinesUserControl.Page.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
        showNavigation(LinesUserControl, LocaleCore.Scales.SwitchLineTitle);
        NavigationUserControl.SwitchUserControl(LinesUserControl);
    }

    /// <summary>
    /// Навигация в существующий WinForms-контрол диалога Отмена/Да.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    public static void NavigateToExistsDialogCancelYes(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DialogUserControl.Page.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DialogUserControl, LocaleCore.Scales.OperationControl);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в новый WinForms-контрол диалога.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="dialogType"></param>
    /// <param name="actions"></param>
    public static void NavigateToNewDialog(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, WsEnumDialogType dialogType, List<Action> actions)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        WsXamlDialogUserControl dialog = new();
        dialog.SetupUserConrol();
        dialog.Tag = DialogTempTag;
        dialog.SetupActions(dialogType, actions);
        dialog.SetupButtons(dialogType, actions, message, NavigationUserControl.Width);
        showNavigation(dialog, LocaleCore.Scales.OperationControl);
        NavigationUserControl.SwitchUserControl(dialog);
    }

    /// <summary>
    /// Очистить новые диалоги.
    /// </summary>
    public static void ClearNewDialogs()
    {
        foreach (Control control in NavigationUserControl.Controls)
        {
            if (control is TableLayoutPanel tableLayoutPanel)
            {
                foreach (Control control2 in tableLayoutPanel.Controls)
                {
                    if (control2 is WsXamlDialogUserControl dialogUserControl)
                    {
                        if (dialogUserControl.Tag is not null && dialogUserControl.Tag.Equals(DialogTempTag))
                            dialogUserControl.Dispose();
                    }
                }
            }
        }
        GC.Collect();
    }

    /// <summary>
    /// Навигация в WinForms-контрол диалога Ок.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    public static void NavigateToExistsDialogOk(Action<WsFormBaseUserControl, string> showNavigation, 
        string message, bool isLog, WsEnumLogType logType)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DialogUserControl.Page.ViewModel.SetupButtonsOk(message, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DialogUserControl, LocaleCore.Scales.OperationControl);
        NavigationUserControl.SwitchUserControl(DialogUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол ввода цифр.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="message"></param>
    /// <param name="isLog"></param>
    /// <param name="logType"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    public static void NavigateToExistsDigitsUserControl(Action<WsFormBaseUserControl, string> showNavigation,
        string message, bool isLog, WsEnumLogType logType, Action actionCancel, Action actionYes)
    {
        if (isLog) ShowNewOperationControlLogType(message, logType);
        DigitsUserControl.Page.ViewModel.SetupButtonsCancelYes(message, actionCancel, actionYes, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(DigitsUserControl, LocaleCore.Scales.OperationControl);
        NavigationUserControl.SwitchUserControl(DigitsUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол ожидания.
    /// </summary>
    /// <param name="showNavigation"></param>
    /// <param name="title"></param>
    /// <param name="message"></param>
    public static void NavigateToExistsWait(Action<WsFormBaseUserControl, string> showNavigation, 
        string title, string message)
    {
        WaitUserControl.Page.ViewModel.Message = message;
        WaitUserControl.Page.ViewModel.SetupButtonsCustom(message, ActionBackFromNavigation, NavigationUserControl.Width);
        showNavigation(WaitUserControl, title);
        NavigationUserControl.SwitchUserControl(WaitUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол замеса.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsKneading(Action<WsFormBaseUserControl, string> showNavigation)
    {
        KneadingUserControl.Page.ViewModel.UpdateCommandsFromActions();
        KneadingUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(KneadingUserControl,
            LabelSession.PluLine.Plu.IsCheckWeight
            ? $"{LocaleCore.Scales.SwitchKneadingTitle} {LocaleCore.Scales.PluWeight} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}"
            : $"{LocaleCore.Scales.SwitchKneadingTitle} {LocaleCore.Scales.PluCount} | {LabelSession.PluLine.Plu.Number} | {LabelSession.PluLine.Plu.Name}");
        NavigationUserControl.SwitchUserControl(KneadingUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены ПЛУ линии.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsPlusLine(Action<WsFormBaseUserControl, string> showNavigation)
    {
        PlusLineUserControl.Page.ViewModel.UpdateCommandsFromActions();
        PlusLineUserControl.Page.ViewModel.SetupButtonsWidth(NavigationUserControl.Width);
        showNavigation(PlusLineUserControl, LocaleCore.Scales.SwitchPluLineTitle);
        NavigationUserControl.SwitchUserControl(PlusLineUserControl);
    }

    /// <summary>
    /// Навигация в WinForms-контрол смены вложенности ПЛУ.
    /// </summary>
    /// <param name="showNavigation"></param>
    public static void NavigateToExistsPlusNesting(Action<WsFormBaseUserControl, string> showNavigation)
    {
        // Загрузка из сесси пользователя.
        ((WsXamlPlusNestingViewModel)PlusNestingUserControl.Page.ViewModel).PlusNestings = 
            LabelSession.ContextManager.ContextView.GetListViewPlusNesting((ushort)LabelSession.PluLine.Plu.Number);
        ((WsXamlPlusNestingViewModel)PlusNestingUserControl.Page.ViewModel).PluNesting = LabelSession.ViewPluNesting;

        PlusNestingUserControl.Page.ViewModel.UpdateCommandsFromActions();
        PlusNestingUserControl.Page.ViewModel.SetupButtonsCancelYes(NavigationUserControl.Width);
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
            // Навигация в WinForms-контрол диалога Отмена/Да.
            NavigateToExistsDialogCancelYes(showNavigation,
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
        // Навигация в WinForms-контрол диалога Ок.
        NavigateToExistsDialogOk(showNavigation, 
            $"{LocaleCore.Scales.Method}: {memberName}." + Environment.NewLine +
            $"{LocaleCore.Scales.Line}: {lineNumber}." + Environment.NewLine + message, true, WsEnumLogType.Error);
    }

    private static void CatchExceptionSimpleCore(Exception ex, string filePath, int lineNumber, string memberName) => 
        ContextManager.ContextItem.SaveLogErrorWithInfo(ex, filePath, lineNumber, memberName);

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

    public static void ActionTryCatch(Action action)
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

    private static void ActionMakeScreenShot(IWin32Window win32Window, WsSqlScaleModel scale)
    {
        if (WsDebugHelper.Instance.IsDevelop) return;
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