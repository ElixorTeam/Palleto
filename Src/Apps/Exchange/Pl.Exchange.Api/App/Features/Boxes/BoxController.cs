using Pl.Exchange.Api.App.Features.Boxes.Common;
using Pl.Exchange.Api.App.Features.Boxes.Dto;

namespace Pl.Exchange.Api.App.Features.Boxes;

[ApiController]
[Route(ApiEndpoints.Boxes)]
public sealed class BoxController(IBoxService boxService) : ControllerBase
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] BoxesWrapper wrapper) => boxService.Load(wrapper.Boxes);
}