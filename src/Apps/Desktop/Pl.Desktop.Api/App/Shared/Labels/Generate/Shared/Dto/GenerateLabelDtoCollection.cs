namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Shared.Dto;

public record PalletForLabel(
    string Barcode,
    Guid WarehouseId1C,
    Guid PalletManId1C
);

public record ArmForLabel(
    Guid Id,
    short Number,
    string Name,
    string Address,
    int Counter
);

public record PluFolLabel(
    Guid Id,
    Guid? TemplateId,
    string Gtin,
    short Number,
    short ShelfLifeDays,
    string Ean13,
    string FullName,
    string Description,
    string StorageMethod,
    decimal Weight,
    decimal ClipWeight,
    decimal BundleWeight
);

public record NestingForLabel(
    Guid Id,
    decimal BoxWeight,
    short BundleCount
)
{
    public decimal CalculateWeightTare(PluFolLabel plu) => (plu.BundleWeight + plu.ClipWeight) * BundleCount + BoxWeight;
    public decimal CalculateWeightNet(PluFolLabel plu) => plu.Weight * BundleCount;
}