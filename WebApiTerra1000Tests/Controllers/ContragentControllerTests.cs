﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WebApiCore.Enums;
using WebApiCore.Models;
using WebApiCore.Models.WebRequests;
using WebApiCore.Utils;

namespace WebApiTerra1000Tests.Controllers;

[TestFixture]
internal class ContragentControllerTests
{
    [Test]
    public void GetListContragent_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WebRequestTerra1000().GetListContragent(ServerType.All))
            {
                foreach (long id in TestsUtils.GetListContragentId)
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
            foreach (string url in new WebRequestTerra1000().GetListContragentV2(ServerType.All))
            {
                foreach (long id in TestsUtils.GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
    }

    private async Task GetContragentAsync(string url, string? code, long? id)
    {
        await WebResponseUtils.GetResponseAsync(url, WebRequestUtils.GetRequestCodeOrId(code, id), (response) =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            
            if (!string.IsNullOrEmpty(response.Content))
            {
                if (code is not null)
                    Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", System.StringComparison.InvariantCultureIgnoreCase));
                if (id is not null)
                    Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", System.StringComparison.InvariantCultureIgnoreCase));
            }
        });
    }
}
