using Ws.DeviceControl.Models.Features.References1C.Brands;

namespace Ws.DeviceControl.Api.App.Features.References1C.Brands.Common;

public interface IBrandService : IGetById<BrandDto>, IGetAll<BrandDto>;