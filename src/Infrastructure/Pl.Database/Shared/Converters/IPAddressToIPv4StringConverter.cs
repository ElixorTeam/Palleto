using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Pl.Database.Shared.Converters;

internal class IpAddressToIPv4StringConverter(ConverterMappingHints? mappingHints)
    : ValueConverter<IPAddress?, string?>(ToDatabase(), FromDatabase(), DefaultHints.With(mappingHints))
{
    private static readonly ConverterMappingHints DefaultHints = new(size: 15);

    public IpAddressToIPv4StringConverter()
        : this(null)
    {
    }

    private static Expression<Func<IPAddress?, string?>> ToDatabase()
        => v => v!.MapToIPv4().ToString();

    private static Expression<Func<string?, IPAddress?>> FromDatabase()
        => v => v != null ? IPAddress.Parse(v).MapToIPv4() : null;
}