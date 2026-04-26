namespace Pl.Admin.Models.Features.References.ProductionSites.Commands;

public sealed record ProductionSiteCreateDto
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
}

public sealed class ProductionSiteCreateValidator : AbstractValidator<ProductionSiteCreateDto>
{
    public ProductionSiteCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Address)
            .NotEmpty().MaximumLength(128)
            .WithName(wsDataLocalizer["ColAddress"]);
    }
}