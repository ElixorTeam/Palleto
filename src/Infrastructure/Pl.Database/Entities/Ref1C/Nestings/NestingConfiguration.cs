namespace Pl.Database.Entities.Ref1C.Nestings;

internal sealed class NestingConfiguration : IEntityTypeConfiguration<NestingEntity>
{
    public void Configure(EntityTypeBuilder<NestingEntity> builder)
    {
        #region Base

        builder.ToTable(DbTables.Nestings, DbSchemas.Ref1C);

        #endregion

        #region FK

        builder.Property(e => e.BoxId)
            .HasColumnName("BOX_UID");

        builder.HasOne(e => e.Box)
            .WithMany()
            .HasForeignKey(nesting => nesting.BoxId)
            .HasConstraintName($"FK_{DbTables.Nestings}__BOX")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        //

        builder.HasOne<PluEntity>()
            .WithOne()
            .HasForeignKey<NestingEntity>(n => n.Id)
            .HasPrincipalKey<PluEntity>(p => p.Id)
            .HasConstraintName($"FK_{DbTables.Nestings}__PLU")
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        builder.Property(e => e.BundleCount)
            .HasColumnName("BUNDLE_COUNT")
            .IsRequired();
    }
}