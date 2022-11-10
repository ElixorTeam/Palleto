﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataCore.Models;
using WebApiCore.Controllers;
using WebApiCore.Utils;

namespace WebApiTerra1000.Controllers;

public class ContragentControllerV2 : BaseController
{
    #region Constructor and destructor

    public ContragentControllerV2(ISessionFactory sessionFactory) : base(sessionFactory)
    {
        //
    }

    #endregion

    #region Public and private methods

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragent/")]
    public ContentResult GetContragentFromCodeIdProd(string code, long id, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodeProd : SqlQueriesContragentsV2.GetContragentFromIdProd,
            code, id, format);

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragent_preview/")]
    public ContentResult GetContragentFromCodeIdPreview(string code, long id, FormatTypeEnum format = FormatTypeEnum.Xml) =>
        GetContragentFromCodeIdWork(code != null
            ? SqlQueriesContragentsV2.GetContragentFromCodePreview : SqlQueriesContragentsV2.GetContragentFromIdPreview,
            code, id, format);

    private ContentResult GetContragentFromCodeIdWork(string url, string code, long id, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url,
                code != null ? WebUtils.Sql.GetParametersV2(code) : WebUtils.Sql.GetParametersV2(id));
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragents/")]
    public ContentResult GetContragentsProd(DateTime? startDate, DateTime? endDate = null,
        int? offset = null, int? rowCount = null, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetProd, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesProd, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDateProd, startDate, endDate, offset, rowCount, format);
        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyProd, format);
    }

    [AllowAnonymous]
    [HttpGet()]
    [Route("api/v2/contragents_preview/")]
    public ContentResult GetContragentsPreview(DateTime? startDate, DateTime? endDate = null,
        int? offset = null, int? rowCount = null, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        if (startDate != null && endDate != null && offset != null && rowCount != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesOffsetPreview, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate != null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromDatesPreview, startDate, endDate, offset, rowCount, format);
        else if (startDate != null && endDate == null)
            return GetContragentsWork(SqlQueriesContragentsV2.GetContragentsFromStartDatePreview, startDate, endDate, offset, rowCount, format);
        return GetContragentsEmptyWork(SqlQueriesContragentsV2.GetContragentsEmptyPreview, format);
    }

    private ContentResult GetContragentsEmptyWork(string url, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Goods} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    private ContentResult GetContragentsWork(string url, DateTime? startDate = null, DateTime? endDate = null,
        int? offset = null, int? rowCount = null, FormatTypeEnum format = FormatTypeEnum.Xml)
    {
        return ControllerHelp.RunTask(new Task<ContentResult>(() =>
        {
            List<SqlParameter> parameters = null;
            if (startDate != null && endDate != null && offset != null && rowCount != null)
                parameters = WebUtils.Sql.GetParametersV2(startDate, endDate, offset, rowCount);
            else if (startDate != null && endDate != null)
                parameters = WebUtils.Sql.GetParametersV2(startDate, endDate);
            else if (startDate != null && endDate == null)
                parameters = WebUtils.Sql.GetParametersV2(startDate);
            string response = WebUtils.Sql.GetResponse<string>(SessionFactory, url, parameters);
            XDocument xml = XDocument.Parse(response ?? $"<{WebConstants.Contragents} />", LoadOptions.None);
            XDocument doc = new(new XElement(WebConstants.Response, xml.Root));
            return SerializeDeprecatedModel<XDocument>.GetResult(format, doc, HttpStatusCode.OK);
        }), format);
    }

    #endregion
}
