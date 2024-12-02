namespace Pl.Admin.Models.Features.Admins.Users.Commands;

public sealed record UserUpdateDto
{
    [JsonPropertyName("productionSiteId")]
    public Guid ProductionSiteId { get; set; }
}

public sealed class UserUpdateValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.ProductionSiteId).NotEmpty().WithName(wsDataLocalizer["ColProductionSite"]);
    }
}