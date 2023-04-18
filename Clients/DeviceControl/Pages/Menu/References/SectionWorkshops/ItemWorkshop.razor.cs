// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.WorkShops;

namespace BlazorDeviceControl.Pages.Menu.References.SectionWorkshops;

public sealed partial class ItemWorkshop : RazorComponentItemBase<WorkShopModel>
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
				SqlItemCast = ContextManager.GetItemNotNullable<WorkShopModel>(IdentityId);
				//if (TableAction == DbTableAction.New)
				//	SqlItemCast.IdentityValueId = (long)IdentityId;
				ContextManager.GetListNotNullable<ProductionFacilityModel>(WsSqlCrudConfigUtils.GetCrudConfigComboBox());
            }
		});
	}

	#endregion
}
