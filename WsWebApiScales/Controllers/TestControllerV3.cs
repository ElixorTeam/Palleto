// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using DataCore.Utils;
using WsLocalization.Models;
using WsStorage.Utils;
using WsWebApi.Controllers;
using WsWebApi.Models;

namespace WsWebApiScales.Controllers;

/// <summary>
/// Test controller v3.
/// </summary>
public class TestControllerV3 : WebControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public TestControllerV3(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get info.
    /// </summary>
    /// <param name="format"></param>
    /// <param name="host"></param>
    /// <param name="version"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpGet]
    [Route("api/info/")]
    public ContentResult GetInfo([FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = ControllerHelp.GetContentResult(() =>
        {
            AppVersion.Setup(Assembly.GetExecutingAssembly());

            using NHibernate.ISession session = SessionFactory.OpenSession();
            using ITransaction transaction = session.BeginTransaction();
            ISQLQuery sqlQuery = session.CreateSQLQuery(SqlQueriesV2.GetDateTimeNow);
            sqlQuery.SetTimeout(session.Connection.ConnectionTimeout);
            string response = sqlQuery.UniqueResult<string>();
            transaction.Commit();
            return new ServiceInfoModel(
                    AppVersion.App,
                    AppVersion.Version,
                    StringUtils.FormatDtEng(DateTime.Now, true),
                    response.ToString(CultureInfo.InvariantCulture),
                    session.Connection.ConnectionString.ToString(),
                    session.Connection.ConnectionTimeout,
                    session.Connection.DataSource,
                    session.Connection.ServerVersion,
                    session.Connection.Database,
                    (ulong)Process.GetCurrentProcess().WorkingSet64 / 1048576,
                    (ulong)Process.GetCurrentProcess().PrivateMemorySize64 / 1048576)
                .GetContentResult<ServiceInfoModel>(format, HttpStatusCode.OK);
        }, format);
        ControllerHelp.LogWebServiceFk(nameof(WsWebApiScales), LocaleCore.WebService.UrlApiInfo,
            requestStampDt, string.Empty, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    [AllowAnonymous]
    [HttpGet]
    [Route("api/exception/")]
    public ContentResult GetException([FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "",
        [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "") =>
        ControllerHelp.GetContentResult(() => 
            new ServiceExceptionModel(filePath, lineNumber, memberName, "Test Exception!", "Test inner exception!")
            .GetContentResult<ServiceExceptionModel>(format, HttpStatusCode.InternalServerError), format);

    [AllowAnonymous]
    [HttpGet]
    [Route("api/simple/")]
    public ContentResult GetSimple([FromQuery(Name = "format")] string format = "",
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "") =>
        new TestControllerV2(SessionFactory).GetSimple(format);

    #endregion
}