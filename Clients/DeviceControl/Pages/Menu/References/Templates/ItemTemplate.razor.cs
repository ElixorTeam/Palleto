// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class ItemTemplate : ItemBase<WsSqlTemplateModel>
{
    #region Public and private fields, properties, constructor

    private List<string> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories = BlazorAppSettingsHelper.Instance.DataSourceDics.GetTemplateCategories();
    }

    #endregion
}
