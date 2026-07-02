using Pl.Database.Entities.Ref.Arms;
using Pl.Database.Shared.Interceptors;
using Pl.Database.Shared.Extensions;

namespace Pl.Database;

public class WsDbContext(DbContextOptions<WsDbContext> options) : DbContext(options)
{
    #region DbSet

    public DbSet<DatabaseTableView> DatabaseTables { get; init; }
    public DbSet<ZplResourceEntity> ZplResources { get; init; }
    public DbSet<PalletManEntity> PalletMen { get; init; }
    public DbSet<ProductionSiteEntity> ProductionSites { get; init; }
    public DbSet<TemplateEntity> Templates { get; init; }
    public DbSet<BoxEntity> Boxes { get; init; }
    public DbSet<ClipEntity> Clips { get; init; }
    public DbSet<BundleEntity> Bundles { get; init; }
    public DbSet<WarehouseEntity> Warehouses { get; init; }
    public DbSet<PrinterEntity> Printers { get; init; }
    public DbSet<UserEntity> Users { get; init; }
    public DbSet<ArmEntity> Arms { get; init; }
    public DbSet<PluEntity> Plus { get; init; }
    public DbSet<NestingEntity> Nestings { get; init; }
    public DbSet<CharacteristicEntity> Characteristics { get; init; }
    public DbSet<LabelEntity> Labels { get; init; }
    public DbSet<PalletEntity> Pallets { get; init; }
    public DbSet<LabelZplEntity> LabelZpl { get; init; }

    #endregion

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(new ChangeDtInterceptor());
        optionsBuilder.UseAsyncSeeding(async (ctx, _, cancellationToken) =>
        {
            ProductionSiteEntity? productionSite =
                await ctx.Set<ProductionSiteEntity>()
                    .FirstOrDefaultAsync(e => e.Id == DefaultTypes.GuidMax, cancellationToken);

            if (productionSite == null)
            {
                ctx.Set<ProductionSiteEntity>().Add(new()
                {
                    Id = DefaultTypes.GuidMax,
                    Name = "Служебная",
                    Address = "Россия, 000000, Служебная обл., г. Служебный, д. 0",
                });
                await ctx.SaveChangesAsync(cancellationToken);
            }
        });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.SetAutoCreateOrChangeDt();

        modelBuilder.UseIpAddressConversion();
        modelBuilder.UseDateTimeConversion();
        modelBuilder.UseEnumStringConversion();

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}