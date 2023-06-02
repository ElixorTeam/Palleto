﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Brands;

namespace DeviceControl.Pages.Menu.References1C.Brands;

public sealed partial class Brands : SectionBase<WsSqlBrandModel>
{
    #region Public and private fields, properties, constructor

    public Brands() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion
}