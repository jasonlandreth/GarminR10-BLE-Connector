namespace GarminR10Connector.ShotDataResults;

/// <summary>
/// Club delivery metrics for a single shot.
/// </summary>
internal sealed class ClubData
{
    /// <summary>
    /// Club head velocity at impact (mph).
    /// </summary>
    internal float ClubHeadSpeed { get; init; }

    /// <summary>
    /// Face angle at impact (degrees).
    /// </summary>
    internal float FaceToTarget { get; init; }
    /// <summary>
    /// Direction club face is pointing at impact (+ Open, - Closed)
    /// </summary>
    public double FaceAngle { get; set; }
    
    /// <summary>
    /// Club path angle (degrees).
    /// </summary>
    internal float PathToTarget { get; init; }
    /// <summary>
    /// Direction of club head motion at impact (+ In-to-Out, - Out-to-In)
    /// </summary>
    public double ClubPath => PathToTarget;
    
    /// <summary>
    /// Angle of attack (degrees).
    /// </summary>
    internal float AttackAngle { get; init; }

    /// <summary>
    /// The difference between the face angle and the path angle (degrees).
    /// </summary>
    public double FaceToPath => FaceToTarget - PathToTarget;
    /// <summary>
    /// Effective loft on the club face at impact in degrees
    /// </summary>
    public double DynamicLoft { get; set; }

}
