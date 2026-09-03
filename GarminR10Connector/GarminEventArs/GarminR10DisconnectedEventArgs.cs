namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.Disconnected"/>.
/// </summary>
public sealed class GarminR10DisconnectedEventArgs : EventArgs
{
    /// <summary>
    /// The paired Bluetooth device name that was disconnected from.
    /// </summary>
    public string DeviceName { get; }

    /// <summary>
    /// Whether <see cref="GarminR10Client"/> will automatically attempt to reconnect.
    /// </summary>
    public bool WillAutoReconnect { get; }

    /// <summary>
    /// Initializes the event args for a disconnect.
    /// </summary>
    /// <param name="DeviceName">The paired Bluetooth device name that was disconnected from.</param>
    /// <param name="WillAutoReconnect">Whether an automatic reconnect attempt will follow.</param>
    public GarminR10DisconnectedEventArgs(string DeviceName, bool WillAutoReconnect)
    {
        this.DeviceName = DeviceName;
        this.WillAutoReconnect = WillAutoReconnect;
    }
}