global using System;
global using System.Collections.Generic;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Diagnostics;
global using System.Drawing;
global using System.Drawing.Imaging;
global using System.Globalization;
global using System.IO;
global using System.Linq;
global using System.Management;
global using System.Reflection;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Threading;
global using System.Threading.Tasks;
global using System.Windows;
global using System.Windows.Controls.Primitives;
global using System.Windows.Data;
global using System.Windows.Input;
global using System.Xml;
global using MDSoft.BarcodePrintUtils.Tsc;
global using MDSoft.BarcodePrintUtils.Utils;
global using MDSoft.NetUtils;
global using MDSoft.WinFormsUtils;
global using MDSoft.Wmi.Enums;
global using MDSoft.Wmi.Models;
global using MvvmHelpers;
global using Nito.AsyncEx;
global using SuperSimpleTcp;
global using WsDataCore.Common;
global using WsDataCore.Enums;
global using WsDataCore.Helpers;
global using WsDataCore.Memory;
global using WsDataCore.Models;
global using WsDataCore.Utils;
global using WsLabelCore.Common;
global using WsLabelCore.Controls;
global using WsLabelCore.Helpers;
global using WsLabelCore.Models;
global using WsLabelCore.Pages;
global using WsLabelCore.Utils;
global using WsLabelCore.ViewModels;
global using WsLocalizationCore.Common;
global using WsLocalizationCore.Models;
global using WsLocalizationCore.Utils;
global using WsMassaCore.Enums;
global using WsMassaCore.Helpers;
global using WsMassaCore.Models;
global using WsPrintCore.Common;
global using WsPrintCore.Zpl;
global using WsStorageCore.Helpers;
global using WsStorageCore.Models;
global using WsStorageCore.Tables.TableConfModels.DeviceSettingsFks;
global using WsStorageCore.Tables.TableDirectModels;
global using WsStorageCore.Tables.TableScaleFkModels.DeviceTypesFks;
global using WsStorageCore.Tables.TableScaleModels.BarCodes;
global using WsStorageCore.Tables.TableScaleModels.Bundles;
global using WsStorageCore.Tables.TableScaleModels.Clips;
global using WsStorageCore.Tables.TableScaleModels.Devices;
global using WsStorageCore.Tables.TableScaleModels.DeviceTypes;
global using WsStorageCore.Tables.TableScaleModels.Plus;
global using WsStorageCore.Tables.TableScaleModels.PlusScales;
global using WsStorageCore.Tables.TableScaleModels.PlusWeighings;
global using WsStorageCore.Tables.TableScaleModels.ProductSeries;
global using WsStorageCore.Tables.TableScaleModels.Scales;
global using WsStorageCore.Tables.TableScaleModels.Templates;
global using WsStorageCore.Tables.TableScaleModels.TemplatesResources;
global using WsStorageCore.Utils;
global using WsStorageCore.Views.ViewRefModels.PluLines;
global using WsStorageCore.Views.ViewRefModels.PluNestings;
global using Zebra.Sdk.Comm;
global using Zebra.Sdk.Printer;
global using ZebraPrinterStatus = Zebra.Sdk.Printer.PrinterStatus;
global using ZebraConnectionBuilder = Zebra.Sdk.Comm.ConnectionBuilder;
global using FontStyle = System.Drawing.FontStyle;
global using ButtonBase = System.Windows.Controls.Primitives.ButtonBase;
global using Binding = System.Windows.Data.Binding;
global using Label = System.Windows.Forms.Label;
global using KeyEventArgs = System.Windows.Input.KeyEventArgs;


