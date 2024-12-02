using Pl.Admin.Api.App.Features.References1C.Bundles.Common;

namespace Pl.Admin.Api.App.Features.References1C.Bundles;

[ApiController]
[Route(ApiEndpoints.Bundles)]
public sealed class BundleController(IBundleService bundleService)
{
    #region Queries

    [HttpGet]
    public Task<PackageDto[]> GetAll() =>
        bundleService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<PackageDto> GetById([FromRoute] Guid id) =>
        bundleService.GetByIdAsync(id);

    #endregion
}