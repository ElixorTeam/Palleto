﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Devices;

/// <summary>
/// Section ProductionFacilities.
/// </summary>
public partial class SectionProductionFacilities : RazorComponentSectionBase<ProductionFacilityModel>
{
    #region Public and private fields, properties, constructor

    public SectionProductionFacilities()
    {
	    RazorComponentConfig.IsShowFilterMarked = true;
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlItemsCast = AppSettings.DataAccess.GetListProductionFacilities(RazorComponentConfig.IsShowMarked, RazorComponentConfig.IsShowOnlyTop, false);

                ButtonSettings = new(true, true, true, true, true, false, false);
            }
        });
    }

    #endregion
}