﻿using Ws.Database.Core.Entities.Ref.Users;
using Ws.Domain.Models.Entities.Ref;

namespace Ws.Domain.Services.Features.User;

internal class UserService : IUserService
{
    public UserEntity GetByUid(Guid uid) => new SqlUserRepository().GetByUid(uid);
    public IEnumerable<UserEntity> GetAll() => new SqlUserRepository().GetEnumerable();
    public UserEntity GetItemByNameOrCreate(string username) => new SqlUserRepository().GetItemByNameOrCreate(username);
}