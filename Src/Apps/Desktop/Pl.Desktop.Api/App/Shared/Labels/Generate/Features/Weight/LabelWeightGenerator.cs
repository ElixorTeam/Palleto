using Microsoft.Extensions.Localization;
using Pl.Database.Entities.Print.Labels;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Services;
using Pl.Desktop.Api.App.Shared.Labels.Localization;
using Pl.Print.Features.Barcodes;
using Pl.Print.Features.Templates;
using Pl.Print.Features.Templates.Models;
using Pl.Print.Features.Templates.Utils;
using Pl.Print.Features.Templates.Variables;
using Pl.Print.Shared.ValueTypes;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight;

internal class LabelWeightGenerator(CacheService cacheService, IStringLocalizer<LabelGenResources> localizer)
{
    public LabelEntity GenerateLabel(GenerateWeightLabelDto dto)
    {
        BarcodeBuilder barcode = dto.ToBarcodeBuilder();

        #region label parse

        TemplateInfo templateInfo =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateId ?? Guid.Empty, dto.Plu.StorageMethod);

        Dictionary<string, string> zplResources =
            cacheService.GetResourcesFromCacheOrDb(TemplateUtils.GetResourcesKeys(templateInfo.Template), templateInfo.Rotate);

        PrintSettings printSettings = new(templateInfo, zplResources);

        BarcodeResult barcodeTop, barcodeRight, barcodeBottom;

        try
        {
            barcodeTop = barcode.Build(printSettings.Settings.BarcodeTopTemplate);
            barcodeRight = barcode.Build(printSettings.Settings.BarcodeRightTemplate);
            barcodeBottom = barcode.Build(printSettings.Settings.BarcodeBottomTemplate);
        }
        catch (Exception ex)
        {
            throw new ApiInternalException
            {
                ErrorDisplayMessage = localizer["BarcodeInvalid"],
                ErrorInternalMessage = ex.Message
            };
        }

        decimal weightNet = dto.Plu.Weight;
        decimal weightTare = dto.Nesting.CalculateWeightTare(dto.Plu);

        TemplateVars data = new(
            plu: new(dto.Plu.FullName, (ushort)dto.Plu.Number, dto.Plu.Description),
            arm: new(dto.Arm.Number, dto.Arm.Name, dto.Arm.Address),
            pallet: new(0,string.Empty),
            barcodes: new(barcodeTop, barcodeBottom, barcodeRight),

            bundleCount: (ushort)dto.Nesting.BundleCount,

            kneading: (ushort)dto.Kneading,
            weightNet: weightNet,
            weightGross: weightNet + weightTare,
            productDt: dto.ProductDt,
            expirationDt: dto.ExpirationDt
        );

        string zpl = ZplBuilder.GenerateZpl(printSettings, data);

        #endregion

        LabelEntity labelSql = new()
        {
            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,

            ExpirationDt = dto.ExpirationDt,

            ProductDt = dto.ProductDt,
            Kneading = dto.Kneading,

            WeightNet = weightNet,
            WeightTare = weightTare,
            BundleCount = data.BundleCount,
            IsWeight = true,

            Zpl = new()
            {
                Zpl = zpl,
                Width = (short)printSettings.Settings.Width,
                Height = (short)printSettings.Settings.Height,
                Rotate = (short)printSettings.Settings.Rotate
            }
        };

        return labelSql;
    }
}