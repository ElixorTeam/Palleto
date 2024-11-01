using Microsoft.EntityFrameworkCore;
using Ws.Database;
using Ws.Database.Entities.Ref.Lines;
using Ws.Desktop.Api.App.Features.Arms.Common;
using Ws.Desktop.Api.App.Features.Arms.Expressions;
using Ws.Desktop.Models.Features.Arms.Input;
using Ws.Desktop.Models.Features.Arms.Output;

namespace Ws.Desktop.Api.App.Features.Arms.Impl;

internal sealed class ArmApiService(WsDbContext dbContext, UserHelper userHelper) : IArmService
{
    #region Queries

    public async Task<ArmValue> GetCurrentAsync()
    {
        ArmValue? arm = await dbContext.Lines
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