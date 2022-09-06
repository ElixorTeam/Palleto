﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core;

namespace BlazorDeviceControl.Razors.Sections;

public partial class SectionBarCodes : RazorPageModel
{
	#region Public and private fields, properties, constructor

	private List<BarCodeModel> ItemsCast
	{
		get => Items == null ? new() : Items.Select(x => (BarCodeModel)x).ToList();
		set => Items = !value.Any() ? null : new(value);
	}

	#endregion

	#region Public and private methods

	protected override void OnInitialized()
	{
		base.OnInitialized();

		Table = new TableScaleModel(ProjectsEnums.TableScale.BarCodes);
		IsShowMarkedFilter = true;
		ItemsCast = new();
	}

	protected override void OnParametersSet()
	{
		base.OnParametersSet();

		RunActionsSilent(new()
		{
			() =>
			{
				ItemsCast = AppSettings.DataAccess.GetListBarCodes(IsShowMarked, IsShowOnlyTop);

				ButtonSettings = new(true, true, true, true, true, false, false);
			}
		});
	}

	#endregion
}