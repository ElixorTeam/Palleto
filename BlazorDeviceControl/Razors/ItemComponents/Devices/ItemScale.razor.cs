﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Devices;

/// <summary>
/// Scale item page.
/// </summary>
public partial class ItemScale : RazorPageItemBase<ScaleModel>
{
	#region Public and private fields, properties, constructor

	private PrinterModel PrinterMain { get => ItemCast.PrinterMain ?? new(); set => ItemCast.PrinterMain = value; }
	private PrinterModel PrinterShipping { get => ItemCast.PrinterShipping ?? new(); set => ItemCast.PrinterShipping = value; }
	private TemplateModel TemplateDefault { get => ItemCast.TemplateDefault ?? new(); set => ItemCast.TemplateDefault = value; }
	//private TemplateModel? TemplateSeries { get => ItemCast.TemplateSeries; set => ItemCast.TemplateSeries = value; }
	//{
	//	get => ItemCast.TemplateSeries;
	//	set => ItemCast.TemplateSeries = !Equals(value?.Identity.EqualsNew(), LocaleCore.Table.FieldNull) ? value : null;
	//}

	private WorkShopModel WorkShop { get => ItemCast.WorkShop ?? new(); set => ItemCast.WorkShop = value; }
	private HostModel Host { get => ItemCast.Host ?? new(); set => ItemCast.Host = value; }
	private List<PrinterModel> Printers { get; set; }
	private List<ProductionFacilityModel> ProductionFacilities { get; set; }
	private List<TemplateModel> Templates { get; set; }
	private List<WorkShopModel> WorkShops { get; set; }
	private List<TypeModel<string>> ComPorts { get; set; }
	private List<HostModel> Hosts { get; set; }
	
	public ItemScale()
	{
		Printers = new();
		ComPorts = new();
		Hosts = new();
		ProductionFacilities = new();
		WorkShops = new();
		Templates = new();
		RazorPageConfig.IsShowFilterAdditional = false;
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				ItemCast = AppSettings.DataAccess.GetItemByIdNotNull<ScaleModel>(IdentityId);
				ItemCast.Host ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterMain ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.PrinterShipping ??= new() { Name = LocaleCore.Table.FieldNull };
				ItemCast.TemplateDefault ??= new() { Title = LocaleCore.Table.FieldNull };
				ItemCast.TemplateSeries ??= new() { Title = LocaleCore.Table.FieldNull };
				ItemCast.WorkShop ??= new() { Name = LocaleCore.Table.FieldNull };

			    // ComPorts
			    ComPorts = SerialPortsUtils.GetListTypeComPorts(LangEnum.English);
			    // ScaleFactor
			    ItemCast.ScaleFactor ??= 1000;
			    Hosts = AppSettings.DataAccess.GetListHosts(false, false, true);
			    Printers = AppSettings.DataAccess.GetListPrinters(false, false, true);
			    Templates = AppSettings.DataAccess.GetListTemplates(false, false, true);
			    ProductionFacilities = AppSettings.DataAccess.GetListProductionFacilities(false, false, true);
			    WorkShops = AppSettings.DataAccess.GetListWorkShops(false, false, true);

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}
