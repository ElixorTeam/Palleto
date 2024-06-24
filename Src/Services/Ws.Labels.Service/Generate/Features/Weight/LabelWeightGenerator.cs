using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Labels;
using Ws.Labels.Service.Generate.Exceptions.LabelGenerate;
using Ws.Labels.Service.Generate.Features.Weight.Dto;
using Ws.Labels.Service.Generate.Features.Weight.Models;
using Ws.Labels.Service.Generate.Models;
using Ws.Labels.Service.Generate.Models.Cache;
using Ws.Labels.Service.Generate.Services;

namespace Ws.Labels.Service.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, ILabelService labelService, ZplService zplService)
{
    public Label GenerateLabel(GenerateWeightLabelDto dto)
    {
        if (!dto.Plu.IsCheckWeight)
            throw new LabelGenerateException(LabelGenExceptions.Invalid);

        TemplateFromCache templateFromCache =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateUid ?? Guid.Empty) ??
            throw new LabelGenerateException(LabelGenExceptions.TemplateNotFound);

        string storageMethod = cacheService.GetStorageByNameFromCacheOrDb(dto.Plu.StorageMethod) ??
                               throw new LabelGenerateException(LabelGenExceptions.StorageMethodNotFound);

        BarcodeWeightLabel barcode = dto.ToBarcodeModel();

        #region label parse

        TemplateVariables data = new(
            dto.Plu.FullName,
            (ushort)dto.Plu.Number,
            dto.Plu.Description,
            dto.Line.Number,
            dto.Line.Name,
            dto.Line.Warehouse.ProductionSite.Address,
            productDt: dto.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),
            bundleCount: (ushort)dto.Plu.PluNesting.BundleCount,
            kneading: (ushort)dto.Kneading,
            weight: dto.Weight,
            storageMethod: storageMethod,
            barcodeTop: barcode.GenerateBarcode(templateFromCache.BarcodeTopTemplate),
            barcodeRight: barcode.GenerateBarcode(templateFromCache.BarcodeRightTemplate),
            barcodeBottom: barcode.GenerateBarcode(templateFromCache.BarcodeBottomTemplate)
        );

        string zpl = zplService.GenerateZpl(templateFromCache.Template, data);

        #endregion

        Label labelSql = new()
        {
            Zpl = zpl,

            Line = dto.Line,
            Plu = dto.Plu,

            BarcodeBottom = data.BarcodeBottom.Replace(">8", ""),
            BarcodeRight = data.BarcodeRight.Replace(">8", ""),
            BarcodeTop = data.BarcodeTop.Replace(">8", ""),

            ExpirationDt = dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays),

            ProductDt = barcode.ProductDt,
            Kneading = dto.Kneading,

            WeightNet = dto.Weight,
            WeightTare = dto.Plu.GetWeightWithNesting,
            BundleCount = data.BundleCount
        };
        labelService.Create(labelSql);

        return labelSql;
    }
}