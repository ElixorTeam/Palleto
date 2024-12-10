namespace Pl.Desktop.Client.Source.Shared.Models;

public enum MassaKStatus
{
    /// <summary>
    /// The device is successfully connected.
    /// </summary>
    Ready,

    /// <summary>
    /// The device is initializing.
    /// </summary>
    Initializing,

    /// <summary>
    /// The USB connection is lost, and an automatic reconnection attempt is in progress.
    /// </summary>
    Detached,

    /// <summary>
    /// The device is not in use.
    /// </summary>
    Disabled
}