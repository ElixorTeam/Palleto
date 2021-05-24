﻿using EntitiesLib;
using Hardware.MassaK;
using Hardware.Print;
using Hardware.Zpl;
using ScalesUI.Forms;
using ScalesUI.Utils;
using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using UICommon;
using ZabbixAgentLib;

namespace ScalesUI.Common
{
    public class SessionState
    {
        #region Design pattern "Lazy Singleton"

        private static SessionState _instance;
        public static SessionState Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Constructor and destructor

        public SessionState()
        {
            //var sql = SqlConnectFactory.GetConnection(Properties.Settings.Default.ConnectionString);
            ProductDate = DateTime.Now;

            //тут загружается ID моноблока из файла токена,
            //а затем загружается сама линия
            //--->
            Host = new HostEntity();
            Host.TokenRead();
            CurrentScale = new ScaleEntity(Host.CurrentScaleId);
            CurrentScale.Load();

            //this.CurrentScaleId = Properties.Settings.Default.CurrentScaleId;
            //this.CurrentScale = new ScaleEntity(this.CurrentScaleId);
            //<---

            // Запустить http-прослушиватель.
            StartHttpListener();

            Kneading = KneadingMinValue;
            ProductDate = DateTime.Now;
            CurrentBox = 1;
            PalletSize = 60;

            // контейнер пока не используем
            // оставим для бурного роста
            // ZebraDeviceСontainer = ZebraDeviceСontainer.Instance;
            // ZebraDeviceСontainer.AddDevice(this.CurrentScale.ZebraIP, this.CurrentScale.ZebraPort);
            // ZebraDeviceСontainer.CheckDeviceStatusOn();
            // создаем устройство ZEBRA
            // с необходимым крннектором (т.е. TCP, а можно и через USB)
            // WeightServices.Common.Zpl.DeviceSocketTcp zplDeviceSocket =
            //    new WeightServices.Common.Zpl.DeviceSocketTcp(this.CurrentScale.ZebraPrinter.Ip, this.CurrentScale.ZebraPrinter.Port);
            // ZebraDeviceEntity = new ZebraDeviceEntity(zplDeviceSocket, Guid.NewGuid());
            // ZebraDeviceEntity.DataCollector.SetIpPort(zplDeviceSocket.DeviceIP, zplDeviceSocket.DevicePort);
            // тут запускается поток 
            // который разбирает очередь 
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доотправляются на устройство
            // zebraDeviceEntity.CheckDeviceStatusOn();
            // тут запускается процесс отправляющий комманды проверки состояния устройства
            // ZplCommander = new ZplCommander(zplDeviceSocket.DeviceIP, zebraDeviceEntity, ZplPipeUtils.ZplHostQuery());

            try
            {
                PrintDevice = new PrintEntity(CurrentScale.ZebraPrinter.Ip, CurrentScale.ZebraPrinter.Port, 120);
                PrintDevice.Open(CurrentScale.ZebraPrinter.PrinterType);
            }
            catch (Exception ex)
            {
                if (CustomMessageBox.Show($"Печатающее устройство недоступно ({CurrentScale.ZebraPrinter}). {ex.Message}") == DialogResult.OK)
                {

                }
                throw new Exception(ex.Message);
            }


            // тут создается устройство работы с MassaK
            // запускаем поток, который разбирает очередь команд
            // т.к. команды пишутся не напрямую, а в очередь
            // а из нее потом доотправляются на устройство
            DeviceSocketRs232 deviceSocketRs232 = new DeviceSocketRs232(CurrentScale.DeviceComPort);
            MkDevice = new MkDeviceEntity(deviceSocketRs232);
            MkDevice.SetZero();

            // тут запускается процесс отправляющий комманды
            // для получения с устройства текущего веса
            MkCommander mkCommander = new MkCommander(MkDevice);

            // начинается новыя серия
            // упаковки продукции 
            // новая паллета, если хотите
            ProductSeries = new ProductSeriesEntity(CurrentScale);
            ProductSeries.New();

        }

        ~SessionState()
        {
            StopHttpListener();
            ZplCommander.Close();
        }

        #endregion

        #region Public and private fields and properties

        private readonly LogHelper _log = LogHelper.Instance;
        public string AppVersion => UtilsAppVersion.GetMainFormText(Assembly.GetExecutingAssembly());

        public ProductSeriesEntity ProductSeries { get; private set; }

        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public HostEntity Host { get; private set; }

        public ZplCommander ZplCommander { get; private set; }

        public PrintEntity PrintDevice { get; }

        public MkDeviceEntity MkDevice { get; }

        public ZabbixHttpListener HttpListener { get; private set; }
        private CancellationToken _token;
        private CancellationToken _tokenHttpListener;
        private ThreadChecker _threadChecker;
        public int CurrentScaleId { get; private set; }

        public OrderEntity CurrentOrder { get; set; }

        [XmlElement(IsNullable = true)]
        public ScaleEntity CurrentScale { get; set; }

        [XmlElement(IsNullable = true)]
        public WeighingFactEntity CurrentWeighingFact { get; set; }
        
