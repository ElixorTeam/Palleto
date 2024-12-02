namespace Pl.Database.Entities.Ref1C.Bundles;

internal sealed class BundleMapConfig : IEntityTypeConfiguration<BundleEntity>
{
    public void Configure(EntityTypeBuilder<BundleEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Bundles, SqlSchemas.Ref1C);

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.Weight)
            .HasColumnName("WEIGHT")
            .IsRequired()
            .HasPrecision(4, 3);
    }
}