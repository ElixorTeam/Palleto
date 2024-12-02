using Pl.Admin.Models.Features.References1C.Brands;

namespace Pl.Admin.Api.App.Features.References1C.Brands.Common;

public interface IBrandService : IGetById<BrandDto>, IGetAll<BrandDto>;