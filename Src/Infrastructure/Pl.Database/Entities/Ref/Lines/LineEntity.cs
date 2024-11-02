using Pl.Shared.Enums;

namespace Pl.Database.Entities.Ref.Lines;

public sealed class LineEntity : EfEntityBase
{
    public Guid SystemKey { get; set; }
    public int Number { get; set; }
    public int Counter { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Version { get; set; } = string.Empty;
    public ArmType Type { get; set; } = ArmType.Tablet;

    #region FK

    public PrinterEntity Printer { get; set; } = new();
    public WarehouseEntity Warehouse { get; set; } = new();
    public ICollection<PluEntity> Plus { get; set; } = [];

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}