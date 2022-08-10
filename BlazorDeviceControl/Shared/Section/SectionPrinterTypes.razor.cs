﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Section;

public partial class SectionPrinterTypes
{
    #region Public and private fields, properties, constructor

    private List<PrinterTypeEntity> ItemsCast => Items == null ? new() : Items.Select(x => (PrinterTypeEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionPrinterTypes()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.PrintersTypes);
        Items = new();
    }

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		base.OnParametersSet();
		SetParametersWithAction(new()
		{
            () =>
            {
                Items = AppSettings.DataAccess.Crud.GetEntities<PrinterTypeEntity>(null, null,
                    IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0)
                    ?.OrderBy(x => x.Name).ToList<BaseEntity>();
                ButtonSettings = new(true, true, true, true, true, false, false);
            }
		});
	}

    #endregion
}
