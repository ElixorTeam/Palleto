﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item;

public partial class ItemHost
{
    #region Public and private fields, properties, constructor

    private HostEntity ItemCast { get => Item == null ? new() : (HostEntity)Item; set => Item = value; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ItemHost()
    {
        Table = new TableScaleEntity(ProjectsEnums.TableScale.Hosts);
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
						ItemCast.Name = "NEW HOST";
						ItemCast.Ip = "127.0.0.1";
						ItemCast.MacAddress.Default();
						break;
					default:
						ItemCast = AppSettings.DataAccess.Crud.GetEntity<HostEntity>(
							new(new() { new(DbField.IdentityId, DbComparer.Equal, IdentityId) }));
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

    #endregion
}
