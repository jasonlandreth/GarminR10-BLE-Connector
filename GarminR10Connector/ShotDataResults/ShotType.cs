namespace GarminR10Connector.ShotDataResults;

/// <summary>
/// Whether a shot was a normal recorded shot or a practice swing.
/// </summary>
internal enum ShotType
{
    /// <summary>
    /// A normal, recorded shot.
    /// </summary>
    Normal,

    /// <summary>
    /// A practice swing, not counted as a recorded shot.
    /// </summary>
    Practice
}