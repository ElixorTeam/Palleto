// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Utils;

/// <summary>
/// Утилиты роутинга.
/// </summary>
public static class WsRouteUtils
{
    #region Администрирование
    
    public static string SystemAppInfo => "/system";
    public static string SystemDatabaseInfo => "/system_sql";
    public static string SectionVersions => "/system_sql_versions";
    
    #endregion
    
    #region ПЛУ

    public static string SectionPlus => "/section/plus";
    public static string SectionPlusLines => "/section/plus_lines";
    public static string SectionPlusGroups => "/section/plus_groups";
    public static string SectionPlusLabels => "/section/plus_labels";
    public static string SectionPlusStorage => "/section/plus_storage";
    public static string SectionPlusNestingFks => "/section/plus_nesting";
    public static string SectionPlusBundlesFks => "/section/plus_bundles";
    public static string SectionPlusWeightings => "/section/plus_weightings";
    public static string SectionPlusWeightingsAggr => "/section/plus_weightings_aggr";
    
    #endregion

    #region Диагностика

    public static string SectionLogs => "/section/logs";
    public static string SectionLogsWebService => "/section/logs_web_service";
    public static string SectionScalesScreenShots => "/section/logs_screenshots";
    public static string SectionLogsMemory => "/section/logs_memory";
    public static string SectionLogsMemoryChart => "/section/logs_memory_chart";
    
    #endregion
    
    #region Принтеры
    
    public static string SectionPrinters => "/section/printers";
    public static string SectionPrinterTypes => "/section/printers_types";
    
    #endregion
    
    #region Устройства
    
    public static string SectionDevices => "/section/devices";
    public static string SectionDevicesTypes => "/section/devices_types";
    
    #endregion

    #region Прочее

    public static string SectionClips => "/section/clips";
    public static string SectionAccess => "/section/access";
    public static string SectionBarCodes => "/section/barcodes";
    public static string SectionBoxes => "/section/boxes";
    public static string SectionBrands => "/section/brands";
    public static string SectionBundles => "/section/bundles";
    public static string SectionContragents => "/section/contragents";
    public static string SectionDevicesScalesFk => "/section/devices_scales_fks";
    public static string SectionOrganizations => "/section/organizations";
    public static string SectionProductionFacilities => "/section/production_facilities";
    public static string SectionLines => "/section/lines";
    public static string SectionTemplateResources => "/section/templates_resources";
    public static string SectionTemplates => "/section/templates";
    public static string SectionWorkShops => "/section/workshops";
    public static string Profile => "/profile";

    #endregion
}