using Blazor.Heroicons;
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

    private bool IsProduction { get; set; }
    private IEnumerable<MenuSection> MenuSections { get; set; } = [];
    private ClaimsPrincipal User { get; set; } = default!;

    protected override void OnInitialized()
    {
        IsProduction = !ConfigurationUtils.IsDevelop;
        MenuSections = CreateNavMenus();
    }

    protected override async Task OnInitializedAsync() => User = (await AuthState).User;

    private IEnumerable<MenuSection> CreateNavMenus() =>
    [
        new(Localizer["MenuDevices"], HeroiconName.ComputerDesktop, [
            new(Localizer["SectionArm"], Routes.Arms),
            new(Localizer["SectionPrinters"], Routes.Printers)
        ], PolicyEnum.Support),

        new(Localizer["Menu1CReferences"], HeroiconName.CurrencyEuro, [
            new(Localizer["SectionPlu"], Routes.Plus),
            new(Localizer["SectionBoxes"], Routes.Boxes),
            new(Localizer["SectionClips"], Routes.Clips),
            new(Localizer["SectionBundles"], Routes.Bundles),
            new(Localizer["SectionBrands"], Routes.Brands)
        ]),

        new(Localizer["MenuReferences"], HeroiconName.BookOpen, [
            new(Localizer["SectionWarehouses"], Routes.Warehouses),
            new(Localizer["SectionProductionSites"], Routes.ProductionSites)
        ], PolicyEnum.Support),

        new(Localizer["MenuPrintSettings"], HeroiconName.Printer, [
            new(Localizer["SectionTemplates"], Routes.Templates),
            new(Localizer["SectionTemplateResources"], Routes.Resources),
        ], PolicyEnum.Support),

        new(Localizer["MenuAdministration"], HeroiconName.UserGroup, [
            new(Localizer["SectionPalletMen"], Routes.PalletMen, PolicyEnum.Support),
            new(Localizer["SectionUsers"], Routes.Users, PolicyEnum.SeniorSupport),
        ], PolicyEnum.Support),

        new(Localizer["MenuDiagnostics"], HeroiconName.Wrench, [
            new(Localizer["SectionMigrations"], Routes.Migrations),
            new(Localizer["SectionTables"], Routes.Tables),
        ], PolicyEnum.Admin),
    ];
}
