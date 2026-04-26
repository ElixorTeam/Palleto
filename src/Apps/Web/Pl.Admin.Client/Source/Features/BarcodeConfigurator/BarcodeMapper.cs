using Pl.Admin.Models.Features.References.Template.Universal;
using Pl.Print.Features.Barcodes.Models;
using Pl.Print.Features.Barcodes.Utils;
using Pl.Print.Shared.ValueTypes;

namespace Pl.Admin.Client.Source.Features.BarcodeConfigurator;

public static class BarcodeMapper
{
    public static BarcodeItemDto ExtendedDtoToDto(ExtendedBarcodeItemDto item) => item;

    public static BarcodeVar ItemDtoToVar(BarcodeItemDto item) => new(item.Property, item.FormatStr);

    public static ExtendedBarcodeItemDto DtoToExtendedDto(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        bool isConst = vars.Find(x => x.Property == item.Property) == null;
        return new()
        {
            Property = item.Property,
            FormatStr = item.FormatStr,
            CachedMask = item.FormatStr,
            Example = GetBarcodeExample(item, vars),
            IsConst = isConst,
            DefaultLength = isConst ? -1 : GetDefaultMaskLength(item, vars)
        };
    }

    public static int GetDefaultMaskLength(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        BarcodeVarInfo? barcode = GetTypedVariable(item, vars);
        if (barcode == null || barcode.Type == typeof(DateTime)) return -1;
        string defaultExample = TryGetFormatedValue(barcode.Format, barcode.Example);
        return string.IsNullOrWhiteSpace(defaultExample) ? -1 : defaultExample.Count(char.IsDigit);
    }

    public static string GetBarcodeExample(BarcodeItemDto item, List<BarcodeVarInfo> vars)
    {
        BarcodeVarInfo? typedVariable = GetTypedVariable(item, vars);
        object objectToFormat = typedVariable == null ? item.Property : typedVariable.Example;
        return new BarcodeResult(TryGetFormatedValue(item.FormatStr, objectToFormat)).Friendly;
    }

    private static BarcodeVarInfo? GetTypedVariable(BarcodeItemDto item, List<BarcodeVarInfo> vars) =>
        vars.Find(x => x.Property == item.Property);

    private static string TryGetFormatedValue(string mask, object value) =>
        BarcodeVarUtils.TryFormat(value, mask, out string? result) ? result : string.Empty;
}