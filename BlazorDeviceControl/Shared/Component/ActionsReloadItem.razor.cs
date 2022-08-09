﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Localizations;
using Microsoft.AspNetCore.Components;

namespace BlazorDeviceControl.Shared.Component;

public partial class ActionsReloadItem : ActionsReloadBase
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    public override async Task SetParametersAsync(ParameterView parameters)
    {
	    await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(true);
	    SetParametersAsyncWithAction(parameters, () => base.SetParametersAsync(parameters).ConfigureAwait(true),
		    null, () =>
            {
				//
            });
    }

    #endregion
}
