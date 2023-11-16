using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCoreTests.Models.SqlCrudConfig;

[TestFixture]
public sealed class SqlCrudConfigOrdersTests
{
    [Test]
    public void CheckAddOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(SqlOrder.Asc("data № 1"));

            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(1));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckAddManyOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(SqlOrder.Desc("Test № 1"));
            sqlCrudConfig.AddOrders(new()
            {
                SqlOrder.Asc("Test № 2"),
                SqlOrder.Asc("Test № 3"),
            });

            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(3));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckAddEqualOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(SqlOrder.Desc("Test № 1"));
            sqlCrudConfig.AddOrders(new()
            {
                SqlOrder.Desc("Test № 1"),
                SqlOrder.Asc("Test № 3"),
            });

            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(2));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckDeleteOneOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrder(SqlOrder.Asc("Test № 1"));
            sqlCrudConfig.RemoveOrder(SqlOrder.Desc("Test № 1"));

            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckDeleteManyOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrders(new()
            {
                SqlOrder.Desc("Test № 1"),
                SqlOrder.Desc("Test № 2"),
                SqlOrder.Asc("Test № 3"),
            });

            sqlCrudConfig.RemoveOrders(new()
            {
                SqlOrder.Desc("Test № 2"),
                SqlOrder.Asc("Test № 3"),
            });

            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(1));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckDeleteAllOrder()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddOrders(new()
            {
                SqlOrder.Desc("Test № 1"),
                SqlOrder.Asc("Test № 2"),
                SqlOrder.Asc("Test № 3"),
            });

            sqlCrudConfig.ClearOrders();
            Assert.That(sqlCrudConfig.Orders, Has.Count.EqualTo(0));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}