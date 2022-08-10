﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemBarCode
{
    #region Public and private fields, properties, constructor

    private BarCodeV2Entity ItemCast { get => Item == null ? new() : (BarCodeV2Entity)Item; set => Item = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemBarCode()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.BarCodeTypes);
        ItemCast = new();
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
				switch (TableAction)
				{
					case DbTableAction.New:
						ItemCast = new();
						ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
						ItemCast.IsMarked = false;
						ItemCast.Value = "NEW BARCODE";
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<BarCodeV2Entity>(
							new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
