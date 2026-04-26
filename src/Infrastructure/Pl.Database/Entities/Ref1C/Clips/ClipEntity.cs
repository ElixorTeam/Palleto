namespace Pl.Database.Entities.Ref1C.Clips;

public sealed class ClipEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}