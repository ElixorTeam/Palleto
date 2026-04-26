namespace Pl.Admin.Models.Features.Admins.PalletMen.Commands;

public sealed record PalletManCreateDto
{
    [JsonPropertyName("id1C")]
    public Guid Id1C { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("patronymic")]
    public string Patronymic { get; set; } = string.Empty;

    [JsonPropertyName("surname")]
    public string Surname { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("warehouseId")]
    public Guid WarehouseId { get; set; }
}

public sealed class PalletManCreateValidator : AbstractValidator<PalletManCreateDto>
{
    public PalletManCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Id1C).NotEmpty().WithName("UID 1C");
        RuleFor(item => item.WarehouseId).NotEmpty().WithName(wsDataLocalizer["ColWarehouse"]);

        RuleFor(item => item.Name)
            .NotEmpty().MaximumLength(32).MinimumLength(2)
            .WithName(wsDataLocalizer["ColName"]);

        RuleFor(item => item.Surname)
            .NotEmpty().MaximumLength(32).MinimumLength(2)
            .WithName(wsDataLocalizer["ColSurname"]);

        RuleFor(item => item.Patronymic)
            .MaximumLength(32)
            .WithName(wsDataLocalizer["ColPatronymic"]);

        RuleFor(item => item.Password)
            .NotEmpty().Length(4)
            .WithName(wsDataLocalizer["ColPassword"]);
    }
}