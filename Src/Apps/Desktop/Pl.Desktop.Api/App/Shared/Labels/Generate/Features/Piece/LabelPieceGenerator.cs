using Microsoft.Extensions.Localization;
using Pl.Database.Entities.Print.Labels;
using Pl.Desktop.Api.App.Shared.Labels.Api;
using Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Input;
using Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Localization;
using Pl.Print.Features.Barcodes;
using Pl.Print.Features.Templates;
using Pl.Print.Features.Templates.Models;
using Pl.Print.Features.Templates.Utils;
using Pl.Print.Features.Templates.Variables;
using Pl.Print.Shared.ValueTypes;
using Services_CacheService=Pl.Desktop.Api.App.Shared.Labels.Generate.Services.CacheService;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;

public record PalletOutputData(Guid Id, string Number, List<LabelEntity> Labels);

internal class LabelPieceGenerator(
    IPalychApi api,
    IStringLocalizer<LabelGenResources> localizer,
    Services_CacheService cacheService)
{
    public async Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto dto, int labelCount)
    {
        if (labelCount is > 240 or < 1)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = localizer["ValidationFailed"],
                ErrorInternalMessage = $"Label count must be between 240 or < 1. But {labelCount}"
            };

        TemplateInfo templateInfo =
            cacheService.GetTemplateByUidFromCacheOrDb(dto.Plu.TemplateId ?? Guid.Empty, dto.Plu.StorageMethod);
        Dictionary<string, string> zplResources =
            cacheService.GetResourcesFromCacheOrDb(TemplateUtils.GetResourcesKeys(templateInfo.Template), templateInfo.Rotate);
        PrintSettings printSettings = new(templateInfo, zplResources);

        BarcodeBuilder barcodeTemplates = dto.ToBarcodeBuilder();

        // FOR TESTING VARS BEFORE 1C (don't touch)
        (_, TemplateVars testVars) = GenerateLabel(barcodeTemplates, 0, printSettings, dto);
        ZplBuilder.GenerateZpl(printSettings, testVars);

        List<LabelCreateApiDto> labelsFor1C = [];
        List<(LabelEntity, TemplateVars)> labelsData = [];

        for (int i = 0 ; i < labelCount ; ++i)
            labelsData.Add(GenerateLabel(barcodeTemplates, i, printSettings, dto));

        foreach ((LabelEntity label, _) in labelsData)
            labelsFor1C.Add(new()
            {
                BarcodeTop = label.BarcodeTop,
                BarcodeRight = label.BarcodeRight,
                BarcodeBottom = label.BarcodeBottom,
                NetWeightKg = label.WeightNet,
                GrossWeightKg = label.WeightNet + label.WeightTare,
                Kneading = (ushort)label.Kneading
            });

        PalletCreateApiDto data = new()
        {
            Organization = PalychConsts.Organization,
            PluUid = dto.Plu.Id,
            PalletManUid = dto.Pallet.PalletManId1C,
            WarehouseUid = dto.Pallet.WarehouseId1C,
            CharacteristicUid = dto.Nesting.Id,
            Barcode = dto.Pallet.Barcode,
            ArmNumber = (uint)dto.Arm.Number,
            TrayWeightKg = dto.TrayWeight,
            Labels = labelsFor1C,
            ProductDt = dto.ProductDt,
            CreatedAt = DateTime.Now,
            NetWeightKg = labelsFor1C.Sum(i => i.NetWeightKg),
            GrossWeightKg = labelsFor1C.Sum(i => i.GrossWeightKg)
        };

        PalletResponseDto response = await api.CreatePallet(data);

        if (response.Successes.Count == 0)
            throw new ApiInternalException
            {
                ErrorDisplayMessage = localizer["ExchangeFailed"],
                ErrorInternalMessage = response.Errors[0].Message
            };

        PalletSuccess success = response.Successes[0];

        return new(success.Uid, success.Number, labelsData.ConvertAll(labelData =>
        {
            (LabelEntity label, TemplateVars vars) = labelData;
            label.PalletId = success.Uid;
            vars.Pallet = vars.Pallet with { Number = success.Number };
            label.Zpl.Zpl = ZplBuilder.GenerateZpl(printSettings, vars);
            return label;
        }));
    }

    private (LabelEntity, TemplateVars) GenerateLabel(BarcodeBuilder barcodeTemplates, int index, PrintSettings printSettings, GeneratePiecePalletDto dto)
    {
        BarcodeBuilder barcode = barcodeTemplates with
        {
            ArmCounter = (uint)(dto.Arm.Counter + index + 1),
            ProductDt = barcodeTemplates.ProductDt.AddSeconds(index)
        };

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

        decimal weightNet = dto.Nesting.CalculateWeightNet(dto.Plu);
        decimal weightTare = dto.Nesting.CalculateWeightTare(dto.Plu);

        TemplateVars data = new(
            plu: new(dto.Plu.FullName, (ushort)dto.Plu.Number, dto.Plu.Description),
            arm: new(dto.Arm.Number, dto.Arm.Name, dto.Arm.Address),
            pallet: new((ushort)(index + 1), string.Empty),
            barcodes: new(barcodeTop, barcodeBottom, barcodeRight),

            bundleCount: (ushort)dto.Nesting.BundleCount,
            kneading: (ushort)dto.Kneading,

            weightNet: weightNet,
            weightGross: weightNet + weightTare,

            productDt: barcode.ProductDt,
            expirationDt: dto.ProductDt.AddDays(dto.Plu.ShelfLifeDays)
        );

        LabelEntity label = new()
        {
            BarcodeBottom = barcodeBottom.Clean,
            BarcodeRight = barcodeRight.Clean,
            BarcodeTop = barcodeTop.Clean,
            WeightNet = weightNet,
            WeightTare = weightTare,
            Kneading = dto.Kneading,
            ProductDt = barcode.ProductDt,
            ExpirationDt = dto.ExpirationDt,
            ArmId = dto.Arm.Id,
            PluId = dto.Plu.Id,
            BundleCount = (ushort)dto.Nesting.BundleCount,
            IsWeight = false,
            Zpl = new()
            {
                Height = (short)printSettings.Settings.Height,
                Width = (short)printSettings.Settings.Width,
                Rotate = (short)printSettings.Settings.Rotate,
                Zpl = string.Empty
            }
        };
        return (label, data);
    }
}