using Ws.Database.Entities.Print.Labels;
using Ws.Database.Entities.Print.LabelsZpl;
using Ws.DeviceControl.Models.Features.Print.Labels;

namespace Ws.DeviceControl.Api.App.Features.Print.Labels.Impl.Expressions;

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
            Arm = new(label.Line.Id, label.Line.Name),
            Warehouse = new(label.Line.Warehouse.Id, label.Line.Warehouse.Name),
            ProductionSite = new(label.Line.Warehouse.ProductionSite.Id, label.Line.Warehouse.ProductionSite.Name),
            Plu = label.Plu != null ? new(label.Plu.Id, label.Plu.Name) : null,
            Pallet = label.PalletId != null ? new(label.Pallet.Id, label.Pallet.Number) : null,
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