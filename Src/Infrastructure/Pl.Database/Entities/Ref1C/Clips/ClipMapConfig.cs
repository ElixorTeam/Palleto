namespace Pl.Database.Entities.Ref1C.Clips;

internal sealed class ClipMapConfig : IEntityTypeConfiguration<ClipEntity>
{
    public void Configure(EntityTypeBuilder<ClipEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Clips, SqlSchemas.Ref1C);

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