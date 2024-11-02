using Pl.Exchange.Api.App.Features.Plus.Dto;

namespace Pl.Exchange.Api.App.Features.Plus.Common;

public interface IPluService
{
    public ResponseDto Load(HashSet<PluDto> dtos);
}