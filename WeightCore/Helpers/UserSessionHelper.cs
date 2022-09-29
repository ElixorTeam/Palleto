﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using DataCore.Protocols;
using DataCore.Settings;
using DataCore.Sql.Core;
using DataCore.Sql.Models;
using DataCore.Sql.TableDirectModels;
using DataCore.Sql.TableScaleModels;
using MDSoft.BarcodePrintUtils;
using MvvmHelpers;
using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Serialization;
using DataCore.Models;
using DataCore.Sql.Fields;
using WeightCore.Gui;
using WeightCore.Managers;
using DataCore.Sql.Tables;
using DataCore.Utils;

namespace WeightCore.Helpers;

public class UserSessionHelper : BaseViewModel
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618
    private static UserSessionHelper _instance;
#pragma warning restore CS8618
    public static UserSessionHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

    #endregion

    #region Public and private fields and properties

    private AppVersionHelper AppVersion { get; } = AppVersionHelper.Instance;
    public DataAccessHelper DataAccess { get; } = DataAccessHelper.Instance;
    public DebugHelper Debug { get; } = DebugHelper.Instance;
    public ManagerControllerHelper ManagerControl { get; } = ManagerControllerHelper.Instance;
    public SqlViewModelHelper SqlViewModel { get; } = SqlViewModelHelper.Instance;
    public ProductSeriesDirect ProductSeries { get; private set; } = new();
    public PrintBrand PrintBrandMain => SqlViewModel.Scale.PrinterMain is not null &&
        SqlViewModel.Scale.PrinterMain.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
    public PrintBrand PrintBrandShipping => SqlViewModel.Scale.PrinterShipping is not null &&
        SqlViewModel.Scale.PrinterShipping.PrinterType.Name.Contains("TSC ") ? PrintBrand.TSC : PrintBrand.Zebra;
    [XmlElement(IsNullable = true)] public PluWeighingModel? PluWeighing { get; private set; }
    public WeighingSettingsEntity WeighingSettings { get; private set; } = new();
    public Stopwatch StopwatchMain { get; set; } = new();
    public bool IsPluCheckWeight => PluScale is { Plu.IsCheckWeight: true };

    private PluScaleModel? _pluScale;
    [XmlElement] public PluScaleModel? PluScale
    {
        get => _pluScale;
        private set
        {
            _pluScale = value;
            ManagerControl.PrintMain.LabelsCount = 1;
            ManagerControl.PrintShipping.LabelsCount = 1;
        }
    }
    private readonly object _locker = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public UserSessionHelper()
    {
        Setup(-1, "");
    }

    #endregion

    #region Public and private methods

    public void Setup(long scaleId, string hostName)
    {
        lock (_locker)
        {
            if (string.IsNullOrEmpty(hostName))
                hostName = NetUtils.GetLocalHostName(false);
            HostModel host = SqlUtils.GetHost(hostName);
            SqlViewModel.Scale = scaleId <= 0 ? SqlUtils.GetScaleFromHost(host.Identity.Id) : SqlUtils.GetScale(scaleId);

            AppVersion.AppDescription = $"{AppVersion.AppTitle}.  {SqlViewModel.Scale.Description}.";
            //AppVersion.AppDescription = $"{AppVersion.AppTitle}. ";
            SqlViewModel.ProductDate = DateTime.Now;
            // начинается новыя серия, упаковки продукции, новая паллета
            ProductSeries = new(SqlViewModel.Scale);
            //ProductSeries.Load();
            WeighingSettings = new();
        }
    }

    public void NewPallet()
    {
        ManagerControl.PrintMain.LabelsCount = 1;
        ProductSeries.Load();
        //if (Manager is null || Manager.Print is null)
        //    return;
        //Manager.Print.ClearPrintBuffer(true, LabelsCurrent);
    }

    public void RotateProductDate(DirectionEnum direction)
    {
        switch (direction)
        {
            case DirectionEnum.Left:
                {
                    SqlViewModel.ProductDate = SqlViewModel.ProductDate.AddDays(-1);
                    if (SqlViewModel.ProductDate < SqlViewModel.ProductDateMinValue)
                        SqlViewModel.ProductDate = SqlViewModel.ProductDateMinValue;
                    break;
                }
            case DirectionEnum.Right:
                {
                    SqlViewModel.ProductDate = SqlViewModel.ProductDate.AddDays(1);
                    if (SqlViewModel.ProductDate > SqlViewModel.ProductDateMaxValue)
                        SqlViewModel.ProductDate = SqlViewModel.ProductDateMaxValue;
                    break;
                }
        }
    }

    public void SetCurrentPlu(PluScaleModel pluScale)
    {
        if ((PluScale = pluScale) is not null)
        {
            DataAccess.LogInformation($"{LocaleCore.Scales.PluSet(PluScale.Plu.Identity.Id, PluScale.Plu.Number, PluScale.Plu.Name)}",
                SqlViewModel.Scale.Host?.HostName);
        }
    }

    /// <summary>
    /// Check PLU is empty.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPluIsEmpty(IWin32Window owner)
    {
        if (PluScale is null)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.PluNotSelect,
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check Massa-K device exists.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightMassaDeviceExists(IWin32Window owner)
    {
        if (ManagerControl is null || ManagerControl.Massa is null)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotFound,
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check Massa-K is stable.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightMassaIsStable(IWin32Window owner)
    {
        if (PluScale.Plu.IsCheckWeight && !ManagerControl.Massa.MassaStable.IsStable)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.MassaIsNotCalc + Environment.NewLine + LocaleCore.Scales.MassaWaitStable,
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check printer connection.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckPrintIsConnect(IWin32Window owner, ManagerPrint managerPrint, bool isMain)
    {
        if (!managerPrint.Printer.IsPing)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, isMain
                ? LocaleCore.Print.DeviceMainIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect
                : LocaleCore.Print.DeviceShippingIsUnavailable + Environment.NewLine + LocaleCore.Print.DeviceCheckConnect,
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check printer status on ready.
    /// </summary>
    /// <param name="owner"></param>
    /// <param name="managerPrint"></param>
    /// <param name="isMain"></param>
    /// <returns></returns>
    public bool CheckPrintStatusReady(IWin32Window owner, ManagerPrint managerPrint, bool isMain)
    {
        if (!managerPrint.CheckDeviceStatus())
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, isMain
                ? LocaleCore.Print.DeviceMainCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus()
                : LocaleCore.Print.DeviceShippingCheckStatus + Environment.NewLine + managerPrint.GetDeviceStatus(),
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check weight is negative.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightIsNegative(IWin32Window owner)
    {
        if (!IsPluCheckWeight)
            return true;
        decimal weight = ManagerControl.Massa.WeightNet - (PluScale is null ? 0 : PluScale.Plu.TareWeight);
        if (weight < LocaleCore.Scales.MassaThresholdValue || weight < LocaleCore.Scales.MassaThresholdPositive)
        {
            GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host is null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    /// <summary>
    /// Check weight is positive.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightIsPositive(IWin32Window owner)
    {
        if (!IsPluCheckWeight)
            return true;
        decimal weight = ManagerControl.Massa.WeightNet - (PluScale is null ? 0 : PluScale.Plu.TareWeight);
        if (weight > LocaleCore.Scales.MassaThresholdValue)
        {
            DialogResult result = GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThreshold(weight),
                true, LogTypeEnum.Warning,
                new() { ButtonCancelVisibility = Visibility.Visible },
                SqlViewModel.Scale.Host is null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return result == DialogResult.Cancel;
        }
        return true;
    }

    /// <summary>
    /// Check weight thresholds.
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public bool CheckWeightThresholds(IWin32Window owner)
    {
        if (!IsPluCheckWeight)
            return true;
        bool isCheck = false;
        if (PluScale?.Plu.NominalWeight > 0)
        {
            if (PluWeighing?.NettoWeight >= PluScale.Plu.LowerThreshold && PluWeighing?.NettoWeight <= PluScale.Plu.UpperThreshold)
                isCheck = true;
        }
        else
            isCheck = true;
        if (!isCheck)
        {
            if (PluWeighing is not null)
                GuiUtils.WpfForm.ShowNewOperationControl(owner, LocaleCore.Scales.CheckWeightThresholds(
                    PluWeighing.NettoWeight, PluScale is null ? 0 : PluScale.Plu.UpperThreshold,
                    PluScale is null ? 0 : PluScale.Plu.NominalWeight,
                    PluScale is null ? 0 : PluScale.Plu.LowerThreshold),
                    true, LogTypeEnum.Warning,
                    new() { ButtonCancelVisibility = Visibility.Visible },
                    SqlViewModel.Scale.Host is null ? string.Empty : SqlViewModel.Scale.Host.HostName, nameof(WeightCore));
            return false;
        }
        return true;
    }

    public void PrintLabel(bool isClearBuffer)
    {
        TemplateModel? template = null;
        if (SqlViewModel.Scale is { IsOrder: true })
        {
            throw new Exception("Order under construct!");
            //template = SqlViewModel.Order.Template;
            //SqlViewModel.Order.FactBoxCount = SqlViewModel.Order.FactBoxCount >= 100 ? 1 : SqlViewModel.Order.FactBoxCount + 1;
        }
        else if (SqlViewModel.Scale.IsOrder != true)
        {
            //template = PluScale?.LoadTemplate();
            if (PluScale is not null)
            {
	            SqlCrudConfigModel sqlCrudConfig = SqlUtils.GetCrudConfig(
                    new SqlFieldFilterModel(nameof(SqlTableBase.IdentityValueId), SqlFieldComparerEnum.Equal, PluScale.Plu.Template.Identity.Id), 0, false,false);
                template = DataAccess.GetItem<TemplateModel>(sqlCrudConfig);
            }
        }

        // Template exist.
        if (template is not null)
        {
            switch (IsPluCheckWeight)
            {
                case true:
                    PrintLabelCore(template, isClearBuffer);
                    break;
                default:
                    PrintLabelCount(template, isClearBuffer);
                    break;
            }
        }
        PluWeighing = null;
        SetNewScaleCounter();
    }

    private void SetNewScaleCounter()
    {
        SqlViewModel.Scale.Counter++;
        DataAccess.Update(SqlViewModel.Scale);
    }

    /// <summary>
    /// Save item.
    /// </summary>
    /// <param name="printCmd"></param>
    /// <param name="pluWeighing"></param>
    private void PrintSaveLabel(string printCmd, PluWeighingModel pluWeighing)
    {
        PluLabelModel pluLabel = new()
        {
            PluWeighing = pluWeighing,
            Zpl = printCmd,
            ProductDt = SqlViewModel.ProductDate,
		};
        DataAccess.Save(pluLabel);
    }

    /// <summary>
    /// Count label printing.
    /// </summary>
    /// <param name="template"></param>
    /// <param name="isClearBuffer"></param>
    private void PrintLabelCount(TemplateModel template, bool isClearBuffer)
    {
        //// Указан номинальный вес.
        //bool isCheck = false;
        //if (CurrentPlu.NominalWeight > 0)
        //{
        //    if (Manager?.Massa is not null)
        //        CurrentWeighingFact.NettoWeight = Manager.Massa.WeightNet - CurrentPlu.GoodsTareWeight;
        //    else
        //        CurrentWeighingFact.NettoWeight -= CurrentPlu.GoodsTareWeight;
        //    if (CurrentWeighingFact.NettoWeight >= CurrentPlu.LowerWeightThreshold &&
        //        CurrentWeighingFact.NettoWeight <= CurrentPlu.UpperWeightThreshold)
        //    {
        //        isCheck = true;
        //    }
        //}
        //else
        //    isCheck = true;
        //if (!isCheck)
        //{
        //    // WPF MessageBox.
        //    using WpfPageLoader wpfPageLoader = new(Page.MessageBox, false) { Width = 700, Height = 400 };
        //    wpfPageLoader.MessageBox.Caption = LocaleCore.Scales.OperationControl;
        //    wpfPageLoader.MessageBox.Message =
        //        LocaleCore.Scales.WeightingControl + Environment.NewLine +
        //        $"Вес нетто: {CurrentWeighingFact.NettoWeight} кг" + Environment.NewLine +
        //        $"Номинальный вес: {CurrentPlu.NominalWeight} кг" + Environment.NewLine +
        //        $"Верхнее значение веса: {CurrentPlu.UpperWeightThreshold} кг" + Environment.NewLine +
        //        $"Нижнее значение веса: {CurrentPlu.LowerWeightThreshold} кг" + Environment.NewLine + Environment.NewLine +
        //        "Для продолжения печати нажмите Ignore.";
        //    wpfPageLoader.MessageBox.ButtonAbortVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.ButtonRetryVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.ButtonIgnoreVisibility = Visibility.Visible;
        //    wpfPageLoader.MessageBox.VisibilitySettings.Localization();
        //    wpfPageLoader.ShowDialog(owner);
        //    DialogResult result = wpfPageLoader.MessageBox.Result;
        //    wpfPageLoader.Close();
        //    wpfPageLoader.Dispose();
        //    if (result != DialogResult.Ignore)
        //        return;
        //}

        // Шаблон с указанием кол-ва и не весовой продукт.
        if (template.ImageData.ValueUnicode.Contains("^PQ1") && !IsPluCheckWeight)
        {
            // Изменить кол-во этикеток.
            if (WeighingSettings.LabelsCountMain > 1)
                template.ImageData.ValueUnicode = template.ImageData.ValueUnicode.Replace(
                    "^PQ1", $"^PQ{WeighingSettings.LabelsCountMain}");
            // Печать этикетки.
            PrintLabelCore(template, isClearBuffer);
        }
        // Шаблон без указания кол-ва.
        else
        {
            for (int i = ManagerControl.PrintMain.LabelsCount; i <= WeighingSettings.LabelsCountMain; i++)
            {
                // Печать этикетки.
                PrintLabelCore(template, isClearBuffer);
            }
        }
    }

    /// <summary>
    /// Вывести серию этикеток по заданному размеру паллеты.
    /// </summary>
    public void SetWeighingFact(IWin32Window owner)
    {
        if (PluScale is null)
            return;

        // Debug check.
        if (IsPluCheckWeight && Debug.IsDebug)
        {
	        DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(owner,
		        LocaleCore.Print.QuestionUseFakeData,
		        true, LogTypeEnum.Question,
		        new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
		        SqlViewModel.Scale.Host is null ? string.Empty : SqlViewModel.Scale.Host.HostName,
		        nameof(WeightCore));
	        if (dialogResult is DialogResult.Yes)
	        {
                Random random = new();
				ManagerControl.Massa.WeightNet = StringUtils.NextDecimal(random, 0.25M, 5.00M);
                ManagerControl.Massa.IsWeightNetFake = true;
	        }
        }
        
        PluWeighing = new()
        {
	        PluScale = PluScale,
	        Kneading = WeighingSettings.Kneading,
	        NettoWeight = IsPluCheckWeight ? ManagerControl.Massa.WeightNet - PluScale.Plu.TareWeight : PluScale.Plu.NominalWeight,
	        TareWeight = PluScale.Plu.TareWeight
        };
	}

	/// <summary>
	/// Weight label printing.
	/// </summary>
	/// <param name="template"></param>
	/// <param name="isClearBuffer"></param>
	private void PrintLabelCore(TemplateModel template, bool isClearBuffer)
    {
        try
        {
            if (PluWeighing is null)
                return;

            DataAccess.Save(PluWeighing);

            string xmlPluWeighing = PluWeighing.SerializeAsXml<PluWeighingModel>(true);
			string xmlProductionFacility = SqlViewModel.Area is null ? string.Empty : SqlViewModel.Area.SerializeAsXml<ProductionFacilityModel>(true);
            xmlPluWeighing = Zpl.ZplUtils.XmlCompatibleReplace(xmlPluWeighing);
            string xml = Zpl.ZplUtils.MergeXml(xmlPluWeighing, xmlProductionFacility);
            // XSLT transform.
            string printCmd = Zpl.ZplUtils.XsltTransformation(template.ImageData.ValueUnicode, xml);
            printCmd = MDSoft.BarcodePrintUtils.Zpl.ZplUtils.ConvertStringToHex(printCmd);
            // Replace ZPL resources.
            printCmd = Zpl.ZplUtils.PrintCmdReplaceZplResources(printCmd);
            // DB save ZPL query to Labels.
            PrintSaveLabel(printCmd, PluWeighing);
            //if (ManagerControl is null || ManagerControl.PrintMain is null)
            //    return;

            // Print.
            if (isClearBuffer)
            {
                ManagerControl.PrintMain.ClearPrintBuffer();
                if (SqlViewModel.Scale.IsShipping)
                    ManagerControl.PrintShipping.ClearPrintBuffer();
            }

			// Debug check.
			if (Debug.IsDebug)
			{
				DialogResult dialogResult = GuiUtils.WpfForm.ShowNewOperationControl(null, LocaleCore.Print.QuestionPrintSendCmd,
					true, LogTypeEnum.Question,
					new() { ButtonYesVisibility = Visibility.Visible, ButtonNoVisibility = Visibility.Visible },
					SqlViewModel.Scale.Host is null ? string.Empty : SqlViewModel.Scale.Host.HostName,
					nameof(WeightCore));
				if (dialogResult != DialogResult.Yes)
					return;
			}

			// Send cmd to the print.
			ManagerControl.PrintMain.SendCmd(printCmd);
        }
        catch (Exception ex)
        {
            GuiUtils.WpfForm.CatchException(ex);
        }
    }

    #endregion
}
