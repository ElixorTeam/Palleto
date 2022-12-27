﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Versions;

namespace DataCoreTests.Sql.TableScaleModels.Versions;

[TestFixture]
internal class VersionValidatorTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

    [Test]
    public void Model_Validate_IsFalse()
    {
        VersionModel item = DataCore.CreateNewSubstitute<VersionModel>(false);
        DataCore.AssertSqlValidate(item, false);
    }

    [Test]
    public void Model_Validate_IsTrue()
    {
        VersionModel item = DataCore.CreateNewSubstitute<VersionModel>(true);
        DataCore.AssertSqlValidate(item, true);
    }
}
