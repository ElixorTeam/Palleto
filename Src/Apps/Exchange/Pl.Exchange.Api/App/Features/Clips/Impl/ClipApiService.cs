using Pl.Exchange.Api.App.Features.Clips.Common;
using Pl.Exchange.Api.App.Features.Clips.Dto;

namespace Pl.Exchange.Api.App.Features.Clips.Impl;

internal sealed partial class ClipApiService(ClipDtoValidator validator, ILogger<ClipApiService> logger) : BaseService<ClipDto>(validator), IClipService
{
    public ResponseDto Load(HashSet<ClipDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        FilterValidDtos(dtos);
        SaveClips(dtos);
        return OutputDto;
    }
}