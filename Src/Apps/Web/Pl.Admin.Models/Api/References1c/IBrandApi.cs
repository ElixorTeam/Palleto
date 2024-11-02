using Pl.Admin.Models.Features.References1C.Brands;

namespace Pl.Admin.Models.Api.References1c;

public interface IBrandApi
{
    #region Queries

    [Get("/brands")]
    Task<BrandDto[]> GetBrands();

    [Get("/brands/{uid}")]
    Task<BrandDto> GetBrandByUid(Guid uid);

    #endregion
}