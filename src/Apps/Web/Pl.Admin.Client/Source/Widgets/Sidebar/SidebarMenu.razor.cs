using Pl.Admin.Client.Source.Shared.Constants;

namespace Pl.Admin.Client.Source.Widgets.Sidebar;

#region Records

public record NavMenuItemModel(string Name, string Link, string Claim = "");
public record MenuSection(string Label, string Icon, NavMenuItemModel[] Items, string Claim = "");

#endregion

public sealed partial class SidebarMenu : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [CascadingParameter] private Task<AuthenticationState> AuthState { get; set; } = default!;

    private IEnumerable<MenuSection> MenuSections { get; set; } = [];

    protected override void OnInitialized()
    {
        MenuSections = CreateNavMenus();
    }

    private IEnumerable<MenuSection> CreateNavMenus() =>
    [
        new(Localizer["MenuDevices"], "computer-desktop", [
            new(Localizer["SectionArm"], Routes.Arms),
            new(Localizer["SectionPrinters"], Routes.Printers)
        ], Policies.Support),

        new(Localizer["Menu1CReferences"], "currency-euro", [
            new(Localizer["SectionPlu"], Routes.Plus),
            new(Localizer["SectionBoxes"], Routes.Boxes),
            new(Localizer["SectionClips"], Routes.Clips),
            new(Localizer["SectionBundles"], Routes.Bundles),
            new(Localizer["SectionBrands"], Routes.Brands)
        ]),

        new(Localizer["MenuReferences"], "book-open", [
            new(Localizer["SectionWarehouses"], Routes.Warehouses),
            new(Localizer["SectionProductionSites"], Routes.ProductionSites)
        ], Policies.Support),

        new(Localizer["MenuPrintSettings"], "printer", [
            new(Localizer["SectionTemplates"], Routes.Templates),
            new(Localizer["SectionTemplateResources"], Routes.Resources),
        ], Policies.Support),

        new(Localizer["MenuAdministration"], "user-group", [
            new(Localizer["SectionPalletMen"], Routes.PalletMen, Policies.Support),
            new(Localizer["SectionUsers"], Routes.Users, Policies.SeniorSupport),
        ], Policies.Support),

        new(Localizer["MenuDiagnostics"], "wrench", [
            new(Localizer["SectionMigrations"], Routes.Migrations),
            new(Localizer["SectionTables"], Routes.Tables),
        ], Policies.Admin),
    ];
}
