﻿using Ws.Database.Core.Entities.Ref.PalletMen;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.PalletMan;

public class PalletManService(SqlPalletManRepository palletManRepo) : IPalletManService
{
    [Transactional] public PalletManEntity GetItemByUid(Guid uid) => palletManRepo.GetByUid(uid);
    [Transactional] public IEnumerable<PalletManEntity> GetAll() => palletManRepo.GetAll();
    [Transactional] public PalletManEntity Create(PalletManEntity item) => palletManRepo.Save(item);
    [Transactional] public PalletManEntity Update(PalletManEntity item) => palletManRepo.Update(item);
}