﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.DAL;
using BlazorCore.DAL.DataModels;
using BlazorCore.DAL.TableModels;
using BlazorCore.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class Plus
    {
        #region Public and private fields and properties

        [Parameter] public int ScaleId { get; set; }
        private List<PluEntity> Items { get; set; }

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationStrings.DeviceControl.Method} {nameof(SetParametersAsync)}", "", LocalizationStrings.Share.DialogResultFail, "",
                new List<Task> {
                    new(async() => {
                        Item = null;
                        Items = AppSettings.DataAccess.PluCrud.GetEntities(
                            new FieldListEntity(
                                new Dictionary<string, object> { 
                                    { "Scale.Id", ScaleId },
                                    { EnumField.Marked.ToString(), false },
                            }),
                            new FieldOrderEntity(EnumField.GoodsName, EnumOrderDirection.Asc))
                            .ToList();
                        await GuiRefreshAsync(false).ConfigureAwait(false);
                    }),
            }, true);
        }

        #endregion
    }
}