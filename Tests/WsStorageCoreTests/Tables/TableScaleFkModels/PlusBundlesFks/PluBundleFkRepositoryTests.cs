﻿namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusBundleFks;

public sealed class PluBundleFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluBundleFkRepository PluBundleFkRepository { get; set; } = new();

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluBundleFkModel> items = PluBundleFkRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
}