// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.ViewModels;

#nullable enable
public sealed class WsPlusViewModel : WsWpfBaseViewModel
{
    #region Public and private fields, properties, constructor

    public WsSqlPluScaleModel PluScale { get; set; }

    public WsPlusViewModel()
    {
        PluScale = new();
    }

    #endregion
}