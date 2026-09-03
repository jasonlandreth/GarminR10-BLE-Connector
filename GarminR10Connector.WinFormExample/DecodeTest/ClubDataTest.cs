namespace GarminR10Connector.WinFormExample.DecodeTest;

/// <summary>
/// Club delivery metrics for a single shot.
/// </summary>
public sealed class ClubDataTest
{
    /// <summary>
    /// Club head velocity at impact (mph).
    /// </summary>
    public float ClubHeadSpeed { get; init; }

    /// <summary>
    /// Face angle at impact (degrees).
    /// </summary>
    public float FaceToTarget { get; init; }
    /// <summary>
    /// Direction club face is pointing at impact (+ Open, - Closed)
    /// </summary>
    public double FaceAngle { get; set; }
    
    /// <summary>
    /// Club path angle (degrees).
    /// </summary>
    public float PathToTarget { get; init; }
    /// <summary>
    /// Direction of club head motion at impact (+ In-to-Out, - Out-to-In)
    /// </summary>
    public double ClubPath => PathToTarget;
    
    /// <summary>
    /// Angle of attack (degrees).
    /// </summary>
    public float AttackAngle { get; init; }

    /// <summary>
    /// The difference between the face angle and the path angle (degrees).
    /// </summary>
    public double FaceToPath => FaceToTarget - PathToTarget;
    /// <summary>
    /// Effective loft on the club face at impact in degrees
    /// </summary>
    public double DynamicLoft { get; set; }

}
