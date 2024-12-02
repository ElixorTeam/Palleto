namespace Pl.Database.Entities.Print.Pallets;

internal sealed class PalletMapConfig : IEntityTypeConfiguration<PalletEntity>
{
    public void Configure(EntityTypeBuilder<PalletEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Pallets, SqlSchemas.Print);

        builder.Property(e => e.Id)
            .HasColumnName("UID")
            .IsRequired();

        builder
            .HasKey(e => e.Id)
            .HasName($"PK_{SqlTables.Pallets}");

        builder.HasIndex(e => e.Barcode)
            .HasDatabaseName($"UQ_{SqlTables.Pallets}__BARCODE")
            .IsUnique();

        builder.HasIndex(e => e.Number)
            .HasDatabaseName($"UQ_{SqlTables.Pallets}__NUMBER")
            .IsUnique();

        builder.HasIndex(e => e.Counter)
            .HasDatabaseName($"UQ_{SqlTables.Pallets}__COUNTER")
            .IsUnique();

        #endregion

        #region FK

        builder.HasOne(e => e.Arm)
            .WithMany()
            .HasForeignKey("ARM_UID")
            .HasConstraintName($"FK_{SqlTables.Pallets}__ARM")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(e => e.PalletMan)
            .WithMany()
            .HasForeignKey("PALLET_MAN_UID")
            .HasConstraintName($"FK_{SqlTables.Pallets}__PALLET_MAN")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(e => e.Warehouse)
            .WithMany()
            .HasForeignKey("WAREHOUSE_UID")
            .HasConstraintName($"FK_{SqlTables.Pallets}__WAREHOUSE")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        #endregion

        builder.Property(e => e.IsShipped)
            .HasColumnName("IS_SHIPPED")
            .IsRequired();

        builder.Property(e => e.Counter)
            .HasColumnName("COUNTER")
            .IsRequired();

        builder.Property(e => e.Number)
            .HasColumnName("NUMBER")
            .HasColumnType("varchar(11)")
            .IsRequired();

        builder.Property(e => e.TrayWeight)
            .HasColumnName("WEIGHT_TRAY")
            .IsRequired()
            .HasPrecision(5, 3);

        builder.Property(e => e.Barcode)
            .HasColumnName("BARCODE")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.ProductDt)
            .HasColumnName("PRODUCT_DT")
            .IsRequired();

        builder.Property(e => e.DeletedAt)
            .HasColumnName("DELETED_AT");
    }
}