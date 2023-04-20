// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Windows.Controls;
using WsStorageCore.TableScaleFkModels.PlusNestingFks;
using WsStorageCore.TableScaleModels.ProductionFacilities;
using ComboBox = System.Windows.Controls.ComboBox;

namespace WsLabelCore.Wpf.Pages;

#nullable enable
public class WpfPageBase : UserControl
{
    #region Public and private fields, properties, constructor

    public WsUserSessionHelper UserSession { get; private set; }
    public System.Windows.Forms.DialogResult Result { get; protected set; }
    public RoutedEventHandler? OnClose { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    protected WpfPageBase()
    {
        UserSession = WsUserSessionHelper.Instance;
        Result = System.Windows.Forms.DialogResult.Cancel;
        OnClose = null;

        if (!Resources.Contains(nameof(UserSession)))
            Resources.Add(nameof(UserSession), WsUserSessionHelper.Instance);
        object context = FindResource(nameof(UserSession));
        if (context is WsUserSessionHelper userSession)
            UserSession = userSession;
        else
            UserSession = WsUserSessionHelper.Instance;
    }

    #endregion

    #region Public and private methods

    protected void SetScale(ComboBox comboBox)
    {
        int i = 0;
        foreach (ScaleModel scale in UserSession.Scales)
        {
            if (Equals(UserSession.Scale.IdentityValueId, scale.IdentityValueId))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    protected void SetProductionFacility(ComboBox comboBox)
    {
        int i = 0;
        foreach (ProductionFacilityModel productionFacility in UserSession.ProductionFacilities)
        {
            if (Equals(UserSession.ProductionFacility.IdentityValueId, productionFacility.IdentityValueId))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    protected void SetPluNestingFk(ComboBox comboBox)
    {
        int i = 0;
        foreach (PluNestingFkModel pluNestingFk in UserSession.PluNestingFks)
        {
            if (Equals(UserSession.PluNestingFk.IdentityValueUid, pluNestingFk.IdentityValueUid))
            {
                comboBox.SelectedIndex = i;
                break;
            }
            i++;
        }
        if (comboBox.SelectedIndex == -1)
            comboBox.SelectedIndex = 0;
    }

    #endregion
}