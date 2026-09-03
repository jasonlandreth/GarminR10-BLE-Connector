namespace GarminR10Connector.WinFormExample.DecodeTest;

/// <summary>
/// Swing timing metrics for a single shot.
/// </summary>
public class SwingDataTest
{
    /// <summary>
    /// The timestamp when the backswing starts, in milliseconds since the device started.
    /// </summary>
    public uint BackswingStart { get; init; }

    /// <summary>
    /// The timestamp when the downswing starts, in milliseconds since the device started.
    /// </summary>
    public uint DownswingStart { get; init; }

    /// <summary>
    /// The timestamp when the impact occurs, in milliseconds since the device started.
    /// </summary>
    public uint Impact { get; init; }

    /// <summary>
    /// The timestamp when the follow-through ends, in milliseconds since the device started.
    /// </summary>
    public uint FollowThroughEnd { get; init; }
}