        #endregion

        #region PalletSize
        
        public static readonly int PalletSizeMinValue = 1;
        public static readonly int PalletSizeMaxValue = 130;

        public delegate void OnResponseHandlerPalletSize(int palletSize);
        public event OnResponseHandlerPalletSize NotifyPalletSize;

        private int _palletSize;
        public int PalletSize { 
            get => _palletSize;
            set 
            {
                _palletSize = value;
                CurrentBox = 1;
                NotifyPalletSize?.Invoke(value);

            } 
        }

        public void RotatePalletSize(Direction direction)
        {
            if (direction == Direction.Back)
            {
                PalletSize--;
                if (PalletSize < PalletSizeMinValue)
                    PalletSize = PalletSizeMinValue;

            }
            if (direction == Direction.Forward)
            {
                PalletSize++;
                if (PalletSize > PalletSizeMaxValue)
                    PalletSize = PalletSizeMaxValue;
            }
        }

        #endregion

        #region CurrentBox

        public delegate void OnResponseHandlerCurrentBox(int currentBox);
        public event OnResponseHandlerCurrentBox NotifyCurrentBox;
        
        private int _currentBox;
        public int CurrentBox
        {
            get => _currentBox;
            set
            {
                _currentBox = value;
                NotifyCurrentBox?.Invoke(value);
            }
        }

        public void NewPallet()
        {
            CurrentBox = 1;
            //если новая паллета - чистим очередь печати
            if (PrintDevice != null)
            {
                PrintDevice.ClearPrintBuffer(CurrentScale.ZebraPrinter.PrinterType);
                PrintDevice.SetOdometorUserLabel(1);
                ProductSeries.New();

            }
        }
        #endregion

        #region Kneading
        public static readonly int KneadingMinValue = 1;
        public static readonly int KneadingMaxValue = 140;

        public delegate void OnResponseHandlerKneading(int kneading);
        public event OnResponseHandlerKneading NotifyKneading;
        private int _kneading;

        public int Kneading
        {
            get => _kneading;
            set
            {
                //если замес изменился - чистим очередь печати
                if (PrintDevice != null)
                {
                    PrintDevice.ClearPrintBuffer(CurrentScale.ZebraPrinter.PrinterType);
                    PrintDevice.SetOdometorUserLabel(CurrentBox);
                }
                _kneading = value;
                NotifyKneading?.Invoke(value);
            }
        }

        public void RotateKneading(Direction direction)
        {
            if (direction == Direction.Back)
            {
                Kneading--;
                if (Kneading < KneadingMinValue)
                    Kneading = KneadingMinValue;

            }
            if (direction == Direction.Forward)
            {
                Kneading++;
                if (Kneading > KneadingMaxValue)
                    Kneading = KneadingMaxValue;

            }
        }
        #endregion

        #region ProductDate

        public static readonly DateTime ProductDateMaxValue = DateTime.Now.AddDays(+7);
        public static readonly DateTime ProductDateMinValue = DateTime.Now.AddDays(-1);

        public delegate void OnResponseHandlerProductDate(DateTime productDate);
        public event OnResponseHandlerProductDate NotifyProductDate;
        private DateTime _productDate;

        public DateTime ProductDate
        {
            get => _productDate;
            set
            {
                //если дата изменилась - чистим очередь печати
                if (PrintDevice != null)
                    PrintDevice.ClearPrintBuffer(CurrentScale.ZebraPrinter.PrinterType);
                _productDate = value;
                NotifyProductDate?.Invoke(value);
            }
        }

