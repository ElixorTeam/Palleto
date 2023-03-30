// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Helpers;

/// <summary>
/// Nomenclatures groups controller.
/// </summary>
public sealed class PlusGroupsController : WsControllerBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="sessionFactory"></param>
    public PlusGroupsController(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(UrlWebService.SendNomenclaturesGroups)]
    public ContentResult SendPluGroups([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false,
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            AcceptVersion.V2 =>
                GetContentResult(() => NewResponse1cIsNotFound(version, format, isDebug, SessionFactory),
                    format),
            _ => GetContentResult(() => PlusGroupsControl.NewResponse1cPluGroups(xml, format, isDebug, SessionFactory), format)
        };
        LogWebServiceFk(nameof(WsWebApiScales), UrlWebService.SendNomenclaturesGroups,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}