using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Database.Entities.Print.Labels;
using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref1C.Nestings;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.Desktop.Api.App.Features.Plu.Common;
using Ws.Desktop.Api.App.Shared.Labels.Generate;
using Ws.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;
using Ws.Desktop.Models.Features.Labels.Input;
using Ws.Desktop.Models.Features.Labels.Output;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Impl.Weight;

internal sealed class PluWeightApiService(
    IPrintLabelService printLabelService,
    WsDbContext dbContext,
    UserHelper userHelper
    ) : IPluWeightService
{
    #region Queries

    public Task<PluWeightDto[]> GetAllWeightAsync()
    {
        return dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Id == userHelper.UserId)
            .SelectMany(i => i.Plus.Where(p => p.IsWeight))
            .OrderBy(i => i.Number)
            .Join(dbContext.Nestings,
            plu => plu.Id,
            nesting => nesting.Id,
            (plu, nesting) => new PluWeightDto
            {
                Id = plu.Id,
                Name = plu.Name,
                FullName = plu.FullName,
                Number = (ushort)Math.Abs(plu.Number),
                BundleCount = (byte)nesting.BundleCount,
                Box = nesting.Box.Name,
                Bundle = plu.Bundle.Name,
                TareWeight = (decimal)Math.Round((double)(plu.Weight + plu.Clip.Weight + plu.Bundle.Weight) * nesting.BundleCount + (double)nesting.Box.Weight, 3)
            })
            .ToArrayAsync();
    }

    #endregion

    #region Commands

    public async Task<PrintSuccessDto> GenerateLabel(Guid pluId, CreateWeightLabelDto dto)
    {
        NestingEntity nesting = await dbContext.Nestings
            .Include(i => i.Box)
            .SingleAsync(i => i.Id == pluId);

        PluEntity plu = await dbContext.Plus
            .Include(i => i.Clip)
            .Include(i => i.Bundle)
            .SingleAsync(i => i.Id == pluId);

        LineEntity line = await dbContext.Lines
            .Include(i => i.Warehouse)
            .ThenInclude(i => i.ProductionSite)
            .SingleAsync(i => i.Id == userHelper.UserId);

        GenerateWeightLabelDto dtoToCreate = new()
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
                dto.WeightNet,
                plu.Clip.Weight,
                plu.Bundle.Weight
            ),
            Line = new(
                line.Id,
                (short)line.Number,
                line.Name,
                line.Warehouse.ProductionSite.Address,
                line.Counter % 1000000 + 1
            ),
            Nesting = new(Guid.Empty, nesting.Box.Weight, nesting.BundleCount),
            Kneading = (short)dto.Kneading,
            ProductDt = dto.ProductDt
        };

        LabelEntity label = printLabelService.GenerateWeightLabel(dtoToCreate);

        line.Counter = dtoToCreate.Line.Counter;

        label.Plu = plu;
        label.Line = line;

        await dbContext.Labels.AddAsync(label);

        await dbContext.SaveChangesAsync();

        return new() { ArmCounter = (uint)line.Counter, Zpl = label.Zpl.Zpl };
    }

    #endregion
}