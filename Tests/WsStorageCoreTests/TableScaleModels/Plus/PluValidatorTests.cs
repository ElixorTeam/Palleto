﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleModels.Plus;

namespace WsStorageCoreTests.TableScaleModels.Plus;

[TestFixture]
public sealed class PluValidatorTests
{
    [Test]
    public void Model_Validate_IsFalse()
    {
        PluModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluModel>(false);
        WsTestsUtils.DataTests.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        PluModel item = WsTestsUtils.DataTests.CreateNewSubstitute<PluModel>(true);
        WsTestsUtils.DataTests.AssertSqlValidate(item, true);
    }
}