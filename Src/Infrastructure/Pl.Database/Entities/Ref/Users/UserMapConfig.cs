namespace Pl.Database.Entities.Ref.Users;

internal sealed class UserMapConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(SqlTables.Users, SqlSchemas.Ref);

        builder.HasOne(l => l.ProductionSite)
            .WithMany()
            .HasForeignKey("PRODUCTION_SITE_UID")
            .HasConstraintName($"FK_{SqlTables.Users}__PRODUCTION_SITE")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}