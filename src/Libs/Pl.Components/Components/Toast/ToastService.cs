namespace Pl.Components;

/// <summary>
/// Service for managing toast notifications.
/// Register as a singleton or scoped service in DI.
/// </summary>
public class ToastService
{
    private readonly List<ToastData> _toasts = [];
    private readonly Lock _sync = new();

    /// <summary>
    /// Event fired when the toast collection changes.
    /// </summary>
    public event Action? OnChange;

    /// <summary>
    /// Gets the current list of toasts.
    /// </summary>
    public IReadOnlyList<ToastData> Toasts
    {
        get
        {
            lock (_sync)
            {
                return _toasts.ToArray();
            }
        }
    }

    /// <summary>
    /// Gets or sets the runtime toast position override.
    /// When set, this takes priority over the ToastProvider's Position parameter.
    /// Set to null to revert to the provider's default.
    /// </summary>
    public ToastPosition? Position { get; private set; }

    /// <summary>
    /// Resets the toast position to the ToastProvider's default.
    /// </summary>
    public void ResetPosition()
    {
        Position = null;
        OnChange?.Invoke();
    }

    /// <summary>
    /// Shows a toast notification.
    /// </summary>
    /// <param name="toast">The toast data to display.</param>
    public void Show(ToastData toast)
    {
        lock (_sync)
            _toasts.Add(toast);
        OnChange?.Invoke();
    }

    /// <summary>
    /// Shows a simple toast with a message.
    /// </summary>
    /// <param name="description">The message to display.</param>
    /// <param name="title">Optional title.</param>
    /// <param name="variant">The visual variant.</param>
    /// <param name="duration">Duration in milliseconds (default 5000).</param>
    public void Show(string description, string? title = null, ToastVariant variant = ToastVariant.Default,
        int duration = 3000)
    {
        Show(new()
        {
            Title = title,
            Description = description,
            Variant = variant,
            Duration = duration
        });
    }

    /// <summary>
    /// Shows a success toast with a green accent and circle-check icon.
    /// </summary>
    /// <param name="description">The message to display.</param>
    /// <param name="title">Optional title.</param>
    public void Success(string description, string? title = null) =>
        Show(description, title, ToastVariant.Success);

    /// <summary>
    /// Shows an info toast with a blue accent and info icon.
    /// </summary>
    /// <param name="description">The message to display.</param>
    /// <param name="title">Optional title.</param>
    public void Info(string description, string? title = null) =>
        Show(description, title, ToastVariant.Info);

    /// <summary>
    /// Shows a warning toast with amber accent and triangle-alert icon.
    /// </summary>
    /// <param name="description">The message to display.</param>
    /// <param name="title">Optional title.</param>
    public void Warning(string description, string? title = null) =>
        Show(description, title, ToastVariant.Warning);

    /// <summary>
    /// Shows an error toast (destructive variant).
    /// </summary>
    /// <param name="description">The message to display.</param>
    /// <param name="title">Optional title.</param>
    public void Error(string description, string? title = null) =>
        Show(description, title, ToastVariant.Destructive);

    /// <summary>
    /// Dismisses a specific toast by ID.
    /// </summary>
    /// <param name="id">The toast ID to dismiss.</param>
    public void Dismiss(string id)
    {
        bool removed;
        lock (_sync)
        {
            var toast = _toasts.FirstOrDefault(t => t.Id == id);
            removed = toast != null && _toasts.Remove(toast);
        }

        if (removed)
            OnChange?.Invoke();
    }

    /// <summary>
    /// Dismisses all toasts.
    /// </summary>
    public void DismissAll()
    {
        lock (_sync)
            _toasts.Clear();
        OnChange?.Invoke();
    }
}