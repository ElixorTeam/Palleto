﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BlazorDeviceControl.Shared.Component
{
    public partial class ActionsReloadBase : BlazorCore.Models.RazorBase
    {
        #region Public and private fields and properties

        [Parameter] public string Title { get; set; } = string.Empty;
        [Parameter] public EventCallback<ParameterView> SetParameters { get; set; }
        public string ItemsCountResult => $"{LocalizationCore.Strings.Main.ItemsCount}: {(Items == null ? 0 : Items.Count):### ### ###}";

        #endregion

        #region Constructor and destructor

        public ActionsReloadBase() : base()
        {
            //
        }

        #endregion

        #region Public and private methods

        public void Default()
        {
            IsLoaded = false;
            if (ParentRazor != null)
            {
                IsShowAdditionalFilter = ParentRazor.IsShowAdditionalFilter;
                IsShowItemsCount = ParentRazor.IsShowItemsCount;
                IsShowMarkedFilter = ParentRazor.IsShowMarkedFilter;
                IsShowMarkedItems = ParentRazor.IsShowMarkedItems;
                IsShowTop100 = ParentRazor.IsShowTop100;
            }
        }

        #endregion
    }
}
