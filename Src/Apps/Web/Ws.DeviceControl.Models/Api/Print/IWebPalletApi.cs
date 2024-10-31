using Ws.DeviceControl.Models.Features.Print.Pallets;

namespace Ws.DeviceControl.Models.Api.Print;

public interface IWebPalletApi
{
    #region Queries

    [Get("/pallets/{id}")]
    Task<PalletDto> GetPalletById(Guid id);

    [Get("/pallets/number/{number}")]
    Task<PalletDto> GetPalletByNumber(string number);

    [Get("/pallets/arm/{armId}")]
    Task<PalletDto[]> GetPalletsWorkShiftByArm(Guid armId);

    [Get("/pallets/{id}/labels")]
    Task<List<LabelPalletDto>> GetPalletLabels(Guid id);

    #endregion
}