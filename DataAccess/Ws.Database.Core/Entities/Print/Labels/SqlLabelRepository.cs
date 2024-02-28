using Ws.Database.Core.Common.Queries.Item;
using Ws.Domain.Models.Entities.Print;

namespace Ws.Database.Core.Entities.Print.Labels;

public sealed class SqlLabelRepository : BaseRepository, IGetItemByUid<LabelEntity>
{
    public LabelEntity GetByUid(Guid uid) => Session.Get<LabelEntity>(uid) ?? new();
    
    public ViewLabel GetViewByUid(Guid uid) => Session.Get<ViewLabel>(uid) ?? new();
    
    public IEnumerable<ViewLabel> GetAllView() => Session.Query<ViewLabel>().ToList();
}