using Ws.DeviceControl.Models.Features.Print.Pallets;

namespace Ws.DeviceControl.Models.Api.Print;

public interface IWebPalletApi
{
    #region Queries

    [Get("/pallets/{id:guid}")]
    Task<PalletDto> GetById(Guid id);

    [Get("/pallets/number/{number}")]
    Task<PalletDto> GetByNumber(string number);

    [Get("/pallets/arm/{armId:guid}")]
    Task<List<PalletDto>> GetPalletsWorkShiftByArmAsync(Guid armId);

    [Get("/pallets/{id:guid}/labels")]
    Task<List<LabelPalletDto>> GetPalletLabels(Guid id);

    #endregion
}