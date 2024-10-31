using Blazor.Heroicons;
using DeviceControl.Source.Shared.Constants;

namespace DeviceControl.Source.Widgets.Sidebar;

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
            new(Localizer["SectionLines"], Urls.Lines),
            new(Localizer["SectionPrinters"], Urls.Printers)
        ], PolicyEnum.Support),

        new(Localizer["Menu1CReferences"], HeroiconName.CurrencyEuro, [
            new(Localizer["SectionPLU"], Urls.Plus),
            new(Localizer["SectionBoxes"], Urls.Boxes),
            new(Localizer["SectionClips"], Urls.Clips),
            new(Localizer["SectionBundles"], Urls.Bundles),
            new(Localizer["SectionBrands"], Urls.Brands)
        ]),

        new(Localizer["MenuReferences"], HeroiconName.BookOpen, [
            new(Localizer["SectionWarehouses"], Urls.Warehouses),
            new(Localizer["SectionProductionSites"], Urls.ProductionSites)
        ], PolicyEnum.SeniorSupport),

        new(Localizer["MenuPrintSettings"], HeroiconName.Printer, [
            new(Localizer["SectionTemplates"], Urls.Templates),
            new(Localizer["SectionTemplateResources"], Urls.Resources),
        ], PolicyEnum.SeniorSupport),

        new(Localizer["MenuAdministration"], HeroiconName.UserGroup, [
            new(Localizer["SectionPalletMen"], Urls.PalletMen, PolicyEnum.Support),
            new(Localizer["SectionUsers"], Urls.Users, PolicyEnum.SeniorSupport),
        ], PolicyEnum.Support),

        new(Localizer["MenuDiagnostics"], HeroiconName.Wrench, [
            new(Localizer["SectionMigrations"], Urls.Migrations),
            new(Localizer["SectionTables"], Urls.Tables),
            new(Localizer["SectionAnalytics"], Urls.Analytics)
        ], PolicyEnum.Admin),
    ];
}
