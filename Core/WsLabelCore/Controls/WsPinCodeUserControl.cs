// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Controls;

#nullable enable
public sealed partial class WsPinCodeUserControl : WsBaseUserControl
{
    #region Public and private fields, properties, constructor

    private ElementHost ElementHost { get; }
    public WsPinCodeViewModel ViewModel { get; }
    private WsPinCodePage Page { get; }

    public WsPinCodeUserControl()
    {
        InitializeComponent();
        
        ViewModel = new();
        Page = new(ViewModel);
        ElementHost = new() { Dock = DockStyle.Fill };
        ElementHost.Child = Page;
        Controls.Add(ElementHost);
    }

    #endregion

    #region Public and private methods

    public override void RefreshAction()
    {
        WsActionUtils.ActionTryCatch(this, UserSession.Scale, () =>
        {
            //
        });
    }

    #endregion
}