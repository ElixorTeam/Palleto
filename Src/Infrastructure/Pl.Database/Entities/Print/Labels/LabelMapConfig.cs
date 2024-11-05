namespace Pl.Database.Entities.Print.Labels;

internal sealed class LabelMapConfig : IEntityTypeConfiguration<LabelEntity>
{
    public void Configure(EntityTypeBuilder<LabelEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Labels, SqlSchemas.Print);

        builder.HasIndex(e => e.BarcodeTop)
            .HasDatabaseName($"UQ_{SqlTables.Labels}__BARCODE_TOP")
            .IsUnique();

        #endregion

        #region FK

        builder.Property(e => e.PalletId)
            .HasColumnName("PALLET_UID");

        builder.HasOne(i => i.Pallet)
            .WithMany()
            .HasForeignKey(e => e.PalletId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName($"FK_{SqlTables.Labels}__PALLET")
            .IsRequired(false);

        //

        builder.Property(e => e.PluId)
            .HasColumnName("PLU_UID").IsRequired(false);

        builder.HasOne(e => e.Plu)
            .WithMany()
            .HasForeignKey(e => e.PluId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName($"FK_{SqlTables.Labels}__PLU");

        //

        builder.Property(e => e.ArmId)
            .HasColumnName("ARM_UID").IsRequired();

        builder.HasOne(e => e.Arm)
            .WithMany()
            .HasForeignKey(e => e.ArmId)
            .HasConstraintName($"FK_{SqlTables.Labels}__ARM")
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Dates

        builder.Property(e => e.ProductDt)
            .HasColumnName("PRODUCT_DT")
            .IsRequired();

        builder.Property(e => e.ExpirationDt)
            .HasColumnName("EXPIRATION_DT")
            .IsRequired();

        #endregion

        #region Barcodes

        builder.Property(e => e.BarcodeTop)
            .HasColumnName("BARCODE_TOP")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.BarcodeRight)
            .HasColumnName("BARCODE_RIGHT")
            .HasColumnType("varchar(128)")
            .IsRequired();

        builder.Property(e => e.BarcodeBottom)
            .HasColumnName("BARCODE_BOTTOM")
            .HasColumnType("varchar(128)")
            .IsRequired();

        #endregion

        #region Weight

        builder.Property(e => e.WeightNet)
            .HasColumnName("WEIGHT_NET")
            .IsRequired()
            .HasPrecision(5, 3);

        builder.Property(e => e.WeightTare)
            .HasColumnName("WEIGHT_TARE")
            .IsRequired()
            .HasPrecision(5, 3);

        #endregion

        #region Other

        builder.Property(e => e.BundleCount)
            .HasColumnName("BUNDLE_COUNT")
            .IsRequired();

        builder.Property(e => e.Kneading)
            .HasColumnName("KNEADING")
            .IsRequired();

        builder.Property(e => e.IsWeight)
            .HasColumnName("IS_WEIGHT")
            .IsRequired();

        #endregion
    }
}