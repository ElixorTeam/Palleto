﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Razors;

namespace BlazorDeviceControl.Razors.ItemComponents.Others;

public partial class ItemContragent : RazorComponentItemBase<ContragentModel>
{
	#region Public and private fields, properties, constructor

	public ItemContragent()
	{
		//
	}

	#endregion

	#region Public and private methods

	protected override void OnParametersSet()
	{
		RunActionsParametersSet(new()
		{
			() =>
			{
				switch (TableAction)
				{
					case SqlTableActionEnum.New:
						SqlItemCast = new();
						SqlItemCast.SetDtNow();
						SqlItemCast.IsMarked = false;
						SqlItemCast.Name = "NEW CONTRAGENT";
						break;
					default:
						SqlItemCast = AppSettings.DataAccess.GetItemByUidNotNull<ContragentModel>(IdentityUid);
						break;
				}

				ButtonSettings = new(false, false, false, false, false, true, true);
			}
		});
	}

	#endregion
}