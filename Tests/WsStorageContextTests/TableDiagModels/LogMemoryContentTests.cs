// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageContextTests.TableDiagModels;

[TestFixture]
public sealed class LogMemoryContentTests
{
    [Test]
    public void Model_Content_Validate()
    {
        WsTestsUtils.DataTests.AssertSqlDbContentValidate<LogMemoryModel>(true);
    }
}