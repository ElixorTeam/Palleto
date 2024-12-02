using Pl.Admin.Models.Api;
using Pl.Admin.Models.Api.Admin;
using Pl.Admin.Models.Api.Devices;
using Pl.Admin.Models.Api.Print;
using Pl.Admin.Models.Api.References;
using Pl.Admin.Models.Api.References1c;

namespace Pl.Admin.Models;

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