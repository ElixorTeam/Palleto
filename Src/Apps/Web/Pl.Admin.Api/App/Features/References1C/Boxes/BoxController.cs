using Pl.Admin.Api.App.Features.References1C.Boxes.Common;

namespace Pl.Admin.Api.App.Features.References1C.Boxes;

[ApiController]
[Route(ApiEndpoints.Boxes)]
public sealed class BoxController(IBoxService boxService)
{
    #region Queries

    [HttpGet]
    public Task<PackageDto[]> GetAll() =>
        boxService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PackageDto> GetById([FromRoute] Guid id) =>
        boxService.GetByIdAsync(id);

    #endregion
}