﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Section;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleModels.Boxes;

namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class Boxes : SectionBase<WsSqlBoxModel>
{
    #region Public and private fields, properties, constructor

    public Boxes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    #endregion
}