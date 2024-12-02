namespace Pl.Database.Entities.Ref1C.Plus;

internal sealed class PluMapConfig : IEntityTypeConfiguration<PluEntity>
{
    public void Configure(EntityTypeBuilder<PluEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Plus, SqlSchemas.Ref1C);

        builder.HasIndex(e => e.Number)
            .HasDatabaseName($"UQ_{SqlTables.Plus}__NUMBER")
            .IsUnique();

        #endregion

        #region FK

        builder.Property(e => e.BundleId)
            .HasColumnName("BUNDLE_UID");

        builder.HasOne(e => e.Bundle)
            .WithMany()
            .HasForeignKey(plu => plu.BundleId)
            .HasConstraintName($"FK_{SqlTables.Plus}__BUNDLE")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        //

        builder.Property(e => e.ClipId)
            .HasColumnName("CLIP_UID");

        builder.HasOne(e => e.Clip)
            .WithMany()
            .HasForeignKey(plu => plu.ClipId)
            .HasConstraintName($"FK_{SqlTables.Plus}__CLIP")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        //

        builder.Property(e => e.BrandId)
            .HasColumnName("BRAND_UID");

        builder.HasOne(e => e.Brand)
            .WithMany()
            .HasForeignKey(plu => plu.BrandId)
            .HasConstraintName($"FK_{SqlTables.Plus}__BRAND")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        //

        builder.Property(e => e.TemplateId)
            .HasColumnName("TEMPLATE_UID");

        builder.HasOne(e => e.Template)
            .WithMany()
            .HasForeignKey(e => e.TemplateId)
            .HasConstraintName($"FK_{SqlTables.Plus}__TEMPLATE")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(100)")
            .IsRequired();

        builder.Property(e => e.FullName)
            .HasColumnName("FULL_NAME")
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(e => e.Description)
            .HasColumnName("DESCRIPTION")
            .HasColumnType("varchar(200)")
            .IsRequired();

        builder.Property(e => e.Number)
            .HasColumnName("NUMBER")
            .IsRequired();

        builder.Property(e => e.ShelfLifeDays)
            .HasColumnName("SHELF_LIFE_DAYS")
            .IsRequired();

        builder.Property(e => e.Ean13)
            .HasColumnName("EAN_13")
            .HasColumnType("varchar(13)")
            .IsRequired();

        builder.Property(e => e.Itf14)
            .HasColumnName("ITF_14")
            .HasColumnType("varchar(14)")
            .IsRequired();

        builder.Property(e => e.IsWeight)
            .HasColumnName("IS_WEIGHT")
            .IsRequired();

        builder.Property(e => e.Weight)
            .HasColumnName("WEIGHT")
            .HasPrecision(4, 3)
            .IsRequired();

        builder.Property(e => e.StorageMethod)
            .HasColumnName("STORAGE_METHOD")
            .HasColumnType("varchar(32)")
            .IsRequired();
    }
}