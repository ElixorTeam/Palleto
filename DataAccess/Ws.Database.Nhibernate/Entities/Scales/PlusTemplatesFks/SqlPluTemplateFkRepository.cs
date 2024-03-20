using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Database.Nhibernate.Entities.Scales.PlusTemplatesFks;

public class SqlPluTemplateFkRepository : BaseRepository
{
    public TemplateEntity GetTemplateByPlu(PluEntity plu) =>
        (Session.Query<PluTemplateFkEntity>().FirstOrDefault(i => i.Plu == plu) ?? new()).Template;
}