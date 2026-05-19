using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pl.Database.Shared.Converters;

namespace Pl.Database.Shared.Extensions;

internal static class ModelBuilderExtensions
{
    public static void SetAutoCreateOrChangeDt(this ModelBuilder modelBuilder)
    {
        const string getDateCmd = "GETUTCDATE()";
        ForEachEntity(modelBuilder, entity => {
            IMutableProperty? createDtProperty = entity.FindProperty(nameof(DbColumns.CreateDt)) ?? null;
            IMutableProperty? changeDtProperty = entity.FindProperty(nameof(DbColumns.ChangeDt)) ?? null;

            if (createDtProperty != null)
            {
                createDtProperty.ValueGenerated = ValueGenerated.OnAdd;
                createDtProperty.SetColumnName(DbColumns.CreateDt);
                createDtProperty.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                createDtProperty.SetDefaultValueSql(getDateCmd);
            }

            if (changeDtProperty != null)
            {
                changeDtProperty.SetColumnName(DbColumns.ChangeDt);
                changeDtProperty.SetDefaultValueSql(getDateCmd);
            }
        });
    }

    public static void UseIpAddressConversion(this ModelBuilder modelBuilder)
    {
        ForEachEntity(modelBuilder, entity => {
            IEnumerable<PropertyInfo> props = entity.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(IPAddress));

            foreach (PropertyInfo property in props)
            {
                modelBuilder.Entity(entity.Name)
                    .Property(property.Name)
                    .HasConversion(new IpAddressToIPv4StringConverter());
            }
        });
    }

    public static void UseDateTimeConversion(this ModelBuilder modelBuilder)
    {
        ForEachEntity(modelBuilder, entity => {
            IEnumerable<PropertyInfo> dateTimeProperties = entity.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (PropertyInfo property in dateTimeProperties)
            {
                modelBuilder.Entity(entity.Name).Property(property.Name)
                    .HasConversion(new UtcDateTimeConverter());
            }
        });
    }

    public static void UseEnumStringConversion(this ModelBuilder modelBuilder)
    {
        ForEachEntity(modelBuilder, entity => {
            IEnumerable<PropertyInfo> enumProps = entity.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (PropertyInfo property in enumProps)
            {
                Type enumType = property.PropertyType;

                Type converterType = typeof(EnumToStringConverter<>)
                    .MakeGenericType(enumType);

                ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                int maxLength = Enum.GetNames(enumType).Max(x => x.Length);

                modelBuilder.Entity(entity.Name)
                    .Property(property.Name)
                    .HasConversion(converter)
                    .HasColumnType($"varchar({maxLength})");
            }
        });
    }

    private static void ForEachEntity(ModelBuilder modelBuilder, Action<IMutableEntityType> action)
    {
        foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
            action(entity);
    }
}