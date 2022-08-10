﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql;
using DataCore.Sql.DataModels;
using DataCore.Sql.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Section.Measurements;

public partial class SectionLabels
{
    #region Public and private fields, properties, constructor

    private List<LabelQuickEntity> ItemsCast => Items == null ? new() : Items.Select(x => (LabelQuickEntity)x).ToList();

    /// <summary>
    /// Constructor.
    /// </summary>
    public SectionLabels()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Labels);
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
                object[] objects = AppSettings.DataAccess.Crud.GetEntitiesNativeObject(
                    SqlQueries.DbScales.Tables.Labels.GetLabels(
                        IsSelectTopRows ? AppSettings.DataAccess.JsonSettingsLocal.SelectTopRowsCount : 0));
                Items = new List<LabelQuickEntity>().ToList<BaseEntity>();
                foreach (object obj in objects)
                {
                    if (obj is object[] { Length: 16 } item)
                    {
                        if (long.TryParse(Convert.ToString(item[0]), out long id))
                        {
                            Items.Add(new LabelQuickEntity()
                            {
                                IdentityId = id, // item[1]
                                CreateDt = Convert.ToDateTime(item[1]),
                                ScaleId = Convert.ToInt64(item[2]),
                                ScaleDescription = item[3] is string scaleDescr ? scaleDescr : string.Empty,
                                PluId = Convert.ToInt32(item[4]),
                                PluNumber = Convert.ToInt32(item[5]),
                                PluGoodName = item[6] is string goodName ? goodName : string.Empty,
                                WeithingDate = Convert.ToDateTime(item[7]),
                                NetWeight = Convert.ToDecimal(item[8]),
                                TareWeight = Convert.ToDecimal(item[9]),
                                ProductDate = Convert.ToDateTime(item[10]),
                                RegNum = Convert.ToInt32(item[11]),
                                Kneading = Convert.ToInt32(item[12]),
                                Zpl = item[13] is string zpl ? zpl : string.Empty,
                                TemplateId = Convert.ToInt64(item[14]),
                                TemplateName = item[15] is string template ? template : string.Empty,
                            });
                        }
                    }
                }
                ButtonSettings = new(true, true, true, false, false, false, false);
            }
		});
	}

    #endregion
}
