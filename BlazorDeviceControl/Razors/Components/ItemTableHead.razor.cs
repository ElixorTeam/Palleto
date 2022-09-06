﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.CssStyles;

namespace BlazorDeviceControl.Razors.Components;

public partial class ItemTableHead : RazorPageModel
{
	#region Public and private fields, properties, constructor

	[Parameter] public TableHeadStyleModel? TableStyle { get; set; }

	#endregion
}