// ReSharper disable ClassNeverInstantiated.Global
namespace Ws.DeviceControl.Api.App.Shared.Helpers;

public sealed class UserHelper(
    ClaimsPrincipal user,
    IAuthorizationService authorizationService,
    WsDbContext dbContext)
{
    #region Private

    private Guid UserId => Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid result)
        ? result : Guid.Empty;

    #endregion

    #region Public

    public Task<ProxyDto?> GetUserProductionSiteAsync() =>
        dbContext.Users
            .Where(i => i.Id == UserId)
            .Select(i => i.ProductionSite)
            .Select(ProductionSiteCommonExpressions.ToProxy)
            .FirstOrDefaultAsync();

    public async Task<bool> ValidatePolicyAsync(string policy) =>
        (await authorizationService.AuthorizeAsync(user, policy)).Succeeded;

    public async Task ValidateUserProductionSiteAsync(Guid productionSiteId)
    {
        if (productionSiteId.IsMax())
        {
            bool isDeveloper = await ValidatePolicyAsync(PolicyEnum.Developer);
            if (isDeveloper) return;
        }

        bool isSenior = await ValidatePolicyAsync(PolicyEnum.SeniorSupport);
        if (isSenior && !productionSiteId.IsMax()) return;

        bool canWork =
            await dbContext.Users.AnyAsync(i => i.Id == UserId && i.ProductionSite.Id == productionSiteId);

        if (!canWork)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Пользователь не может работать с выбранной площадкой",
                StatusCode = HttpStatusCode.Forbidden
            };
    }

    #endregion
}