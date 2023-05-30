// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsLabelCore.Controls;

namespace ScalesUI.Forms;

public partial class WsMainForm : Form
{
    #region Public and private fields, properties, constructor

    private ActionSettingsModel ActionSettings { get; set; }
    private AppVersionHelper AppVersion => AppVersionHelper.Instance;
    private DebugHelper Debug => DebugHelper.Instance;
    private WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private ProcHelper Proc => ProcHelper.Instance;
    private WsSchedulerHelper WsScheduler => WsSchedulerHelper.Instance;
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private Button ButtonLine { get; set; }
    private Button ButtonPluNestingFk { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonMore { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }

    /// <summary>
    /// Отладочный флаг для сквозных тестов печати, без диалогов.
    /// </summary>
    private const bool IsSkipDialogs = false;
    /// <summary>
    /// Магический флаг закрытия после нажатия кнопки OK.
    /// </summary>
    private bool IsMagicClose { get; set; }

    public WsMainForm()
    {
        InitializeComponent();
    }

    #endregion

    #region Public and private methods - MainForm

    /// <summary>
    /// Загрузка фоном.
    /// </summary>
    private void MainFormLoadAtBackground()
    {
        // Хуки мышки.
        KeyboardMouseEvents = Hook.AppEvents();
        KeyboardMouseEvents.MouseDownExt += MouseDownExt;
        // Настройка кнопок.
        SetupButtons();
        // Загрузка шрифтов.
        LoadFonts();
        // Прочее.
        LabelSession.NewPallet();
        MdInvokeControl.SetText(this, AppVersion.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
        LoadMainControls();
        LoadLocalizationStatic(Lang.Russian);
        // Планировщик.
        WsScheduler.Load(this);
        // Загрузка остальных контролов.
        LoadNavigationUserControl();
    }

    /// <summary>
    /// Загрузка контрола ожидания.
    /// </summary>
    private void LoadNavigationWaitUserControl()
    {
        // Навигация.
        WsFormNavigationUtils.NavigationUserControl.Dock = DockStyle.Fill;
        WsFormNavigationUtils.NavigationUserControl.Visible = false;
        // Контрол ожидания.
        WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.AddAction(ActionFinally);
        // Настройка главной формы.
        this.SwitchResolution(Debug.IsDevelop ? WsEnumScreenResolution.Value1366x768 : WsEnumScreenResolution.FullScreen);
        CenterToScreen();
        // Добавить контрол.
        Controls.Add(WsFormNavigationUtils.NavigationUserControl);
        // Настройки главной формы.
        FormBorderStyle = Debug.IsDevelop ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
        TopMost = !Debug.IsDevelop;
    }

    /// <summary>
    /// Загрузка остальных контролов.
    /// </summary>
    private void LoadNavigationUserControl()
    {
        // Контрол диалога.
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.DialogUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        // Контрол замеса.
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnFromKneading);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(ReturnFromKneading);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.KneadingUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.KneadingUserControl.RefreshUserConrol();
        // Контрол ещё.
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnFromMore);
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdYes.AddAction(ReturnFromMore);
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.MoreUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.MoreUserControl.RefreshUserConrol();
        // Контрол смены линии.
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromLines);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromLines);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.LinesUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.LinesUserControl.RefreshUserConrol();
        // Контрол смены ПЛУ.
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromPlusLine);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusLineUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusLineUserControl.RefreshUserConrol();
        // Контрол смены вложенности ПЛУ.
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(ReturnCancelFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdCancel.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(ReturnOkFromPlusNesting);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(WsFormNavigationUtils.ActionBackFromNavigation);
        WsFormNavigationUtils.PlusNestingUserControl.Page.ViewModel.CmdYes.AddAction(ActionFinally);
        WsFormNavigationUtils.PlusNestingUserControl.RefreshUserConrol();
    }

    private MdPrinterModel GetMdPrinter(WsSqlPrinterModel scalePrinter) => new()
    {
        Name = scalePrinter.Name,
        Ip = scalePrinter.Ip,
        Port = scalePrinter.Port,
        Password = scalePrinter.Password,
        PeelOffSet = scalePrinter.PeelOffSet,
        DarknessLevel = scalePrinter.DarknessLevel,
        HttpStatusCode = scalePrinter.HttpStatusCode,
        PingStatus = scalePrinter.PingStatus,
        HttpStatusException = scalePrinter.HttpStatusException,
    };

    private void MainForm_Load(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowNavigation, () =>
        {
            UserSession.StopwatchMain = Stopwatch.StartNew();
            UserSession.StopwatchMain.Restart();
            // Загрузка контрола ожидания.
            LoadNavigationWaitUserControl();
            // Проверка линии.
            LabelSession.SetSessionForLabelPrint(ShowNavigation);
            if (LabelSession.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningLineNotFound(LabelSession.DeviceName);
                WsFormNavigationUtils.DialogUserControl.Page.ViewModel.SetupButtonsOk(
                    message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin,
                    ActionBackFromLineNotFound, WsFormNavigationUtils.NavigationUserControl.Width);
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowNavigation, message, true, WsEnumLogType.Error);
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                return;
            }
            // Проверка повторного запуска.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool isCreatedNew);
            if (!isCreatedNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WsFormNavigationUtils.DialogUserControl.Page.ViewModel.SetupButtonsOk(message, ActionBackFromDuplicateRun, 
                    WsFormNavigationUtils.NavigationUserControl.Width);
                WsFormNavigationUtils.NavigateToMessageUserControlOk(ShowNavigation, message, true, WsEnumLogType.Error);
                ContextManager.ContextItem.SaveLogWarning(message);
                return;
            }
            // Навигация в кастом контрол.
            WsFormNavigationUtils.NavigateToWaitUserControl(ShowNavigation, LocaleCore.Scales.AppWaitLoad);
            // Загрузка фоном.
            MainFormLoadAtBackground();
            // Авто-возврат из контрола на главную форму.
            WsFormNavigationUtils.WaitUserControl.Page.ViewModel.CmdCustom.Relay();
            // Логи.
            UserSession.StopwatchMain.Stop();
            ContextManager.ContextItem.SaveLogMemory(
                UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
            ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleData.Program.IsLoaded}. " + Environment.NewLine +
                $"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}." + Environment.NewLine +
                $"{nameof(LocaleData.Program.TimeSpent)}: {UserSession.StopwatchMain.Elapsed}.");
            // Скриншот релиза.
            if (Debug.IsRelease)
                WsFormNavigationUtils.ActionMakeScreenShot(this, LabelSession.Line);
        });
    }

    private static void ActionBackFromLineNotFound() => System.Windows.Forms.Application.Exit();

    private static void ActionBackFromDuplicateRun() => System.Windows.Forms.Application.Exit();

    private void LoadMainControls()
    {
        // Память.
        UserSession.PluginMemory.Init(new(1_000, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldMemory, fieldMemoryExt);
        UserSession.PluginMemory.Execute();
        MdInvokeControl.SetVisible(fieldMemoryExt, Debug.IsDevelop);

        // Весовая платформа Масса-К.
        UserSession.PluginMassa.Init(new(1_000, 1_000), new(0_100, 0_100),
            new(0_050, 0_100), fieldNettoWeight, fieldMassa, fieldMassaExt, ResetWarning);
        UserSession.PluginMassa.Execute();
        MdInvokeControl.SetVisible(fieldMassaExt, Debug.IsDevelop);

        // Основной принтер.
        if (LabelSession.Line.PrinterMain is not null)
        {
            LabelSession.PluginPrintMain.Init(new(0_500, 0_250), new(0_250, 0_250),
                new(0_250, 0_250),
                LabelSession.PrintBrandMain, GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
            MdInvokeControl.SetVisible(fieldPrintMain, true);
            MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
            LabelSession.PluginPrintMain.Execute();
            LabelSession.PluginPrintMain.SetOdometorUserLabel(1);
        }

        // Транспортный принтер.
        if (LabelSession.Line.IsShipping)
        {
            if (LabelSession.Line.PrinterShipping is not null)
            {
                LabelSession.PluginPrintShipping.Init(new(0_500, 0_250),
                    new(0_250, 0_250), new(0_250, 0_250),
                    LabelSession.PrintBrandShipping, GetMdPrinter(LabelSession.Line.PrinterShipping), fieldPrintShipping, fieldPrintShippingExt, false);
                MdInvokeControl.SetVisible(fieldPrintShipping, true);
                MdInvokeControl.SetVisible(fieldPrintShippingExt, Debug.IsDevelop);
                LabelSession.PluginPrintShipping.Execute();
                LabelSession.PluginPrintShipping.SetOdometorUserLabel(1);
            }
        }

        // Метки.
        UserSession.PluginLabels.Init(
            new(0_250, 0_250), new(0_250, 0_250),
            new(0_250, 0_250), fieldPlu, fieldProductDate, fieldKneading);
        UserSession.PluginLabels.Execute();
        MdInvokeControl.SetText(fieldTitle, $"{AppVersionHelper.Instance.AppTitle}. {LabelSession.PublishDescription}.");
        MdInvokeControl.SetBackColor(fieldTitle, Color.Transparent);
    }

    /// <summary>
    /// Закрытие программы.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (IsMagicClose) return;
        WsFormNavigationUtils.ActionTryCatchSimple(() =>
        {
            // Сброс предупреждения.
            ResetWarning();
            // Навигация в контрол сообщений.
            WsFormNavigationUtils.NavigateToMessageUserControlCancelYes(ShowNavigation, 
                $"{LocaleCore.Scales.QuestionCloseApp}?", true, WsEnumLogType.Question, 
                ActionCloseCancel, ActionCloseYes);
            e.Cancel = true;
        });
    }

    /// <summary>
    /// Возврат Да из контрола закрытия.
    /// </summary>
    private void ActionCloseYes()
    {
        ActionFinally();
        UserSession.StopwatchMain.Restart();
        // Скриншот.
        if (Debug.IsRelease)
            WsFormNavigationUtils.ActionMakeScreenShot(this, LabelSession.Line);
        // Навигация в кастом контрол.
        WsFormNavigationUtils.NavigateToWaitUserControl(ShowNavigation, LocaleCore.Scales.AppWaitExit);
        // Планировщик.
        WsScheduler.Close();
        // Плагины.
        UserSession.PluginsClose();
        // Шрифты.
        FontsSettings.Close();
        // Хуки мышки.
        KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
        KeyboardMouseEvents.Dispose();
        // Логи.
        UserSession.StopwatchMain.Stop();
        ContextManager.ContextItem.SaveLogMemory(UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
        ContextManager.ContextItem.SaveLogInformation(
            LocaleData.Program.IsClosed + Environment.NewLine + $"{LocaleData.Program.TimeSpent}: {UserSession.StopwatchMain.Elapsed}.");
        // Магический флаг.
        IsMagicClose = true;
        Close();
    }

    /// <summary>
    /// Возврат Отмена из контрола закрытия.
    /// </summary>
    private void ActionCloseCancel()
    {
        IsMagicClose = false;
        ActionFinally();
    }

    /// <summary>
    /// Загрузка шрифтов.
    /// </summary>
    private void LoadFonts()
    {
        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPackageWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

        fieldMemoryExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintMain.Font = FontsSettings.FontLabelsGray;
        fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
        fieldMassaExt.Font = FontsSettings.FontLabelsGray;
        fieldMassa.Font = FontsSettings.FontLabelsGray;
        fieldPrintMainExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintShippingExt.Font = FontsSettings.FontLabelsGray;
        fieldMemory.Font = FontsSettings.FontLabelsGray;

        fieldWarning.Font = FontsSettings.FontLabelsBlack;
        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelPackageWeight.Font = FontsSettings.FontLabelsBlack;
        labelKneading.Font = FontsSettings.FontLabelsBlack;
        fieldKneading.Font = FontsSettings.FontLabelsBlack;
        labelProductDate.Font = FontsSettings.FontLabelsBlack;

        ButtonLine.Font = FontsSettings.FontButtonsSmall;
        ButtonPlu.Font = FontsSettings.FontButtonsSmall;
        ButtonPluNestingFk.Font = FontsSettings.FontButtonsSmall;

        ButtonScalesTerminal.Font = FontsSettings.FontButtons;
        ButtonScalesInit.Font = FontsSettings.FontButtons;
        ButtonNewPallet.Font = FontsSettings.FontButtons;
        ButtonKneading.Font = FontsSettings.FontButtons;
        ButtonMore.Font = FontsSettings.FontButtons;
        ButtonPrint.Font = FontsSettings.FontButtons;
        ButtonPrint.BackColor = ColorTranslator.FromHtml("#ff7f50");
    }

    /// <summary>
    /// Настройка кнопок.
    /// </summary>
    private void SetupButtons()
    {
        ActionSettings = new()
        {
            // Device.
            IsDevice = true,
            IsPlu = true,
            IsNesting = true,
            // Actions.
            IsKneading = false,
            IsMore = true,
            IsNewPallet = false,
            IsOrder = LabelSession.Line.IsOrder,
            IsPrint = true,
            IsScalesInit = false,
            IsScalesTerminal = true,
        };

        CreateButtonsDevices();
        CreateButtonsActions();
    }

    private void CreateButtonsDevices()
    {
        TableLayoutPanel layoutPanelDevice = WsFormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelDevice),
            1, 13, 2, 98);
        int rowCount = 0;

        if (ActionSettings.IsDevice)
        {
            ButtonLine = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonLine), 1, rowCount++);
            ButtonLine.Click += ActionSwitchLine;
        }
        else ButtonLine = new();

        if (ActionSettings.IsPlu)
        {
            ButtonPlu = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPlu), 1, rowCount++);
            ButtonPlu.Click += ActionSwitchPlu;
        }
        else ButtonPlu = new();

        if (ActionSettings.IsNesting)
        {
            ButtonPluNestingFk = WsFormUtils.NewTableLayoutPanelButton(layoutPanelDevice, nameof(ButtonPluNestingFk), 1, rowCount++);
            ButtonPluNestingFk.Click += ActionSwitchPluNesting;
        }
        else ActionSettings = new();

        layoutPanelDevice.ColumnCount = 1;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelDevice);
        layoutPanelDevice.RowCount = rowCount;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelDevice);
    }

    private void CreateButtonsActions()
    {
        TableLayoutPanel layoutPanelActions = WsFormUtils.NewTableLayoutPanel(layoutPanelMain, nameof(layoutPanelActions),
            3, 13, layoutPanelMain.ColumnCount - 3, 99);
        int columnCount = 0;

        if (ActionSettings.IsScalesTerminal)
        {
            ButtonScalesTerminal =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesTerminal), columnCount++, 0);
            ButtonScalesTerminal.Click += ActionScalesTerminal;
        }
        else
        {
            ButtonScalesTerminal = new();
        }

        if (ActionSettings.IsScalesInit)
        {
            ButtonScalesInit =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonScalesInit), columnCount++, 0);
            ButtonScalesInit.Click += ActionScalesInit;
        }
        else
        {
            ButtonScalesInit = new();
        }

        if (ActionSettings.IsNewPallet)
        {
            ButtonNewPallet =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonNewPallet), columnCount++, 0);
            ButtonNewPallet.Click += ActionNewPallet;
        }
        else
        {
            ButtonNewPallet = new();
        }

        if (ActionSettings.IsKneading)
        {
            ButtonKneading =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonKneading), columnCount++, 0);
            ButtonKneading.Click += ActionKneading;
        }
        else
        {
            ButtonKneading = new();
        }

        if (ActionSettings.IsMore)
        {
            ButtonMore = WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonMore), columnCount++, 0);
            ButtonMore.Click += ActionMore;
        }
        else
        {
            ButtonMore = new();
        }

        if (ActionSettings.IsPrint)
        {
            ButtonPrint =
                WsFormUtils.NewTableLayoutPanelButton(layoutPanelActions, nameof(ButtonPrint), columnCount++, 0);
            ButtonPrint.Click += ActionPreparePrint;
            ButtonPrint.Focus();
        }
        else
        {
            ButtonPrint = new();
        }

        layoutPanelActions.ColumnCount = columnCount;
        WsFormUtils.SetTableLayoutPanelColumnStyles(layoutPanelActions);
        layoutPanelActions.RowCount = 1;
        WsFormUtils.SetTableLayoutPanelRowStyles(layoutPanelActions);
    }

    #endregion

    #region Public and private methods - Controls

    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button == MouseButtons.Middle)
            ActionPreparePrint(sender, e);
    }

    private void FieldPrintManager_Click(object sender, EventArgs e)
    {
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16) { Width = 700, Height = 450 };
        //wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
        //wpfPageLoader.MessageBoxViewModel.Caption = LocaleCore.Print.InfoCaption;
        //wpfPageLoader.MessageBoxViewModel.Message = GetPrintInfo(UserSession.PluginPrintMain, true);
        //if (LabelSession.Scale.IsShipping)
        //{
        //    wpfPageLoader.MessageBoxViewModel.Message += Environment.NewLine + Environment.NewLine +
        //        GetPrintInfo(UserSession.PluginPrintShipping, false);
        //    wpfPageLoader.Height = 700;
        //}
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonCustomVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonCustomContent = LocaleCore.Print.ClearQueue;
        //DialogResult result = wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
        //if (result == DialogResult.Retry)
        //{
        //    UserSession.PluginPrintMain.ClearPrintBuffer(1);
        //    if (LabelSession.Scale.IsShipping)
        //        UserSession.PluginPrintShipping.ClearPrintBuffer(1);
        //}
    }

    //private string GetPrintInfo(WsPluginPrintModel pluginPrint, bool isMain)
    //{
    //    string peeler = isMain
    //        ? LabelSession.PluginPrintMain.ZebraPeelerStatus : LabelSession.PluginPrintShipping.ZebraPeelerStatus;
    //    string printMode = isMain
    //        ? LabelSession.PluginPrintMain.GetZebraPrintMode() :
    //        LabelSession.PluginPrintShipping.GetZebraPrintMode();
    //    PrintBrand printBrand = isMain ? LabelSession.PluginPrintMain.PrintBrand : LabelSession.PluginPrintShipping.PrintBrand;
    //    MdWmiWinPrinterModel wmiPrinter = pluginPrint.TscWmiPrinter;
    //    return
    //        $"{LabelSession.WeighingSettings.GetPrintName(isMain, printBrand)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.DeviceCommunication} ({pluginPrint.Printer.Ip}): {pluginPrint.Printer.PingStatus}" + Environment.NewLine +
    //        $"{LocaleCore.Print.PrinterStatus}: {pluginPrint.GetDeviceStatus()}" + Environment.NewLine +
    //        Environment.NewLine +
    //        $"{LocaleCore.Print.Name}: {wmiPrinter.Name}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Driver}: {wmiPrinter.DriverName}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Port}: {wmiPrinter.PortName}" + Environment.NewLine +
    //        $"{LocaleCore.Print.StateCode}: {wmiPrinter.PrinterState}" + Environment.NewLine +
    //        $"{LocaleCore.Print.StatusCode}: {wmiPrinter.PrinterStatus}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Status}: {pluginPrint.GetPrinterStatusDescription(LocaleCore.Lang, wmiPrinter.PrinterStatus)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.State} (ENG): {wmiPrinter.Status}" + Environment.NewLine +
    //        $"{LocaleCore.Print.State}: {MdWmiHelper.Instance.GetStatusDescription(
    //            LocaleCore.Lang == Lang.English ? MDSoft.Wmi.Enums.MdLang.English : MDSoft.Wmi.Enums.MdLang.Russian, wmiPrinter.Status)}" + Environment.NewLine +
    //        $"{LocaleCore.Print.SensorPeeler}: {peeler}" + Environment.NewLine +
    //        $"{LocaleCore.Print.Mode}: {printMode}" + Environment.NewLine;
    //}

    private void FieldSscc_Click(object sender, EventArgs e)
    {
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
        //wpfPageLoader.Text = LocaleCore.Scales.FieldSsccShort;
        //wpfPageLoader.MessageBoxViewModel.Caption = LocaleCore.Scales.FieldSscc;
        //wpfPageLoader.MessageBoxViewModel.Message =
        //    $"{LocaleCore.Scales.FieldSscc}: {UserSession.ProductSeries.Sscc.Sscc}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccGln}: {UserSession.ProductSeries.Sscc.Gln}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccUnitId}: {UserSession.ProductSeries.Sscc.UnitId}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccUnitType}: {UserSession.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.Field•SsccSynonym}: {UserSession.ProductSeries.Sscc.SynonymSscc}" + Environment.NewLine +
        //    $"{LocaleCore.Scales.FieldSsccControlNumber}: {UserSession.ProductSeries.Sscc.Check}";
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.Localization();
        //wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
    }

    private void FieldTasks_Click(object sender, EventArgs e)
    {
        //string message = string.Empty;
        //foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
        //{
        //    message += $"{LocaleCore.Scales.ThreadId}: {thread.Id}. " +
        //        $"{LocaleCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
        //        $"{LocaleCore.Scales.ThreadState}: {thread.ThreadState}. " +
        //        $"{LocaleCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
        //}
        //using WsWpfPageLoader wpfPageLoader = new(WsEnumPage.MessageBox, false, FormBorderStyle.FixedDialog,
        //    20, 14, 18, 0, 12)
        //{ Width = Width - 50, Height = Height - 50 };
        //wpfPageLoader.Text = $@"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
        //wpfPageLoader.MessageBoxViewModel.Message = message;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.ButtonOkVisibility = Visibility.Visible;
        //wpfPageLoader.MessageBoxViewModel.ButtonVisibility.Localization();
        //wpfPageLoader.ShowDialog(this);
        //wpfPageLoader.Close();
    }

    private void LoadLocalizationStatic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        MdInvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
        MdInvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
        MdInvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
        MdInvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
        MdInvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonPlu);
        MdInvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
        MdInvokeControl.SetText(labelPackageWeight, LocaleCore.Scales.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, LocaleCore.Scales.FieldDate);
        MdInvokeControl.SetText(labelKneading, LocaleCore.Scales.FieldKneading);
    }

    #endregion
}