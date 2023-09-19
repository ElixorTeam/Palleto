﻿namespace WsStorageCoreTests.Tables.TableScaleFkModels.PlusStorageMethodsFks;

[TestFixture]
public sealed class PluStorageMethodsFkRepositoryTests : TableRepositoryTests
{
    private WsSqlPluStorageMethodFkRepository PluStorageMethodFkRepository { get; } = new();

    private WsSqlPluStorageMethodFkModel GetFirstPluStorageMethodFk()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        return PluStorageMethodFkRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlPluStorageMethodFkModel> items = PluStorageMethodFkRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByPlu()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlPluStorageMethodFkModel oldPluStorageMethodFk = GetFirstPluStorageMethodFk();
            WsSqlPluModel plu = oldPluStorageMethodFk.Plu;
            WsSqlPluStorageMethodFkModel pluStorageMethodFksByPlu = PluStorageMethodFkRepository.GetItemByPlu(plu);

            Assert.That(pluStorageMethodFksByPlu.IsExists, Is.True);
            Assert.That(pluStorageMethodFksByPlu, Is.EqualTo(oldPluStorageMethodFk));

            TestContext.WriteLine(pluStorageMethodFksByPlu);
        }, false, DefaultConfigurations);
    }
}