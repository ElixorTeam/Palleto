using Pl.Desktop.Models.Features.PalletMen;

namespace Pl.Desktop.Client.Source.Shared.Services.Stores;

[FeatureState]
public record PalletManState(PalletManDto? PalletMan)
{
    private PalletManState() : this(PalletMan: null) { }
}

public record ChangePalletManAction(PalletManDto PalletManDto);

public class ChangePalletManReducer : Reducer<PalletManState, ChangePalletManAction>
{
    public override PalletManState Reduce(PalletManState state, ChangePalletManAction action) =>
        state.PalletMan?.Equals(action.PalletManDto) == true ? state : new(action.PalletManDto);
}

public record ResetPalletManAction;

public class ResetPalletManReducer : Reducer<PalletManState, ResetPalletManAction>
{
    public override PalletManState Reduce(PalletManState state, ResetPalletManAction action) => new(null);
}