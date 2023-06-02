// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Components.Item;
using WsBlazorCore.Settings;
using WsStorageCore.TableScaleFkModels.PlusBundlesFks;

namespace DeviceControl.Pages.Menu.References1C.PlusBundlesFks;

public sealed partial class ItemPluBundleFk : ItemBase<WsSqlPluBundleFkModel>
{
    public ItemPluBundleFk() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticItem();
    }
}