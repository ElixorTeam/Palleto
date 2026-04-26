namespace Pl.Database.Entities.Ref1C.Boxes;

internal sealed class BoxMapConfig : IEntityTypeConfiguration<BoxEntity>
{
    public void Configure(EntityTypeBuilder<BoxEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Boxes, SqlSchemas.Ref1C);

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