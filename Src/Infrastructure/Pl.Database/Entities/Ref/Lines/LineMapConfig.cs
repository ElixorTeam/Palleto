namespace Pl.Database.Entities.Ref.Lines;

internal sealed class LineMapConfig : IEntityTypeConfiguration<LineEntity>
{
    public void Configure(EntityTypeBuilder<LineEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Arms, SqlSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.Arms}__NAME")
            .IsUnique();

        builder.HasIndex(e => e.SystemKey)
            .HasDatabaseName($"UQ_{SqlTables.Arms}__SYSTEM_KEY")
            .IsUnique();

        builder.HasIndex(e => e.Number)
            .HasDatabaseName($"UQ_{SqlTables.Arms}__NUMBER")
            .IsUnique();

        #endregion

        #region Fk

        builder.HasOne(l => l.Warehouse)
            .WithMany()
            .HasForeignKey("WAREHOUSE_UID")
            .HasConstraintName($"FK_{SqlTables.Arms}__WAREHOUSE")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(l => l.Printer)
            .WithMany()
            .HasForeignKey("PRINTER_UID")
            .HasConstraintName($"FK_{SqlTables.Arms}__PRINTER")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(e => e.Plus)
            .WithMany()
            .UsingEntity(
                "ARMS_PLUS_FK",
                l => l.HasOne(typeof(PluEntity))
                    .WithMany()
                    .HasForeignKey("PLU_UID")
                    .HasConstraintName("FK_ARMS_PLUS__PLU")
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(nameof(PluEntity.Id)),
                r => r.HasOne(typeof(LineEntity))
                    .WithMany()
                    .HasForeignKey("ARM_UID")
                    .HasConstraintName("FK_ARMS_PLUS__ARM")
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasPrincipalKey(nameof(LineEntity.Id)),
                j => j.HasKey("PLU_UID", "ARM_UID"));

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName("NAME")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.SystemKey)
            .HasColumnName("SYSTEM_KEY")
            .IsRequired();

        builder.Property(e => e.Counter)
            .HasColumnName("COUNTER")
            .IsRequired();

        builder.Property(e => e.Number)
            .HasColumnName("NUMBER")
            .IsRequired();

        builder.Property(e => e.Version)
            .HasColumnName("VERSION")
            .HasColumnType("varchar(16)")
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("TYPE")
            .IsRequired();
    }
}