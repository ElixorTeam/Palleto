// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiTerra1000Tests.Controllers;

[TestFixture]
public sealed class ContragentControllerTests
{
    [Test]
    public void GetListContragent_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListContragent(WsServerType.All))
            {
                foreach (long id in WsServiceTestsUtils.GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
        TestContext.WriteLine();
    }

    [Test]
    public void GetListContragentV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListContragentV2(WsServerType.All))
            {
                foreach (long id in WsServiceTestsUtils.GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
    }

    private async Task GetContragentAsync(string url, string? code, long? id)
    {
        await WsServiceResponseUtils.GetResponseAsync(url, WsServiceRequestUtils.GetRequestCodeOrId(code, id), (response) =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                if (code is not null)
                    Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", StringComparison.InvariantCultureIgnoreCase));
                if (id is not null)
                    Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", StringComparison.InvariantCultureIgnoreCase));
            }
        });
    }
}