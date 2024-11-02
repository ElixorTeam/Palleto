using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Api.App.Features.Warehouses.Common;

public interface IWarehouseService
{
    #region Queries

    List<WarehouseDto> GetAll();

    #endregion
}