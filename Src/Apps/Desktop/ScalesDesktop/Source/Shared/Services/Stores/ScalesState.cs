using ScalesDesktop.Source.Shared.Services.Devices;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record ScalesState(MassaKStatus Status)
{
    private ScalesState() : this(MassaKStatus.Disabled) { }
}

public record ChangeScalesStatusAction(MassaKStatus Status);

public class ChangeScalesStatusReducer : Reducer<ScalesState, ChangeScalesStatusAction>
{
    public override ScalesState Reduce(ScalesState state, ChangeScalesStatusAction action) =>
        state.Status == action.Status ? state : new(action.Status);
}