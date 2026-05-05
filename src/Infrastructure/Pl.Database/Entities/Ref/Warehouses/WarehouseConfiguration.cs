namespace Pl.Database.Entities.Ref.Warehouses;

internal sealed class WarehouseConfiguration : IEntityTypeConfiguration<WarehouseEntity>
{
    public void Configure(EntityTypeBuilder<WarehouseEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.Warehouses, DbSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{DbTables.Warehouses}__NAME")
            .IsUnique();

        builder.HasIndex(e => e.Uid1C)
            .HasDatabaseName($"UQ_{DbTables.Warehouses}__{DbColumns.Uid1C}")
            .IsUnique();

        #endregion

        #region FK

        builder.Property(e => e.ProductionSiteId)
            .HasColumnName("PRODUCTION_SITE_UID")
            .IsRequired();

        builder.HasOne(e => e.ProductionSite)
            .WithMany()
            .HasForeignKey(e => e.ProductionSiteId)
            .HasConstraintName($"FK_{DbTables.Warehouses}__PRODUCTION_SITE")
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(DbColumns.Name)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.Uid1C)
            .HasColumnName(DbColumns.Uid1C)
            .IsRequired();
    }
}