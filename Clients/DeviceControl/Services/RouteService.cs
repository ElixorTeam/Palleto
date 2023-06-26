using DeviceControl.Utils;
using WsStorageCore.TableDiagModels.Logs;
using WsStorageCore.TableDiagModels.LogsTypes;
using WsStorageCore.TableDiagModels.LogsWebsFks;
using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.TableScaleFkModels.DeviceTypesFks;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleFkModels.PlusLabels;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleFkModels.PlusWeighingsFks;
using WsStorageCore.TableScaleFkModels.PrintersResourcesFks;
using WsStorageCore.TableScaleModels.Access;
using WsStorageCore.TableScaleModels.Apps;
using WsStorageCore.TableScaleModels.BarCodes;
using WsStorageCore.TableScaleModels.Boxes;
using WsStorageCore.TableScaleModels.Brands;
using WsStorageCore.TableScaleModels.Bundles;
using WsStorageCore.TableScaleModels.Contragents;
using WsStorageCore.TableScaleModels.Devices;
using WsStorageCore.TableScaleModels.DeviceTypes;
using WsStorageCore.TableScaleModels.Orders;
using WsStorageCore.TableScaleModels.OrdersWeighings;
using WsStorageCore.TableScaleModels.Organizations;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusGroups;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.PlusStorageMethods;
using WsStorageCore.TableScaleModels.Printers;
using WsStorageCore.TableScaleModels.PrintersTypes;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using WsStorageCore.TableScaleModels.ProductSeries;
using WsStorageCore.TableScaleModels.Scales;
using WsStorageCore.TableScaleModels.Tasks;
using WsStorageCore.TableScaleModels.TasksTypes;
using WsStorageCore.TableScaleModels.Templates;
using WsStorageCore.TableScaleModels.TemplatesResources;
using WsStorageCore.TableScaleModels.Versions;
using WsStorageCore.TableScaleModels.WorkShops;
using WsStorageCore.ViewScaleModels;

namespace DeviceControl.Services;

public class RouteService
{

    private readonly NavigationManager _navigationManager;

    public RouteService(NavigationManager navigationManager)
    {
        _navigationManager = navigationManager;
    }

    public void NavigateItemRoute(WsSqlTableBase? item)
    {
        if (item == null)
            return;
        _navigationManager.NavigateTo(GetItemRoute(item));
    }

    public void NavigateSectionRoute(WsSqlTableBase item)
    {
        _navigationManager.NavigateTo(GetSectionRoute(item));
    }

    public static string GetItemRoute(WsSqlTableBase? item)
    {
        if (item == null)
            return string.Empty;
        string page = GetSectionRoute(item);
        return item.Identity.Name switch
        {
            WsSqlEnumFieldIdentity.Id => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueId}",
            WsSqlEnumFieldIdentity.Uid => item.IsNew ? $"{page}/new" : $"{page}/{item.IdentityValueUid}",
            _ => page
        };
    }

    public static string GetSectionRoute(WsSqlTableBase? item)
    {
        return item switch
        {
            WsSqlAccessModel => RouteUtil.SectionAccess,
            WsSqlBarCodeModel => RouteUtil.SectionBarCodes,
            WsSqlBoxModel => RouteUtil.SectionBoxes,
            WsSqlBrandModel => RouteUtil.SectionBrands,
            WsSqlBundleModel => RouteUtil.SectionBundles,
            WsSqlContragentModel => RouteUtil.SectionContragents,
            WsSqlDeviceModel => RouteUtil.SectionDevices,
            WsSqlDeviceScaleFkModel => RouteUtil.SectionDevicesScalesFk,
            WsSqlDeviceTypeModel => RouteUtil.SectionDevicesTypes,
            WsSqlLogModel => RouteUtil.SectionLogs,
            WsSqlLogWebFkModel => RouteUtil.SectionLogsWebService,
            WsSqlPluGroupModel => RouteUtil.SectionPlusGroups,
            WsSqlOrganizationModel => RouteUtil.SectionOrganizations,
            WsSqlPluBundleFkModel => RouteUtil.SectionPlusBundlesFks,
            WsSqlPluLabelModel => RouteUtil.SectionPlusLabels,
            WsSqlPluModel => RouteUtil.SectionPlus,
            WsSqlPluNestingFkModel => RouteUtil.SectionPlusNestingFks,
            WsSqlPluStorageMethodModel => RouteUtil.SectionPlusStorage,
            WsSqlPluWeighingModel => RouteUtil.SectionPlusWeightings,
            WsSqlPrinterModel => RouteUtil.SectionPrinters,
            WsSqlPrinterTypeModel => RouteUtil.SectionPrinterTypes,
            WsSqlProductionFacilityModel => RouteUtil.SectionProductionFacilities,
            WsSqlScaleModel => RouteUtil.SectionLines,
            WsSqlScaleScreenShotModel => RouteUtil.SectionScalesScreenShots,
            WsSqlTemplateModel => RouteUtil.SectionTemplates,
            WsSqlTemplateResourceModel => RouteUtil.SectionTemplateResources,
            WsSqlVersionModel => RouteUtil.SectionVersions,
            WsSqlWorkShopModel => RouteUtil.SectionWorkShops,

            WsSqlViewLogModel => RouteUtil.SectionLogs,
            WsSqlViewLineModel => RouteUtil.SectionLines,
            WsSqlViewBarcodeModel => RouteUtil.SectionBarCodes,
            WsSqlViewPluLabelModel => RouteUtil.SectionPlusLabels,
            WsSqlViewPluWeighting => RouteUtil.SectionPlusWeightings,
            WsSqlViewDeviceModel => RouteUtil.SectionDevices,
            WsSqlViewWebLogModel => RouteUtil.SectionLogsWebService,
            _ => string.Empty
        };
    }

}