using Microsoft.EntityFrameworkCore;
using Pl.Database;
using Pl.Database.Entities.Ref.Lines;
using Pl.Desktop.Api.App.Features.Arms.Common;
using Pl.Desktop.Api.App.Features.Arms.Expressions;
using Pl.Desktop.Models.Features.Arms.Input;
using Pl.Desktop.Models.Features.Arms.Output;

namespace Pl.Desktop.Api.App.Features.Arms.Impl;

internal sealed class ArmApiService(WsDbContext dbContext, UserHelper userHelper) : IArmService
{
    #region Queries

    public async Task<ArmDto> GetCurrentAsync()
    {
        ArmDto? arm = await dbContext.Lines
            .AsNoTracking()
            .Where(i => i.Id == userHelper.UserId)
            .Select(ArmExpressions.ToDto)
            .FirstOrDefaultAsync();

        return arm ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Линия не зарегестрирована",
            StatusCode = HttpStatusCode.NotFound
        };
    }

    #endregion

    #region Commands

    public async Task UpdateAsync(UpdateArmDto dto)
    {
        LineEntity arm =
            await dbContext.Lines.FindAsync(userHelper.UserId)
            ?? throw new ApiInternalException
        {
            ErrorDisplayMessage = "Линия не зарегестрирована",
            StatusCode = HttpStatusCode.NotFound
        };

        arm.Version = dto.Version;
        await dbContext.SaveChangesAsync();
    }

    #endregion
}