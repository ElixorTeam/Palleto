namespace Ws.Database.EntityFramework.Entities.Ref1C.Clips;

public sealed class ClipEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public decimal Weight { get; set; }

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public ClipEntity() { }

    public ClipEntity(Guid uid, string name, decimal weight, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        Weight = weight;
        ChangeDt = updateDate;
    }
}