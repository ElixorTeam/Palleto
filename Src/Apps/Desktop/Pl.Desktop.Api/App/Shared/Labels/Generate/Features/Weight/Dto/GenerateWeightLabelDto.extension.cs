using Pl.Print.Features.Barcodes;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;

internal static class GenerateWeightPluDtoExtension
{
    internal static BarcodeBuilder ToBarcodeBuilder(this GenerateWeightLabelDto dto) =>
        new()
        {
            ArmNumber = (uint)dto.Arm.Number,
            ArmCounter = (uint)dto.Arm.Counter,
            ProductDt = dto.ProductDt,
            PluGtin = dto.Plu.Gtin,
            PluNumber = (ushort)dto.Plu.Number,
            PluEan13 = dto.Plu.Ean13,
            WeightNet = dto.Plu.Weight,
            Kneading = (ushort)dto.Kneading,
            ExpirationDt = dto.ExpirationDt,
            BundleCount = (ushort)dto.Nesting.BundleCount,
            ExpirationDay = (ushort)dto.ExpirationDt.DayOfYear
        };
}