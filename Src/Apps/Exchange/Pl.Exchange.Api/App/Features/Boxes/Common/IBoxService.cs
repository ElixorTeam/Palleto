using Pl.Exchange.Api.App.Features.Boxes.Dto;

namespace Pl.Exchange.Api.App.Features.Boxes.Common;

public interface IBoxService
{
    ResponseDto Load(HashSet<BoxDto> dtos);
}