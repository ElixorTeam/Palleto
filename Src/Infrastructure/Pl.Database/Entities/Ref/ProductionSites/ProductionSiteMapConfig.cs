namespace Pl.Database.Entities.Ref.ProductionSites;

internal sealed class ProductionSiteMapConfig : IEntityTypeConfiguration<ProductionSiteEntity>
{
    public void Configure(EntityTypeBuilder<ProductionSiteEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.ProductionSites, SqlSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.ProductionSites}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.Address)
            .HasColumnName("ADDRESS")
            .HasColumnType("varchar(128)")
            .IsRequired();
    }
}