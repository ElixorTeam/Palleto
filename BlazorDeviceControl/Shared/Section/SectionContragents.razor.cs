﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using DataCore.DAL.Models;
using DataCore.DAL.TableScaleModels;
using DataCore.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataCore.ShareEnums;

namespace BlazorDeviceControl.Shared.Section
{
    public partial class SectionContragents
    {
        #region Public and private fields and properties

        private List<ContragentEntityV2>? ItemsCast => Items?.Select(x => (ContragentEntityV2)x).ToList();
        private readonly object _locker = new();

        #endregion

        #region Constructor and destructor

        public SectionContragents()
        {
            Default();
        }

        #endregion

        #region Public and private methods

        private void Default()
        {
            lock (_locker)
            {
                Table = new TableScaleEntity(ProjectsEnums.TableScale.Contragents);
                Items = null;
                ButtonSettings = new();
            }
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters).ConfigureAwait(true);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(SetParametersAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async () =>
                {
                    Default();
                    await GuiRefreshWithWaitAsync();
                    
                    lock (_locker)
                    {
                        if (AppSettings.DataAccess != null)
                            Items = AppSettings.DataAccess.Crud.GetEntities<ContragentEntityV2>(
                                (IsShowMarkedItems == true) ? null
                                    : new FieldListEntity(new Dictionary<string, object?> { { DbField.IsMarked.ToString(), false } }),
                                new FieldOrderEntity(DbField.Name, DbOrderDirection.Asc))
                            ?.ToList<BaseEntity>();
                        ButtonSettings = new(true, true, true, true, true, false, false);
                    }
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
