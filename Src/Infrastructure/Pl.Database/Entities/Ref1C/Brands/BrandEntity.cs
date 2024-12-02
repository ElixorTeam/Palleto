namespace Pl.Database.Entities.Ref1C.Brands;

public sealed class BrandEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}