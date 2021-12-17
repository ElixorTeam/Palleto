﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataProjectsCore;
using DataProjectsCore.DAL;
using DataProjectsCore.DAL.TableSystemModels;
using DataProjectsCore.Models;
using DataShareCore;
using DataShareCore.DAL.Interfaces;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Sys
{
    public partial class Access
    {
        #region Public and private fields and properties

        private List<AccessEntity> ItemsCast => Items == null ? new List<AccessEntity>() : Items.Select(x => (AccessEntity)x).ToList();

        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
                    UidItem = null;
                    Items = null;
                    await GuiRefreshWithWaitAsync();

                    object[] objects = AppSettings.DataAccess.GetEntitiesNativeObject(SqlQueries.DbServiceManaging.Tables.Access.GetAccess, string.Empty, 0, string.Empty);
                    Items = new List<AccessEntity>().ToList<IBaseEntity>();
                    foreach (object obj in objects)
                    {
                        if (obj is object[] { Length: 5 } item)
                        {
                            if (Guid.TryParse(Convert.ToString(item[0]), out Guid uid))
                            {
                                Items.Add(new AccessEntity()
                                {
                                    Uid = uid,
                                    CreateDt = Convert.ToDateTime(item[1]),
                                    ChangeDt = Convert.ToDateTime(item[2]),
                                    User = Convert.ToString(item[3]),
                                    Level = item[4] == null ? null : Convert.ToBoolean(item[4]),
                                });
                            }
                        }
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
