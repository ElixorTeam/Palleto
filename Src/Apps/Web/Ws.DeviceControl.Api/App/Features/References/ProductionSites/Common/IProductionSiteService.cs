using Ws.DeviceControl.Models.Features.References.ProductionSites.Commands;
using Ws.DeviceControl.Models.Features.References.ProductionSites.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.ProductionSites.Common;

public interface IProductionSiteService :
    IGetById<ProductionSiteDto>,
    IGetAll<ProductionSiteDto>,
    IDeleteById
{
    #region Queries

    Task<ProxyDto> GetProxyByUserAsync();
    Task<List<ProxyDto>> GetProxiesAsync();

    #endregion

    #region Commands

    Task<ProductionSiteDto> CreateAsync(ProductionSiteCreateDto dto);
    Task<ProductionSiteDto> UpdateAsync(Guid id, ProductionSiteUpdateDto dto);

    #endregion
}