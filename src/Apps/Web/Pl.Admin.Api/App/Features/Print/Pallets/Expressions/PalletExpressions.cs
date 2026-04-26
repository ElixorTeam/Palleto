using Pl.Database.Entities.Print.Labels;
using Pl.Database.Entities.Print.Pallets;
using Pl.Admin.Models.Features.Print.Pallets;

namespace Pl.Admin.Api.App.Features.Print.Pallets.Expressions;

internal static class PalletExpressions
{
    public static IQueryable<PalletDto> ToPalletDto(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
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
                Arm = ProxyUtils.Arm(result.Pallet.Arm),
                Warehouse = ProxyUtils.Warehouse(result.Pallet.Warehouse),
                Number = result.Pallet.Number,
                Plus = result.Labels
                    .GroupBy(label => new { label.Plu!.Id, label.Kneading })
                    .Select(group => new PluPalletDto
                    {
                        Plu = ProxyUtils.Plu(group.First().Plu!),
                        Kneading = (ushort)group.First().Kneading,
                        BoxCount = (ushort)group.Count(),
                        BundleCount = (ushort)group.Sum(label => label.BundleCount),
                        WeightBrutto = result.Labels.Sum(label => label.WeightTare + label.WeightNet),
                        WeightNet = group.Sum(label => label.WeightNet),
                    }).OrderBy(i => i.Kneading)
                    .ToHashSet(),
                PalletMan = ProxyUtils.PalletMan(result.Pallet.PalletMan),
                WeightTray = result.Pallet.TrayWeight,
                Barcode = result.Pallet.Barcode,
                ProdDt = result.Pallet.ProductDt,
                CreateDt = result.Pallet.CreateDt,
                DeletedAt = result.Pallet.DeletedAt,
                IsShipped = result.Pallet.IsShipped
            });
    }

    public static IQueryable<LabelPalletDto> ToLabelPalletDto(this IQueryable<PalletEntity> query, DbSet<LabelEntity> labelContext)
    {
        return query
            .GroupJoin(
                labelContext,
                pallet => pallet.Id,
                label => label.PalletId,
                (pallet, labels) => new { Pallet = pallet, Labels = labels })
            .SelectMany(
                result => result.Labels,
                (_, label) => new { label.Zpl, label.BarcodeTop, label.ProductDt, label.Id })
            .OrderBy(temp => temp.ProductDt)
            .Select(temp => new LabelPalletDto
            {
                Id = temp.Id,
                Barcode = temp.BarcodeTop,
            });
    }
}