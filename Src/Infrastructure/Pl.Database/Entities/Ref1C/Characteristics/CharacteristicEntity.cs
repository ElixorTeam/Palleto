namespace Pl.Database.Entities.Ref1C.Characteristics;

public sealed class CharacteristicEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public short BundleCount { get; set; }

    #region Box

    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = null!;

    #endregion

    public Guid PluId { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}