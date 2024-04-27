using Ws.Labels.Service.Features.Generate.Common;
using Ws.Labels.Service.Features.Generate.Common.XmlBarcode;
using Ws.Labels.Service.Features.Generate.Models;

namespace Ws.Labels.Service.Features.Generate.Utils;

public partial class TemplateTypesUtils
{
    private class BarcodeBaseTemp : IXmlBarcodeModel
    {
        public int LineNumber { get; set; }
        public int LineCounter { get; set; }
        public short Kneading { get; set; }
        public short PluNumber { get; set; }
        public string PluGtin { get; set; } = null!;
        public DateTime ProductDt { get; set; }
    }

    private class BarcodeWeightTemp : IXmlBarcodeWeightXml
    {
        public decimal Weight { get; set; }
    }

    private class BarcodePieceTemp : IXmlBarcodePieceXml
    {
        public short BundleCount { get; set; }
    }

    private static List<IBarcodeFieldModel> GetBaseVariable()
    {
        BarcodeBaseTemp data = new();
        return [
            new BarcodeFieldModel<int>(nameof(BarcodeBaseTemp.LineNumber),5),
            new BarcodeFieldModel<int>(nameof(BarcodeBaseTemp.LineCounter), 7),
            new BarcodeFieldModel<short>(nameof(BarcodeBaseTemp.PluNumber), 3),
            new BarcodeFieldModel<string>(nameof(BarcodeBaseTemp.PluGtin), 14),
            new BarcodeFieldModel<short>(nameof(BarcodeBaseTemp.Kneading), 3),
            new BarcodeFieldModel<DateTime>(nameof(BarcodeBaseTemp.ProductDt), 0, true),
        ];
    }
}