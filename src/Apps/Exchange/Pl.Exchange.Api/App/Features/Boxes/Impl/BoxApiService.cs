using Pl.Exchange.Api.App.Features.Boxes.Common;
using Pl.Exchange.Api.App.Features.Boxes.Dto;

namespace Pl.Exchange.Api.App.Features.Boxes.Impl;

internal sealed partial class BoxApiService(BoxDtoValidator validator, ILogger<BoxApiService> logger) :
    BaseService<BoxDto>(validator), IBoxService
{
    public ResponseDto Load(HashSet<BoxDto> dtos)
    {
        ResolveUniqueUidLocal(dtos);
        FilterValidDtos(dtos);
        SaveBoxes(dtos);
        return OutputDto;
    }
}