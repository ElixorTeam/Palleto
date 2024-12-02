using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Database.Entities.Ref.Arms;
using Pl.Database.Entities.Ref1C.Characteristics;
using Pl.Database.Entities.Ref1C.Nestings;
using Pl.Database.Entities.Ref1C.Plus;
using Pl.Desktop.Api.App.Features.Plu.Common;
using Pl.Desktop.Models.Features.Plus.Piece.Output;

namespace Pl.Desktop.Api.App.Features.Plu.Impl.Piece;

internal sealed class PluPieceApiService(WsDbContext dbContext, UserHelper userHelper) : IPluPieceService
{
    #region Queries

    public async Task<PluPieceDto[]> GetAllPieceAsync()
    {
        ArmEntity arm = await dbContext.Arms
            .AsNoTracking()
            .Include(i => i.Plus)
            .ThenInclude(pluEntity => pluEntity.Bundle)
            .SingleAsync(i => i.Id == userHelper.UserId);

        Dictionary<PluEntity, List<NestingDto>> data = new();

        foreach (PluEntity plu in arm.Plus.Where(i => !i.IsWeight).OrderBy(i => i.Number))
        {
            List<NestingDto> pluNesting = [];
            NestingEntity nesting = await dbContext.Nestings.AsNoTracking()
                .Include(i => i.Box).SingleAsync(i => i.Id == plu.Id);
            List<CharacteristicEntity> characteristics = await dbContext.Characteristics.AsNoTracking()
                .Include(i => i.Box).Where(i => i.PluId == plu.Id).ToListAsync();

            pluNesting.Add(
                new()
                {
                    Id = Guid.Empty,
                    BundleCount = (byte)nesting.BundleCount,
                    Box = nesting.Box.Name,
                    Name = $"{nesting.BundleCount} (По умолчанию)"
                }
            );

            pluNesting.AddRange(characteristics.Select(characteristic => new NestingDto()
            {
                Id = characteristic.Id,
                BundleCount = (byte)characteristic.BundleCount,
                Box = characteristic.Box.Name,
                Name = $"{characteristic.BundleCount} (Кор)"
            }));

            data.Add(plu, pluNesting);
        }

        return data.Select(plu =>
            new PluPieceDto
            {
                Id = plu.Key.Id,
                Number = (ushort)plu.Key.Number,
                Name = plu.Key.Name,
                FullName = plu.Key.FullName,
                Bundle = plu.Key.Bundle.Name,
                WeightNet = plu.Key.Weight,
                Nestings = plu.Value
            }).ToArray();
    }

    #endregion
}