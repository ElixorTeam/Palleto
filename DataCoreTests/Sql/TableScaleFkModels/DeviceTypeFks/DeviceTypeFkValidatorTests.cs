﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.DeviceTypesFks;

namespace DataCoreTests.Sql.TableScaleFkModels.DeviceTypeFks;

[TestFixture]
internal class DeviceTypeFkValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        // Arrange & Act.
        DeviceTypeFkModel item = DataCore.CreateNewSubstitute<DeviceTypeFkModel>(false);
        // Assert.
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        // Arrange & Act.
        DeviceTypeFkModel item = DataCore.CreateNewSubstitute<DeviceTypeFkModel>(true);
        // Assert.
        DataCore.AssertSqlValidate(item, true);
    }
}