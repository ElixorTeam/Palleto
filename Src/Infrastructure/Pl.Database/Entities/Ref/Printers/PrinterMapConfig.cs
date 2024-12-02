namespace Pl.Database.Entities.Ref.Printers;

internal sealed class PrinterMapConfig : IEntityTypeConfiguration<PrinterEntity>
{
    public void Configure(EntityTypeBuilder<PrinterEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Printers, SqlSchemas.Ref);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{SqlTables.Printers}__NAME")
            .IsUnique();

        builder.HasIndex(e => e.Ip)
            .HasDatabaseName($"UQ_{SqlTables.Printers}__IP")
            .IsUnique();

        #endregion

        #region Fk

        builder.Property(e => e.ProductionSiteId)
            .HasColumnName("PRODUCTION_SITE_UID").IsRequired();

        builder.HasOne(e => e.ProductionSite)
            .WithMany()
            .HasForeignKey(plu => plu.ProductionSiteId)
            .HasConstraintName($"FK_{SqlTables.Printers}__PRODUCTION_SITE")
            .OnDelete(DeleteBehavior.Restrict);

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(SqlColumns.Name)
            .HasColumnType("varchar(16)")
            .IsRequired();

        builder.Property(e => e.Ip)
            .HasColumnName("IP")
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("TYPE")
            .IsRequired();
    }
}