namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.BatteryUpdated"/>.
/// </summary>
public sealed class GarminR10BatteryEventArgs : EventArgs
{
    /// <summary>
    /// The device's battery level, as a percentage from 0 to 100.
    /// </summary>
    public int BatteryPercent { get; }

    /// <summary>
    /// Initializes the event args with the battery level.
    /// </summary>
    /// <param name="BatteryPercent">The device's battery level, as a percentage from 0 to 100.</param>
    public GarminR10BatteryEventArgs(int BatteryPercent)
    {
        this.BatteryPercent = BatteryPercent;
    }
}
