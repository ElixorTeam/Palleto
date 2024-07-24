namespace Ws.DeviceControl.Models.Dto.References.ProductionSites.Commands.Create;

public class ProductionSiteCreateValidator : AbstractValidator<ProductionSiteCreateDto>
{
    public ProductionSiteCreateValidator(IStringLocalizer<WsDataResources> wsDataLocalizer)
    {
        RuleFor(item => item.Name).NotEmpty().WithName(wsDataLocalizer["ColName"]);
        RuleFor(item => item.Address).NotEmpty().WithName(wsDataLocalizer["ColAddress"]);
    }
}