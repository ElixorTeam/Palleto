using Ws.Database.Entities.Print.Pallets;
using Ws.Database.Entities.Ref.Lines;
using Ws.Database.Entities.Ref.PalletMen;
using Ws.Database.Entities.Ref.Printers;
using Ws.Database.Entities.Ref.ProductionSites;
using Ws.Database.Entities.Ref.Warehouses;
using Ws.Database.Entities.Ref1C.Brands;
using Ws.Database.Entities.Ref1C.Bundles;
using Ws.Database.Entities.Ref1C.Clips;
using Ws.Database.Entities.Ref1C.Plus;
using Ws.Database.Entities.Zpl.Templates;

namespace Ws.DeviceControl.Api.App.Shared.Utils;

public static class ProxyUtils
{
    [Pure]
    public static ProxyDto Brand(BrandEntity i) => new(i.Id,  i.Name);

    [Pure]
    public static ProxyDto Bundle(BundleEntity i) => new(i.Id, i.Name);

    [Pure]
    public static ProxyDto Clip(ClipEntity i) => new(i.Id, i.Name);

    [Pure]
    public static ProxyDto Plu(PluEntity i) => new(i.Id,  $"{i.Number} | {i.Name}");

    [Pure]
    public static ProxyDto Pallet(PalletEntity i) => new(i.Id,  $"{i.Number}");

    [Pure]
    public static ProxyDto Arm(LineEntity i) => new(i.Id, i.Name);

    [Pure]
    public static ProxyDto Printer(PrinterEntity i) => new(i.Id, $"{i.Name} | {i.Ip}");

    [Pure]
    public static ProxyDto Warehouse(WarehouseEntity i) => new(i.Id, i.Name);

    [Pure]
    public static ProxyDto ProductionSite(ProductionSiteEntity i) => new(i.Id, i.Name);

    [Pure]
    public static ProxyDto PalletMan(PalletManEntity i) => new(i.Id, $"{i.Surname} {i.Name} {i.Patronymic}");

    [Pure]
    public static ProxyDto Template(TemplateEntity i) => new(i.Id, i.Name);
}