using Pl.Exchange.Api.App.Features.Clips.Dto;

namespace Pl.Exchange.Api.App.Features.Clips.Common;

public interface IClipService
{
    ResponseDto Load(HashSet<ClipDto> dtos);
}