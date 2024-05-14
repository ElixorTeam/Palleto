namespace Ws.Database.EntityFramework.Entities.Ref1C.Boxes;

[Table(SqlTables.Boxes, Schema = SqlSchemas.Ref1C)]
public sealed class BoxEntity : EfEntityBase
{

    // public ICollection<PluNestingEntity> PlusNestingFks { get; set; } = [];

    public BoxEntity() { }

    public BoxEntity(Guid uid, string name, decimal weight, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        Weight = weight;
        ChangeDt = updateDate;
    }

    [Column(SqlColumns.Name), StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}