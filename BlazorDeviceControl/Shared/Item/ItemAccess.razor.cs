﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Item
{
    public partial class ItemAccess
    {
        #region Public and private fields and properties

        public AccessEntity? ItemCast { get => Item == null ? null : (AccessEntity)Item; set => Item = value; }
        private readonly object _locker = new();
        public List<TypeEntity<AccessRights>>? TemplateAccessRights { get; set; }
        public AccessRights Rights
        {
            get => ItemCast == null ? AccessRights.None : (AccessRights)ItemCast.Rights;
            set { if (ItemCast != null) ItemCast.Rights = (byte)value; }
        }


        #endregion

        #region Public and private methods

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    lock (_locker)
                    {
                        Table = new TableSystemEntity(ProjectsEnums.TableSystem.Accesses);
                        ItemCast = null;
                        TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights();
                        ButtonSettings = new();
                    }
                    await GuiRefreshWithWaitAsync();

                    lock (_locker)
                    {
                        switch (TableAction)
                        {
                            case DbTableAction.New:
                                ItemCast = new();
                                ItemCast.ChangeDt = ItemCast.CreateDt = DateTime.Now;
                                ItemCast.IsMarked = false;
                                ItemCast.User = "NEW USER";
                                break;
                            default:
                                ItemCast = AppSettings.DataAccess.Crud.GetEntity<AccessEntity>(
                                    new FieldListEntity(new Dictionary<string, object?>
                                    { { DbField.Uid.ToString(), Uid } }), null);
                                break;
                        }
                        TemplateAccessRights = AppSettings.DataSourceDics.GetTemplateAccessRights(ItemCast.Rights);
                        ButtonSettings = new(false, false, false, false, false, true, true);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
