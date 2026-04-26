namespace Pl.Database.Entities.Ref.Users;

public sealed class UserEntity
{
    public Guid Id { get; set; }
    public ProductionSiteEntity ProductionSite { get; set; } = null!;
}