        public void RotateProductDate(Direction direction)
        {
            if (direction == Direction.Back)
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;

            }
            if (direction == Direction.Forward)
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
            }
        }
        #endregion

        #region PluEntity
        public delegate void OnResponseHandlerPLU(PluEntity plu);
        public event OnResponseHandlerPLU NotifyPLU;
        private PluEntity _currentPlu;
        [XmlElement(IsNullable = true)]
        public PluEntity CurrentPlu
        {
            get => _currentPlu;
            set
            {
                // если ПЛУ изменился - чистим очередь печати
                PrintDevice?.ClearPrintBuffer(CurrentScale.ZebraPrinter.PrinterType);
                PrintDevice?.SetOdometorUserLabel(1);
                _currentPlu = value;
                CurrentBox = 1;
                NotifyPLU?.Invoke(value);
            }
        }

        #endregion

        #region PrintMethods

        public void ProcessWeighingResult()
        {
            CurrentWeighingFact = null;
            TemplateEntity template = null;
            if (CurrentOrder != null && CurrentScale != null && CurrentScale.UseOrder)
            {
                template = CurrentOrder.Template;
                CurrentOrder.FactBoxCount++;
            }
            else if (CurrentPlu != null && CurrentScale != null && !CurrentScale.UseOrder)
            {
                template = CurrentPlu.Template;
            }

            if (template != null && CurrentPlu != null)
            {
                if (CurrentPlu.CheckWeight == false)
                {
                    // если печатать надо МНОГО!!! маленьких этикеток 
                    // и при этом правильный вес не нужен
                    PrintCountLabel(template);
                }
                else if (CurrentPlu.CheckWeight == true)
                {
                    // если необходимо опрашивать платформу 
                    // для КАЖДОЙ!!! коробки отдельно
                    // и при этом получать правильный вес
                    PrintWeightLabel(template);
                }
            }
        }

        /// <summary>
        /// Подменить картинки ZPL.
        /// </summary>
        /// <param name="value"></param>
        public void PrintCmdReplacePics(ref string value)
        {
            // Подменить картинки ZPL.
            if (CurrentScale.ZebraPrinter.PrinterType.Contains("TSC "))
            {
                var templateEac = new TemplateEntity("EAC_107x109_090");
                var templateFish = new TemplateEntity("FISH_94x115_000");
                var templateTemp6 = new TemplateEntity("TEMP6_116x113_090");
                value = value.Replace("[EAC_107x109_090]", templateEac.XslContent);
                value = value.Replace("[FISH_94x115_000]", templateFish.XslContent);
                value = value.Replace("[TEMP6_116x113_090]", templateTemp6.XslContent);
            }
        }

        /// <summary>
        /// Сохранить ZPL-запрос в таблицу [Labels].
        /// </summary>
        /// <param name="printCmd"></param>
        /// <param name="labelId"></param>
        public void PrintSaveLabel(ref string printCmd, int labelId)
        {
            var zplLabel = new ZplLabel
            {
                Content = printCmd,
                WeighingFactId = labelId,
            };
            zplLabel.Save();
        }

        /// <summary>
        /// Печать штучных этикеток.
        /// </summary>
        /// <param name="template"></param>
        private void PrintCountLabel(TemplateEntity template)
        {
            // Вывести серию этикеток по заданному размеру паллеты.
            for (var i = CurrentBox; i <= PalletSize; i++)
            {
                CurrentWeighingFact = WeighingFactEntity.New(
                    CurrentScale,
                    CurrentPlu,
                    ProductDate,
                    Kneading,
                    CurrentPlu.Scale.ScaleFactor,
                    CurrentPlu.NominalWeight,
                    CurrentPlu.GoodsTareWeight
                );

                // Печать этикетки.
                PrintLabel(template);
            }
        }

        /// <summary>
        /// Печать весовых этикеток.
        /// </summary>
        /// <param name="template"></param>
        private void PrintWeightLabel(TemplateEntity template)
        {
            // Проверка наличия устройства весов.
            if (MkDevice == null)
            {
                _log.Info($@"Устройство весов не обнаружено!");
                return;
            }
            // Проверка товара на весах.
            if (MkDevice.WeightNet - CurrentPlu.GoodsTareWeight <= 0)
            {
                _log.Info($@"Вес товара: {MkDevice.WeightNet} кг, печать этикетки невозможна!");
                return;
            }

            CurrentWeighingFact = WeighingFactEntity.New(
                CurrentScale,
                CurrentPlu,
                ProductDate,
                Kneading,
                CurrentPlu.Scale.ScaleFactor,
                MkDevice.WeightNet - CurrentPlu.GoodsTareWeight,
                CurrentPlu.GoodsTareWeight
            );
            
            // Печать этикетки.
            PrintLabel(template);
        }

        /// <summary>
        /// Печать этикетки.
        /// </summary>
        /// <param name="template"></param>
        private void PrintLabel(TemplateEntity template)
        {
            // Сохранить запись в таблице [WeithingFact].
            CurrentWeighingFact.Save();

            var xmlInput = CurrentWeighingFact.SerializeObject();
            var printCmd = ZplPipeUtils.XsltTransformationPipe(template.XslContent, xmlInput);
            
            // Подменить картинки ZPL.
            PrintCmdReplacePics(ref printCmd);
            // Отправить задание в очередь печати.
            PrintDevice.SendAsync(printCmd);
            // Сохранить ZPL-запрос в таблицу [Labels].
            PrintSaveLabel(ref printCmd, CurrentWeighingFact.Id);
        }

        #endregion

        #region Public and private methods - Http listener

        private void StartHttpListener()
        {
            _log.Info("Запистить http-listener. начало.");
            _log.Info("http://localhost:18086/status");
            try
            {
                var cancelTokenSource = new CancellationTokenSource();
                _token = cancelTokenSource.Token;
                _threadChecker = new ThreadChecker(_token, 2_500);
                // Подписка на событие.
                //_threadChecker.EventReloadValues += EventHttpListenerReloadValues;
                _tokenHttpListener = cancelTokenSource.Token;
                HttpListener = new ZabbixHttpListener(_tokenHttpListener, 10);
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            _log.Info("Запистить http-listener. Финиш.");
        }

        private void StopHttpListener()
        {
            _log.Info("Остановить http-listener. Начало.");
            try
            {
                HttpListener?.Stop();
                _token.ThrowIfCancellationRequested();
                _tokenHttpListener.ThrowIfCancellationRequested();
                _threadChecker.Stop();
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
            _log.Info("Остановить http-listener. Финиш.");
        }

        #endregion
    }
}
