namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.Connected"/>.
/// </summary>
public sealed class GarminR10ConnectedEventArgs : EventArgs
{
    /// <summary>
    /// The paired Bluetooth device name that was connected to.
    /// </summary>
    public string DeviceName { get; }

    /// <summary>
    /// The device's reported model name, if available.
    /// </summary>
    public string? Model { get; }

    /// <summary>
    /// The device's reported firmware version, if available.
    /// </summary>
    public string? Firmware { get; }

    /// <summary>
    /// The device's reported serial number, if available.
    /// </summary>
    public string? Serial { get; }

    /// <summary>
    /// The device's battery level, as a percentage from 0 to 100, at the time of connection.
    /// </summary>
    public int Battery { get; }

    /// <summary>
    /// Initializes the event args with the device's info at the time of connection.
    /// </summary>
    /// <param name="DeviceName">The paired Bluetooth device name that was connected to.</param>
    /// <param name="Model">The device's reported model name, if available.</param>
    /// <param name="Firmware">The device's reported firmware version, if available.</param>
    /// <param name="Serial">The device's reported serial number, if available.</param>
    /// <param name="Battery">The device's battery level, as a percentage from 0 to 100.</param>
    public GarminR10ConnectedEventArgs(string DeviceName, string? Model, string? Firmware, string? Serial, int Battery)
    {
        this.DeviceName = DeviceName;
        this.Model = Model;
        this.Firmware = Firmware;
        this.Serial = Serial;
        this.Battery = Battery;
    }
}