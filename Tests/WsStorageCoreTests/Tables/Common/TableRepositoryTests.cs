namespace WsStorageCoreTests.Tables.Common;

public class TableRepositoryTests
{
    protected WsSqlCrudConfigModel SqlCrudConfig { get; private set; }
    protected List<WsEnumConfiguration> DefaultConfigurations { get; }

    public TableRepositoryTests()
    {
        DefaultConfigurations = new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS };
        SqlCrudConfig = new();
    }

    [SetUp]
    public void SetUp()
    {
        SqlCrudConfig = new() { SelectTopRowsCount = 10, IsResultOrder = true };
    }

    protected virtual IResolveConstraint SortOrderValue => Is.Ordered.By(nameof(WsSqlTableBase.Name)).Ascending;

    protected void ParseRecords<T>(IEnumerable<T> items) where T : WsSqlTableBase, new()
    {
        List<T> list = items.ToList();
        Assert.That(list.Any(), Is.True, $"{WsLocaleCore.Tests.NoDataInDb}!");
        Assert.That(list, SortOrderValue, $"{WsLocaleCore.Tests.SortingError}!");

        TestContext.WriteLine($"{WsLocaleCore.Tests.Print} {list.Count} {WsLocaleCore.Tests.Records}.");

        foreach (T item in list)
        {
            TestContext.WriteLine(WsSqlQueries.TrimQuery(item.ToString()));

            ValidationResult validationResult = WsSqlValidationUtils.GetValidationResult(item, true);
            Assert.That(validationResult.IsValid, Is.True, validationResult.ToString());

            if (item is not SerializeBase sitem)
                continue;

            string xml = WsDataFormatUtils.SerializeAsXmlString<T>(sitem, true, false);
            Assert.That(xml, Is.Not.Empty, "XML is empty");
        }
    }
}