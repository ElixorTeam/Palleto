using Pl.Exchange.Api.App.Features.Bundles.Dto;

namespace Pl.Exchange.Api.App.Features.Bundles.Common;

public interface IBundleService
{
    ResponseDto Load(HashSet<BundleDto> dto);
}