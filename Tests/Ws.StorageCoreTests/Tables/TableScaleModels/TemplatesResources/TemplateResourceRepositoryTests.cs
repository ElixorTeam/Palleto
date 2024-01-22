﻿using Ws.Database.Core.Entities.Scales.TemplatesResources;
using Ws.Domain.Models.Entities.SchemaScale;

namespace Ws.StorageCoreTests.Tables.TableScaleModels.TemplatesResources;

[TestFixture]
public sealed class TemplateResourceRepositoryTests : TableRepositoryTests
{
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.By(nameof(TemplateResourceEntity.Name)).Ascending.Then.By(nameof(TemplateResourceEntity.Type)).Ascending;

    [Test]
    public void GetList()
    {
        TestsUtils.DataTests.AssertAction(() =>
        {
            IEnumerable<TemplateResourceEntity> items = TemplateResourceRepository.GetList();
            ParseRecords(items);
        });
    }
}