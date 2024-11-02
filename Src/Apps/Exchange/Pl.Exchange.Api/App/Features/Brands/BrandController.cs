using Pl.Exchange.Api.App.Features.Brands.Common;
using Pl.Exchange.Api.App.Features.Brands.Dto;

namespace Pl.Exchange.Api.App.Features.Brands;

[ApiController]
[Route(ApiEndpoints.Brands)]
public sealed class BrandController(IBrandService brandService)
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] BrandsWrapper wrapper) => brandService.Load(wrapper.Brands);
}