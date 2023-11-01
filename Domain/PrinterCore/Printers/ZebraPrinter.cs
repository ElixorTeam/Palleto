﻿// using PrinterCore.Common;
// using PrinterCore.Connectors;
// using WsStorageCore.Tables.TableScaleModels.PlusLabels;
//
// namespace PrinterCore.Printers;
//
// public class ZebraPrinter : IPrinter
// {
//     public IPrinterConnector Connector { get; set; }
//
//     public ZebraPrinter()
//     {
//         Connector = new ZebraConnector();
//     }
//     
//     public bool PrintLabel(WsSqlPluLabelEntity pluLabel) =>
//         Connector.SendCommand(pluLabel.Zpl);
//     
//     public void Dispose()
//     {
//         Connector.Dispose();
//     }
//
// }