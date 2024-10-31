using Ws.DeviceControl.Api.App.Features.References1C.Clips.Common;

namespace Ws.DeviceControl.Api.App.Features.References1C.Clips;

[ApiController]
[Route(ApiEndpoints.Clips)]
public sealed class ClipController(IClipService clipService)
{
    #region Queries

    [HttpGet]
    public Task<PackageDto[]> GetAll() => clipService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PackageDto> GetById([FromRoute] Guid id) => clipService.GetByIdAsync(id);

    #endregion
}