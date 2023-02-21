// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCoreTests.TableScaleFkModels.LogsWebsFks;

[TestFixture]
internal class LogWebFkSerializeTests
{
	[Test]
    public void Item_Serialize_Validate()
    {
		DataCoreTestsUtils.DataCore.AssertSqlDbContentSerialize<LogWebFkModel>();
	}
}