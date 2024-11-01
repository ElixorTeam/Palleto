using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref1C.Characteristics;
using Ws.Database.Entities.Ref1C.Nestings;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.Desktop.Api.App.Features.Plu.Common;
using Ws.Desktop.Models.Features.Plus.Piece.Output;

namespace Ws.Desktop.Api.App.Features.Plu.Impl.Piece;

internal sealed class PluPieceApiService(WsDbContext dbContext, UserHelper userHelper) : IPluPieceService
{
    #region Queries

    public async Task<PluPiece[]> GetAllPieceAsync()
    {
        LineEntity line = await dbContext.Lines
            .AsNoTracking()
            .Include(i => i.Plus)
            .ThenInclude(pluEntity => pluEntity.Bundle)
            .SingleAsync(i => i.Id == userHelper.UserId);

        Dictionary<PluEntity, List<Nesting>> data = new();

        foreach (PluEntity plu in line.Plus.Where(i => !i.IsWeight).OrderBy(i => i.Number))
        {
            List<Nesting> pluNesting = [];
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

            pluNesting.AddRange(characteristics.Select(characteristic => new Nesting()
            {
                Id = characteristic.Id,
                BundleCount = (byte)characteristic.BundleCount,
                Box = characteristic.Box.Name,
                Name = $"{characteristic.BundleCount} (Кор)"
            }));

            data.Add(plu, pluNesting);
        }

        return data.Select(plu =>
            new PluPiece
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