// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
#nullable enable

using System.Windows.Forms;
using WsStorageCore.TableScaleModels.Clips;

namespace WsLabelCore.Helpers;

/// <summary>
/// User session.
/// </summary>
public sealed class WsUserSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static WsUserSessionHelper _instance;
#pragma warning restore CS8618
    public static WsUserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    public WsSqlContextManagerHelper ContextManager => WsSqlContextManagerHelper.Instance;
    public WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    private WsSqlBarCodeHelper BarCode => WsSqlBarCodeHelper.Instance;
    public DebugHelper Debug => DebugHelper.Instance;
    public WsPluginLabelsHelper PluginLabels => WsPluginLabelsHelper.Instance;
    public WsPluginMassaHelper PluginMassa => WsPluginMassaHelper.Instance;
    public WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;
    public WsPluginPrintModel PluginPrintMain { get; } = new();
    public WsPluginPrintModel PluginPrintShipping { get; } = new();

    private ProductSeriesDirect _productSeries;
    public ProductSeriesDirect ProductSeries
    {
        get => _productSeries;
        private set
        {
            _productSeries = value;
            OnPropertyChanged();
        }
    }
    public PrintBrand PrintBrandMain =>
        Scale.PrinterMain is not null && Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping =>
        Scale.PrinterShipping is not null && Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.Tsc : PrintBrand.Zebra;
    private WsSqlPluWeighingModel _pluWeighing;
    [XmlElement]
    public WsSqlPluWeighingModel PluWeighing
    {
        get => _pluWeighing;
        private set
        {
            _pluWeighing = value;
            OnPropertyChanged();
        }
    }
    private WeighingSettingsModel _weighingSettings;
    [XmlElement]
    public WeighingSettingsModel WeighingSettings
    {
        get => _weighingSettings;
        set
        {
            _weighingSettings = value;
            OnPropertyChanged();
        }
    }
    public Stopwatch StopwatchMain { get; set; } = new();

    public WsPluNestingViewModel PluNestingView { get; }

    private List<WsSqlPluStorageMethodFkModel> PluStorageMethodsFks
    {
        get
        {
            return ContextCache.PluStorageMethodsFks;
        }
    }

    private WsSqlPluScaleModel _pluScale;
    [XmlElement]
    public WsSqlPluScaleModel PluScale
    {
        get => _pluScale;
        set
        {
            _pluScale = value;
            if (_pluScale.IsNotNew)
            {
                ContextManager.ContextItem.SaveLogInformation(
                $"{LocaleCore.Scales.PluSet(_pluScale.Plu.IdentityValueId, _pluScale.Plu.Number, _pluScale.Plu.Name)}");
                //WsSqlBoxModel box = WsSqlContextManagerHelper.Instance.ContextBox.GetItem(pluScale.Plu);
                //if (box.IsExists)
                //{
                //    pluScale.Plu.BoxTypeGuid = box.IdentityValueUid;
                //    pluScale.Plu.BoxTypeName = box.Name;
                //    pluScale.Plu.BoxTypeWeight = box.Weight;
                //}
                WsSqlBundleModel bundle = ContextManager.ContextBundle.GetItem(_pluScale.Plu);
                if (bundle.IsExists)
                {
                    _pluScale.Plu.PackageTypeGuid = bundle.IdentityValueUid;
                    _pluScale.Plu.PackageTypeName = bundle.Name;
                    _pluScale.Plu.PackageTypeWeight = bundle.Weight;
                }
                WsSqlClipModel clip = WsSqlContextManagerHelper.Instance.ContextClip.GetItem(_pluScale.Plu);
                if (clip.IsExists)
                {
                    _pluScale.Plu.ClipTypeGuid = clip.IdentityValueUid;
                    _pluScale.Plu.ClipTypeName = clip.Name;
                    _pluScale.Plu.ClipTypeWeight = clip.Weight;
                }
            }
            PluginPrintMain.LabelPrintedCount = 1;
            PluginPrintShipping.LabelPrintedCount = 1;
            PluNestingView.SetList(value.Plu);
            OnPropertyChanged();
        }
    }

    public readonly ushort PageColumnCount = 4;
    public readonly ushort PageSize = 20;
    public readonly ushort PageRowCount = 5;
    public int PageNumber { get; set; }
    public List<WsSqlPluScaleModel> PluScales { get; private set; }

    //public WsSqlPluStorageMethodFkModel PluStorageMethodFk
    //{
    //    get
    //    {
    //        if (PluScale.Plu.IsNotExists)
    //            return ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluStorageMethodFkModel>();
    //        if (!ContextCache.PluStorageMethodsFks.Any())
    //            return ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluStorageMethodFkModel>();

    //        WsSqlPluStorageMethodFkModel result = ContextCache.PluStorageMethodsFks.Find(
    //            x => Equals(x.Plu.Number, PluScale.Plu.Number));
    //            //ContextManager.ContextPluStorage.GetItemFk(plu, PluStorageMethodsFks);
    //        if (result.IsNotExists)
    //            result.FillProperties();
    //        return result;
    //    }
    //}

    private WsSqlDeviceScaleFkModel _deviceScaleFk;
    [XmlElement]
    public WsSqlDeviceScaleFkModel DeviceScaleFk
    {
        get => _deviceScaleFk;
        private set
        {
            _deviceScaleFk = value;
            OnPropertyChanged();
        }
    }
    private WsSqlScaleModel _scale;
    [XmlElement]
    public WsSqlScaleModel Scale
    {
        get => _scale;
        set
        {
            _scale = value;
            _ = ProductionFacility;
            PluScale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();
            OnPropertyChanged();
        }
    }
    private List<WsSqlScaleModel> _scales;
    [XmlElement]
    public List<WsSqlScaleModel> Scales
    {
        get => _scales;
        set
        {
            _scales = value;
            OnPropertyChanged();
        }
    }

    private WsSqlProductionFacilityModel _productionFacility;
    [XmlElement]
    public WsSqlProductionFacilityModel ProductionFacility
    {
        get =>
            _productionFacility.IsNotNew
                ? _productionFacility : Scale.WorkShop is not null ? Scale.WorkShop.ProductionFacility : _productionFacility;
        set
        {
            _productionFacility = value;
            OnPropertyChanged();
        }
    }
    private List<WsSqlProductionFacilityModel> _productionFacilities;
    [XmlElement]
    public List<WsSqlProductionFacilityModel> ProductionFacilities
    {
        get => _productionFacilities;
        set
        {
            _productionFacilities = value;
            OnPropertyChanged();
        }
    }
    private string _publishDescription;
    [XmlElement]
    public string PublishDescription
    {
        get => _publishDescription;
        set
        {
            _publishDescription = value;
            OnPropertyChanged();
        }
    }

    private DateTime ProductDateMaxValue => DateTime.Now.AddDays(+31);
    private DateTime ProductDateMinValue => DateTime.Now.AddDays(-31);
    private DateTime _productDate;
    public DateTime ProductDate
    {
        get => _productDate;
        set
        {
            _productDate = value;
            OnPropertyChanged();
        }
    }
    private readonly object _locker = new();
    public string DeviceName => MdNetUtils.GetLocalDeviceName(false);

    public WsUserSessionHelper()
    {
        // Items.
        _pluScale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluScaleModel>();
        _pluWeighing = ContextManager.AccessItem.GetItemNewEmpty<WsSqlPluWeighingModel>();
        _deviceScaleFk = ContextManager.AccessItem.GetItemNewEmpty<WsSqlDeviceScaleFkModel>();
        _productionFacility = ContextManager.AccessItem.GetItemNewEmpty<WsSqlProductionFacilityModel>();
        _scale = ContextManager.AccessItem.GetItemNewEmpty<WsSqlScaleModel>();
        // Lists.
        _productionFacilities = new();
        _productSeries = new();
        _scales = new();
        PluScales = new();
        // Strings
        _publishDescription = string.Empty;
        // Others.
        _weighingSettings = new();
        PluNestingView = new();
    }

    #endregion

    #region Public and private methods

    public void PluginsClose()
    {
        PluginMemory.Close();
        PluginMassa.Close();
        PluginPrintMain.Close();
        PluginPrintShipping.Close();
        PluginLabels.Close();
    }

    public void SetMain(long scaleId = -1, string productionFacilityName = "")
    {
        SetSqlPublish();
        SetScale(scaleId, productionFacilityName);

        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlScaleModel.Description), Direction = WsSqlOrderDirection.Asc });
        Scales = ContextManager.ContextList.GetListNotNullableScales(sqlCrudConfig);

        sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfigSection(false);
        sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlProductionFacilityModel.Name), Direction = WsSqlOrderDirection.Asc });
        ProductionFacilities = ContextManager.ContextList.GetListNotNullableProductionFacilities(WsSqlCrudConfigUtils.GetCrudConfigSection(false));
    }

    private void SetScale(long scaleId, string productionFacilityName)
    {
        lock (_locker)
        {
            // Device.
            WsSqlDeviceModel device = ContextManager.ContextItem.GetItemDeviceNotNullable(DeviceName);
            device = WsWpfUtils.SetNewDeviceWithQuestion(device, MdNetUtils.GetLocalIpAddress(), MdNetUtils.GetLocalMacAddress());
            // DeviceTypeFk.
            WsSqlDeviceTypeFkModel deviceTypeFk = ContextManager.ContextItem.GetItemDeviceTypeFkNotNullable(device);
            if (deviceTypeFk.IsNew)
            {
                // DeviceType.
                WsSqlDeviceTypeModel deviceType = ContextManager.ContextItem.GetItemDeviceTypeNotNullable("Monoblock");
                // DeviceTypeFk.
                deviceTypeFk.Device = device;
                deviceTypeFk.Type = deviceType;
                ContextManager.AccessItem.Save(deviceTypeFk);
            }
            // DeviceTypeFk.
            DeviceScaleFk = ContextManager.ContextItem.GetItemDeviceScaleFkNotNullable(deviceTypeFk.Device);
            // Scale.
            Scale = scaleId <= 0 ? DeviceScaleFk.Scale : ContextManager.ContextItem.GetScaleNotNullable(scaleId);
            // Area.
            ProductionFacility = ContextManager.ContextItem.GetProductionFacilityNotNullable(productionFacilityName);
            // Other.
            ProductDate = DateTime.Now;
            // Новая серия, упаковка продукции, новая паллета.
            ProductSeries = new(Scale);
            WeighingSettings = new();
        }
    }

    public void NewPallet()
    {
        PluginPrintMain.LabelPrintedCount = 1;
        ProductSeries.Load();
    }

    public void RotateProductDate(DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.Left:
            {
                ProductDate = ProductDate.AddDays(-1);
                if (ProductDate < ProductDateMinValue)
                    ProductDate = ProductDateMinValue;
                break;
            }
            case DirectionEnum.Right:
            {
                ProductDate = ProductDate.AddDays(1);
                if (ProductDate > ProductDateMaxValue)
                    ProductDate = ProductDateMaxValue;
                break;
            }
        }
    }

    /// <summary>
    /// Проверить наличие ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(Label fieldWarning)
    {
        if (PluScale.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluNotSelect);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluNotSelect);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить наличие весовой платформы Масса-К.
    /// </summary>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists() => PluScale is { IsNew: false, Plu.IsCheckWeight: false } || true;

    /// <summary>
    /// Проверить стабилизацию весовой платформы Масса-К.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightMassaIsStable(Label fieldWarning)
    {
        if (PluScale.Plu.IsCheckWeight && !PluginMassa.IsStable)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, $"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
            ContextManager.ContextItem.SaveLogError($"{LocaleCore.Scales.MassaIsNotCalc} {LocaleCore.Scales.MassaWaitStable}");
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить ГТИН ПЛУ.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckPluGtin(Label fieldWarning)
    {
        if (string.IsNullOrEmpty(PluScale.Plu.Gtin))
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluGtinIsNotSet);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluGtinIsNotSet);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить подключение и готовность принтера.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <param name="managerPrint"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnectAndReady(Label fieldWarning, WsPluginPrintModel managerPrint, bool isMain)
    {
        // Подключение.
        if (managerPrint.Printer.PingStatus != IPStatus.Success)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}"
                : $"{LocaleCore.Print.DeviceShippingIsUnavailable} {LocaleCore.Print.DeviceCheckConnect}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        // Готовность.
        if (!managerPrint.CheckDeviceStatus())
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            string message = isMain
                ? $"{LocaleCore.Print.DeviceMainCheckStatus} {managerPrint.GetDeviceStatus()}"
                : $"{LocaleCore.Print.DeviceShippingCheckStatus} {managerPrint.GetDeviceStatus()}";
            MdInvokeControl.SetText(fieldWarning, message);
            ContextManager.ContextItem.SaveLogError(message);
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить отрицательный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsNegative(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluginMassa.WeightNet <= 0)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightIsZero);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightIsZero);
            return false;
        }

        decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingView.Item.WeightTare);
        if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить положительный вес.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightIsPositive(Label fieldWarning)
    {
        if (!PluScale.Plu.IsCheckWeight) return true;

        decimal weight = PluginMassa.WeightNet - (PluScale.IsNew ? 0 : PluNestingView.Item.WeightTare);
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.CheckWeightThreshold(weight));
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.CheckWeightThreshold(weight));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Проверить границы веса.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <returns></returns>
    public bool CheckWeightThresholds(Label fieldWarning)
    {
        if (PluginMassa.IsWeightNetFake) return true;
        if (!PluScale.Plu.IsCheckWeight) return true;

        if (PluNestingView.Item is { WeightNom: > 0, WeightMin: not 0, WeightMax: not 0 })
        {
            if (!(PluWeighing.NettoWeight >= PluNestingView.Item.WeightMin && PluWeighing.NettoWeight <= PluNestingView.Item.WeightMax))
            {
                if (PluWeighing.IsNotNew)
                {
                    MdInvokeControl.SetVisible(fieldWarning, true);
                    string message = LocaleCore.Scales.CheckWeightThresholds(PluWeighing.NettoWeight,
                        PluScale.IsNew ? 0 : PluNestingView.Item.WeightMax,
                        PluScale.IsNew ? 0 : PluNestingView.Item.WeightNom,
                        PluScale.IsNew ? 0 : PluNestingView.Item.WeightMin);
                    MdInvokeControl.SetText(fieldWarning, message);
                    ContextManager.ContextItem.SaveLogError(message);
                }
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Печать этикетки.
    /// </summary>
    /// <param name="fieldWarning"></param>
    /// <param name="isClearBuffer"></param>
    public void PrintLabel(Label fieldWarning, bool isClearBuffer)
    {
        if (Scale is { IsOrder: true })
            throw new("Order under construct!");

        // #WS-T-710 Печать этикеток. Исправление счётчика этикеток
        PluScale.Scale = Scale;
        // Получить шаблон этикетки.
        WsSqlTemplateModel template = ContextManager.ContextItem.GetItemTemplateNotNullable(PluScale);
        // Проверить наличие шаблона этикетки.
        if (template.IsNew)
        {
            MdInvokeControl.SetVisible(fieldWarning, true);
            MdInvokeControl.SetText(fieldWarning, LocaleCore.Scales.PluTemplateNotSet);
            ContextManager.ContextItem.SaveLogError(LocaleCore.Scales.PluTemplateNotSet);
            return;
        }
        // Выбор типа ПЛУ.
        switch (PluScale.Plu.IsCheckWeight)
        {
            // Весовая ПЛУ.
            case true:
                PrintLabelCore(template, isClearBuffer);
                break;
            // Штучная ПЛУ.
            default:
                PrintLabelCount(template, isClearBuffer);
                break;
        }

        PluWeighing = new();
    }

    /// <summary>
    /// Инкремент счётчика этикеток.
    /// </summary>
    public void AddScaleCounter()
    {
        Scale.Counter++;
        ContextManager.AccessItem.Update(Scale);
    }

    /// <summary>
    /// Печать этикетки штучной ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(WsSqlTemplateModel template, bool isClearBuffer)
    {
        // Шаблон с указанием кол-ва и не весовой продукт.
        if (template.Data.Contains("^PQ1") && !PluScale.Plu.IsCheckWeight)
        {
            // Изменить кол-во этикеток.
            if (WeighingSettings.LabelsCountMain > 1)
                template.Data = template.Data.Replace("^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
            // Печать этикетки ПЛУ.
            PrintLabelCore(template, isClearBuffer);
        }
        // Шаблон без указания кол-ва.
        else
        {
            for (int i = PluginPrintMain.LabelPrintedCount; i <= WeighingSettings.LabelsCountMain; i++)
            {
                // Печать этикетки ПЛУ.
                PrintLabelCore(template, isClearBuffer);
            }
        }
    }

    /// <summary>
    /// Создать новое взвешивание ПЛУ.
    /// </summary>
    public void NewPluWeighing()
    {
        WsSqlProductSeriesModel productSeries = ContextManager.ContextItem.GetItemProductSeriesNotNullable(PluScale.Scale);

        PluWeighing = new()
        {
            PluScale = PluScale,
            Kneading = WeighingSettings.Kneading,
            NettoWeight = PluScale.Plu.IsCheckWeight ? PluginMassa.WeightNet - PluNestingView.Item.WeightTare : PluNestingView.Item.WeightNom,
            WeightTare = PluNestingView.Item.WeightTare,
            Series = productSeries,
        };

        // Save or update weighing products.
        SaveOrUpdatePluWeighing();
    }

    /// <summary>
    /// Задать фейк данные веса ПЛУ для режима разработки.
    /// </summary>
    /// <param name="owner"></param>
    public void SetPluWeighingFakeForDevelop(IWin32Window owner)
    {
        if (!PluScale.Plu.IsCheckWeight) return;
        if (PluginMassa.WeightNet > 0) return;

        DialogResult dialogResult = WsWpfUtils.ShowNewOperationControl(owner,
            LocaleCore.Print.QuestionUseFakeData,
            true, LogType.Question,
            new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
        if (dialogResult is DialogResult.Yes)
        {
            PluginMassa.WeightNet = StrUtils.NextDecimal(PluNestingView.Item.WeightMin, PluNestingView.Item.WeightMax);
            PluginMassa.IsWeightNetFake = true;
        }
    }

    /// <summary>
    /// Печать этикетки ПЛУ.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCore(WsSqlTemplateModel template, bool isClearBuffer)
    {
        try
        {
            (WsSqlPluLabelModel PluLabel, WsSqlPluLabelContextModel PluLabelContext) pluLabelWithContext = CreateAndSavePluLabel(template);
            CreateAndSaveBarCodes(pluLabelWithContext.PluLabel, pluLabelWithContext.PluLabelContext);

            // Print.
            if (isClearBuffer)
            {
                PluginPrintMain.ClearPrintBuffer();
                if (Scale.IsShipping)
                    PluginPrintShipping.ClearPrintBuffer();
            }

            // Send cmd to the print.
            if (Debug.IsDevelop)
            {
                DialogResult dialogResult = WsWpfUtils.ShowNewOperationControl(
                    LocaleCore.Print.QuestionPrintSendCmd, true, LogType.Question,
                    new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible });
                if (dialogResult != DialogResult.Yes)
                    return;
            }

            // Send cmd to the print.
            PluginPrintMain.SendCmd(pluLabelWithContext.PluLabel);
        }
        catch (Exception ex)
        {
            WsWpfUtils.CatchException(ex, true, true);
        }
    }

    /// <summary>
    /// Save or update weighing products.
    /// </summary>
    private void SaveOrUpdatePluWeighing()
    {
        if (!PluWeighing.PluScale.Plu.IsCheckWeight) return;

        if (PluWeighing.IsNew)
            ContextManager.AccessItem.Save(PluWeighing);
        else
            ContextManager.AccessItem.Update(PluWeighing);
    }

    /// <summary>
    /// Create PluLabel from Template.
    /// </summary>
    /// <param name="template"></param>
    /// <returns></returns>
    private (WsSqlPluLabelModel, WsSqlPluLabelContextModel) CreateAndSavePluLabel(WsSqlTemplateModel template)
    {
        WsSqlPluLabelModel pluLabel = new() { PluWeighing = PluWeighing, PluScale = PluScale, ProductDt = ProductDate };
        //pluLabel.PluWeighing.PluScale = PluScale;
        //pluLabel.PluWeighing.PluScale.Scale = Scale;
        //pluLabel.PluScale.Scale = Scale;
        string str;

        str = WsDataFormatUtils.SerializeAsXmlString<WsSqlScaleModel>(Scale, true, true);
        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlScaleModel>(Scale, true, true);

        str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluScaleModel>(PluScale, true, true);
        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluScaleModel>(PluScale, true, true);

        str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluWeighingModel>(PluWeighing, true, true);
        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluWeighingModel>(PluWeighing, true, true);

        str = WsDataFormatUtils.SerializeAsXmlString<WsSqlPluLabelModel>(pluLabel, true, true);
        pluLabel.Xml = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelModel>(pluLabel, true, true);

        XmlDocument xmlArea = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlProductionFacilityModel>(ProductionFacility, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlArea);

        WsSqlPluLabelContextModel pluLabelContext = new(pluLabel, PluNestingView.Item, pluLabel.PluScale, ProductionFacility, PluWeighing);
        XmlDocument xmlLabelContext = WsDataFormatUtils.SerializeAsXmlDocument<WsSqlPluLabelContextModel>(pluLabelContext, true, true);
        pluLabel.Xml = WsDataFormatUtils.XmlMerge(pluLabel.Xml, xmlLabelContext);

        // Патч шаблона: PluLabelContextModel -> WsPluLabelContextModel
        template.Data = template.Data.Replace("PluLabelModel", nameof(WsSqlPluLabelModel));
        template.Data = template.Data.Replace("PluLabelContextModel", nameof(WsSqlPluLabelContextModel));

        pluLabel.Zpl = WsDataFormatUtils.XsltTransformation(template.Data, pluLabel.Xml.OuterXml);
        pluLabel.Zpl = WsDataFormatUtils.XmlReplaceNextLine(pluLabel.Zpl);
        pluLabel.Zpl = ZplUtils.ConvertStringToHex(pluLabel.Zpl);
        _ = DataFormatUtils.PrintCmdReplaceZplResources(pluLabel.Zpl, ActionReplaceStorageMethod(pluLabel));

        // Save.
        ContextManager.AccessItem.Save(pluLabel);

        return (pluLabel, pluLabelContext);
    }

    /// <summary>
    /// Replace temperature storage method.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <returns></returns>
    private Action<string> ActionReplaceStorageMethod(WsSqlPluLabelModel pluLabel) =>
        zpl =>
        {
            // Patch for using table `PLUS_STORAGE_METHODS_FK`.
            if (PluStorageMethodsFks.Any() && zpl.Contains("[@PLUS_STORAGE_METHODS_FK]"))
            {
                WsSqlTemplateResourceModel resource = ContextManager.ContextPluStorage.GetItemResource(
                    pluLabel.PluScale.Plu, PluStorageMethodsFks);
                string resourceHex = ZplUtils.ConvertStringToHex(resource.Data.ValueUnicode);
                zpl = zpl.Replace("[@PLUS_STORAGE_METHODS_FK]", resourceHex);
            }
            pluLabel.Zpl = zpl;
        };

    /// <summary>
    /// Create BarCode from PluLabel.
    /// </summary>
    /// <param name="pluLabel"></param>
    /// <param name="pluLabelContext"></param>
    private void CreateAndSaveBarCodes(WsSqlPluLabelModel pluLabel, WsSqlPluLabelContextModel pluLabelContext)
    {
        WsSqlBarCodeModel barCode = new(pluLabel);
        BarCode.SetBarCodeTop(barCode, pluLabelContext);
        BarCode.SetBarCodeRight(barCode, pluLabelContext);
        BarCode.SetBarCodeBottom(barCode, pluLabelContext);
        ContextManager.AccessItem.Save(barCode);
    }

    private void SetSqlPublish() =>
        PublishDescription = Debug.Config switch
        {
            WsConfiguration.DevelopAleksandrov => LocaleCore.Sql.SqlServerDevelopAleksandrov,
            WsConfiguration.DevelopMorozov => LocaleCore.Sql.SqlServerDevelopMorozov,
            WsConfiguration.DevelopVS => LocaleCore.Sql.SqlServerVS,
            WsConfiguration.ReleaseAleksandrov => LocaleCore.Sql.SqlServerReleaseAleksandrov,
            WsConfiguration.ReleaseMorozov => LocaleCore.Sql.SqlServerReleaseMorozov,
            WsConfiguration.ReleaseVS => LocaleCore.Sql.SqlServerReleaseVS,
            _ => throw new ArgumentOutOfRangeException()
        };

    public void SetPluScales()
    {
        WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(Scale, nameof(WsSqlPluScaleModel.Scale),
            false, false, false, false);
        sqlCrudConfig.AddFilters(new WsSqlFieldFilterModel { Name = nameof(WsSqlPluScaleModel.IsActive), Value = true });
        sqlCrudConfig.AddOrders(new() { Name = nameof(WsSqlPluScaleModel.Plu), Direction = WsSqlOrderDirection.Asc });
        sqlCrudConfig.IsResultOrder = true;
        PluScales = ContextManager.ContextList.GetListNotNullablePlusScales(sqlCrudConfig);
    }

    public List<WsSqlPluScaleModel> GetCurrentPlus()
    {
        IEnumerable<WsSqlPluScaleModel> plusSkip = PluScales.Skip(PageNumber * PageSize);
        IEnumerable<WsSqlPluScaleModel> plusTake = plusSkip.Take(PageSize);
        return plusTake.ToList();
    }

    #endregion
}