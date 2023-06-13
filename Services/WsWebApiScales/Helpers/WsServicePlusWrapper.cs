// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiScales.Helpers;

/// <summary>
/// Nomenclatures controller.
/// </summary>
[Tags(WsLocalizationCore.Utils.WsLocaleWebServiceUtils.Tag1CNomenclatures)]
public sealed class WsServicePlusWrapper : WsServiceControllerBase
{
    #region Public and private fields, properties, constructor

    private WsServicePlusController PlusController { get; }

    public WsServicePlusWrapper(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        PlusController = new(sessionFactory);
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [Produces("application/xml")]
    [HttpPost]
    [Route(WsLocaleWebServiceUtils.SendNomenclatures)]
    public ContentResult SendPlus([FromBody] XElement xml, [FromQuery(Name = "format")] string format = "",
        [FromQuery(Name = "debug")] bool isDebug = false, 
        [FromHeader(Name = "host")] string host = "", [FromHeader(Name = "accept")] string version = "")
    {
        DateTime requestStampDt = DateTime.Now;
        ContentResult result = GetAcceptVersion(version) switch
        {
            // Новый ответ 1С - не найдено.
            WsSqlAcceptVersion.V2 => GetContentResult(() => 
                NewResponse1CIsNotFound($"Version {version} {WsLocaleCore.WebService.IsNotFound}!", format, isDebug, SessionFactory), format),
            // Находится в разработке, свяжитесь с разработчиком.
            _ => NewResponse1CIsNotFound($"{WsLocaleCore.WebService.Underdevelopment}!", format, isDebug, SessionFactory)
            //_ => GetContentResult(() => PlusController.NewResponsePlus(xml, format, isDebug, SessionFactory), format)
        };
        LogWebServiceFk(nameof(WsWebApiScales), WsLocaleWebServiceUtils.SendNomenclatures,
            requestStampDt, xml, result.Content ?? string.Empty, format, host, version).ConfigureAwait(false);
        return result;
    }

    #endregion
}