// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsLabelCore.Common;

/// <summary>
/// Базовый класс модели представления.
/// </summary>
#nullable enable
[DebuggerDisplay("{ToString()}")]
public class WsXamlBaseViewModel : WsMvvmBaseViewModel, INotifyPropertyChanged
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Кэш БД.
    /// </summary>
    protected WsSqlContextCacheHelper ContextCache => WsSqlContextCacheHelper.Instance;
    /// <summary>
    /// Прервать.
    /// </summary>
    public WsActionCommandModel CmdAbort { get; private set; }
    /// <summary>
    /// Отменить.
    /// </summary>
    public WsActionCommandModel CmdCancel { get; private set; }
    /// <summary>
    /// Настроить.
    /// </summary>
    public WsActionCommandModel CmdCustom { get; private set; }
    /// <summary>
    /// Игнорировать.
    /// </summary>
    public WsActionCommandModel CmdIgnore { get; private set; }
    /// <summary>
    /// Нет.
    /// </summary>
    public WsActionCommandModel CmdNo { get; private set; }
    /// <summary>
    /// Ок.
    /// </summary>
    public WsActionCommandModel CmdOk { get; private set; }
    /// <summary>
    /// Повторить.
    /// </summary>
    public WsActionCommandModel CmdRetry { get; private set; }
    /// <summary>
    /// Да.
    /// </summary>
    public WsActionCommandModel CmdYes { get; private set; }
    /// <summary>
    /// Список команд.
    /// </summary>
    public ObservableCollection<WsActionCommandModel> Commands { get; } = new();
    /// <summary>
    /// Ширина кнопки.
    /// </summary>
    public int ButtonWidth { get; set; } = 100;
    /// <summary>
    /// Размер шрифта сообщения.
    /// </summary>
    public double FontSizeMessage => 26;
    /// <summary>
    /// Размер шрифта кнопок.
    /// </summary>
    public double FontSizeButton => 24;
    /// <summary>
    /// Сообщение.
    /// </summary>
    public string Message { get; set; } = "";
    /// <summary>
    /// Видимость сообщения.
    /// </summary>
    public Visibility MessageVisibility => string.IsNullOrEmpty(Message) ? Visibility.Hidden : Visibility.Visible;

    public WsXamlBaseViewModel()
    {
        SetupActionsEmpty();
    }

    #endregion

    #region Public and private methods - Commands

    public override string ToString() => 
        (Commands.Any() ? $"{string.Join(" | ", Commands.Select(item => item.ToString()))}" : "<Empty>") + 
        (string.IsNullOrEmpty(Message) ? string.Empty : $" | {Message}");

    #endregion

    #region Public and private methods

    /// <summary>
    /// Настройка действия Ок.
    /// </summary>
    /// <param name="actionOk"></param>
    private void AddActionsOk(Action actionOk)
    {
        CmdOk.AddAction(actionOk);
        CmdOk.Visibility = Visibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка кастом действия.
    /// </summary>
    /// <param name="actionCustom"></param>
    private void AddActionsCustom(Action actionCustom)
    {
        CmdCustom.AddAction(actionCustom);
        CmdCustom.Visibility = Visibility.Hidden;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка действий Отмена/Да.
    /// </summary>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    private void AddActionsCancelYes(Action actionCancel, Action actionYes)
    {
        CmdCancel.AddAction(actionCancel);
        CmdCancel.Visibility = Visibility.Visible;
        CmdYes.AddAction(actionYes);
        CmdYes.Visibility = Visibility.Visible;
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Настройка пустых действий.
    /// </summary>
    private void SetupActionsEmpty()
    {
        CmdAbort = new(nameof(CmdAbort), LocaleCore.Buttons.Abort, Visibility.Hidden);
        CmdCancel = new(nameof(CmdCancel), LocaleCore.Buttons.Cancel, Visibility.Hidden);
        CmdCustom = new(nameof(CmdCustom), LocaleCore.Buttons.Custom, Visibility.Hidden);
        CmdIgnore = new(nameof(CmdIgnore), LocaleCore.Buttons.Ignore, Visibility.Hidden);
        CmdNo = new(nameof(CmdNo), LocaleCore.Buttons.No, Visibility.Hidden);
        CmdOk = new(nameof(CmdOk), LocaleCore.Buttons.Ok, Visibility.Hidden);
        CmdRetry = new(nameof(CmdRetry), LocaleCore.Buttons.Retry, Visibility.Hidden);
        CmdYes = new(nameof(CmdYes), LocaleCore.Buttons.Yes, Visibility.Hidden);
        UpdateCommandsFromActions();
    }

    /// <summary>
    /// Обновить список команд из действий.
    /// </summary>
    public void UpdateCommandsFromActions()
    {
        Commands.Clear();
        if (CmdAbort.Action is not null) Commands.Add(CmdAbort);
        //if (CmdCustom.Action is not null) Commands.Add(CmdCustom);
        if (CmdIgnore.Action is not null) Commands.Add(CmdIgnore);
        if (CmdNo.Action is not null) Commands.Add(CmdNo);
        if (CmdOk.Action is not null) Commands.Add(CmdOk);
        if (CmdRetry.Action is not null) Commands.Add(CmdRetry);
        if (CmdYes.Action is not null) Commands.Add(CmdYes);
        if (CmdCancel.Action is not null) Commands.Add(CmdCancel);
    }

    /// <summary>
    /// Обработчик нажатия кнопки.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Button_KeyUp(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                CmdCancel.Relay();
                break;
            case Key.Enter:
                CmdOk.Relay();
                break;
        }
    }

    /// <summary>
    /// Настройка ширины кнопок.
    /// </summary>
    /// <param name="controlWidth"></param>
    public void SetupButtonsWidth(int controlWidth) => ButtonWidth = !Commands.Any() ? controlWidth - 22 : controlWidth / Commands.Count - 22;

    /// <summary>
    /// Настройка кнопок Отмена/Да.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionCancel"></param>
    /// <param name="actionYes"></param>
    /// <param name="actionBack"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsCancelYes(string message, Action actionCancel, Action actionYes, Action actionBack, int controlWidth)
    {
        Message = message;
        actionCancel += actionBack;
        actionYes += actionBack;
        AddActionsCancelYes(actionCancel, actionYes);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кнопок Ок.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionOk"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsOk(string message, Action actionOk, int controlWidth)
    {
        Message = message;
        AddActionsOk(actionOk);
        SetupButtonsWidth(controlWidth);
    }

    /// <summary>
    /// Настройка кастом кнопки.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="actionCustom"></param>
    /// <param name="controlWidth"></param>
    public void SetupButtonsCustom(string message, Action actionCustom, int controlWidth)
    {
        Message = message;
        AddActionsCustom(actionCustom);
        SetupButtonsWidth(controlWidth);
    }

    #endregion
}