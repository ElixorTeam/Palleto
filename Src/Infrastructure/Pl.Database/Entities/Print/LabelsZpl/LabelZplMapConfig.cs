namespace Pl.Database.Entities.Print.LabelsZpl;

internal sealed class LabelZplMapConfig : IEntityTypeConfiguration<LabelZplEntity>
{
    public void Configure(EntityTypeBuilder<LabelZplEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.LabelsZpl, SqlSchemas.Print);

        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("UID");

        #endregion

        #region FK

        builder.HasOne<LabelEntity>()
            .WithOne(l => l.Zpl)
            .HasForeignKey<LabelZplEntity>(n => n.Id)
            .HasPrincipalKey<LabelEntity>(p => p.Id)
            .HasConstraintName($"FK_{SqlTables.LabelsZpl}__LABEL")
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        builder.Property(e => e.Zpl)
            .HasColumnName("ZPL")
            .IsUnicode(false)
            .HasColumnType("varchar(8000)")
            .IsRequired();

        builder.Property(e => e.Rotate)
            .HasColumnName("ROTATE")
            .IsRequired();

        builder.Property(e => e.Width)
            .HasColumnName("WIDTH")
            .IsRequired();

        builder.Property(e => e.Height)
            .HasColumnName("HEIGHT")
            .IsRequired();
    }
}