namespace Pl.Database.Entities.Ref.PalletMen;

internal sealed class PalletManMapConfig : IEntityTypeConfiguration<PalletManEntity>
{
    public void Configure(EntityTypeBuilder<PalletManEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.PalletMen, SqlSchemas.Ref);

        builder.HasIndex(e => new { e.Name, e.Surname, e.Patronymic })
            .HasDatabaseName($"UQ_{SqlTables.PalletMen}__FIO")
            .IsUnique();

        builder.HasIndex(e => e.Uid1C)
            .HasDatabaseName($"UQ_{SqlTables.PalletMen}__UID_1C")
            .IsUnique();

        #endregion

        #region FK

        builder.HasOne(e => e.Warehouse)
            .WithMany()
            .HasForeignKey("WAREHOUSE_UID")
            .HasConstraintName($"FK_{SqlTables.PalletMen}__WAREHOUSE")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        #endregion

        builder.Property(e => e.Uid1C)
            .HasColumnName(SqlColumns.Uid1C)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.Surname)
            .HasColumnName("SURNAME")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.Patronymic)
            .HasColumnName("PATRONYMIC")
            .HasColumnType("varchar(32)")
            .IsRequired();

        builder.Property(e => e.Password)
            .HasColumnName("PASSWORD")
            .HasColumnType("varchar(4)")
            .IsRequired();
    }
}