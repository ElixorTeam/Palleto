using Pl.Exchange.Api.App.Features.Characteristics.Impl.Models;

namespace Pl.Exchange.Api.App.Features.Characteristics.Common;

public interface ICharacteristicService
{
    public ResponseDto Load(HashSet<GroupedCharacteristic> dtos);
}