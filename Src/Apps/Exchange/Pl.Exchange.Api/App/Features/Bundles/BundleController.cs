using Pl.Exchange.Api.App.Features.Bundles.Common;
using Pl.Exchange.Api.App.Features.Bundles.Dto;

namespace Pl.Exchange.Api.App.Features.Bundles;

[ApiController]
[Route(ApiEndpoints.Bundles)]
public sealed class BundleController(IBundleService bundleService) : ControllerBase
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] BundlesWrapper wrapper) => bundleService.Load(wrapper.Bundles);
}