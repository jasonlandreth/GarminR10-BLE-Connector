namespace GarminR10Connector.ShotDataResults;

/// <summary>
/// Spin calculation method used to derive spin metrics.
/// </summary>
internal enum SpinCalcType
{
    /// <summary>
    /// Spin derived from a launch-condition ratio model.
    /// </summary>
    Ratio,

    /// <summary>
    /// Spin derived from observed ball flight.
    /// </summary>
    BallFlight,

    /// <summary>
    /// Spin measured directly by the device.
    /// </summary>
    Measured,

    /// <summary>
    /// Any other or unrecognized calculation method.
    /// </summary>
    Other
}