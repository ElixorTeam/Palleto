using System.Net;
using Ws.DeviceControl.Api.App.Shared.Expressions;
using Ws.Shared.Api.ApiException;
using Ws.Shared.Constants;

namespace Ws.DeviceControl.Api.App.Shared.Internal;

public class UserManager(
    IHttpContextAccessor httpContextAccessor,
    IAuthorizationService authorizationService,
    WsDbContext dbContext)
{
    #region Private

    private Guid UserId => Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out Guid result)
        ? result : Guid.Empty;
    private ClaimsPrincipal User => httpContextAccessor.HttpContext!.User;

    #endregion

    #region Public

    public Task<ProxyDto?> GetUserProductionSiteAsync() =>
        dbContext.Users
            .Where(i => i.Id == UserId)
            .Select(i => i.ProductionSite)
            .Select(ProductionSiteCommonExpressions.ToProxy)
            .FirstOrDefaultAsync();

    public async Task<bool> ValidatePolicyAsync(string policy) =>
        (await authorizationService.AuthorizeAsync(User, policy)).Succeeded;

    public async Task CanUserWorkWithProductionSiteAsync(Guid productionSiteId)
    {
        bool isSenior = await ValidatePolicyAsync(PolicyEnum.SeniorSupport);
        if (isSenior && productionSiteId != BaseConstants.GuidMax) return;

        bool canWork =
            await dbContext.Users.AnyAsync(i => i.Id == UserId && i.ProductionSite.Id == productionSiteId);

        if (!canWork)
            throw new ApiExceptionServer
            {
                ErrorDisplayMessage = "Пользователь не может работать с выбранной площадкой",
                StatusCode = HttpStatusCode.Conflict
            };
    }

    #endregion
}