using Microsoft.Extensions.Localization;
using Pl.Database.Entities.Print.Labels;
using Pl.Desktop.Api.App.Shared.Labels.Api;
using Pl.Desktop.Api.App.Shared.Labels.Api.Pallet.Output;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Localization;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate;

internal class PrintLabelService(
    LabelPieceGenerator labelPieceGenerator,
    LabelWeightGenerator labelWeightGenerator,
    IStringLocalizer<LabelGenResources> localizer,
    IPalychApi palychApi
    )
    : IPrintLabelService
{
    public LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto) =>
        labelWeightGenerator.GenerateLabel(weightLabelDto);

    public async Task<bool> DeletePallet(string palletNumber, bool isDelete)
    {
        PalletDeleteWrapperMsg ans =
            await palychApi.Delete(new() { Pallet = new() { IsDelete = isDelete, Number = palletNumber } });

        if (ans.Status.IsSuccess)
            return true;

        throw new ApiInternalException
        {
            ErrorDisplayMessage = localizer["ExchangeFailed"],
            ErrorInternalMessage = ans.Status.Message
        };
    }

    public Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount) =>
        labelPieceGenerator.GeneratePiecePallet(piecePalletDto, labelCount);
}