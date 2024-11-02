namespace Pl.Admin.Api.App.Common;

public interface IGetByProdSite<T>
{
    Task<T[]> GetAllByProdSiteAsync(Guid prodSiteId);
}

public interface IGetProxiesByProdSite
{
    Task<ProxyDto[]> GetProxiesByProdSiteAsync(Guid prodSiteId);
}