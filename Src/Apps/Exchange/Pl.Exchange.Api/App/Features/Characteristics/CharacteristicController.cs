using Pl.Exchange.Api.App.Features.Characteristics.Common;
using Pl.Exchange.Api.App.Features.Characteristics.Dto;

namespace Pl.Exchange.Api.App.Features.Characteristics;

[ApiController]
[Route(ApiEndpoints.Characteristics)]
public sealed class CharacteristicController(ICharacteristicService characteristicService)
{
    [HttpPost("load")]
    public ResponseDto Load([FromBody] PluCharacteristicsWrapper wrapper) =>
        characteristicService.Load(wrapper.ToGrouped());
}