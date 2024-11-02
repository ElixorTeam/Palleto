namespace Pl.Admin.Models.Api.References1c;

public interface IWebBoxApi
{
    #region Queries

    [Get("/boxes")]
    Task<PackageDto[]> GetBoxes();

    [Get("/boxes/{uid}")]
    Task<PackageDto> GetBoxByUid(Guid uid);

    #endregion
}