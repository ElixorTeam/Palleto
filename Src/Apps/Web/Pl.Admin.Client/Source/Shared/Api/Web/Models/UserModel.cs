namespace Pl.Admin.Client.Source.Shared.Api.Web.Models;

public sealed record UserModel
{
    public required Guid Id { get; set; }
    public required Guid KcId { get; set; }
    public required Fio Fio { get; set; }
    public required string Username { get; set; }
    public required ProxyDto ProductionSite { get; set; }
    public required List<string> Roles { get; set; }
}
