using Pl.Admin.Models.Features.Print.Pallets;

namespace Pl.Admin.Api.App.Features.Print.Pallets.Common;

public interface IPalletService
{
    public Task<PalletDto> GetByIdAsync(Guid id);
    public Task<PalletDto> GetByNumber(string number);
    public Task<PalletDto[]> GetPalletsWorkShiftByArmAsync(Guid armId);
    public Task<LabelPalletDto[]> GetPalletLabels(Guid id);
}