﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.SectionComponents.Others;

public partial class SectionPackages : RazorComponentSectionBase<PackageModel>
{
    #region Public and private fields, properties, constructor

    public SectionPackages()
    {
        RazorComponentConfig.IsShowFilterMarked = false;
	}

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlItemsCast = AppSettings.DataAccess.GetListPackages();

                ButtonSettings = new(false, false, false, false, false, false, false);
            }
        });
    }

    #endregion
}
