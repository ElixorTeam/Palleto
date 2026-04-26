using Pl.Admin.Api.App.Features.Print.Pallets.Common;
using Pl.Admin.Models.Features.Print.Pallets;

namespace Pl.Admin.Api.App.Features.Print.Pallets;

[ApiController]
[Route(ApiEndpoints.Pallets)]
public sealed class PalletController(IPalletService palletService)
{
    #region Queries

    [HttpGet("{id:guid}")]
    public Task<PalletDto> GetById(Guid id) =>
        palletService.GetByIdAsync(id);

    [HttpGet("number/{number}")]
    public Task<PalletDto> GetByNumber(string number) =>
        palletService.GetByNumber(number);

    [HttpGet("arm/{armId:guid}")]
    public Task<PalletDto[]> GetPalletsWorkShiftByArmAsync(Guid armId) =>
        palletService.GetPalletsWorkShiftByArmAsync(armId);

    [HttpGet("{id:guid}/labels")]
    public Task<LabelPalletDto[]> GetPalletLabels(Guid id) =>
        palletService.GetPalletLabels(id);

    #endregion
}