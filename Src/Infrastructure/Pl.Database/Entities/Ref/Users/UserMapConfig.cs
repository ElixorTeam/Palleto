namespace Pl.Database.Entities.Ref.Users;

internal sealed class UserMapConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable(SqlTables.Users, SqlSchemas.Ref);

        builder.HasKey(e => e.Id);
        builder
            .Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("UID");

        builder.HasOne(l => l.ProductionSite)
            .WithMany()
            .HasForeignKey("PRODUCTION_SITE_UID")
            .HasConstraintName($"FK_{SqlTables.Users}__PRODUCTION_SITE")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}