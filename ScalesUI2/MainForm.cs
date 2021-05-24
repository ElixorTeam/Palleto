﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using log4net;
using ScalesUI.Common;
using ScalesUI.Forms;
using ScalesUI.Helpers;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using EntitiesLib;
using Hardware.MassaK;
using Hardware.Print;
using Hardware.Print.Zebra;
using UICommon;
using UICommon.WinForms.Utils;

// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo
// ReSharper disable IdentifierTypo

namespace ScalesUI
{
    public partial class MainForm : Form
    {
        #region Private fields and properties

        private readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        // Состояние устройства.
        private readonly SessionState _ws = SessionState.Instance;
        // Помощник мыши.
        private readonly MouseHookHelper _mouse = MouseHookHelper.Instance;

        private readonly Thread _dtThread;
        //private readonly MkDeviceEntity _mkDevice = MkDeviceEntity.Instance;
        private readonly decimal _threshold = 0.05M;

        private LightEmittingDiode _ledZebra;
        private LightEmittingDiode _ledMk;

        #endregion

        #region MainForm methods

        public MainForm()
        {
            InitializeComponent();

            _dtThread = new Thread(t =>
                {
                    while (true)
                    {
                        AsyncControl.Properties.SetText.Sync(lbCurrentTime, DateTime.Now.ToString(@"dd.MM.yyyy HH:mm:ss"));
                        Thread.Sleep(150);
                    }
                }
            )
            { IsBackground = true };
            _dtThread.Start();

            FormBorderStyle = _ws.IsDebug ? FormBorderStyle.FixedSingle : FormBorderStyle.None;
            TopMost = !_ws.IsDebug;
            fieldResolution.Visible = _ws.IsDebug;
            fieldResolution.SelectedIndex = _ws.IsDebug ? 1 : 0;
       }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (_ws.CurrentScale != null)
            {
                // _ws.CurrentScale.Load(_app.GuidToString());
                buttonSelectOrder.Visible = !(buttonSelectPlu.Visible = !_ws.CurrentScale.UseOrder);
            }

            //_mouse.Init(progressBarCountBox);

            // Загрузить ресурсы.
            LoadResources();

            InitLeds();

            // Подписка.
            _ws.MkDevice.Notify   += DisplayMessageMassa;
            _ws.NotifyProductDate += DisplayMessageProductDate;
            _ws.NotifyKneading += DisplayMessageKneading;
            _ws.NotifyPLU += DisplayMessagePlu;
            _ws.PrintDevice.Notify += DisplayMessageZebraState;
            _ws.NotifyPalletSize += DisplayMessagePalletSize;
            _ws.NotifyCurrentBox += DisplayMessageCurrentBox;
            _ws.NewPallet();
        }

        private void LoadResources()
        {
            try
            {
                //var bmpExit = new System.Drawing.Bitmap(Properties.Resources.exit_2);
                var resourceManager = new System.Resources.ResourceManager("ScalesUI.Properties.Resources", Assembly.GetExecutingAssembly());
                var exit = resourceManager.GetObject("exit_2");
                if (exit != null)
                {
                    var bmpExit = new Bitmap((System.Drawing.Bitmap)exit);
                    pictureBoxClose.Image = bmpExit;
                    //btnScaleOpt.Image = bmpSettings;
                    //btnScaleOpt.Text = string.Empty;
                }

                Text = _ws.AppVersion;
                fieldTitle.Text = $@"{_ws.AppVersion}.  {_ws.CurrentScale.Description}. SQL: {GetSqlInstance()}";
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка загрузки ресурсов!" + Environment.NewLine + ex.Message);
            }
        }

        public SqlCommand GetSqlInstanceCmd(SqlConnection con)
        {
            if (con == null)
                return null;
            var query = @"select serverproperty('InstanceName') [InstanceName]";
            var cmd = new SqlCommand(query, con);
            cmd.Prepare();
            return cmd;
        }

