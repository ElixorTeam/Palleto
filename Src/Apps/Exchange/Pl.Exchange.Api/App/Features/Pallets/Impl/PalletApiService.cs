using Pl.Database;
using Pl.Database.Entities.Print.Pallets;
using Pl.Exchange.Api.App.Features.Pallets.Common;
using Pl.Exchange.Api.App.Features.Pallets.Dto;

namespace Pl.Exchange.Api.App.Features.Pallets.Impl;

internal sealed class PalletApiService(WsDbContext context) : IPalletService
{
    public PalletUpdateStatus Update(PalletUpdateDto dto)
    {
        PalletEntity? pallet = context.Pallets.FirstOrDefault(i => i.Number == dto.Number);

        if (pallet == null)
            return  new()
            {
                IsSuccess = false,
                Message = $"Паллета {dto.Number}: не найдена"
            };

        pallet.IsShipped = dto.IsShipped;
        pallet.DeletedAt = dto.IsDelete ? DateTime.Now : null;
        context.SaveChanges();

        return new() { IsSuccess = true };
    }
}