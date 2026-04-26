namespace Pl.Database.Views.Diag.DatabaseTables;

internal sealed class DatabaseTableViewMapConfig : IEntityTypeConfiguration<DatabaseTableView>
{
    public void Configure(EntityTypeBuilder<DatabaseTableView> builder)
    {
        builder.HasNoKey();

        builder.ToView(SqlViews.DatabaseTables, SqlSchemas.Diag);

        builder.Property(e => e.Schema)
            .HasColumnName("SCHEMA");

        builder.Property(e => e.Table)
            .HasColumnName("TABLE");

        builder.Property(e => e.Rows)
            .HasColumnName("ROWS_COUNT");

        builder.Property(e => e.UsedMb)
            .HasColumnName("USED_MB");

        builder.Property(e => e.FileName)
            .HasColumnName("FILE_NAME");
    }
}