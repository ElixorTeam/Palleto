using Ws.Database.Nhibernate.Common;
using Ws.Database.Nhibernate.Common.Queries.List;
using Ws.Domain.Models.Entities.Diag;

namespace Ws.Database.Nhibernate.Entities.Diag.TableSizes;

public sealed class SqlViewTableSizeRepository : BaseRepository, IGetAll<TableSizeEntity>
{
    public IEnumerable<TableSizeEntity> GetAll() => Session.Query<TableSizeEntity>().ToList();
}