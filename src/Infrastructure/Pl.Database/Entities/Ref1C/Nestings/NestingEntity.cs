namespace Pl.Database.Entities.Ref1C.Nestings;

public sealed class NestingEntity : EntityBase
{
    public short BundleCount { get; set; }

    #region Box

    public Guid BoxId { get; set; }
    public BoxEntity Box { get; set; } = null!;

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}