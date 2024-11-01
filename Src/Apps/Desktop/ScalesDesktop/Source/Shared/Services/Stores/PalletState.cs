using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record PalletState(PalletDto? Pallet, PalletViewTabType PalletViewTabType)
{
    private PalletState() : this(null, PalletViewTabType.Info) { }
}

public record ChangePalletAction(PalletDto Pallet);

public class ChangePalletReducer : Reducer<PalletState, ChangePalletAction>
{
    public override PalletState Reduce(PalletState state, ChangePalletAction action) =>
        state.Pallet?.Equals(action.Pallet) == true ? state : state with { Pallet = action.Pallet };
}

public record ResetPalletAction;

public class ResetPalletReducer : Reducer<PalletState, ResetPalletAction>
{
    public override PalletState Reduce(PalletState state, ResetPalletAction action) => state with { Pallet = null };
}

public record SwitchPalletDeleteFlagAction;

public class SwitchPalletDeleteFlagReducer : Reducer<PalletState, SwitchPalletDeleteFlagAction>
{
    public override PalletState Reduce(PalletState state, SwitchPalletDeleteFlagAction action)
    {
        PalletDto? currentPallet = state.Pallet;
        if (currentPallet == null) return state;
        DateTime? newDeleteFlag = !currentPallet.IsDelete ? DateTime.Now : null;
        return state with { Pallet = currentPallet with { DeletedAt = newDeleteFlag } };
    }
}

public record ChangePalletViewTabAction(PalletViewTabType TabType);

public class ChangePalletViewTabReducer : Reducer<PalletState, ChangePalletViewTabAction>
{
    public override PalletState Reduce(PalletState state, ChangePalletViewTabAction action) => state with { PalletViewTabType = action.TabType };
}

public enum PalletViewTabType
{
    Info,
    Labels
}