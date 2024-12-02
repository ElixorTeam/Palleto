using Pl.Tablet.Api.App.Features.Arms.Common;
using Pl.Tablet.Models.Features.Arms;

namespace Pl.Tablet.Api.App.Features.Arms;

[ApiController]
[Route(ApiEndpoints.Arms)]
public sealed class ArmController(IArmService armService)
{
    #region Queries

    [HttpGet]
    public ArmDto GetCurrent() => armService.GetCurrent();

    #endregion
}