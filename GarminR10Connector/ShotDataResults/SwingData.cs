namespace GarminR10Connector.ShotDataResults;

/// <summary>
/// Swing timing metrics for a single shot.
/// </summary>
internal class SwingData
{
    /// <summary>
    /// The timestamp when the backswing starts, in milliseconds since the device started.
    /// </summary>
    internal uint BackswingStart { get; init; }

    /// <summary>
    /// The timestamp when the downswing starts, in milliseconds since the device started.
    /// </summary>
    internal uint DownswingStart { get; init; }

    /// <summary>
    /// The timestamp when the impact occurs, in milliseconds since the device started.
    /// </summary>
    internal uint Impact { get; init; }

    /// <summary>
    /// The timestamp when the follow-through ends, in milliseconds since the device started.
    /// </summary>
    internal uint FollowThroughEnd { get; init; }
}
