using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Pallets;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Piece.Dto;
using Ws.Labels.Service.Generate.Features.Piece.Models;
using Ws.Labels.Service.Generate.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Services;

namespace Ws.Labels.Service.Generate.Features.Piece;

internal class LabelPieceGenerator(
    IPalletService palletService,
    CacheService cacheService,
    ZplService zplService
    )
{
    public Guid GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount)
    {
        if (dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        if (labelCount > 240)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);


        TemplateFromCache templateFromCache = cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
                                              throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        string storageMethod = cacheService.GetStorageByNameFromCacheOrDb(dto.Plu.StorageMethod) ??
                               throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);


        List<Label> labels = [];
        DateTime productDt = dto.ProductDt;

        BarcodePieceModel barcodeTemplates = dto.ToBarcodeModel(productDt);

        for (int i = 0 ; i < labelCount ; ++i)
            labels.Add(GenerateLabel(barcodeTemplates, i, templateFromCache, dto, storageMethod));


        Pallet pallet = new()
        {
            Barcode = new Random().Next(0, 1000001).ToString(),
            Weight = dto.Weight,
            ProdDt = dto.ProductDt,
            PalletMan = dto.PalletMan,
            Arm = dto.Line,
            Counter = new Random().Next(0, 1000001),
            Number = new Random().Next(0, 1000001),
        };
        palletService.Create(pallet, labels);

        return pallet.Uid;
    }

    private Label GenerateLabel(BarcodePieceModel barcodeTemplates, int index, TemplateFromCache templateFromCache, GeneratePiecePalletDto dto, string storageMethod)
    {
        dto.Line.Counter += 1;

        BarcodePieceModel barcode = barcodeTemplates with
        {
            LineCounter =  dto.Line.Counter,
            ProductDt = barcodeTemplates.ProductDt.AddSeconds(index)
        };

        TemplateVariables data = new(
            pluName: dto.Plu.FullName,
            pluNumber: (ushort)dto.Plu.Number,
            pluDescription: dto.Plu.Description,

            lineNumber: dto.Line.Number,
            lineName: dto.Line.Name,
            lineAddress: dto.Line.Warehouse.ProductionSite.Address,

            productDt: dto.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),

            bundleCount: (ushort)dto.Plu.PluNesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weight: dto.Weight,
            storageMethod: storageMethod,

            barcodeTop: barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate),
            barcodeBottom: barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate),
            barcodeRight: barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate)
        );

        string zpl = zplService.GenerateZpl(templateFromCache.Template, data);

        return new()
        {
            Zpl = zpl,
            BarcodeBottom = data.BarcodeBottom.Replace(">8", ""),
            BarcodeRight = data.BarcodeRight.Replace(">8", ""),
            BarcodeTop = data.BarcodeTop.Replace(">8", ""),
            WeightNet = dto.Plu.Weight,
            WeightTare = dto.Plu.GetTareWeightByCharacteristic(dto.PluCharacteristic),
            Kneading = dto.Kneading,
            ProductDt = dto.ProductDt,
            ExpirationDt = dto.ExpirationDt,
            Line = dto.Line,
            Plu = dto.Plu,
            BundleCount = dto.PluCharacteristic.BundleCount
        };
    }
}