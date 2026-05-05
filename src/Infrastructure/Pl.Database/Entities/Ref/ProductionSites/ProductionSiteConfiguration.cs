namespace Pl.Database.Entities.Ref.ProductionSites;

internal sealed class ProductionSiteConfiguration : IEntityTypeConfiguration<ProductionSiteEntity>
{
    public void Configure(EntityTypeBuilder<ProductionSiteEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.ProductionSites, DbSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{DbTables.ProductionSites}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(DbColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.Address)
            .HasColumnName("ADDRESS")
            .HasColumnType("varchar(128)")
            .IsRequired();
    }
}