﻿using WsStorageCore.Entities.SchemaScale.Tasks;
namespace WsStorageCoreTests.Tables.TableScaleModels.Tasks;

[TestFixture]
public sealed class TaskRepositoryTests : TableRepositoryTests
{
    private WsSqlTaskRepository TaskRepository { get; } = new();

    protected override IResolveConstraint SortOrderValue =>
        Is.Ordered.Using((IComparer<WsSqlTaskEntity>)Comparer<WsSqlTaskEntity>.
            // ReSharper disable once StringCompareToIsCultureSpecific
            Create((x, y) => x.Scale.Description.CompareTo(y.Scale.Description))).Ascending;


    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlTaskEntity> items = TaskRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }
}