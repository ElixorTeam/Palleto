// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels.Plus;

namespace DataCoreTests.Sql.TableScaleModels.Plus;

[TestFixture]
internal class PluContentTests
{
    private static DataCoreHelper DataCore => DataCoreHelper.Instance;

	[Test]
    public void Model_Content_Validate()
    {
		DataCore.AssertSqlDbContentValidate<PluModel>();
	}
}