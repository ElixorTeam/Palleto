namespace Pl.Database.Entities.Ref1C.Nestings;

internal sealed class NestingMapConfig : IEntityTypeConfiguration<NestingEntity>
{
    public void Configure(EntityTypeBuilder<NestingEntity> builder)
    {
        #region Base

        builder.ToTable(SqlTables.Nestings, SqlSchemas.Ref1C);

        #endregion

        #region FK

        builder.Property(e => e.BoxId)
            .HasColumnName("BOX_UID");

        builder.HasOne(e => e.Box)
            .WithMany()
            .HasForeignKey(nesting => nesting.BoxId)
            .HasConstraintName($"FK_{SqlTables.Nestings}__BOX")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        //

        builder.HasOne<PluEntity>()
            .WithOne()
            .HasForeignKey<NestingEntity>(n => n.Id)
            .HasPrincipalKey<PluEntity>(p => p.Id)
            .HasConstraintName($"FK_{SqlTables.Nestings}__PLU")
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        builder.Property(e => e.BundleCount)
            .HasColumnName("BUNDLE_COUNT")
            .IsRequired();
    }
}