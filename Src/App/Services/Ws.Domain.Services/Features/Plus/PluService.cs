using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref1c.Plus;
using Ws.Domain.Models.Entities.Ref1c.Plu;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plus;

internal class PluService(SqlPluRepository pluRepo, IRedisCachingProvider provider) : IPluService
{
    #region Items

    [Transactional]
    public Plu GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    #endregion

    #region List

    [Transactional]
    public IList<Plu> GetAll() => pluRepo.GetAll();

    #endregion

    #region CRUD

    [Transactional]
    public Plu Update(Plu item) => pluRepo.Update(item);

    #endregion

    public string GetPluCachedTemplate(Plu plu)
    {
        List<string> templatesKeys = provider.SearchKeys("TEMPLATE-*:PLUS");

        foreach (string templateKey in templatesKeys)
        {
            if (!provider.SIsMember(templateKey, $"{plu.Uid}"))
                continue;

            string? zpl = provider.StringGet($"{templateKey.Replace(":PLUS", ":ZPL")}");

            if (zpl != null)
                return zpl;
            break;
        }

        return "";
    }
}