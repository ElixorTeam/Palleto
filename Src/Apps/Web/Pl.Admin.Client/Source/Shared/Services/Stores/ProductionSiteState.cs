using Fluxor;
// ReSharper disable ClassNeverInstantiated.Global

namespace Pl.Admin.Client.Source.Shared.Services.Stores;

[FeatureState]
public record ProductionSiteState(ProxyDto ProductionSite)
{
    public static ProxyDto EmptyProductionSite { get; } = new(Guid.Empty, string.Empty);
    private ProductionSiteState() : this(EmptyProductionSite) { }
}

public record ChangeProductionSiteAction(ProxyDto ProductionSite);

public class ChangeProductionSiteReducer : Reducer<ProductionSiteState, ChangeProductionSiteAction>
{
    public override ProductionSiteState Reduce(ProductionSiteState state, ChangeProductionSiteAction action) =>
        !state.ProductionSite.Id.IsEmpty() && state.ProductionSite.Equals(action.ProductionSite) ? state : new(action.ProductionSite);
}

public record ResetProductionSiteAction;

public class ResetProductionSiteReducer : Reducer<ProductionSiteState, ResetProductionSiteAction>
{
    public override ProductionSiteState Reduce(ProductionSiteState state, ResetProductionSiteAction action) =>
        new(ProductionSiteState.EmptyProductionSite);
}