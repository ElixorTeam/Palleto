namespace Pl.Database.Entities.Ref1C.Brands;

internal sealed class BrandConfiguration : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.Brands, DbSchemas.Ref1C);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{DbTables.Brands}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(DbColumns.Name)
            .HasColumnType("varchar(32)")
            .IsRequired();
    }
}