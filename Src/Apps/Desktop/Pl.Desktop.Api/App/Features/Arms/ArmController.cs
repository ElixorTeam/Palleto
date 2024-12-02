using Pl.Desktop.Api.App.Features.Arms.Common;
using Pl.Desktop.Models.Features.Arms.Input;
using Pl.Desktop.Models.Features.Arms.Output;

namespace Pl.Desktop.Api.App.Features.Arms;

[Authorize]
[ApiController]
[Route(ApiEndpoints.Arms)]
public sealed class ArmController(IArmService armService) : ControllerBase
{
    #region Queries

    [HttpGet]
    public Task<ArmDto> GetCurrent() =>
        armService.GetCurrentAsync();

    #endregion

    #region Commands

    [HttpPut]
    public Task Update([FromBody] UpdateArmDto dto) =>
        armService.UpdateAsync(dto);

    #endregion
}