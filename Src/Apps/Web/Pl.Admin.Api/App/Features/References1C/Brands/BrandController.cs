using Pl.Admin.Api.App.Features.References1C.Brands.Common;
using Pl.Admin.Models.Features.References1C.Brands;

namespace Pl.Admin.Api.App.Features.References1C.Brands;


[ApiController]
[Route(ApiEndpoints.Brands)]
public sealed class BrandController(IBrandService brandService)
{
    #region Queries

    [HttpGet]
    public Task<BrandDto[]> GetAll() =>
        brandService.GetAllAsync();

    [HttpGet("{id:guid}")]
    public Task<BrandDto> GetById([FromRoute] Guid id) =>
        brandService.GetByIdAsync(id);

    #endregion
}