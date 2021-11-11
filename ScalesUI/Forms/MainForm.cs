﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore;
using DataProjectsCore.Helpers;
using DataProjectsCore.Utils;
using DataShareCore;
using DataShareCore.Helpers;
using DataShareCore.Schedulers;
using ScalesCore.Helpers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WeightCore.Gui;
using WeightCore.Helpers;

namespace ScalesUI.Forms
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly AppVersionHelper _appVersion = AppVersionHelper.Instance;
        private readonly DebugHelper _debug = DebugHelper.Instance;
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        private readonly ProcHelper _proc = ProcHelper.Instance;
        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly QuartzEntity _quartz = QuartzEntity.Instance;
        private bool _isShowInfoLabels = false;

        #endregion

        #region MainForm methods

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = _debug.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !_debug.IsDebug;
            fieldResolution.Visible = _debug.IsDebug;
            fieldResolution.SelectedIndex = _debug.IsDebug ? 1 : 0;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                if (_sessionState.CurrentScale != null)
                {
                    // _sessionState.CurrentScale.Load(_app.GuidToString());
                    buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !(_sessionState.CurrentScale.UseOrder == true));
                }

                LoadResources();

                _sessionState.NewPallet();

                //_quartz.AddJob(QuartzUtils.CronExpression.EveryDays(), delegate { ScheduleEveryDays(); }, "jobScheduleEveryDays");
                _quartz.AddJob(QuartzUtils.CronExpression.EverySeconds(), delegate { ScheduleEverySeconds(); }, "jobScheduleEverySeconds");

                if (_debug.IsDebug)
                    FieldCurrentTime_Click(sender, e);
                
                _log.Information("The program is runned");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
                ButtonScalesInit_Click(sender, e);
            }
        }

        private void LoadResources()
        {
            try
            {
                System.Resources.ResourceManager resourceManager = new("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                object exit = resourceManager.GetObject("exit_2");
                if (exit != null)
                {
                    Bitmap bmpExit = new((Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                }

                // Text = _appVersionHelper.AppTitle;
                MDSoft.WinFormsUtils.InvokeControl.SetText(this, _appVersion.AppTitle);
                switch (_sessionState.SqlViewModel.PublishType)
                {
                    case ShareEnums.PublishType.Debug:
                    case ShareEnums.PublishType.Dev:
                        //fieldTitle.Text = $@"{_sessionState.AppVersion}.  {_sessionState.CurrentScale.Description}. SQL: {_sessionState.SqlViewModel.PublishDescription}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_sessionState.CurrentScale.Description}. SQL: {_sessionState.SqlViewModel.PublishDescription}.");
                        fieldTitle.BackColor = Color.Yellow;
                        break;
                    case ShareEnums.PublishType.Release:
                        //fieldTitle.Text = $@"{_sessionState.AppVersion}.  {_sessionState.CurrentScale.Description}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_sessionState.CurrentScale.Description}.");
                        fieldTitle.BackColor = Color.LightGreen;
                        break;
                    case ShareEnums.PublishType.Default:
                    default:
                        //fieldTitle.Text = $@"{_sessionState.AppVersion}.  {_sessionState.CurrentScale.Description}. SQL: {_sessionState.SqlViewModel.PublishDescription}.";
                        MDSoft.WinFormsUtils.InvokeControl.SetText(fieldTitle, $@"{_appVersion.AppTitle}.  {_sessionState.CurrentScale.Description}. SQL: {_sessionState.SqlViewModel.PublishDescription}.");
                        fieldTitle.BackColor = Color.DarkRed;
                        break;
                }

                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, string.Empty);
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
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
                bool isClose;
                if (_debug.IsDebug)
                {
                    isClose = true;
                }
                else
                {
                    using PasswordForm pinForm = new() { TopMost = !_debug.IsDebug };
                    isClose = pinForm.ShowDialog() == DialogResult.OK;
                    pinForm.Close();
                }
                if (isClose)
                {
                    _sessionState.TaskManager.Close();
                    _sessionState.TaskManager.ClosePrintManager();
                    _quartz.Close();
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
                _log.Information("The program is closed");
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
        }

        #endregion

        #region Public and private methods - Schedulers

        private void ScheduleEveryDays()
        {
            _log.Information("ScheduleIsNextDay");
        }

        private void ScheduleEverySeconds()
        {
            _log.Information("ScheduleEverySeconds");

            //if (_sessionState.ProductDate.Date < DateTime.Now.Date && !_sessionState.IsChangedProductDate)
            //    _sessionState.ProductDate = DateTime.Now;
            //MDSoft.WinFormsUtils.InvokeControl.SetText(fieldCurrentTime, "Сейчас: " + DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
            //MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, $"{_sessionState.ProductDate:dd.MM.yyyy}");
            //MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, $"{_sessionState.Kneading}");

            //if (!Equals(buttonPrint.Enabled, _sessionState.CurrentPlu != null))
            //    MDSoft.WinFormsUtils.InvokeControl.SetEnabled(buttonPrint, _sessionState.CurrentPlu != null);

            //string strCheckWeight = _sessionState.CurrentPlu?.CheckWeight == true ? "вес" : "шт";
            //MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPlu, _sessionState.CurrentPlu != null
            //    ? $"{_sessionState.CurrentPlu.PLU} | {strCheckWeight} | {_sessionState.CurrentPlu.GoodsName}" : string.Empty);
            //MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightTare, _sessionState.CurrentPlu != null
            //    ? $"{_sessionState.CurrentPlu.GoodsTareWeight:0.000} кг" : "0,000 кг");

            _log.Information("ScheduleClock finish");
        }

        #endregion

        #region Public and private methods - Callbacks

        private void TaskManagerOpen()
        {
            _sessionState.TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager, 
                CallbackPrintManager, CallbackPrintManagerClose, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            Application.DoEvents();
        }

        private void CheckEnabledManager(ProjectsEnums.TaskType taskType, Control control)
        {
            if (_sessionState.SqlViewModel.IsTaskEnabled(taskType))
            {
                if (!control.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(control, true);
            }
            else
            {
                if (control.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(control, false);
            }
        }

        private void CallbackDeviceManager()
        {
        }

        private void CallbackMemoryManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MemoryManager, fieldMemoryManager);

            if (_sessionState.SqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMemoryManager,
                    $"Память: {_sessionState.TaskManager.MemoryManager.MemorySize.PhysicalCurrent.MegaBytes:N0} MB {_sessionState.TaskManager.MemoryManagerProgressString}");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMemoryManagerTotal,
                    _sessionState.TaskManager.MemoryManager.MemorySize.DtChanged.ToString(@"HH:mm:ss") + 
                    $"  Физическая память: свободно {_sessionState.TaskManager.MemoryManager.MemorySize.PhysicalFree.MegaBytes:N0} из " +
                    $"{_sessionState.TaskManager.MemoryManager.MemorySize.PhysicalTotal.MegaBytes:N0} MB.");
                MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldMemoryProgress, 
                    (int)_sessionState.TaskManager.MemoryManager.MemorySize.PhysicalTotal.MegaBytes);
                MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldMemoryProgress, 0);
                MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldMemoryProgress, 
                    (int)(_sessionState.TaskManager.MemoryManager.MemorySize.PhysicalTotal.MegaBytes - 
                    _sessionState.TaskManager.MemoryManager.MemorySize.PhysicalFree.MegaBytes));
                _sessionState.TaskManager.MemoryManagerProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.MemoryManagerProgressString);
            }
        }

        private void CallbackPrintManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldPrintManager);
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldLabelsCount);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldLabelsCount, $"Этикетки: {_sessionState.LabelsCurrent} / {_sessionState.LabelsCount}");

            // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
            _sessionState.LabelsCurrent = _sessionState.TaskManager.PrintManager.UserLabelCount < _sessionState.LabelsCount 
                ? _sessionState.TaskManager.PrintManager.UserLabelCount : _sessionState.LabelsCount;
            // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
            if (_sessionState.LabelsCurrent == 0)
                _sessionState.LabelsCurrent = 1;

            // TSC printers.
            //if (_sessionState.CurrentScale?.ZebraPrinter != null && _sessionState.IsTscPrinter)
            if (_sessionState.IsTscPrinter)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, 
                    $"Принтер: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.PrinterStatusDescription} " +
                    $"{_sessionState.TaskManager.PrintManagerProgressString}");
            }
            // Zebra printers.
            else
            {
                _sessionState.LabelsCurrent = _sessionState.TaskManager.PrintManager.UserLabelCount < _sessionState.LabelsCount 
                    ? _sessionState.TaskManager.PrintManager.UserLabelCount : _sessionState.LabelsCount;
                // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
                if (_sessionState.LabelsCurrent == 0)
                    _sessionState.LabelsCurrent = 1;
                if (_sessionState.TaskManager.PrintManager.CurrentStatus != null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, _sessionState.TaskManager.PrintManager.CurrentStatus.isReadyToPrint
                        ? $"Принтер {_sessionState.CurrentScale.ZebraPrinter.Ip}: доступен {_sessionState.TaskManager.PrintManagerProgressString}" 
                        : $"Принтер {_sessionState.CurrentScale.ZebraPrinter.Ip}: недоступен {_sessionState.TaskManager.PrintManagerProgressString}");
                }
            }
            _sessionState.TaskManager.PrintManagerProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.PrintManagerProgressString);
        }

        private void CallbackPrintManagerClose()
        {
            _sessionState.TaskManager.ClosePrintManager();
        }

        private void CallbackMassaManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, fieldMassaManager);
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, fieldMassaComPort);
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, fieldMassaQueries);
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, buttonScalesInit);
            bool flag = false;
            if (_sessionState.CurrentPlu != null)
            {
                flag = true;
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, _sessionState.CurrentPlu.CheckWeight == false
                    ? $"PLU (шт): {_sessionState.CurrentPlu.PLU}"
                    : $"PLU (вес): {_sessionState.CurrentPlu.PLU}");
                decimal weight = _sessionState.TaskManager.MassaManager.WeightNet - _sessionState.CurrentPlu.GoodsTareWeight;
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, $"{weight:0.000} кг");
            }

            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaManager, _sessionState.TaskManager.MassaManager.IsStable == 0
                ? $"Взвешивание | Вес брутто: { _sessionState.TaskManager.MassaManager.WeightNet:0.000} кг {_sessionState.TaskManager.MassaManagerProgressString}"
                : $"Весы стабильны | Вес брутто: { _sessionState.TaskManager.MassaManager.WeightNet:0.000} кг {_sessionState.TaskManager.MassaManagerProgressString}");
            _sessionState.TaskManager.MassaManagerProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.MassaManagerProgressString);
            // Состояние COM-порта.
            CallbackMassaManagerIsResponse();
            // Запрос параметров.
            CallbackMassaManagerResponseGetScalePar();
            // Сообщение взвешивания.
            CallbackMassaManagerResponseGetMassa();
            // Состояние запроса к весам.
            CallbackMassaManagerResponseSetAll();
            // Пакетов в очереди.
            CallbackMassaManagerRequestQueue();

            if (!flag)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, "PLU");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, "0,000 кг");
            }
        }

        /// <summary>
        /// Состояние COM-порта.
        /// </summary>
        private void CallbackMassaManagerIsResponse()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaComPort, _sessionState.TaskManager.MassaManager.IsResponse
                ? "Состояние COM-порта: отвечает" : "Состояние COM-порта: не отвечает");
        }

        /// <summary>
        /// Запрос параметров.
        /// </summary>
        private void CallbackMassaManagerResponseGetScalePar()
        {
            if (_sessionState.TaskManager.MassaManager.ResponseParseScalePar == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaScalePar, "Запрос параметров: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaScalePar,
                    _sessionState.TaskManager.MassaManager.ResponseParseScalePar.DtCreated.ToString(@"HH:mm:ss") + "  Запрос параметров: " +
                    _sessionState.TaskManager.MassaManager.ResponseParseScalePar.Message);
            }
        }

        /// <summary>
        /// Сообщение взвешивания.
        /// </summary>
        private void CallbackMassaManagerResponseGetMassa()
        {
            if (_sessionState.TaskManager.MassaManager.ResponseParseGet == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaGet, " Сообщение взвешивания: ...");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaGetCrc, "CRC: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaGet, 
                    _sessionState.TaskManager.MassaManager.ResponseParseGet.DtCreated.ToString(@"HH:mm:ss") + "  Сообщение взвешивания: " + 
                    _sessionState.TaskManager.MassaManager.ResponseParseGet.Message);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaGetCrc, "CRC: " +
                    (_sessionState.TaskManager.MassaManager.ResponseParseGet.IsValidAll 
                    ? $"верна {_sessionState.TaskManager.MassaRequestProgressString}" : $"ошибка! {_sessionState.TaskManager.MassaRequestProgressString}"));
                _sessionState.TaskManager.MassaRequestProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.MassaRequestProgressString);
            }
        }

        /// <summary>
        /// Состояние запроса к весам.
        /// </summary>
        private void CallbackMassaManagerResponseSetAll()
        {
            if (_sessionState.TaskManager.MassaManager.ResponseParseSet == null)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaSet, "Команда для весов: ...");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaSetCrc, "CRC: ...");
            }
            else
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaSet,
                    _sessionState.TaskManager.MassaManager.ResponseParseSet.DtCreated.ToString(@"HH:mm:ss") + "  Команда для весов: " +
                    _sessionState.TaskManager.MassaManager.ResponseParseSet.Message);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaSetCrc, "CRC: " +
                    (_sessionState.TaskManager.MassaManager.ResponseParseSet.IsValidAll 
                    ? $"верна {_sessionState.TaskManager.MassaResponseProgressString}" : $"ошибка! {_sessionState.TaskManager.MassaResponseProgressString}"));
                _sessionState.TaskManager.MassaResponseProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.MassaResponseProgressString);
            }
        }

        /// <summary>
        /// Пакетов в очереди.
        /// </summary>
        private void CallbackMassaManagerRequestQueue()
        {
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaQueries, 
                $"Пакетов в очереди: {_sessionState.TaskManager.MassaManager.RequestQueue.Count} {_sessionState.TaskManager.MassaQueriesProgressString}");
            _sessionState.TaskManager.MassaQueriesProgressString = StringUtils.GetProgressString(_sessionState.TaskManager.MassaQueriesProgressString);
        }

        #endregion

        #region Private methods

        private void FieldPrintManager_DoubleClick(object sender, EventArgs e)
        {
            CustomMessageBox messageBox = CustomMessageBox.Show(this,
                $"Принтер: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.Name}" + Environment.NewLine +
                $"Драйвер: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.DriverName}" + Environment.NewLine +
                $"Порт: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.PortName}" + Environment.NewLine +
                $"Код состояния: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.PrinterState}" + Environment.NewLine +
                $"Код статуса: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.PrinterStatus}" + Environment.NewLine +
                $"Статус: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.PrinterStatusDescription}" + Environment.NewLine +
                $"Состояние (ENG): {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.Status}" + Environment.NewLine +
                $"Состояние: {_sessionState.TaskManager.PrintManager.TscPrintControl.Win32Printer.StatusDescription}" + Environment.NewLine,
                LocalizationData.ScalesUI.PrinterInfoCaption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            messageBox.Wait();
        }

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                if (_debug.IsDebug)
                {
                    OpenFormSettings();
                }
                else
                {
                    PasswordForm pinForm = new();
                    if (pinForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenFormSettings();
                    }
                    pinForm.Close();
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void OpenFormSettings()
        {
            using SettingsForm settingsForm = new();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void ButtonScalesInit_Click(object sender, EventArgs e)
        {
            try
            {
                //if (_sessionState.TaskManager.MassaManagerIsExit)
                if (_sessionState.TaskManager.MassaManager.IsExecuteResponse)
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaNotQuering, 
                        LocalizationData.ScalesUI.OperationControl, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    return;
                }

                if (_sessionState.TaskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold ||
                    _sessionState.TaskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaCheck, 
                        LocalizationData.ScalesUI.OperationControl, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    if (messageBox.Result != DialogResult.Yes)
                        return;
                }

                _sessionState.TaskManager.MassaManager.GetInit();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        private void ButtonSelectPlu_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                // Weight check.
                //if (_sessionState.TaskManager.MassaManager != null)
                {
                    if (_sessionState.TaskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold || 
                        _sessionState.TaskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
                    {
                        CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaCheck, LocalizationData.ScalesUI.OperationControl,
                            MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        messageBox.Wait();
                        if (messageBox.Result != DialogResult.Yes)
                            return;
                    }
                }

                // PLU form.
                using PluListForm pluListForm = new() { Owner = this };
                if (pluListForm.ShowDialog() == DialogResult.OK)
                {
                    _sessionState.Kneading = 1;
                    _sessionState.ProductDate = DateTime.Now;
                    _sessionState.NewPallet();
                    //_mkDevice.SetTareWeight((int) (_sessionState.CurrentPLU.GoodsTareWeight * _sessionState.CurrentPLU.Scale.ScaleFactor));

                    // сразу перейдем к форме с замесами, размерами паллет и прочее
                    ButtonSetKneading_Click(null, null);
                }
                else if (_sessionState.CurrentPlu != null)
                {
                    _sessionState.CurrentPlu = null;
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void ButtonSelectOrder_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                if (_sessionState.CurrentOrder == null)
                {
                    using OrderListForm settingsForm = new();
                    if (settingsForm.ShowDialog() == DialogResult.OK)
                    {

                    }
                }
                else
                {
                    using OrderDetailForm settingsForm = new();
                    DialogResult dialogResult = settingsForm.ShowDialog();

                    if (dialogResult == DialogResult.Retry)
                    {
                        _sessionState.CurrentOrder = null;
                    }

                    if (dialogResult == DialogResult.OK)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }

                    if (dialogResult == DialogResult.Cancel)
                    {
                        //ws.Kneading = (int)settingsForm.currentKneading;
                    }
                }
                if (_sessionState.CurrentOrder != null)
                {
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMaximum(fieldCountBox, _sessionState.CurrentOrder.PlaneBoxCount);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetMinimum(fieldCountBox, 0);
                    MDSoft.WinFormsUtils.InvokeProgressBar.SetValue(fieldCountBox, _sessionState.CurrentOrder.FactBoxCount);
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void ButtonSetKneading_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                using SetKneadingNumberForm kneadingNumberForm = new()
                { Owner = this };
                if (kneadingNumberForm.ShowDialog() == DialogResult.OK)
                {
                    //_sessionState.Kneading = settingsForm.CurrentKneading;
                    //_sessionState.ProductDate = settingsForm.CurrentProductDate;
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                _sessionState.PrintLabel(Owner);
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void PictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FieldResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (fieldResolution.SelectedIndex)
                {
                    // 1024х768
                    case 1:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1024, 768);
                        break;
                    // 1366х768
                    case 2:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1366, 768);
                        break;
                    // 1920х1080
                    case 3:
                        WindowState = FormWindowState.Normal;
                        Size = new Size(1920, 1080);
                        break;
                    // Максимальное
                    default:
                        WindowState = FormWindowState.Maximized;
                        break;
                }
                CenterToScreen();
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        private void FieldCurrentTime_Click(object sender, EventArgs e)
        {
            _isShowInfoLabels = !_isShowInfoLabels;
            // MemoryManager.
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryManagerTotal, _isShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMemoryProgress, _isShowInfoLabels);
            // PrintManager.
            // MassaManager.
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaScalePar, _isShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaGet, _isShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaGetCrc, _isShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaSet, _isShowInfoLabels);
            MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldMassaSetCrc, _isShowInfoLabels);
        }

        private void FieldDt_DoubleClick(object sender, EventArgs e)
        {
            ServiceMessagesWindow.BuildServiceMessagesWindow(this);
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (Equals(e.Button, MouseButtons.Middle))
            {
                ButtonPrint_Click(sender, e);
            }
        }

        private void ButtonAddKneading_Click(object sender, EventArgs e)
        {
            //_sessionState.RotateKneading(Direction.forward);
            using NumberInputForm numberInputForm = new()
            { InputValue = 0 };
            // _sessionState.Kneading;
            if (numberInputForm.ShowDialog() == DialogResult.OK)
            {
                _sessionState.Kneading = numberInputForm.InputValue;
            }
        }

        private void ButtonNewPallet_Click(object sender, EventArgs e)
        {
            _sessionState.NewPallet();
        }

        private void FieldTitle_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                using WpfPageLoader wpfPageLoader = new(ProjectsEnums.Page.SqlSettings, false) { Width = 400, Height = 400 };
                wpfPageLoader.ShowDialog(this);
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        private void ButtonRunScalesTerminal_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = @"C:\Program Files (x86)\Massa-K\ScalesTerminal 100\ScalesTerminal.exe";
                if (File.Exists(fileName))
                {
                    _sessionState.TaskManager.Close();
                    _sessionState.TaskManager.ClosePrintManager();
                    Application.DoEvents();
                    _proc.Run(fileName, string.Empty, false, ProcessWindowStyle.Normal, true);
                }
                else
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.ProgramNotFound(fileName), 
                        LocalizationData.ScalesUI.Exception, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    messageBox.Wait();
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        #endregion

        #region Public and private methods - Share

#pragma warning disable IDE0051 // Remove unused private members
        private void TemplateJobWithTaskManager()
#pragma warning restore IDE0051 // Remove unused private members
        {
            try
            {
                _sessionState.TaskManager.Close();
                _sessionState.TaskManager.ClosePrintManager();

                // .. methods
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManagerOpen();
            }
        }

        #endregion
    }
}
