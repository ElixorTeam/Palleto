using Ws.Database.Entities.Ref.ProductionSites;
using Ws.Database.Entities.Ref.Users;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Common;
using Ws.DeviceControl.Api.App.Features.Admins.Users.Impl.Expressions;
using Ws.DeviceControl.Api.App.Shared.Enums;
using Ws.DeviceControl.Models.Features.Admins.Users.Commands;
using Ws.DeviceControl.Models.Features.Admins.Users.Queries;

namespace Ws.DeviceControl.Api.App.Features.Admins.Users.Impl;

internal sealed class UserApiService(WsDbContext dbContext) : IUserService
{
    #region Queries

    public Task<UserDto[]> GetAllAsync()
    {
        return dbContext.Users
            .AsNoTracking()
            .Select(UserExpressions.ToDto)
            .ToArrayAsync();
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        UserEntity user = await dbContext.Users.SafeGetById(id, FkProperty.User);
        await LoadDefaultForeignKeysAsync(user);
        return UserExpressions.ToDto.Compile().Invoke(user);
    }

    #endregion

    #region Commands

    public Task DeleteAsync(Guid id) =>
        dbContext.Users.SafeDeleteAsync(i => i.Id == id, FkProperty.User);

    public async Task<UserDto> SaveOrUpdateUser(Guid uid, UserUpdateDto updateDto)
    {
        UserEntity? user = await dbContext.Users.FindAsync(uid);

        ProductionSiteEntity site = await dbContext.ProductionSites.SafeGetById(updateDto.ProductionSiteId, FkProperty.ProductionSite);

        if (user is null)
        {
            user = new()
            {
                Id = uid,
                ProductionSite = site
            };

            await dbContext.AddAsync(user);
        }
        else
            user.ProductionSite = site;

        await dbContext.SaveChangesAsync();
        return UserExpressions.ToDto.Compile().Invoke(user);
    }

    #endregion

    #region Private

    private async Task LoadDefaultForeignKeysAsync(UserEntity entity)
    {
        await dbContext.Entry(entity).Reference(e => e.ProductionSite).LoadAsync();
    }

    #endregion
}