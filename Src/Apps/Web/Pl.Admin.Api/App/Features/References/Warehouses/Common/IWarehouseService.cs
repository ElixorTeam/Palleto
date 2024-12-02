using Pl.Admin.Models.Features.References.Warehouses.Commands;
using Pl.Admin.Models.Features.References.Warehouses.Queries;

namespace Pl.Admin.Api.App.Features.References.Warehouses.Common;

public interface IWarehouseService :
    IDeleteById,
    IGetByProdSite<WarehouseDto>,
    IGetProxiesByProdSite
{
    #region Queries

    Task<WarehouseDto> GetByIdAsync(Guid id);

    #endregion

    #region Commands

    Task<WarehouseDto> CreateAsync(WarehouseCreateDto dto);
    Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto dto);

    #endregion
}