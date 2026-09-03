using GarminR10Connector.Attributes;

namespace GarminR10Connector;

public enum GolfShotMetric
{
    #region Ball Data

    [GarminShot("Ball speed in miles per hour", "mph", "Ball Speed")]
    BallSpeed,

    [GarminShot("Efficiency ratio of ball speed to club speed", "mph", "Smash Factor")]
    SmashFactor,

    [GarminShot("Carry distance in yards", "yds", "Carry Distance")]
    CarryDistance,

    [GarminShot("Total distance including roll in yards", "yds", "Total Distance")]
    TotalDistance,

    [GarminShot("Distance rolled after landing in yards", "yds", "Roll Distance")]
    RollDistance,

    [GarminShot("Vertical launch angle in degrees relative to horizontal", "deg", "Vertical Launch Angle")]
    VerticalLaunchAngle,

    [GarminShot("Horizontal launch angle (HLA) in degrees (+ Right, - Left)", "deg", "Horizontal Launch Direction")]
    HorizontalLaunchAngle,

    [GarminShot("Total spin rate in revolutions per minute", "rpm", "Spin Rate")]
    SpinRate,

    [GarminShot("Backspin component (RPM). Computed: total_spin * cos(spin_axis)", "rpm", "Back Spin")]
    BackSpin,

    [GarminShot("Side spin. Computed: total_spin * sin(spin_axis) component in RPM (+ Right / Fade, - Left / Draw)",
        "rpm", "Side Spin")]
    SideSpin,

    [GarminShot("Spin axis tilt angle in degrees (-15° Draw, +15° Fade)", "deg", "Spin Axis")]
    SpinAxis,

    [GarminShot("Peak trajectory height in feet", "feet", "Apex Height")]
    ApexHeight,

    [GarminShot("Total time of flight in seconds", "duration", "Flight Time")]
    FlightTime,

    [GarminShot("Offline distance from target line in yards (+ Right, - Left)", "yds", "Lateral Offline")]
    LateralOffline,

    [GarminShot("Spin calculation method used to derive spin metrics.", "", "Spin Calculation Type")]
    SpinCalcType,

    #endregion

    #region Club Data

    [GarminShot("Club head speed at impact in miles per hour", "mph", "Club Head Speed")]
    ClubHeadSpeed,

    [GarminShot("Direction of club head motion at impact(Club Path) (+ In-to-Out, - Out-to-In)", "deg", "Club Path")]
    ClubPath,

    [GarminShot("Direction club face(FaceToTarget-Face angle at impact) is pointing at impact (+ Open, - Closed)",
        "deg", "Face To Target")]
    FaceToTarget,

    [GarminShot("The difference between the face angle(Face to Target and the path angle(Path to Target)", "deg", "Face To Path")]
    FaceToPath,

    [GarminShot("Vertical angle of attack club head motion (+ Ascending, - Descending)", "deg", "Attack Angle")]
    AttackAngle,

    #endregion

    #region SwingData

    [GarminShot("The timestamp when the backswing starts, in milliseconds since the device started", "duration", "Backswing Start")]
    BackswingStart,

    [GarminShot("The timestamp when the downswing starts, in milliseconds since the device started", "duration", "Downswing Start")]
    DownswingStart,

    [GarminShot("The timestamp when the impact occurs, in milliseconds since the device started", "duration", "Impact")]
    Impact,

    [GarminShot("The timestamp when the follow-through ends, in milliseconds since the device started", "duration", "Follow Through End")]
    FollowThroughEnd,

    [GarminShot("Effective loft on the club face at impact in degrees", "deg", "Dynamic Loft")]
    DynamicLoft,

    [GarminShot("Impact location relative to center (+ Toe, - Heel in mm)", "duration", "Impact Location X")]
    ImpactLocationX,

    [GarminShot("Impact location relative to center (+ High, - Low in mm)", "duration", "Impact Location Y")]
    ImpactLocationY

    #endregion
}
