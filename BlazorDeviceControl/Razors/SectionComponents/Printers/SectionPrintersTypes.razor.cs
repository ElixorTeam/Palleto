﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PrintersTypes;

namespace BlazorDeviceControl.Razors.SectionComponents.Printers;

public partial class SectionPrintersTypes : RazorComponentSectionBase<PrinterTypeModel, SqlTableBase>
{
    #region Public and private fields, properties, constructor

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
	            SqlSectionCast = DataContext.GetListNotNullable<PrinterTypeModel>(SqlCrudConfigSection);
                AutoShowFilterOnlyTopSetup();
            }
        });
    }

    #endregion
}
