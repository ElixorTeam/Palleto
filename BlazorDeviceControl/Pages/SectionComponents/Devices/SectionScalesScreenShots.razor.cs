﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Scales;
using DataCore.Sql.TableScaleModels.ScalesScreenshots;

namespace BlazorDeviceControl.Pages.SectionComponents.Devices;

public partial class SectionScalesScreenShots : RazorComponentSectionBase<ScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    public SectionScalesScreenShots() : base()
    {
        ButtonSettings = new(true, true, true, true, true, true, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlCrudConfigSection.AddFilters(nameof(ScaleScreenShotModel.Scale), SqlItem);
        base.SetSqlSectionCast();
    }

    #endregion
}