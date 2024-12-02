namespace Pl.Database.Entities.Ref.Warehouses;

public sealed class WarehouseEntity : EfEntityBase
{
    public Guid Uid1C { get; set; }
    public string Name { get; set; } = string.Empty;

    #region ProductionSite

    public Guid ProductionSiteId { get; set; }
    public ProductionSiteEntity ProductionSite { get; set; } = null!;

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}