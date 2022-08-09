﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.Localizations;
using DataCore.Models;
using DataCore.Sql.TableScaleModels;
using Microsoft.AspNetCore.Components;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemBarCodeType
    {
		#region Public and private fields, properties, constructor

		private BarCodeTypeV2Entity ItemCast { get => Item == null ? new() : (BarCodeTypeV2Entity)Item; set => Item = value; }

        #endregion

        #region Constructor and destructor

        public ItemBarCodeType()
        {
            Table = new TableScaleEntity(ProjectsEnums.TableScale.BarCodeTypes);
            ItemCast = new();
            ButtonSettings = new();
        }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
	        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
	        SetParametersAsyncWithAction(parameters, () => base.SetParametersAsync(parameters).ConfigureAwait(true),
		        null, () =>
                {
					switch (TableAction)
                    {
                        case DbTableAction.New:
                            ItemCast = new();
                            ItemCast.ChangeDt = ItemCast.CreateDt = System.DateTime.Now;
                            ItemCast.IsMarked = false;
                            ItemCast.Name = "NEW BARCODE_TYPE";
                            break;
                        default:
                            ItemCast = AppSettings.DataAccess.Crud.GetEntity<BarCodeTypeV2Entity>(
                                new(new() { new(DbField.IdentityUid, DbComparer.Equal, IdentityUid) }));
                            break;
                    }
                    ButtonSettings = new(false, false, false, false, false, true, true);
				});
        }

        #endregion
    }
}
