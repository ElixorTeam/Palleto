using Pl.Mobile.Api.App.Features.Warehouses.Common;
using Pl.Mobile.Models.Features.Warehouses;

namespace Pl.Mobile.Api.App.Features.Warehouses.Impl;

internal sealed class WarehouseApiService : IWarehouseService
{
    #region Queries

    public List<WarehouseDto> GetAll()
    {
        return
        [
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад №1",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад №2",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка камешково",
                WarehouseName = "Склад №3",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка 16",
                WarehouseName = "Склад №4",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка 23",
                WarehouseName = "Склад №5",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка 23",
                WarehouseName = "Склад №6",
            },
            new()
            {
                Id = Guid.NewGuid(),
                ProductionSiteName = "Площадка 23",
                WarehouseName = "Склад №7",
            },
        ];
    }

    #endregion
}