using TscZebra.Plugin.Abstractions.Enums;

namespace Pl.Desktop.Client.Source.Shared.Services.Stores;

[FeatureState]
public record PrinterState(PrinterStatus Status)
{
    private PrinterState() : this(PrinterStatus.Disconnected) { }
}

public record ChangePrinterStatusAction(PrinterStatus Status);

public class ChangePrinterStatusReducer : Reducer<PrinterState, ChangePrinterStatusAction>
{
    public override PrinterState Reduce(PrinterState state, ChangePrinterStatusAction action) =>
        state.Status == action.Status ? state : new(action.Status);
}