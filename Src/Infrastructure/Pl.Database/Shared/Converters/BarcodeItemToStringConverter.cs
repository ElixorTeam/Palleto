using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pl.Shared.ValueTypes;

// ReSharper disable ConvertToPrimaryConstructor
// ReSharper disable MemberCanBePrivate.Global
namespace Pl.Database.Shared.Converters;

internal class BarcodeItemListComparer : ValueComparer<List<BarcodeItem>>
{
    public BarcodeItemListComparer()
        : base(
        (c1, c2) => SequenceEqual(c1, c2),
        c => c.Aggregate(0, HashCode.Combine),
        c => new(c)
        )
    { }

    private static bool SequenceEqual(List<BarcodeItem>? c1, List<BarcodeItem>? c2)
    {
        if (c1 == null || c2 == null) return c1 == c2;
        return c1.SequenceEqual(c2);
    }
}

public class BarcodeItemListConverter : ValueConverter<List<BarcodeItem>, string?>
{
    private static readonly ConverterMappingHints DefaultHints = new(size: 2048);

    public BarcodeItemListConverter(ConverterMappingHints? mappingHints)
        : base(
        list => SerializeJson(list),
        json => DeserializeJson(json),
        DefaultHints.With(mappingHints)
        )
    { }

    public BarcodeItemListConverter() : this(DefaultHints) { }

    private static List<BarcodeItem> DeserializeJson(string? json)
    {
        if (string.IsNullOrWhiteSpace(json)) return [];
        return JsonSerializer.Deserialize<List<BarcodeItem>>(json) ?? [];
    }

    private static string SerializeJson(List<BarcodeItem> list) => JsonSerializer.Serialize(list);
}