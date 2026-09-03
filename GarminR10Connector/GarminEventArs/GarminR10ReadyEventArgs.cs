namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.ReadyChanged"/>.
/// </summary>
public sealed class GarminR10ReadyEventArgs : EventArgs
{
    /// <summary>
    /// Whether the device is currently ready to record a shot.
    /// </summary>
    public bool Ready { get; }

    /// <summary>
    /// Initializes the event args with the readiness state.
    /// </summary>
    /// <param name="Ready">Whether the device is currently ready to record a shot.</param>
    public GarminR10ReadyEventArgs(bool Ready)
    {
        this.Ready = Ready;
    }
}