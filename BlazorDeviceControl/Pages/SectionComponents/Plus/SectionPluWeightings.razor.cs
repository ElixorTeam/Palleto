﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceScalesFks;
using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace BlazorDeviceControl.Pages.SectionComponents.Plus;

public sealed partial class SectionPluWeightings : RazorComponentSectionBase<PluWeighingModel>
{
	#region Public and private fields, properties, constructor
    
    private List<DeviceScaleFkModel> DeviceScaleFk { get; set; }
	
    public SectionPluWeightings() : base()
	{
        ButtonSettings = new(false, true, false, true, false, false, false);
    }

	#endregion

	#region Public and private methods
    
    protected override void SetSqlSectionCast()
    {
        base.SetSqlSectionCast();
        DeviceScaleFk = DataContext.GetListNotNullable<DeviceScaleFkModel>(new());
    }
    
    private string GetDeviceName(PluWeighingModel pluWeighing)
    {
        DeviceScaleFkModel? deviceScale = DeviceScaleFk.Find((x) => x.Scale.Equals(pluWeighing.PluScale.Scale));
        return deviceScale != null ? deviceScale.Device.Name : "";
    }
    
	#endregion
}
