namespace Pl.Admin.Models.Api.References1c;

public interface IWebClipApi
{
    #region Queries

    [Get("/clips")]
    Task<PackageDto[]> GetClips();

    [Get("/clips/{uid}")]
    Task<PackageDto> GetClipByUid(Guid uid);

    #endregion
}