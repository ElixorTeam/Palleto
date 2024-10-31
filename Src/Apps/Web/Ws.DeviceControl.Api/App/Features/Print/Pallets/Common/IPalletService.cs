using Ws.DeviceControl.Models.Features.Print.Pallets;

namespace Ws.DeviceControl.Api.App.Features.Print.Pallets.Common;

public interface IPalletService
{
    public Task<PalletDto> GetByIdAsync(Guid id);
    public Task<PalletDto> GetByNumber(string number);
    public Task<List<PalletDto>> GetPalletsWorkShiftByArmAsync(Guid armId);
    public Task<List<LabelPalletDto>> GetPalletLabels(Guid id);
}