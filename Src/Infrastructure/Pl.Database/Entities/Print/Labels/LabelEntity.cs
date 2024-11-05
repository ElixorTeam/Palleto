using Pl.Database.Entities.Ref.Arms;

namespace Pl.Database.Entities.Print.Labels;

public sealed class LabelEntity : EfEntityBase
{
    #region FK

    public Guid? PalletId { get; set; }
    public PalletEntity? Pallet { get; set; }

    public Guid? PluId { get; set; }
    public PluEntity? Plu { get; set; }

    public Guid ArmId { get; set; }
    public ArmEntity Arm { get; set; } = null!;

    public LabelZplEntity Zpl { get; set; } = new();

    #endregion

    #region ProductDt
    public DateTime ProductDt { get; set; }
    public DateTime ExpirationDt { get; set; }

    #endregion

    #region Barcodes

    public string BarcodeTop { get; set; } = string.Empty;
    public string BarcodeRight { get; set; } = string.Empty;
    public string BarcodeBottom { get; set; } = string.Empty;

    #endregion

    #region Weight

    public bool IsWeight { get; set; }
    public decimal WeightNet { get; set; }
    public decimal WeightTare { get; set; }

    #endregion

    #region Other

    public ushort BundleCount { get; set; }
    public short Kneading { get; set; }

    #endregion

    #region Date

    public DateTime CreateDt { get; init; }

    #endregion
}