﻿using System.Security.Principal;

namespace WsStorageCoreTests.Tables.TableScaleModels.Access;

[TestFixture]
public sealed class AccessRepositoryTests : TableRepositoryTests
{
    private WsSqlAccessRepository AccessRepository { get; set; } = new();
    
    private string CurrentUser { get; set; }

    public AccessRepositoryTests() : base()
    {
        #pragma warning disable CA1416
        CurrentUser = WindowsIdentity.GetCurrent().Name;
        #pragma warning restore CA1416
    }

    [Test, Order(1)]
    public void GetList()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            List<WsSqlAccessModel> items = AccessRepository.GetList(SqlCrudConfig);
            Assert.That(items.Any(), Is.True);
            WsTestsUtils.DataTests.PrintTopRecords(items, 10);
        }, false, DefaultPublishTypes);
    }
    
    [Test, Order(2)]
    public void GetOrCreate()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel access = AccessRepository.GetItemByNameOrCreate(CurrentUser);
            WsSqlAccessModel accessByUid = AccessRepository.GetItemByUid(access.IdentityValueUid);
            Assert.That(access.IsExists, Is.True);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Success created/updated: {access.Name} / {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }

    [Test, Order(3)]
    public void GetByUid()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel accessByName = AccessRepository.GetItemByUsername(CurrentUser);
            WsSqlAccessModel accessByUid= AccessRepository.GetItemByUid(accessByName.IdentityValueUid);
            Assert.That(accessByUid.IsExists, Is.True);
            TestContext.WriteLine($"Get item success: {accessByUid.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
    
    [Test, Order(4)]
    public void GetNewItem()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            WsSqlAccessModel access = AccessRepository.GetNewItem();
            Assert.That(access.IsNotExists, Is.True);
            TestContext.WriteLine($"New item: {access.IdentityValueUid}");
        }, false, new() { WsEnumConfiguration.DevelopVS, WsEnumConfiguration.ReleaseVS });
    }
}