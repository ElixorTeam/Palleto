﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataShareCore;
using DataShareCore.DAL.Interfaces;
using DataShareCore.DAL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BlazorProjectsCore.Models
{
    public class BaseRazorIdEntity : BaseRazorEntity
    {
        #region Public and private fields and properties

        [Parameter] public int Id { get; set; }
        public IBaseIdEntity IdItem { get => (BaseIdEntity)Item; set => SetItem(value); }

        #endregion

        #region Constructor and destructor

        public BaseRazorIdEntity() { }

        #endregion

        #region Public and private methods

        public void SetItem(IBaseIdEntity item)
        {
            SetItem((IBaseEntity)item);
        }

        public void SetParentItem(IBaseIdEntity parentItem)
        {
            SetParentItem((IBaseEntity)parentItem);
        }

        public async Task ItemSelectAsync(IBaseIdEntity item)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            RunTasks($"{LocalizationCore.Strings.Method} {nameof(ItemSelectAsync)}", "", LocalizationCore.Strings.DialogResultFail, "",
                new Task(async() => {
                    IdItem = item;
                    await GuiRefreshWithWaitAsync();
                }), true);
        }

        #endregion
    }
}
