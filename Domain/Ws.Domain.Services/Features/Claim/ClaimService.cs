﻿using Ws.Database.Core.Entities.Ref.Claims;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Claim;

internal class ClaimService(SqlClaimRepository claimRepo) : IClaimService
{
    [Transactional] public IEnumerable<ClaimEntity> GetAll() => claimRepo.GetAll();
    [Transactional] public ClaimEntity GetItemByUid(Guid uid) => claimRepo.GetByUid(uid);
    [Transactional] public ClaimEntity Create(ClaimEntity item) => claimRepo.Save(item);
    [Transactional] public ClaimEntity Update(ClaimEntity item) => claimRepo.Update(item);
}