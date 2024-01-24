﻿using System.Xml.Linq;
using Ws.Domain.Services.Features.LogWeb;
using Ws.WebApiScales.Common;
using Ws.WebApiScales.Dto;
using Ws.WebApiScales.Features.Plu.Dto;
using Ws.WebApiScales.Features.Plu.Services;

namespace Ws.WebApiScales.Features.Plu;

[AllowAnonymous]
[ApiController]
[Route("api/plu/")]
internal sealed class  PluController(
    IPluApiService pluApiService,
    ILogWebService logWebService,
    IHttpContextAccessor httpContextAccessor,
    ResponseDto responseDto) : BaseController(responseDto, logWebService, httpContextAccessor)
{

    [HttpPost("load")]
    [Produces("application/xml")]
    public ContentResult LoadPlu([FromBody] XElement xml) => 
        HandleXmlRequest<PlusWrapper>(xml, pluApiService.Load);
}