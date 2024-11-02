namespace Pl.Desktop.Client.Source.Shared.Services.Stores;

[FeatureState]
public record WeightState(int Weight, bool IsStable)
{
    private WeightState() : this(0, false) { }
}

public record ChangeWeightAction(int Weight, bool IsStable);

public class ChangeWeightReducer : Reducer<WeightState, ChangeWeightAction>
{
    public override WeightState Reduce(WeightState state, ChangeWeightAction action) =>
        new(action.Weight, action.IsStable);
}