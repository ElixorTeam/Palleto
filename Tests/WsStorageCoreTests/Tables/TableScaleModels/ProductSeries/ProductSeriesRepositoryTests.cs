﻿namespace WsStorageCoreTests.Tables.TableScaleModels.ProductSeries;

[TestFixture]
public sealed class ProductSeriesRepositoryTests : TableRepositoryTests
{
    private WsSqlProductSeriesRepository ProductSeriesRepository { get; } = new();
    protected override IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.ChangeDt)).Descending;

    private WsSqlProductSeriesModel GetFirstNotCloseSeriesModel()
    {
        SqlCrudConfig.SelectTopRowsCount = 1;
        SqlCrudConfig.AddFilter(SqlRestrictions.Equal(nameof(WsSqlProductSeriesModel.IsClose), false));
        return ProductSeriesRepository.GetList(SqlCrudConfig).First();
    }

    [Test]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlProductSeriesModel> items = ProductSeriesRepository.GetList(SqlCrudConfig);
            ParseRecords(items);
        }, false, DefaultConfigurations);
    }

    [Test]
    public void GetItemByLineNotClose()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlProductSeriesModel oldProductSeries = GetFirstNotCloseSeriesModel();
            WsSqlScaleModel line = oldProductSeries.Scale;
            WsSqlProductSeriesModel seriesByLine = ProductSeriesRepository.GetItemByLineNotClose(line);

            Assert.That(seriesByLine.IsExists, Is.True);
            Assert.That(seriesByLine.IsClose, Is.EqualTo(false));
            Assert.That(seriesByLine, Is.EqualTo(oldProductSeries));

            TestContext.WriteLine(seriesByLine);
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}