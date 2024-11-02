using Pl.Database.Entities.Print.Labels;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Piece.Dto;
using Pl.Desktop.Api.App.Shared.Labels.Generate.Features.Weight.Dto;

namespace Pl.Desktop.Api.App.Shared.Labels.Generate;

public interface IPrintLabelService
{
    /// <summary>
    /// Создает весовую этикетку
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    LabelEntity GenerateWeightLabel(GenerateWeightLabelDto weightLabelDto);

    Task<bool> DeletePallet(string palletNumber, bool isDelete);

    /// <summary>
    /// Создает паллету
    /// </summary>
    /// <exception cref="LabelGenerateException">Ошибка формирования.</exception>
    Task<PalletOutputData> GeneratePiecePallet(GeneratePiecePalletDto piecePalletDto, int labelCount);
}