// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableDiagModels.ScalesScreenshots;
using WsStorageCore.TableDiagModels.ScalesScreenshots;

namespace BlazorDeviceControl.Pages.Menu.Logs.SectionScalesScreenShots;

public sealed partial class ItemScaleScreenShot : RazorComponentItemBase<ScaleScreenShotModel>
{
    #region Public and private fields, properties, constructor

    private string ImagePath { get; set; }

    public ItemScaleScreenShot() : base()
    {
	    ImagePath = string.Empty;
        ButtonSettings = new(false, false, false, false, false, false, true);
    }

    #endregion

    #region Public and private methods

    protected override void OnParametersSet()
    {
        RunActionsParametersSet(new()
        {
            () =>
            {
                SqlItemCast = ContextManager.AccessManager.AccessItem.GetItemNotNullable<ScaleScreenShotModel>(IdentityUid);
                if (SqlItemCast.ScreenShot.Length > 1)
					ImagePath = "data:image/png;base64, " + Convert.ToBase64String(SqlItemCast.ScreenShot);
				if (SqlItemCast.IsNew)
                {
                    SqlItemCast = SqlItemNew<ScaleScreenShotModel>();
                }
            }
        });
    }

    #endregion
}