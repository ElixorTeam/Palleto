using Ws.DeviceControl.Models.Api;
using Ws.DeviceControl.Models.Api.Admin;
using Ws.DeviceControl.Models.Api.Devices;
using Ws.DeviceControl.Models.Api.Print;
using Ws.DeviceControl.Models.Api.References;
using Ws.DeviceControl.Models.Api.References1c;

namespace Ws.DeviceControl.Models;

public interface IWebApi :
    IWebUserApi,
    IWebPalletManApi,
    IWebArmApi,
    IWebPrinterApi,
    IWebBoxApi,
    IWebClipApi,
    IWebBundleApi,
    IBrandApi,
    IPluApi,
    IWebProductionSiteApi,
    IWebWarehouseApi,
    IWebLabelApi,
    IWebDatabaseApi,
    IWebTemplateApi,
    IWebTemplateResourceApi,
    IWebPalletApi;