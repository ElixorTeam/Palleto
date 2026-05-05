namespace Pl.Database.Entities.Ref.PalletMen;

internal sealed class PalletManConfiguration : IEntityTypeConfiguration<PalletManEntity>
{
    public void Configure(EntityTypeBuilder<PalletManEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.PalletMen, DbSchemas.Ref);

        builder.HasIndex(e => new { e.Name, e.Surname, e.Patronymic })
            .HasDatabaseName($"UQ_{DbTables.PalletMen}__FIO")
            .IsUnique();

        builder.HasIndex(e => e.Uid1C)
            .HasDatabaseName($"UQ_{DbTables.PalletMen}__UID_1C")
            .IsUnique();

        #endregion

        #region FK

        builder.HasOne(e => e.Warehouse)
            .WithMany()
            .HasForeignKey("WAREHOUSE_UID")
            .HasConstraintName($"FK_{DbTables.PalletMen}__WAREHOUSE")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        #endregion

        builder.Property(e => e.Uid1C)
            .HasColumnName(DbColumns.Uid1C)
            .IsRequired();

        builder.Property(e => e.Name)
            .HasColumnName(DbColumns.Name)
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