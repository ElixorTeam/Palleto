﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Versions;

namespace WsStorageCoreTests.TableScaleModels.Versions;

[TestFixture]
public sealed class VersionValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        VersionModel item = WsTestsUtils.DataTests.CreateNewSubstitute<VersionModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        VersionModel item = WsTestsUtils.DataTests.CreateNewSubstitute<VersionModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}