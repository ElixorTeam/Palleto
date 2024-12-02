using Pl.Admin.Models.Features.References.Template.Universal;
using Pl.Print.Features.Barcodes.Models;
using Pl.Shared.ValueTypes;

namespace Pl.Admin.Api.App.Features.References.Templates.Impl.Extensions;

internal static class BarcodeItemExtensions
{
    public static List<BarcodeItemDto> ToDto(this List<BarcodeItem> item)
    {
        return item.ConvertAll(i => new BarcodeItemDto
        {
            Property = i.Property,
            FormatStr = i.Format,
        });
    }

    public static List<BarcodeItem> ToItem(this List<BarcodeItemDto> item)
    {
        return item.ConvertAll(i => new BarcodeItem
        {
            Property = i.Property,
            Format = i.FormatStr
        });
    }

    public static List<BarcodeVar> ToBarcodeVar(this List<BarcodeItemDto> item) =>
        item.ConvertAll(i => new BarcodeVar(i.Property, i.FormatStr));
}