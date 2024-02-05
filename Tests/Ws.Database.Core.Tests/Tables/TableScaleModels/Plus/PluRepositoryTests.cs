﻿using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.Plus;

[TestFixture]
public sealed class PluRepositoryTests : TableRepositoryTests
{
    private SqlPluRepository PluRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(PluEntity.Number)).Ascending;

    [Test]
    public void GetList()
    {
        // AssertAction(() =>
        // {
        //     IEnumerable<PluEntity> items = PluRepository.GetEnumerable(SqlCrudConfig);
        //     ParseRecords(items);
        // });
    }
}