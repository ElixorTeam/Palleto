using Pl.Database.Entities.Ref.Arms;

namespace Pl.Database.Entities.Print.Pallets;

public class PalletEntity
{
    public Guid Id { get; set; }

    #region FK

    public ArmEntity Arm { get; set; } = null!;
    public WarehouseEntity Warehouse { get; set; } = null!;
    public PalletManEntity PalletMan { get; set; } = null!;

    #endregion

    public uint Counter { get; set; }
    public bool IsShipped { get; set; }
    public decimal TrayWeight { get; set; }
    public DateTime ProductDt { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Barcode { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime? DeletedAt { get; set; }

    #endregion
}