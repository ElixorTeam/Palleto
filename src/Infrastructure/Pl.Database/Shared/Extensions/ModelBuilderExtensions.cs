using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pl.Database.Shared.Converters;

namespace Pl.Database.Shared.Extensions;

internal static class ModelBuilderExtensions
{
    #region Set

    public static void SetDefaultTypeForString(this ModelBuilder modelBuilder)
    {
         foreach (IMutableEntityType entity in modelBuilder.Model.GetEntityTypes())
         foreach (IMutableProperty property in entity.GetProperties().Where(i => i.ClrType == typeof(string)))
         {
             int? maxValue = property.GetMaxLength();
             property.SetColumnType($"varchar({(object?)maxValue ?? "max"})");
         }
    }

    public static void SetAutoCreateOrChangeDt(this ModelBuilder modelBuilder)
    {
        const string getDateCmd = "GETUTCDATE()";
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IMutableProperty? createDtProperty = entityType.FindProperty(nameof(SqlColumns.CreateDt)) ?? null;
            IMutableProperty? changeDtProperty = entityType.FindProperty(nameof(SqlColumns.ChangeDt)) ?? null;

            if (createDtProperty != null)
            {
                createDtProperty.ValueGenerated = ValueGenerated.OnAdd;
                createDtProperty.SetColumnName(SqlColumns.CreateDt);
                createDtProperty.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
                createDtProperty.SetDefaultValueSql(getDateCmd);
            }

            if (changeDtProperty != null)
            {
                changeDtProperty.SetColumnName(SqlColumns.ChangeDt);
                changeDtProperty.SetDefaultValueSql(getDateCmd);
            }
        }
    }

    #endregion

    #region Use

    public static void UseIpAddressConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> ipAddressProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(IPAddress));

            foreach (PropertyInfo property in ipAddressProperties)
                modelBuilder.Entity(entityType.Name).Property(property.Name)
                    .HasConversion(new IpAddressToIPv4StringConverter());
        }
    }

    public static void UseDateTimeConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> dateTimeProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));

            foreach (PropertyInfo property in dateTimeProperties)
                modelBuilder.Entity(entityType.Name).Property(property.Name)
                    .HasConversion(new UtcDateTimeConverter());
        }
    }

    public static void UseEnumStringConversion(this ModelBuilder modelBuilder)
    {
        foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
        {
            IEnumerable<PropertyInfo> enumProperties = entityType.ClrType.GetProperties()
                .Where(p => p.PropertyType.IsEnum);

            foreach (PropertyInfo property in enumProperties)
            {
                Type converterType = typeof(EnumToStringConverter<>)
                    .MakeGenericType(property.PropertyType);

                int maxLength = Enum.GetNames(property.PropertyType).Max(name => name.Length);

                ValueConverter converter = (ValueConverter)Activator.CreateInstance(converterType)!;

                modelBuilder.Entity(entityType.Name).Property(property.Name).HasConversion(converter)
                    .HasColumnType($"varchar({maxLength})");
            }
        }
    }

    #endregion
}