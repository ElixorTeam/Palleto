using Ws.DeviceControl.Models.Shared;

namespace Ws.DeviceControl.Models.Api.References1c;

public interface IWebBundleApi
{
    #region Queries

    [Get("/bundles")]
    Task<PackageDto[]> GetBundles();

    [Get("/bundles/{uid}")]
    Task<PackageDto> GetBundleByUid(Guid uid);

    #endregion
}