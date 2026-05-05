namespace Pl.Database.Entities.Zpl.ZplResources;

internal sealed class ZplResourceConfiguration : IEntityTypeConfiguration<ZplResourceEntity>
{
    public void Configure(EntityTypeBuilder<ZplResourceEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.ZplResources, DbSchemas.Zpl);

        builder.HasIndex(e => e.Name)
            .HasDatabaseName($"UQ_{DbTables.ZplResources}__NAME")
            .IsUnique();

        #endregion

        builder.Property(e => e.Name)
            .HasColumnName(DbColumns.Name)
            .HasColumnType("varchar(64)")
            .IsRequired();

        builder.Property(e => e.Zpl)
            .HasColumnName("BODY")
            .HasColumnType("varchar(8000)")
            .IsRequired();

        builder.Property(e => e.Type)
            .HasColumnName("TYPE")
            .IsRequired();
    }
}