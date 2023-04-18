﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Brands;
using WsStorageCore.TableScaleModels.Brands;

namespace BlazorDeviceControl.Pages.Menu.References1C.SectionBrands;

public sealed partial class ItemBrand : RazorComponentItemBase<BrandModel>
{
    #region Public and private fields, properties, constructor

    //

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = ContextManager.GetItemNotNullable<BrandModel>(IdentityUid);
                if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<BrandModel>();
                }
            }
        });
    }

    #endregion
}