        public string GetSqlInstance()
        {
            var result = string.Empty;
            using (var con = SqlConnectFactory.GetConnection())
            {
                con.Open();
                var cmd = GetSqlInstanceCmd(con);
                if (cmd != null)
                {
                    var reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            result = SqlConnectFactory.GetValue<string>(reader, "InstanceName");
                        }
                    }

                    reader.Close();
                }
            }
            return result;
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
            var isClose = false;
            if (_ws.IsDebug)
            {
                isClose = true;
            }
            else
            {
                using (var pinForm = new PasswordForm() { TopMost = !_ws.IsDebug })
                {
                    isClose = pinForm.ShowDialog() == DialogResult.OK;
                    pinForm.Close();
                }
            }

            Application.DoEvents();
            Thread.Sleep(100);

            if (isClose)
            {
                _mouse?.Close();
                _dtThread?.Abort();
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region Private methods - DisplayMessage

        private void DisplayMessageMassa(MkDeviceEntity message)
        {
            var flag = false;
            if (message != null)
            {
                AsyncControl.Properties.SetText.Async(fieldGrossWeight, $"Вес брутто: {message.WeightNet:0.000} кг");
                if (_ws.CurrentPlu != null)
                {
                    flag = true;
                    AsyncControl.Properties.SetText.Async(labelPlu, _ws.CurrentPlu.CheckWeight == false
                        ? $"PLU (шт): {_ws.CurrentPlu.PLU}" : $"PLU (вес): {_ws.CurrentPlu.PLU}");
                    var weight = message.WeightNet - _ws.CurrentPlu.GoodsTareWeight;
                    AsyncControl.Properties.SetText.Async(fieldWeightNetto, $"{weight:0.000} кг");
                    AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto,
                        message.IsStable == 0x01 ? Color.FromArgb(150, 255, 150) : Color.Transparent);
                    //AsyncControl.Properties.SetText.Async(fieldWeightTare, 
                    //    $"{(float)getMassa.Tare / getMassa.ScaleFactor:0.000} кг");
                }
                if (message.IsReady)
                    _ledMk.State = message.IsStable == 1;
            }
            if (!flag)
            {
                AsyncControl.Properties.SetText.Async(labelPlu, "PLU");
                AsyncControl.Properties.SetText.Async(fieldWeightNetto, "0,000 кг");
                AsyncControl.Properties.SetBackColor.Async(fieldWeightNetto, Color.Transparent);
            }
        }
        
        private void DisplayMessageProductDate(DateTime productDate)
        {
            AsyncControl.Properties.SetText.Async(fieldProductDate, $"{productDate.ToString("dd.MM.yyyy")}");
        }

        private void DisplayMessageKneading(int kneading)
        {
            AsyncControl.Properties.SetText.Async(fieldKneading, $"{kneading}");
        }

        private void DisplayMessagePalletSize(int palletSize)
        {
            AsyncControl.Properties.SetText.Async(fieldPalletSize, $"{_ws.CurrentBox}/{_ws.PalletSize}");
        }
        private void DisplayMessageCurrentBox(int currentBox)
        {
            AsyncControl.Properties.SetText.Async(fieldPalletSize, $"{_ws.CurrentBox}/{_ws.PalletSize}");
        }


        private void DisplayMessagePlu(PluEntity plu)
        {
            AsyncControl.Properties.SetText.Async(fieldPlu, $"{plu?.GoodsName}");
            AsyncControl.Properties.SetEnabled.Async(buttonPrint, plu != null);
            AsyncControl.Properties.SetText.Async(fieldWeightTare, plu != null ? $"{plu.GoodsTareWeight:0.000} кг" : "0,000 кг");
            _log.Info($"Смена PLU: {plu?.GoodsName}");
            _log.Debug($"PLU.GoodsTareWeight: {plu?.GoodsTareWeight}");
        }

        private void DisplayMessageZebraState(PrintEntity zebraPrinter)
        {
            var state = zebraPrinter.CurrentStatus;

            // надо переприсвоить
            // т.к. на CurrentBox сделан Notify 
            // чтоб выводить на экран
            _ws.CurrentBox = _ws.PrintDevice.UserLabelCount < _ws.PalletSize ? _ws.PrintDevice.UserLabelCount : _ws.PalletSize;
            // а когда зебра поддергивает ленту
            // то счетчик увеличивается на 1
            // не может быть что-бы напечатано 3, а на форме 4
            if (_ws.CurrentBox == 0)
                _ws.CurrentBox = 1;

            if (state != null && !state.isReadyToPrint)  
                AsyncControl.Properties.SetBackColor.Async(buttonPrint, state.isReadyToPrint ? Color.FromArgb(192, 255, 192) : Color.Transparent);

            if (state != null) 
                _ledZebra.State = state.isReadyToPrint;
        }

        #endregion

        #region Private methods

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ws.IsDebug)
                {
                    OpenFormSettings();
                }
                else
                {
                    var pinForm = new PasswordForm();
                    if (pinForm.ShowDialog() == DialogResult.OK)
                    {
                        OpenFormSettings();
                    }
                    pinForm.Close();
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка формы настроек!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void OpenFormSettings()
        {
            using (var settingsForm = new SettingsForm())
            {
                if (settingsForm.ShowDialog() == DialogResult.OK)
                {
                }
            }
        }

        private void buttonSetZero_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ws.MkDevice.WeightNet > _threshold || _ws.MkDevice.WeightNet < -_threshold)
                {
                    if (CustomMessageBox.Show(@"Разгрузите весовую платформу!" + Environment.NewLine +
                                              $@"Пороговое значение: {_threshold}кг" + Environment.NewLine +
                                              @"Yes - Игнорировать, No - Разгрузить", @"Контроль операций",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                }

                _ws.MkDevice.SetZero();
                _ws.CurrentPlu = null;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка задания 0!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void buttonSetWeightTare_Click(object sender, EventArgs e)
        {
            //_ws.WeightTare = _ws.WeightReal;
            //_weightPlatformControl.SetTareWeight(_ws);
        }

        private void buttonSelectPlu_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ws.MkDevice.WeightNet > _threshold || _ws.MkDevice.WeightNet < -_threshold)
                {
                    if (CustomMessageBox.Show(@"Разгрузите весовую платформу!" + Environment.NewLine +
                                              $@"Пороговое значение: {_threshold}кг" + Environment.NewLine +
                                              @"Yes - Игнорировать, No - Разгрузить", @"Контроль операций", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        return;
                }
                using (var pluListForm = new PluListForm() )
                {
                    pluListForm.Owner = this;
                    // Комментировано 2021-03-05.
                    //buttonSetZero_Click(sender, e);
                    if (pluListForm.ShowDialog() == DialogResult.OK)
                    {
                        _ws.Kneading = 1;
                        _ws.ProductDate = DateTime.Now;
                        _ws.NewPallet();
                        //_mkDevice.SetTareWeight((int) (_ws.CurrentPLU.GoodsTareWeight * _ws.CurrentPLU.Scale.ScaleFactor));

                        // сразу перейдем к форме с замесами, размерами паллет и прочее
                        buttonSetKneading_Click(sender, e);
                    }
                    else
                    {
                        _ws.CurrentPlu = null;
                    }
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка формы выбора PLU!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void buttonSelectOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ws.CurrentOrder == null)
                {
                    using (OrderListForm settingsForm = new OrderListForm())
                    {
                        if (settingsForm.ShowDialog() == DialogResult.OK)
                        {

                        }
                    }
                }
                else
                {
                    using (OrderDetailForm settingsForm = new OrderDetailForm())
                    {
                        var dialogResult = settingsForm.ShowDialog();

                        if (dialogResult == DialogResult.Retry)
                        {
                            _ws.CurrentOrder = null;
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
                }
                if (_ws.CurrentOrder != null)
                {
                    progressBarCountBox.Maximum = _ws.CurrentOrder.PlaneBoxCount;
                    progressBarCountBox.Minimum = 0;
                    progressBarCountBox.Value = _ws.CurrentOrder.FactBoxCount;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка формы выбора заказа!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void buttonSetKneading_Click(object sender, EventArgs e)
        {
            try
            {
                using (var settingsForm = new SetKneadingNumberForm())
                {
                    settingsForm.Owner = this;

                    if (settingsForm.ShowDialog() == DialogResult.OK)
                    {
                        //_ws.Kneading = settingsForm.CurrentKneading;
                        //_ws.ProductDate = settingsForm.CurrentProductDate;
                    }
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка формы выбора замеса!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                _mouse.OnMouseEvent(sender, e);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(@"Ошибка вызова печати!" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonPrint.Select();
            }
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void fieldResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (fieldResolution.SelectedIndex)
                {
                    // 1024х768
                    case 1:
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1024, 768);
                        break;
                    // 1366х768
                    case 2:
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1366, 768);
                        break;
                    // 1920х1080
                    case 3:
                        WindowState = FormWindowState.Normal;
                        Size = new System.Drawing.Size(1920, 1080);
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
                CustomMessageBox.Show(@"Ошибка изменения разрешения формы!" + Environment.NewLine + ex.Message);
            }
        }

        private void fieldDt_DoubleClick(object sender, EventArgs e)
        {
            ServiceMessagesWindow.BuildServiceMessagesWindow(this);
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            //if (e.Button.Equals(MouseButtons.Middle))
            //{
            //    buttonPrint_Click(sender, e);
            //}
        }

        private void btAddKneading_Click(object sender, EventArgs e)
        {
            //_ws.RotateKneading(Direction.forward);
            var numberInputForm = new NumberInputForm();
            numberInputForm.InputValue = 0;// _ws.Kneading;
            if (numberInputForm.ShowDialog() == DialogResult.OK)
            {
                _ws.Kneading = numberInputForm.InputValue;
            }
        }

        private void btNewPallet_Click(object sender, EventArgs e)
        {
            _ws.NewPallet();
        }

        private void InitLeds()
        {
            if (_ledZebra == null)
                _ledZebra = new LightEmittingDiode(pnlLed1)
                {
                    ColorOn = Color.LightGreen,
                    Description = "ZEBRA",
                    CheckChangesTimeoutMsec = 5000,
                    CheckChanges = true,
                    State = false
                };

            if (_ledMk == null)
                _ledMk = new LightEmittingDiode(pnlLed2)
                {
                    ColorOn = Color.LightGreen,
                    Description = "MK",
                    CheckChangesTimeoutMsec = 5000,
                    CheckChanges = true,
                    State = false
                };
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            InitLeds();
        }

        #endregion
    }
}
