﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataProjectsCore;
using DataProjectsCore.Helpers;
using DataProjectsCore.Utils;
using DataShareCore;
using DataShareCore.Helpers;
using DataShareCore.Schedulers;
using System;
using System.Drawing;
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
        private readonly ExceptionHelper _exception = ExceptionHelper.Instance;
        private readonly SessionStateHelper _sessionState = SessionStateHelper.Instance;
        private readonly LogHelper _log = LogHelper.Instance;
        private readonly QuartzHelper _quartz = QuartzHelper.Instance;

        #endregion

        #region MainForm methods

        public MainForm()
        {
            InitializeComponent();

            FormBorderStyle = _sessionState.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !_sessionState.IsDebug;
            fieldResolution.Visible = _sessionState.IsDebug;
            fieldResolution.SelectedIndex = _sessionState.IsDebug ? 1 : 0;
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

                _log.Information("The program is runned");

                _quartz.AddJob(QuartzUtils.CronExpression.EveryDay(), delegate { ScheduleIsNextDay(); });
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                _sessionState.TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
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

                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, string.Empty);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, string.Empty);
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
                if (_sessionState.IsDebug)
                {
                    isClose = true;
                }
                else
                {
                    using PasswordForm pinForm = new() { TopMost = !_sessionState.IsDebug };
                    isClose = pinForm.ShowDialog() == DialogResult.OK;
                    pinForm.Close();
                }
                if (isClose)
                {
                    TaskManager.Close();
                    TaskManager.ClosePrintManager();
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
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
            }
        }

        #endregion

        #region Public and private methods - Schedulers

        private void ScheduleIsNextDay()
        {
            _log.Information("ScheduleIsNextDay");
        }

        #endregion

        #region Public and private methods - Callbacks

        private void CheckEnabledManager(ProjectsEnums.TaskType taskType, Control fieldManager)
        {
            if (_sessionState.SqlViewModel.IsTaskEnabled(taskType))
            {
                if (!fieldManager.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldManager, true);
            }
            else
            {
                if (fieldManager.Visible)
                    MDSoft.WinFormsUtils.InvokeControl.SetVisible(fieldManager, false);
            }
        }

        private void CallbackDeviceManager()
        {
            //if (_sessionState.ProductDate.Date < DateTime.Now.Date && !_sessionState.IsChangedProductDate)
            //    _sessionState.ProductDate = DateTime.Now;

            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldProductDate, $"{_sessionState.ProductDate:dd.MM.yyyy}");
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldKneading, $"{_sessionState.Kneading}");

            if (!Equals(buttonPrint.Enabled, _sessionState.CurrentPlu != null))
                MDSoft.WinFormsUtils.InvokeControl.SetEnabled(buttonPrint, _sessionState.CurrentPlu != null);

            string strCheckWeight = _sessionState.CurrentPlu?.CheckWeight == true ? "вес" : "шт";
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPlu, _sessionState.CurrentPlu != null
                ? $"{_sessionState.CurrentPlu.PLU} | {strCheckWeight} | {_sessionState.CurrentPlu.GoodsName}" : string.Empty);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightTare, _sessionState.CurrentPlu != null
                ? $"{_sessionState.CurrentPlu.GoodsTareWeight:0.000} кг" : "0,000 кг");
        }

        private void CallbackMemoryManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MemoryManager, fieldMemoryManager);
            if (_sessionState.SqlViewModel.IsTaskEnabled(ProjectsEnums.TaskType.MemoryManager))
            {
                char ch = StringUtils.GetProgressChar(TaskManager.MemoryManagerProgressChar);
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMemoryManager,
                    $"Использовано памяти: {TaskManager.MemoryManager.MemorySize.Physical.MegaBytes:N0} MB | {ch}");
                TaskManager.MemoryManagerProgressChar = ch;
            }
        }

        private void CallbackPrintManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldPrintManager);
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, labelLabelsTitle);
            CheckEnabledManager(ProjectsEnums.TaskType.PrintManager, fieldLabelsCount);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldLabelsCount, $"{_sessionState.LabelsCurrent}/{_sessionState.LabelsCount}");

            // надо переприсвоить т.к. на CurrentBox сделан Notify чтоб выводить на экран
            _sessionState.LabelsCurrent = TaskManager.PrintManager.UserLabelCount < _sessionState.LabelsCount ? TaskManager.PrintManager.UserLabelCount : _sessionState.LabelsCount;
            // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
            if (_sessionState.LabelsCurrent == 0)
                _sessionState.LabelsCurrent = 1;

            char ch = StringUtils.GetProgressChar(TaskManager.PrintManagerProgressChar);
            // TSC printers.
            if (_sessionState.CurrentScale?.ZebraPrinter != null && _sessionState.IsTscPrinter)
            {
                if (TaskManager.PrintManager != null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, TaskManager.PrintManager.TscPrintControl.IsOpen
                        ? $"Принтер: доступен | {ch}" : $"Принтер: недоступен | {ch}");
                }
            }
            // Zebra printers.
            else
            {
                _sessionState.LabelsCurrent = TaskManager.PrintManager.UserLabelCount < _sessionState.LabelsCount ? TaskManager.PrintManager.UserLabelCount : _sessionState.LabelsCount;
                // а когда зебра поддергивает ленту то счетчик увеличивается на 1 не может быть что-бы напечатано 3, а на форме 4
                if (_sessionState.LabelsCurrent == 0)
                    _sessionState.LabelsCurrent = 1;
                if (TaskManager.PrintManager.CurrentStatus != null)
                {
                    MDSoft.WinFormsUtils.InvokeControl.SetText(fieldPrintManager, TaskManager.PrintManager.CurrentStatus.isReadyToPrint
                        ? $"Принтер: доступен | {TaskManager.PrintManager.TscPrintControl.IpAddress} | {ch}" : $"Принтер: недоступен | {ch}");
                }
            }
            TaskManager.PrintManagerProgressChar = ch;
        }

        private void CallbackMassaManager()
        {
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, fieldMassaManager);
            CheckEnabledManager(ProjectsEnums.TaskType.MassaManager, buttonSetZero);
            bool flag = false;
            if (_sessionState.CurrentPlu != null)
            {
                flag = true;
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, _sessionState.CurrentPlu.CheckWeight == false
                    ? $"PLU (шт): {_sessionState.CurrentPlu.PLU}"
                    : $"PLU (вес): {_sessionState.CurrentPlu.PLU}");
                decimal weight = TaskManager.MassaManager.WeightNet - _sessionState.CurrentPlu.GoodsTareWeight;
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, $"{weight:0.000} кг");
                //await MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto,
                //    _sessionState.MassaManager.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent).ConfigureAwait(false);
                //MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightTare, 
                //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
            }

            //LedMassa.State = _sessionState.MassaManager.IsStable == 1;
            char ch = StringUtils.GetProgressChar(TaskManager.MassaManagerProgressChar);
            MDSoft.WinFormsUtils.InvokeControl.SetText(fieldMassaManager, TaskManager.MassaManager.IsReady || TaskManager.MassaManager.IsStable == 1
                ? $"Весы: доступны | Вес брутто: { TaskManager.MassaManager.WeightNet:0.000} кг | {ch}"
                : $"Весы: недоступны | Вес брутто: { TaskManager.MassaManager.WeightNet:0.000} кг | {ch}");
            TaskManager.MassaManagerProgressChar = ch;
            if (!flag)
            {
                MDSoft.WinFormsUtils.InvokeControl.SetText(labelPlu, "PLU");
                MDSoft.WinFormsUtils.InvokeControl.SetText(fieldWeightNetto, "0,000 кг");
                //await MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto, Color.Transparent).ConfigureAwait(false);
            }
        }

        //private void NotifyMassa(MassaManagerEntity message)
        //{
        //    var flag = false;
        //    if (message != null)
        //    {
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldGrossWeight, $"Вес брутто: {message.WeightNet:0.000} кг");
        //        if (_sessionState.CurrentPlu != null)
        //        {
        //            flag = true;
        //            MDSoft.WinFormsUtils.InvokeControl.SetText.Async(labelPlu, _sessionState.CurrentPlu.CheckWeight == false
        //                ? $"PLU (шт): {_sessionState.CurrentPlu.PLU}" : $"PLU (вес): {_sessionState.CurrentPlu.PLU}");
        //            var weight = message.WeightNet - _sessionState.CurrentPlu.GoodsTareWeight;
        //            MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг");
        //            MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto,
        //                message.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent);
        //            //MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightTare, 
        //            //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
        //        }
        //        if (message.IsReady)
        //            LedMassa.State = message.IsStable == 1;
        //    }
        //    if (!flag)
        //    {
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(labelPlu, "PLU");
        //        MDSoft.WinFormsUtils.InvokeControl.SetText.Async(fieldWeightNetto, "0,000 кг");
        //        MDSoft.WinFormsUtils.InvokeControl.SetBackColor.Async(fieldWeightNetto, Color.Transparent);
        //    }
        //}

        #endregion

        #region Private methods

        private void ButtonSettings_Click(object sender, EventArgs e)
        {
            try
            {
                TaskManager.Close();
                TaskManager.ClosePrintManager();

                if (_sessionState.IsDebug)
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
                TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        private void OpenFormSettings()
        {
            using SettingsForm settingsForm = new();
            if (settingsForm.ShowDialog() == DialogResult.OK)
            {
            }
        }

        private void ButtonSetZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (TaskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold || TaskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
                {
                    CustomMessageBox messageBox = CustomMessageBox.Show(this, LocalizationData.ScalesUI.MassaCheck, LocalizationData.ScalesUI.OperationControl,
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    messageBox.Wait();
                    if (messageBox.Result != DialogResult.Yes)
                        return;
                }

                TaskManager.MassaManager.SetZero();
                _sessionState.CurrentPlu = null;
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
                TaskManager.Close();
                TaskManager.ClosePrintManager();

                // Weight check.
                if (TaskManager.MassaManager != null)
                {
                    if (TaskManager.MassaManager.WeightNet > LocalizationData.ScalesUI.MassaThreshold || TaskManager.MassaManager.WeightNet < -LocalizationData.ScalesUI.MassaThreshold)
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
                // Commented from 2021-03-05.
                //buttonSetZero_Click(sender, e);
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
                TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        private void ButtonSelectOrder_Click(object sender, EventArgs e)
        {
            try
            {
                TaskManager.Close();
                TaskManager.ClosePrintManager();

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
                    fieldCountBox.Maximum = _sessionState.CurrentOrder.PlaneBoxCount;
                    fieldCountBox.Minimum = 0;
                    fieldCountBox.Value = _sessionState.CurrentOrder.FactBoxCount;
                }
            }
            catch (Exception ex)
            {
                _exception.Catch(this, ref ex);
            }
            finally
            {
                MDSoft.WinFormsUtils.InvokeControl.Select(buttonPrint);
                TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        private void ButtonSetKneading_Click(object sender, EventArgs e)
        {
            try
            {
                TaskManager.Close();
                TaskManager.ClosePrintManager();

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
                TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        private void ButtonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                TaskManager.OpenPrintManager(CallbackPrintManager, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
                _sessionState.PrintLabel(Owner);
                TaskManager.ClosePrintManager();
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

        private void fieldTitle_DoubleClick(object sender, EventArgs e)
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
                _sessionState.TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        #endregion

        #region Public and private methods - Share

        private void TemplateJobWithTaskManager()
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
                _sessionState.TaskManager.Open(CallbackDeviceManager, CallbackMemoryManager, CallbackMassaManager,
                    ButtonSetZero_Click, _sessionState.SqlViewModel, _sessionState.IsTscPrinter, _sessionState.CurrentScale);
            }
        }

        #endregion
    }
}
