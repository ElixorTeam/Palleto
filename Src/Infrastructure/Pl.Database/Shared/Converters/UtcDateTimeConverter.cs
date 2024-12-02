using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Pl.Database.Shared.Converters;

internal class UtcDateTimeConverter() : ValueConverter<DateTime?, DateTime?>(ToDatabase(), FromDatabase())
{
    private static Expression<Func<DateTime?, DateTime?>> ToDatabase()
        => date => date.HasValue ? date.Value.ToUniversalTime() : null;

    private static Expression<Func<DateTime?, DateTime?>> FromDatabase()
        => date => date.HasValue ? DateTime.SpecifyKind(date.Value.Add(TimeSpan.FromHours(3)), DateTimeKind.Local) : null;
}