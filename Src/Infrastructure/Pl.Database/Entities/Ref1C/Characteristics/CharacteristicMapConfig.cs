namespace Pl.Database.Entities.Ref1C.Characteristics;

internal sealed class CharacteristicMapConfig : IEntityTypeConfiguration<CharacteristicEntity>
{
    public void Configure(EntityTypeBuilder<CharacteristicEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Characteristics, SqlSchemas.Ref1C);

        builder.HasIndex(e => new { e.PluId, e.BoxId, e.BundleCount })
            .HasDatabaseName($"UQ_{SqlTables.Characteristics}__UNIQ")
            .IsUnique();

        #endregion

        #region FK

        builder.Property(e => e.PluId)
            .HasColumnName("PLU_UID");

        builder.HasOne<PluEntity>()
            .WithMany()
            .HasForeignKey(characteristic => characteristic.PluId)
            .HasConstraintName($"FK_{SqlTables.Characteristics}__PLU")
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder.Property(e => e.BoxId)
            .HasColumnName("BOX_UID");

        builder.HasOne(e => e.Box)
            .WithMany()
            .HasForeignKey(characteristic => characteristic.BoxId)
            .HasConstraintName($"FK_{SqlTables.Characteristics}__BOX")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.BundleCount)
            .HasColumnName("BUNDLE_COUNT")
            .IsRequired();
    }
}