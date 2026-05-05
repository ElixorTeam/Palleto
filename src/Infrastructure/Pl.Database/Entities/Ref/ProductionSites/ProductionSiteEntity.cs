namespace Pl.Database.Entities.Ref.ProductionSites;

public sealed class ProductionSiteEntity : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}