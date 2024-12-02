namespace Pl.Admin.Models.Features.References.Warehouses.Commands;

public sealed record WarehouseCreateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("productionSiteId")]
    public Guid ProductionSiteId { get; set; }
}

public sealed class WarehouseCreateValidator : AbstractValidator<WarehouseCreateDto>
{
    public WarehouseCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(32)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.ProductionSiteId).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}