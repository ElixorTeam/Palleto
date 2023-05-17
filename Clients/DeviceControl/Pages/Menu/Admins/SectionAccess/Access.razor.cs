﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Access;

namespace BlazorDeviceControl.Pages.Menu.Admins.SectionAccess;

public sealed partial class Access : RazorComponentSectionBase<WsSqlAccessModel>
{
    #region Public and private fields, properties, constructor

    public Access() : base()
    {
        SqlCrudConfigSection.IsResultOrder = true;
    }

    #endregion

    #region Public and private methods

    #endregion
}
