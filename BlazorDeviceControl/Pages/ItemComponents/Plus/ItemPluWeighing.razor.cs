﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.PlusWeighings;

namespace BlazorDeviceControl.Pages.ItemComponents.Plus;

public partial class ItemPluWeighing : RazorComponentItemBase<PluWeighingModel>
{
    #region Public and private fields, properties, constructor

    public ItemPluWeighing() : base()
    {
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

	protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = DataAccess.GetItemNotNullable<PluWeighingModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<PluWeighingModel>();
                }
            }
        });
    }

    #endregion
}