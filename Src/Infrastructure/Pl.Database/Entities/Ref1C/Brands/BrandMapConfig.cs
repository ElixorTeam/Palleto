namespace Pl.Database.Entities.Ref1C.Brands;

internal sealed class BrandMapConfig : IEntityTypeConfiguration<BrandEntity>
{
    public void Configure(EntityTypeBuilder<BrandEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Brands, SqlSchemas.Ref1C);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.Brands}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(32)")
            .IsRequired();
    }
}