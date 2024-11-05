using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Database.Entities.Print.Pallets;
using Pl.Database.Entities.Ref.Arms;
using Pl.Database.Entities.Ref.PalletMen;
using Pl.Database.Entities.Ref1C.Characteristics;
using Pl.Database.Entities.Ref1C.Nestings;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Desktop.Api.App.Features.Pallets.Common;
using Pl.Desktop.Api.App.Shared.Labels.Generate;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Shared.Dto;
using Pl.Desktop.Api.App.Features.Pallets.Expressions;
using Pl.Desktop.Models.Features.Pallets.Input;
using Pl.Desktop.Models.Features.Pallets.Output;

namespace Pl.Desktop.Api.App.Features.Pallets.Impl;

internal sealed class PalletApiService(
    WsDbContext dbContext,
    IPrintLabelService printLabelService,
    UserHelper userHelper
    ) : IPalletApiService
{
    #region Quieries

    public Task<LabelDto[]> GetAllZplByPalletAsync(Guid palletId)
    {
        return dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Id == palletId)
            .ToLabelInfo(dbContext.Labels)
            .ToArrayAsync();
    }

    public Task<PalletDto[]> GetByDateAsync(DateTime startTime, DateTime endTime)
    {
        bool dateCondition = startTime != DateTime.MinValue && endTime != DateTime.MaxValue && startTime < endTime;
        return dbContext.Pallets
            .AsNoTracking()
            .IfWhere(dateCondition, p => p.CreateDt.AddHours(3) > startTime && p.CreateDt.AddHours(3) < endTime)
            .Where(p => p.Warehouse.Id == userHelper.WarehouseId)
            .OrderByDescending(p => p.CreateDt)
            .ToPalletInfo(dbContext.Labels).ToArrayAsync();
    }

    public Task<PalletDto[]> GetByNumberAsync(string number)
    {
        return dbContext.Pallets
            .AsNoTracking()
            .Where(p => p.Number.Contains(number))
            .ToPalletInfo(dbContext.Labels)
            .Take(10)
            .ToArrayAsync();
    }

    #endregion

    #region Commands

    public async Task DeleteAsync(Guid id)
    {
        PalletEntity? pallet = await dbContext.Pallets
            .Include(i => i.Warehouse)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pallet == null)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Паллета не найдена",
                StatusCode = HttpStatusCode.NotFound
            };

        if (pallet == null)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Паллета уже отгружена",
                StatusCode = HttpStatusCode.Conflict
            };

        if (pallet.Warehouse.Id != userHelper.WarehouseId)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = "Отказано в правах по складу",
                StatusCode = HttpStatusCode.Conflict
            };

        bool isDelete = pallet.DeletedAt == null;
        await printLabelService.DeletePallet(pallet.Number, isDelete);
        pallet.DeletedAt = isDelete ? DateTime.Now : null;
        await dbContext.SaveChangesAsync();
    }

    public async Task<PalletDto> CreatePiecePalletAsync(PalletPieceCreateDto dto)
    {
        uint palletCounter = (dbContext.Pallets.Any() ? dbContext.Pallets.Max(i => i.Counter) : 0) + 1;

        NestingForLabel nestingForLabel;

        if (dto.CharacteristicId.IsEmpty())
        {
            NestingEntity data1 = await dbContext.Nestings
                .Include(i => i.Box)
                .SingleAsync(i => i.Id == dto.PluId);

            nestingForLabel = new(Guid.Empty, data1.Box.Weight, data1.BundleCount);
        }
        else
        {
            CharacteristicEntity data2 = await dbContext.Characteristics
                .Include(i => i.Box)
                .SingleAsync(i => i.Id == dto.CharacteristicId);

            nestingForLabel = new(dto.CharacteristicId, data2.Box.Weight, data2.BundleCount);
        }

        ArmEntity arm = await dbContext.Arms
            .Include(i => i.Warehouse)
            .ThenInclude(i => i.ProductionSite)
            .SingleAsync(i => i.Id == userHelper.UserId);

        PluEntity plu = await dbContext.Plus
            .Include(i => i.Clip)
            .Include(i => i.Bundle)
            .SingleAsync(i => i.Id == dto.PluId);

        PalletManEntity palletMan = await dbContext.PalletMen
            .SingleAsync(i => i.Id == dto.PalletManId);

        PalletEntity pallet = new()
        {
            Arm = arm,
            PalletMan = palletMan,
            Counter = palletCounter,
            IsShipped = false,
            Warehouse = arm.Warehouse,
            TrayWeight = dto.WeightTray,
            ProductDt = dto.ProdDt,
            Barcode = $"001460910023{palletCounter:D7}",
        };

        GeneratePiecePalletDto data = new()
        {
            Plu = new(
                plu.Id,
                plu.TemplateId,
                plu.Gtin,
                plu.Number,
                plu.ShelfLifeDays,
                plu.Ean13,
                plu.FullName,
                plu.Description,
                plu.StorageMethod,
                plu.Weight,
                plu.Clip.Weight,
                plu.Bundle.Weight
            ),
            Arm = new(
                arm.Id,
                (short)arm.Number,
                arm.Name,
                arm.Warehouse.ProductionSite.Address,
                arm.Counter
                ),
            Nesting = nestingForLabel,
            Pallet = new(pallet.Barcode, arm.Warehouse.Uid1C, palletMan.Uid1C),
            Kneading = (short)dto.Kneading,
            TrayWeight = dto.WeightTray,
            ProductDt = dto.ProdDt
        };
        PalletOutputData palletData = await printLabelService.GeneratePiecePallet(data, dto.LabelCount);

        pallet.Id = palletData.Id;
        pallet.Number = palletData.Number;

        arm.Counter += dto.LabelCount;

        await dbContext.Pallets.AddAsync(pallet);
        await dbContext.Labels.AddRangeAsync(palletData.Labels);

        await dbContext.SaveChangesAsync();

        return dbContext.Pallets
            .ToPalletInfo(dbContext.Labels)
            .Single(p => p.Id == palletData.Id);
    }

    #endregion
}