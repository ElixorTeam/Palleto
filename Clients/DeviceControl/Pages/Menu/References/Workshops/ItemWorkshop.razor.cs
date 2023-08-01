// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.Workshops;

public sealed partial class ItemWorkshop : ItemBase<WsSqlWorkShopModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlProductionFacilityModel> ProductionFacilityModels { get; set; }

    #endregion

    #region Public and private methods

    protected override void SetSqlItemCast()
    {
        base.SetSqlItemCast();
        ProductionFacilityModels = new WsSqlAreaRepository().GetList(WsSqlCrudConfigFactory.GetCrudActual());
    }

    #endregion
}