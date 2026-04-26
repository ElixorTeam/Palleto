namespace Pl.Admin.Models.Features.References.ProductionSites.Commands;

public sealed record ProductionSiteUpdateDto
{
    [JsonPropertyName("name")]
    public required string Name { get; set; } = string.Empty;

    [JsonPropertyName("address")]
    public required string Address { get; set; } = string.Empty;
}

public sealed class ProductionSiteUpdateValidator : AbstractValidator<ProductionSiteUpdateDto>
{
    public ProductionSiteUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(64)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Address)
            .NotEmpty().MaximumLength(128)
            .WithName(wsDataLocalizer["ColAddress"]);
    }
}