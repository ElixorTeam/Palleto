using Microsoft.EntityFrameworkCore;
using Ws.Database.Entities.Print.Labels;
using Ws.Database.Entities.Print.Pallets;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace Ws.Desktop.Api.App.Features.Pallets.Expressions;

internal static class PalletExpressions
{
    public static IQueryable<PalletDto> ToPalletInfo(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
    {
        return query
            .GroupJoin(
                labelContext,
                pallet => pallet.Id,
                label => label.PalletId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .Select(result => new PalletDto
            {
                Id = result.Pallet.Id,
                Arm = result.Pallet.Arm.Name,
                Warehouse = new(result.Pallet.Warehouse.Id, result.Pallet.Warehouse.Name),
                Number = result.Pallet.Number,
                Plus = result.Labels
                    .GroupBy(label => new { label.Plu!.Id, label.Kneading })
                    .Select(group => new PluPalletDto
                    {
                        Name = group.First().Plu!.Name,
                        Number = (ushort)group.First().Plu!.Number,
                        Kneading = (ushort)group.First().Kneading,
                        BoxCount = (ushort)group.Count(),
                        BundleCount = (ushort)group.Sum(label => label.BundleCount),
                        WeightBrutto = result.Labels.Sum(label => label.WeightTare + label.WeightNet),
                        WeightNet = group.Sum(label => label.WeightNet),
                    }).OrderBy(i => i.Kneading)
                    .ToHashSet(),
                PalletMan = new(result.Pallet.PalletMan.Surname,
                    result.Pallet.PalletMan.Name, result.Pallet.PalletMan.Patronymic),
                WeightTray = result.Pallet.TrayWeight,
                Barcode = result.Pallet.Barcode,
                ProdDt = result.Pallet.ProductDt,
                CreateDt = result.Pallet.CreateDt,
                DeletedAt = result.Pallet.DeletedAt,
                IsShipped = result.Pallet.IsShipped
            });
    }

    public static IQueryable<LabelDto> ToLabelInfo(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
    {
        return query
            .GroupJoin(
                labelContext,
                pallet => pallet.Id,
                label => label.PalletId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .SelectMany(
                result => result.Labels,
                (result, label) => new { label.Zpl, label.BarcodeTop, label.ProductDt })
            .OrderBy(temp => temp.ProductDt)
            .Select(temp => new LabelDto
            {
                Zpl = temp.Zpl.Zpl,
                Barcode = temp.BarcodeTop
            });
    }
}