namespace Pl.Admin.Models.Features.References.Warehouses.Commands;

public sealed record WarehouseUpdateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}

public sealed class WarehouseUpdateValidator : AbstractValidator<WarehouseUpdateDto>
{
    public WarehouseUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(32)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
    }
}