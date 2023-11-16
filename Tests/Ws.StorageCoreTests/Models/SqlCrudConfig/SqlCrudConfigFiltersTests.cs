using NHibernate.Criterion;
using Ws.StorageCore.OrmUtils;

namespace Ws.StorageCoreTests.Models.WsSqlCrudConfig;

[TestFixture]
public sealed class SqlCrudConfigFiltersTests
{
    [Test]
    public void CheckAddFilter()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(SqlRestrictions.Equal("Test № 1", "data"));
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckAddManyFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            sqlCrudConfig.AddFilter(SqlRestrictions.Equal("Test № 1", "data"));
            sqlCrudConfig.AddFilters(new()
            {
                SqlRestrictions.Equal("Test № 2", "data2"),
                SqlRestrictions.Equal("Test № 3", "data3"),
            });

            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(3));

            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckAddEqualFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            ICriterion filter1 = SqlRestrictions.Equal("Test № 1", "data");
            sqlCrudConfig.AddFilter(filter1);
            sqlCrudConfig.AddFilters(new()
            {
                SqlRestrictions.Equal("Test № 1", "data4"),
                SqlRestrictions.Less("Test № 1", "data56"),
                SqlRestrictions.Equal("Test № 1", "data")
            });
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(3));
            
            TestContext.WriteLine(sqlCrudConfig);
        });
    }

    [Test]
    public void CheckDeleteOneFilter()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            ICriterion isMarkedTrue = SqlRestrictions.IsMarked();
            
            sqlCrudConfig.AddFilter(isMarkedTrue);
            sqlCrudConfig.RemoveFilter(isMarkedTrue);
    
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
    
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteManyFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
            
            ICriterion isMarkedTrue = SqlRestrictions.IsMarked();
            ICriterion isMarkedFalse = SqlRestrictions.IsActual();

            sqlCrudConfig.AddFilters(new() { isMarkedTrue, isMarkedFalse });
            sqlCrudConfig.RemoveFilters(new() { isMarkedTrue });
            
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(1));
    
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
    
    [Test]
    public void CheckDeleteAllFilters()
    {
        Assert.DoesNotThrow(() =>
        {
            SqlCrudConfigModel sqlCrudConfig = new();
                  
            ICriterion isMarkedTrue = SqlRestrictions.IsMarked();
            ICriterion isMarkedFalse = SqlRestrictions.IsActual();

            sqlCrudConfig.AddFilters(new() { isMarkedTrue, isMarkedFalse });
            sqlCrudConfig.RemoveFilters(new() { isMarkedTrue, isMarkedFalse});
            
            sqlCrudConfig.ClearFilters();
            Assert.That(sqlCrudConfig.Filters, Has.Count.EqualTo(0));
    
            TestContext.WriteLine(sqlCrudConfig);
        });
    }
}