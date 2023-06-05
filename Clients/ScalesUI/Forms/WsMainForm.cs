// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsPrintCore.Common;

namespace ScalesUI.Forms;

public partial class WsMainForm : Form
{
    #region Public and private fields, properties, constructor

    private ActionSettingsModel ActionSettings { get; set; }
    private WsDebugHelper Debug => WsDebugHelper.Instance;
    private WsFontsSettingsHelper FontsSettings => WsFontsSettingsHelper.Instance;
    private IKeyboardMouseEvents KeyboardMouseEvents { get; set; }
    private WsSchedulerHelper WsScheduler => WsSchedulerHelper.Instance;
    private WsUserSessionHelper UserSession => WsUserSessionHelper.Instance;
    private WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    private WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsLabelSessionHelper LabelSession => WsLabelSessionHelper.Instance;
    private Button ButtonLine { get; set; }
    private Button ButtonPluNestingFk { get; set; }
    private Button ButtonKneading { get; set; }
    private Button ButtonNewPallet { get; set; }
    private Button ButtonPlu { get; set; }
    private Button ButtonPrint { get; set; }
    private Button ButtonScalesInit { get; set; }
    private Button ButtonScalesTerminal { get; set; }
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
    /// Загрузить данные в фоне.
    /// </summary>
    private void MainFormLoadAtBackground()
    {
        // Назначить хуки мышки.
        MouseSubscribe();
        // Настройка кнопок.
        SetupButtons();
        // Загрузка шрифтов.
        LoadFonts();
        // Прочее.
        LabelSession.NewPallet();
        MdInvokeControl.SetText(this, WsAppVersionHelper.Instance.AppTitle);
        MdInvokeControl.SetText(fieldProductDate, string.Empty);
        // Настроить плагины.
        SetupPlugins();
        LoadLocalizationStatic(Lang.Russian);
        // Планировщик.
        WsScheduler.Load(this);
        // Загрузить WinForms-контролы.
        LoadNavigationUserControl();
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

    /// <summary>
    /// Загрузить форму.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MainForm_Load(object sender, EventArgs e)
    {
        WsFormNavigationUtils.ActionTryCatch(this, ShowFormUserControl, () =>
        {
            UserSession.StopwatchMain = Stopwatch.StartNew();
            UserSession.StopwatchMain.Restart();
            LocaleCore.Lang = LocaleData.Lang = Lang.Russian;
            // Загрузить WinForms-контрол ожидания.
            LoadNavigationWaitUserControl();
            // Проверка линии.
            LabelSession.SetSessionForLabelPrint(ShowFormUserControl);
            if (LabelSession.DeviceScaleFk.IsNew)
            {
                string message = LocaleCore.Scales.RegistrationWarningLineNotFound(LabelSession.DeviceName);
                WsFormNavigationUtils.DialogUserControl.ViewModel.SetupButtonsOk(
                    message + Environment.NewLine + Environment.NewLine + LocaleCore.Scales.CommunicateWithAdmin,
                    ActionExit, WsFormNavigationUtils.NavigationUserControl.Width);
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, WsEnumLogType.Error,
                    WsEnumDialogType.Ok, new() { ActionFinally });
                ContextManager.ContextItem.SaveLogError(new Exception(message));
                return;
            }
            // Проверка повторного запуска.
            _ = new Mutex(true, System.Windows.Forms.Application.ProductName, out bool isCreatedNew);
            if (!isCreatedNew)
            {
                string message = $"{LocaleCore.Strings.Application} {System.Windows.Forms.Application.ProductName} {LocaleCore.Scales.AlreadyRunning}!";
                WsFormNavigationUtils.DialogUserControl.ViewModel.SetupButtonsOk(message, ActionExit, 
                    WsFormNavigationUtils.NavigationUserControl.Width);
                // Навигация в контрол диалога Ок.
                WsFormNavigationUtils.NavigateToNewDialog(ShowFormUserControl, message, true, WsEnumLogType.Error,
                    WsEnumDialogType.Ok, new() { ActionFinally });
                ContextManager.ContextItem.SaveLogWarning(message);
                return;
            }
            // Навигация в контрол ожидания.
            WsFormNavigationUtils.NavigateToExistsWait(ShowFormUserControl,  LocaleCore.Scales.AppLoad, LocaleCore.Scales.AppLoadDescription);
            // Загрузка фоном.
            MainFormLoadAtBackground();
            // Авто-возврат из контрола на главную форму.
            WsFormNavigationUtils.WaitUserControl.ViewModel.CmdCustom.Relay();
            // Логи.
            UserSession.StopwatchMain.Stop();
            ContextManager.ContextItem.SaveLogMemory(
                UserSession.PluginMemory.GetMemorySizeAppMb(), UserSession.PluginMemory.GetMemorySizeFreeMb());
            ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleData.Program.IsLoaded}. " + Environment.NewLine +
                $"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}." + Environment.NewLine +
                $"{nameof(LocaleData.Program.TimeSpent)}: {UserSession.StopwatchMain.Elapsed}.");
        });
    }

    private static void ActionExit() => System.Windows.Forms.Application.Exit();

    /// <summary>
    /// Настроить плагины.
    /// </summary>
    private void SetupPlugins()
    {
        // Память.
        UserSession.PluginMemory.Init(new(2_000, 1_000), new(1_000, 1_000),
            new(1_000, 1_000), fieldMemory);
        UserSession.PluginMemory.Execute();

        // Весовая платформа Масса-К.
        //UserSession.PluginMassa.Init(new(1_000, 1_000), new(0_100, 0_100),
        //    new(0_050, 0_100), fieldNettoWeight, fieldMassa, fieldMassaExt, ResetWarning);
        UserSession.PluginMassa.Init(new(1_000, 1_000), new(0_150, 0_150),
            new(0_150, 0_150), fieldNettoWeight, fieldMassa, fieldMassaExt, ResetWarning);
        PluginMassaExecute();
        MdInvokeControl.SetVisible(fieldMassaExt, Debug.IsDevelop);

        // Основной принтер.
        if (LabelSession.Line.PrinterMain is not null)
        {
            switch (LabelSession.PrintModelMain)
            {
                case WsEnumPrintModel.Tsc:
                    LabelSession.PluginPrintTscMain = new();
                    LabelSession.PluginPrintTscMain.InitTsc(new(0_500, 0_500),
                        new(0_500, 0_500), new(0_500, 0_500),
                        GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
                    MdInvokeControl.SetVisible(fieldPrintMain, true);
                    MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
                    LabelSession.PluginPrintTscMain.Execute();
                    break;
                case WsEnumPrintModel.Zebra:
                    LabelSession.PluginPrintZebraMain = new();
                    LabelSession.PluginPrintZebraMain.InitZebra(new(0_500, 0_500),
                        new(0_500, 0_500), new(0_500, 0_500),
                        GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
                    MdInvokeControl.SetVisible(fieldPrintMain, true);
                    MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
                    LabelSession.PluginPrintZebraMain.Execute();
                    LabelSession.PluginPrintZebraMain.SetOdometorUserLabel(1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Транспортный принтер.
        if (LabelSession.Line.IsShipping)
        {
            if (LabelSession.Line.PrinterShipping is not null)
            {
                switch (LabelSession.PrintModelShipping)
                {
                    case WsEnumPrintModel.Tsc:
                        LabelSession.PluginPrintTscShipping = new();
                        LabelSession.PluginPrintTscShipping.InitTsc(new(0_500, 0_500),
                            new(0_500, 0_500), new(0_500, 0_500),
                            GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
                        MdInvokeControl.SetVisible(fieldPrintMain, true);
                        MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
                        LabelSession.PluginPrintTscShipping.Execute();
                        break;
                    case WsEnumPrintModel.Zebra:
                        LabelSession.PluginPrintZebraShipping = new WsPluginPrintZebraModel();
                        LabelSession.PluginPrintZebraShipping.InitZebra(new(0_500, 0_500),
                            new(0_500, 0_500), new(0_500, 0_500),
                            GetMdPrinter(LabelSession.Line.PrinterMain), fieldPrintMain, fieldPrintMainExt, true);
                        MdInvokeControl.SetVisible(fieldPrintMain, true);
                        MdInvokeControl.SetVisible(fieldPrintMainExt, Debug.IsDevelop);
                        LabelSession.PluginPrintZebraShipping.Execute();
                        LabelSession.PluginPrintZebraShipping.SetOdometorUserLabel(1);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        // Метки.
        UserSession.PluginLabels.Init(
            new(0_500, 0_500), new(0_500, 0_500),
            new(0_500, 0_500), fieldPlu, fieldProductDate, fieldKneading);
        UserSession.PluginLabels.Execute();
        MdInvokeControl.SetText(fieldTitle, $"{WsAppVersionHelper.Instance.AppTitle}. {LabelSession.PublishDescription}.");
        MdInvokeControl.SetBackColor(fieldTitle, Color.Transparent);
    }

    /// <summary>
    /// Загрузить шрифты.
    /// </summary>
    private void LoadFonts()
    {
        fieldTitle.Font = FontsSettings.FontLabelsTitle;

        fieldNettoWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldTareWeight.Font = FontsSettings.FontLabelsMaximum;
        fieldPlu.Font = FontsSettings.FontLabelsMaximum;
        fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

        fieldPrintMain.Font = FontsSettings.FontLabelsGray;
        fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
        fieldMassaExt.Font = FontsSettings.FontLabelsGray;
        fieldMassa.Font = FontsSettings.FontLabelsGray;
        fieldPrintMainExt.Font = FontsSettings.FontLabelsGray;
        fieldPrintShippingExt.Font = FontsSettings.FontLabelsGray;
        fieldMemory.Font = FontsSettings.FontLabelsGray;

        fieldWarning.Font = FontsSettings.FontLabelsBlack;
        labelNettoWeight.Font = FontsSettings.FontLabelsBlack;
        labelTareWeight.Font = FontsSettings.FontLabelsBlack;
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
        ButtonPrint.Font = FontsSettings.FontButtons;
        ButtonPrint.BackColor = ColorTranslator.FromHtml("#ff7f50");
    }

    /// <summary>
    /// Настроить кнопки.
    /// </summary>
    private void SetupButtons()
    {
        ActionSettings = new()
        {
            // Устройства.
            IsDevice = true,
            IsPlu = true,
            IsNesting = true,
            // Действия.
            IsKneading = true,
            IsNewPallet = false,
            IsOrder = LabelSession.Line.IsOrder,
            IsPrint = true,
            IsScalesInit = false,
            IsScalesTerminal = true,
        };

        CreateButtonsDevices();
        CreateButtonsActions();
    }

    /// <summary>
    /// Создать кнопки устройств.
    /// </summary>
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
            ButtonPlu.Click += ActionSwitchPluLine;
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

    /// <summary>
    /// Создать кнопки действий.
    /// </summary>
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

    /// <summary>
    /// Назначить хуки мышки.
    /// </summary>
    private void MouseSubscribe()
    {
        KeyboardMouseEvents = Hook.AppEvents();
        KeyboardMouseEvents.MouseDownExt += MouseDownExt;
    }

    /// <summary>
    /// Завершить хуки мышки.
    /// </summary>
    private void MouseUnsubscribe()
    {
        KeyboardMouseEvents.MouseDownExt -= MouseDownExt;
        KeyboardMouseEvents.Dispose();
    }

    /// <summary>
    /// Обработчик нажатий мышки.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void MouseDownExt(object sender, MouseEventExtArgs e)
    {
        if (e.Button.Equals(MouseButtons.Middle))
        {
            e.Handled = true;
            ActionPreparePrint(sender, e);
        }
    }

    /// <summary>
    /// Загрузить локализацию.
    /// </summary>
    /// <param name="lang"></param>
    private void LoadLocalizationStatic(Lang lang)
    {
        LocaleCore.Lang = LocaleData.Lang = lang;
        MdInvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
        MdInvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
        MdInvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
        MdInvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonSetKneading);
        MdInvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonPlu);
        MdInvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
        MdInvokeControl.SetText(labelNettoWeight, LocaleCore.Scales.FieldWeightNetto);
        MdInvokeControl.SetText(labelTareWeight, LocaleCore.Scales.FieldWeightTare);
        MdInvokeControl.SetText(labelProductDate, LocaleCore.Scales.FieldDate);
        MdInvokeControl.SetText(labelKneading, LocaleCore.Scales.FieldKneading);
    }

    #endregion
}