﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Schedulers;
using DataCore.Settings;
using DataCore.Wmi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;
using WeightCore.Managers;
using static DataCore.ShareEnums;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties - Helpers

        private AppVersionHelper AppVersion { get; set; } = AppVersionHelper.Instance;
        private DebugHelper Debug { get; set; } = DebugHelper.Instance;
        private ProcHelper Proc { get; set; } = ProcHelper.Instance;
        private QuartzHelper Quartz { get; set; } = QuartzHelper.Instance;
        private UserSessionHelper UserSession { get; set; } = UserSessionHelper.Instance;

        #endregion

        #region Private fields and properties

        private Button ButtonKneading { get; set; }
        private Button ButtonMore { get; set; }
        private Button ButtonNewPallet { get; set; }
        private Button ButtonOrder { get; set; }
        private Button ButtonPlu { get; set; }
        private Button ButtonPrint { get; set; }
        private Button ButtonScalesInit { get; set; }
        private Button ButtonScalesTerminal { get; set; }
        private FontsSettingsEntity FontsSettings { get; set; }
        private readonly object _lockerDays = new();
        private TableLayoutPanel TableLayoutPanelButtons { get; set; }

        #endregion

        #region Public and private methods - MainForm

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                UserSession.StopwatchMain = Stopwatch.StartNew();
                UserSession.StopwatchMain.Restart();
                UserSession.DataAccess.Log.Setup(UserSession.Scale.Host.Name, typeof(Program).Assembly.GetName().Name);
                FormBorderStyle = Debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
                TopMost = !Debug.IsDebug;
                FontsSettings = new();
                MainForm_ButtonsCreate();
                LoadResources();
                UserSession.NewPallet();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
                UserSession.StopwatchMain.Stop();
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
                    Quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), delegate { ScheduleEveryDays(); },
                        "jobScheduleEveryDays", "triggerScheduleEveryDays", "triggerGroupScheduleEveryDays");
                    //Quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { ScheduleEverySeconds(); },
                    //    "jobScheduleEverySeconds", "triggerScheduleEverySeconds", "triggerGroupScheduleEverySeconds");
                    LoadManagerControl();
                }
                catch (Exception ex)
                {
                    GuiUtils.WpfForm.CatchException(this, ex);
                }
                finally
                {
                    MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                    SetComboBoxItems(fieldLang, FieldLang_SelectedIndexChanged, LocaleCore.Scales.ListLanguages);
                    UserSession.DataAccess.Log.LogInformation($"{LocaleCore.Scales.ScreenResolution}: {Width} x {Height}");
                    UserSession.DataAccess.Log.LogInformation(LocaleData.Program.IsLoaded + $" {nameof(UserSession.StopwatchMain.Elapsed)}: {UserSession.StopwatchMain.Elapsed}.");
                    UserSession.StopwatchMain.Stop();
                }
            }).ConfigureAwait(false);
        }

        private void LoadResources()
        {
            try
            {
                System.Resources.ResourceManager resourceManager = new("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                object exit = resourceManager.GetObject("exit_1");
                if (exit != null)
                {
                    Bitmap bmpExit = new((Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                }

                MDSoft.WinFormsUtils.InvokeControl.SetText(this, AppVersion.AppTitle);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void LoadManagerControl()
        {
            try
            {
                // Labels.
                UserSession.ManagerControl.Labels.Init(fieldTitle, fieldPlu, fieldSscc,
                    labelProductDate, fieldProductDate, labelKneading, fieldKneading, fieldResolution, fieldLang,
                    ButtonKneading, ButtonMore, ButtonNewPallet, ButtonOrder, ButtonPlu, ButtonPrint,
                    ButtonScalesInit, ButtonScalesTerminal, pictureBoxClose, 
                    fieldPrintMainManager, fieldPrintShippingManager, fieldMassaManager);
                UserSession.ManagerControl.Labels.Open();
                // Memory.
                UserSession.ManagerControl.Memory.Init(fieldMemory, fieldTasks);
                UserSession.ManagerControl.Memory.Open();
                // Massa.
                UserSession.ManagerControl.Massa.Init(labelWeightNetto, fieldWeightNetto, labelWeightTare, fieldWeightTare,
                    fieldMassaThreshold, fieldMassaGet, fieldMassaPluDescription);
                UserSession.ManagerControl.Massa.Open();
                // PrintMain.
                UserSession.ManagerControl.PrintMain.Init(UserSession.PrintBrandMain, UserSession.Scale.PrinterMain,
                    UserSession.Scale.PrinterMain.Name, UserSession.Scale.PrinterMain.Ip,
                    UserSession.Scale.PrinterMain.Port, fieldPrintMain, true);
                UserSession.ManagerControl.PrintMain.Open(true);
                UserSession.ManagerControl.PrintMain.SetOdometorUserLabel(1);
                // PrintShipping.
                if (UserSession.Scale.IsShipping)
                {
                    UserSession.ManagerControl.PrintShipping.Init(UserSession.PrintBrandShipping, UserSession.Scale.PrinterShipping,
                        UserSession.Scale.PrinterShipping.Name, UserSession.Scale.PrinterShipping.Ip,
                        UserSession.Scale.PrinterShipping.Port, fieldPrintShipping, false);
                    UserSession.ManagerControl.PrintShipping.Open(false);
                    UserSession.ManagerControl.PrintShipping.SetOdometorUserLabel(1);
                }
                //ButtonScalesInit_Click(sender, e);
                UserSession.ManagerControl.Labels.SetControlsVisible();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.Alt && e.KeyCode == Keys.Q)
                || (e.Alt && e.KeyCode == Keys.X)
                || (e.Control && e.KeyCode == Keys.Q)
                || e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                UserSession.StopwatchMain.Restart();
                if (Quartz != null)
                {
                    Quartz.Close();
                    Quartz.Dispose();
                }
                UserSession.ManagerControl.Labels.Close();
                UserSession.ManagerControl.Massa.Close();
                UserSession.ManagerControl.Memory.Close();
                UserSession.ManagerControl.PrintMain.Close();
                UserSession.ManagerControl.PrintShipping.Close();
                UserSession.ManagerControl.Close();
                UserSession.ManagerControl.Dispose();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                UserSession.DataAccess.Log.LogInformation(LocaleData.Program.IsClosed + $" {nameof(UserSession.StopwatchMain.Elapsed)}: {UserSession.StopwatchMain.Elapsed}.");
                UserSession.StopwatchMain.Stop();
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (Equals(e.Button, MouseButtons.Middle))
            {
                ActionPrint_Click(sender, e);
            }
        }

        private void MainForm_FontsSet()
        {
            fieldResolution.Font = FontsSettings.FontMinimum;
            fieldLang.Font = FontsSettings.FontMinimum;

            fieldTitle.Font = FontsSettings.FontLabelsTitle;

            fieldWeightNetto.Font = FontsSettings.FontLabelsMaximum;
            fieldWeightTare.Font = FontsSettings.FontLabelsMaximum;
            fieldPlu.Font = FontsSettings.FontLabelsMaximum;
            fieldProductDate.Font = FontsSettings.FontLabelsMaximum;

            fieldMassaThreshold.Font = FontsSettings.FontLabelsGray;
            fieldSscc.Font = FontsSettings.FontLabelsGray;
            fieldTasks.Font = FontsSettings.FontLabelsGray;
            fieldPrintMain.Font = FontsSettings.FontLabelsGray;
            fieldPrintShipping.Font = FontsSettings.FontLabelsGray;
            fieldMassaPluDescription.Font = FontsSettings.FontLabelsGray;
            fieldMassaManager.Font = FontsSettings.FontLabelsGray;
            fieldMassaGet.Font = FontsSettings.FontLabelsGray;
            fieldPrintMainManager.Font = FontsSettings.FontLabelsGray;
            fieldPrintShippingManager.Font = FontsSettings.FontLabelsGray;
            fieldMemory.Font = FontsSettings.FontLabelsGray;

            labelWeightNetto.Font = FontsSettings.FontLabelsBlack;
            labelWeightTare.Font = FontsSettings.FontLabelsBlack;
            labelKneading.Font = FontsSettings.FontLabelsBlack;
            fieldKneading.Font = FontsSettings.FontLabelsBlack;
            labelProductDate.Font = FontsSettings.FontLabelsBlack;

            if (ButtonScalesTerminal != null)
                ButtonScalesTerminal.Font = FontsSettings.FontButtons;
            if (ButtonScalesInit != null)
                ButtonScalesInit.Font = FontsSettings.FontButtons;
            if (ButtonOrder != null)
                ButtonOrder.Font = FontsSettings.FontButtons;
            if (ButtonNewPallet != null)
                ButtonNewPallet.Font = FontsSettings.FontButtons;
            if (ButtonKneading != null)
                ButtonKneading.Font = FontsSettings.FontButtons;
            if (ButtonPlu != null)
                ButtonPlu.Font = FontsSettings.FontButtons;
            if (ButtonMore != null)
                ButtonMore.Font = FontsSettings.FontButtons;
            if (ButtonPrint != null)
                ButtonPrint.Font = FontsSettings.FontButtons;
        }

        private void MainForm_ButtonsCreate()
        {
            ButtonsSettingsEntity buttonsSettings = new()
            {
                IsKneading = false,
                IsMore = true,
                IsNewPallet = true,
                IsOrder = UserSession.Scale.IsOrder,
                IsPlu = true,
                IsPrint = true,
                IsScalesInit = false,
                IsScalesTerminal = true,
            };

            int column = 0;

            TableLayoutPanelButtons = GuiUtils.WinForm.NewTableLayoutPanel(tableLayoutPanelMain, nameof(TableLayoutPanelButtons),
                0, tableLayoutPanelMain.RowCount - 1, tableLayoutPanelMain.ColumnCount);

            if (buttonsSettings.IsScalesTerminal)
            {
                ButtonScalesTerminal = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesTerminal), column++);
                ButtonScalesTerminal.Click += new EventHandler(ActionScalesTerminal_Click);
                ButtonScalesTerminal.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsScalesInit)
            {
                ButtonScalesInit = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonScalesInit), column++);
                ButtonScalesInit.Click += new EventHandler(ActionScalesInit_Click);
                ButtonScalesInit.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsOrder)
            {
                ButtonOrder = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonOrder), column++);
                ButtonOrder.Click += new EventHandler(ActionOrder_Click);
                ButtonOrder.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsNewPallet)
            {
                ButtonNewPallet = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonNewPallet), column++);
                ButtonNewPallet.Click += new EventHandler(ActionNewPallet_Click);
                ButtonNewPallet.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsKneading)
            {
                ButtonKneading = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonKneading), column++);
                ButtonKneading.Click += new EventHandler(ActionKneading_Click);
                ButtonKneading.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPlu)
            {
                ButtonPlu = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPlu), column++);
                ButtonPlu.Click += new EventHandler(ActionPlu_Click);
                ButtonPlu.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsMore)
            {
                ButtonMore = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonMore), column++);
                ButtonMore.Click += new EventHandler(ActionMore_Click);
                ButtonMore.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            if (buttonsSettings.IsPrint)
            {
                ButtonPrint = GuiUtils.WinForm.NewTableLayoutPanelButton(TableLayoutPanelButtons, nameof(ButtonPrint), column++);
                ButtonPrint.Click += new EventHandler(ActionPrint_Click);
                ButtonPrint.KeyUp += new KeyEventHandler(MainForm_KeyUp);
            }

            GuiUtils.WinForm.SetTableLayoutPanelColumnStyles(TableLayoutPanelButtons, column);
        }

        #endregion

        #region Public and private methods - Schedulers

        private void ScheduleEveryDays()
        {
            lock (_lockerDays)
            {
                if (Quartz == null)
                    return;
                UserSession.ProductDate = DateTime.Now;
                UserSession.DataAccess.Log.LogInformation("ScheduleIsNextDay");
            }
        }

        #endregion

        #region Private methods

        public void SetComboBoxItems(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
        {
            if (comboBox == null || sourceList == null || sourceList.Count == 0 || selectedIndex < 0)
                return;

            if (comboBox.InvokeRequired)
            {
                comboBox.Invoke((Action)delegate
                {
                    Work(comboBox, eventHandler, sourceList, selectedIndex);
                });
            }
            else
            {
                Work(comboBox, eventHandler, sourceList, selectedIndex);
            }

            static void Work(ComboBox comboBox, EventHandler eventHandler, List<string> sourceList, int selectedIndex = 0)
            {
                comboBox.SelectedIndexChanged -= eventHandler;

                int backupIndex = comboBox.SelectedIndex;
                comboBox.Items.Clear();
                comboBox.Items.AddRange(sourceList.ToArray());
                comboBox.SelectedIndex = selectedIndex <= 0
                    ? backupIndex <= 0 ? 0 : backupIndex
                    : selectedIndex < comboBox.Items.Count ? selectedIndex : 0;

                comboBox.SelectedIndexChanged += eventHandler;
                eventHandler.Invoke(comboBox, null);
            }
        }

        private void FieldPrintManager_Click(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 22, 16, 16) 
                { Width = 700, Height = 450 };
            wpfPageLoader.Text = LocaleCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Caption = LocaleCore.Print.InfoCaption;
            wpfPageLoader.MessageBox.Message = GetPrintInfo(UserSession.ManagerControl.PrintMain, true);
            if (UserSession.Scale.IsShipping)
            {
                wpfPageLoader.MessageBox.Message += Environment.NewLine + Environment.NewLine +
                    GetPrintInfo(UserSession.ManagerControl.PrintShipping, false);
                wpfPageLoader.Height = 700;
            }
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonCustomContent = LocaleCore.Print.ClearQueue;
            DialogResult result = wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
            if (result == DialogResult.Retry)
            {
                UserSession.ManagerControl.PrintMain.ClearPrintBuffer(1);
                if (UserSession.Scale.IsShipping)
                    UserSession.ManagerControl.PrintShipping.ClearPrintBuffer(1);
            }
        }

        private string GetPrintInfo(ManagerPrint managerPrint, bool isMain)
        {
            string peeler = isMain
                ? UserSession.ManagerControl.PrintMain.ZebraPeelerStatus : UserSession.ManagerControl.PrintShipping.ZebraPeelerStatus;
            string printMode = isMain
                ? UserSession.ManagerControl.PrintMain.GetZebraPrintMode() :
                UserSession.ManagerControl.PrintShipping.GetZebraPrintMode();
            WmiWin32PrinterEntity win32Printer = managerPrint.Win32Printer;
            return
                $"{managerPrint.GetDeviceName(isMain)}" + Environment.NewLine +
                $"{LocaleCore.Print.DeviceCommunication} ({managerPrint.Ip}): {managerPrint.Printer.PingStatus}" + Environment.NewLine +
                $"{LocaleCore.Print.PrinterStatus}: {managerPrint.GetDeviceStatus()}" + Environment.NewLine +
                Environment.NewLine +
                $"{LocaleCore.Print.Driver}: {win32Printer.DriverName}" + Environment.NewLine +
                $"{LocaleCore.Print.Port}: {win32Printer.PortName}" + Environment.NewLine +
                $"{LocaleCore.Print.StateCode}: {win32Printer.PrinterState}" + Environment.NewLine +
                $"{LocaleCore.Print.StatusCode}: {win32Printer.PrinterStatus}" + Environment.NewLine +
                $"{LocaleCore.Print.Status}: {win32Printer.PrinterStatusDescription}" + Environment.NewLine +
                $"{LocaleCore.Print.State} (ENG): {win32Printer.Status}" + Environment.NewLine +
                $"{LocaleCore.Print.State}: {win32Printer.StatusDescription}" + Environment.NewLine +
                $"{LocaleCore.Print.SensorPeeler}: {peeler}" + Environment.NewLine +
                $"{LocaleCore.Print.Mode}: {printMode}" + Environment.NewLine;
        }

        private void FieldSscc_Click(object sender, EventArgs e)
        {
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog, 26, 20, 18) { Width = 700, Height = 400 };
            wpfPageLoader.Text = LocaleCore.Scales.FieldSsccShort;
            wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.FieldSscc;
            wpfPageLoader.MessageBox.Message =
                $"{LocaleCore.Scales.FieldSscc}: {UserSession.ProductSeries.Sscc.SSCC}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccGln}: {UserSession.ProductSeries.Sscc.GLN}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccUnitId}: {UserSession.ProductSeries.Sscc.UnitID}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccUnitType}: {UserSession.ProductSeries.Sscc.UnitType}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccSynonym}: {UserSession.ProductSeries.Sscc.SynonymSSCC}" + Environment.NewLine +
                $"{LocaleCore.Scales.FieldSsccControlNumber}: {UserSession.ProductSeries.Sscc.Check}";
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
        }

        private void FieldTasks_Click(object sender, EventArgs e)
        {
            string message = string.Empty;
            foreach (ProcessThread thread in Process.GetCurrentProcess().Threads)
            {
                message += $"{LocaleCore.Scales.ThreadId}: {thread.Id}. " +
                    $"{LocaleCore.Scales.ThreadPriorityLevel}: {thread.PriorityLevel}. " +
                    $"{LocaleCore.Scales.ThreadState}: {thread.ThreadState}. " +
                    $"{LocaleCore.Scales.ThreadStartTime}: {thread.StartTime}. " + Environment.NewLine;
            }
            using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.MessageBox, false, FormBorderStyle.FixedDialog,
                20, 14, 18, 0, 12, 1)
            { Width = Width - 50, Height = Height - 50 };
            wpfPageLoader.Text = $"{LocaleCore.Scales.ThreadsCount}: {Process.GetCurrentProcess().Threads.Count}";
            wpfPageLoader.MessageBox.Message = message;
            wpfPageLoader.MessageBox.VisibilitySettings.ButtonOkVisibility = Visibility.Visible;
            wpfPageLoader.MessageBox.VisibilitySettings.Localization();
            wpfPageLoader.ShowDialog(this);
            wpfPageLoader.Close();
        }

        private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (fieldResolution.Items[fieldResolution.SelectedIndex])
                {
                    case "800x600":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(800, 600);
                        break;
                    case "1024x768":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1024, 768);
                        break;
                    case "1366x768":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1366, 768);
                        break;
                    case "1600x1024":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1600, 1024);
                        break;
                    case "1920x1080":
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1920, 1080);
                        break;
                    default:
                        WindowState = FormWindowState.Maximized;
                        break;
                }
                CenterToScreen();
                FontsSettings.Transform(Width, Height);
                MainForm_FontsSet();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void FieldLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LocaleCore.Lang = LocaleData.Lang = fieldLang.SelectedIndex switch { 1 => Lang.English, _ => Lang.Russian, };
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesTerminal, LocaleCore.Scales.ButtonRunScalesTerminal);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonScalesInit, LocaleCore.Scales.ButtonScalesInitShort);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonOrder, LocaleCore.Scales.ButtonSelectOrder);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonNewPallet, LocaleCore.Scales.ButtonNewPallet);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonKneading, LocaleCore.Scales.ButtonAddKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPlu, LocaleCore.Scales.ButtonSelectPlu);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonMore, LocaleCore.Scales.ButtonSetKneading);
                MDSoft.WinFormsUtils.InvokeControl.SetText(ButtonPrint, LocaleCore.Print.ActionPrint);
                SetComboBoxItems(fieldResolution, FieldResolution_SelectedIndexChanged, LocaleCore.Scales.ListResolutions,
                    Debug.IsDebug ? 2 : LocaleCore.Scales.ListResolutions.Count - 1);
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void FieldTitle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.Massa.Close();

                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
                wpfPageLoader.ShowDialog(this);
                wpfPageLoader.Close();

                UserSession.ManagerControl.Massa.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        #endregion

        #region Public and private methods - Actions

        private void ActionClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ActionScalesTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this, $"{LocaleCore.Scales.QuestionRunApp} ScalesTerminal?", true,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result != DialogResult.Yes)
                    return;

                // Run app.
                if (File.Exists(LocaleData.Paths.ScalesTerminal))
                {
                    UserSession.ManagerControl.Massa.Close();
                    Proc.Run(LocaleData.Paths.ScalesTerminal, string.Empty, false, ProcessWindowStyle.Normal, true);
                }
                else
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this,
                        LocaleCore.Scales.ProgramNotFound(LocaleData.Paths.ScalesTerminal), true);
                }
                UserSession.ManagerControl.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionScalesInit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSession.IsPluCheckWeight)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.PluNotSelectWeight, true);
                    return;
                }
                if (!UserSession.ManagerControl.Massa.MassaDevice.IsOpenPort)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.MassaIsNotRespond, true);
                    return;
                }

                DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.QuestionPerformOperation, true, 
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (result != DialogResult.Yes)
                    return;

                UserSession.ManagerControl.Massa.Close();

                // Fix negative weight.
                if (UserSession.ManagerControl.Massa.WeightNet < 0)
                {
                    UserSession.ManagerControl.Massa.ResetMassa();
                }

                UserSession.CheckWeightMassaDeviceExists(this);
                UserSession.SetCurrentPlu(null);

                UserSession.ManagerControl.Massa.Open();
                UserSession.ManagerControl.Massa.GetInit();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionOrder_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.ManagerControl.Massa.Close();

                if (UserSession.Order == null)
                {
                    using OrderListForm settingsForm = new();
                    settingsForm.ShowDialog(this);
                    settingsForm.Close();
                    settingsForm.Dispose();
                }
                else
                {
                    using OrderDetailForm settingsForm = new();
                    DialogResult result = settingsForm.ShowDialog(this);
                    settingsForm.Close();
                    settingsForm.Dispose();
                    if (result == DialogResult.Retry)
                    {
                        UserSession.Order = null;
                    }
                    if (result == DialogResult.OK)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                    if (result == DialogResult.Cancel)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                }
                //if (UserSession.Order != null)
                //{
                //    MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldPrintProgressMain, UserSession.Order.PlaneBoxCount);
                //    MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldPrintProgressMain, 0);
                //    MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldPrintProgressMain, UserSession.Order.FactBoxCount);
                //}

                UserSession.ManagerControl.Massa.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionNewPallet_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.NewPallet();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionKneading_Click(object sender, EventArgs e)
        {
            try
            {
                using NumberInputForm numberInputForm = new() { InputValue = 0 };
                DialogResult result = numberInputForm.ShowDialog(this);
                numberInputForm.Close();
                numberInputForm.Dispose();
                if (result == DialogResult.OK)
                    UserSession.WeighingSettings.Kneading = (byte)numberInputForm.InputValue;
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionPlu_Click(object sender, EventArgs e)
        {
            try
            {
                UserSession.SetCurrentPlu(null);
                if (UserSession.CheckWeightMassaDeviceExists(this))
                {
                    if (!UserSession.CheckWeightIsNegative(this) || !UserSession.CheckWeightIsPositive(this))
                        return;
                }
                UserSession.ManagerControl.Massa.Close();

                // PLU form.
                using PluListForm pluListForm = new() { Owner = this };
                DialogResult result = pluListForm.ShowDialog(this);
                pluListForm.Close();
                pluListForm.Dispose();
                if (result == DialogResult.OK)
                {
                    UserSession.WeighingSettings.Kneading = 1;
                    UserSession.ProductDate = DateTime.Now;
                    UserSession.NewPallet();
                    //_mkDevice.SetTareWeight((int) (_sessionState.CurrentPLU.GoodsTareWeight * _sessionState.CurrentPLU.Scale.ScaleFactor));

                    // сразу перейдем к форме с замесами, размерами паллет и прочее
                    ActionMore_Click(null, null);
                }
                else
                {
                    UserSession.SetCurrentPlu(null);
                }

                UserSession.ManagerControl.Massa.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionMore_Click(object sender, EventArgs e)
        {
            try
            {
                if (UserSession.Plu == null)
                {
                    GuiUtils.WpfForm.ShowNewOperationControl(this, LocaleCore.Scales.PluNotSelect, true);
                    return;
                }

                UserSession.ManagerControl.Massa.Close();

                using KneadingForm kneadingForm = new() { Owner = this };
                DialogResult result = kneadingForm.ShowDialog();
                kneadingForm.Close();
                kneadingForm.Dispose();
                if (result == DialogResult.OK)
                {
                    //_sessionState.Kneading = settingsForm.CurrentKneading;
                    //_sessionState.ProductDate = settingsForm.CurrentProductDate;
                }

                UserSession.ManagerControl.Massa.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
            }
        }

        private void ActionPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!UserSession.CheckPluIsEmpty(this))
                    return;
                if (!UserSession.CheckWeightMassaDeviceExists(this))
                    return;
                if (!UserSession.CheckWeightMassaIsStable(this))
                    return;
                UserSession.SetWeighingFact();
                if (!UserSession.CheckWeightIsNegative(this))
                    return;
                if (!UserSession.CheckWeightThresholds(this))
                    return;
                // Check printers connections.
                if (!UserSession.CheckPrintIsConnect(this, UserSession.ManagerControl.PrintMain, true))
                    return;
                if (UserSession.Scale.IsShipping)
                    if (!UserSession.CheckPrintIsConnect(this, UserSession.ManagerControl.PrintShipping, false))
                        return;
                // Check printers statuses.
                if (!UserSession.CheckPrintStatusReady(this, UserSession.ManagerControl.PrintMain, true))
                    return;
                if (UserSession.Scale.IsShipping)
                    if (!UserSession.CheckPrintStatusReady(this, UserSession.ManagerControl.PrintShipping, false))
                        return;

                UserSession.PrintLabel(false);
                //UserSession.Manager.Massa.Open();
            }
            catch (Exception ex)
            {
                GuiUtils.WpfForm.CatchException(this, ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(ButtonPrint);
                //_sessionState.TaskManager.OpenPrintManager(CallbackPrintManagerClose, _sessionState.SqlViewModel,
                //_sessionState.PrintBrand, _sessionState.CurrentScale);
            }
        }

        #endregion
    }
}
