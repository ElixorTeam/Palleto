using Pl.Exchange.Api.App.Features.Brands.Dto;

namespace Pl.Exchange.Api.App.Features.Brands.Common;

public interface IBrandService
{
    public ResponseDto Load(HashSet<BrandDto> dtos);
}