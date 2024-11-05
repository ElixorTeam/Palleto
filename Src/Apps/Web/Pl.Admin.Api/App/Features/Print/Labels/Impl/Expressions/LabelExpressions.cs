using Pl.Database.Entities.Print.Labels;
using Pl.Database.Entities.Print.LabelsZpl;
using Pl.Admin.Models.Features.Print.Labels;

namespace Pl.Admin.Api.App.Features.Print.Labels.Impl.Expressions;

internal static class LabelExpressions
{
    public static Expression<Func<LabelEntity, LabelDto>> ToLabelDto =>
        label => new()
        {
            Id = label.Id,
            IsWeight = label.IsWeight,
            BundleCount = (byte)label.BundleCount,
            Kneading = (ushort)label.Kneading,
            WeightNet = label.WeightNet,
            WeightTare = label.WeightTare,
            Arm = ProxyUtils.Arm(label.Arm),
            Warehouse = ProxyUtils.Warehouse(label.Arm.Warehouse),
            Plu = label.Plu != null ? ProxyUtils.Plu(label.Plu) : null,
            Pallet = label.Pallet != null ? ProxyUtils.Pallet(label.Pallet) : null,
            BarcodeTop = label.BarcodeTop,
            BarcodeBottom = label.BarcodeBottom,
            BarcodeRight = label.BarcodeRight,
            ProductDt = DateOnly.FromDateTime(label.ProductDt),
            ExpirationDt = DateOnly.FromDateTime(label.ExpirationDt),
            CreateDt = label.CreateDt,
        };

    public static Expression<Func<LabelZplEntity, ZplDto>> ToZplDto =>
        zpl => new()
        {
            Width = (ushort)zpl.Width,
            Height = (ushort)zpl.Height,
            Rotate = (ushort)zpl.Rotate,
            Zpl = zpl.Zpl
        };
}