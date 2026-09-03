namespace GarminR10Connector.WinFormExample.DecodeTest;

/// <summary>
/// Whether a shot was a normal recorded shot or a practice swing.
/// </summary>
public enum ShotTypeTest
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
