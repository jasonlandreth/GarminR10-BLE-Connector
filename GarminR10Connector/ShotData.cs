// Copyright 2026 Jason Landreth
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using LaunchMonitor.Proto;

namespace GarminR10Connector
{
    /// <summary>
    /// Spin calculation method used to derive spin metrics.
    /// </summary>
    public enum SpinCalcType
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

    /// <summary>
    /// Whether a shot was a normal recorded shot or a practice swing.
    /// </summary>
    public enum ShotType
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

    /// <summary>
    /// Ball flight metrics for a single shot.
    /// </summary>
    public sealed class BallData
    {
        /// <summary>
        /// Vertical launch angle (degrees).
        /// </summary>
        public float VerticalLaunchAngle { get; init; }

        /// <summary>
        /// Horizontal launch direction (degrees).
        /// </summary>
        public float HorizontalLaunchDirection { get; init; }

        /// <summary>
        /// Initial ball velocity (mph).
        /// </summary>
        public float BallSpeed { get; init; }

        /// <summary>
        /// Spin axis tilt (degrees).
        /// </summary>
        public float SpinAxis { get; init; }

        /// <summary>
        /// Total spin rate (RPM).
        /// </summary>
        public float TotalSpin { get; init; }

        /// <summary>
        /// Backspin component (RPM). Computed: total_spin * cos(spin_axis).
        /// </summary>
        public float Backspin { get; init; }

        /// <summary>
        /// Sidespin component (RPM). Computed: total_spin * sin(spin_axis).
        /// </summary>
        public float SideSpin { get; init; }

        /// <summary>
        /// Spin calculation method used to derive spin metrics.
        /// </summary>
        public SpinCalcType SpinCalcType { get; init; }
    }

    /// <summary>
    /// Club delivery metrics for a single shot.
    /// </summary>
    public sealed class ClubData
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
        /// Club path angle (degrees).
        /// </summary>
        public float PathToTarget { get; init; }

        /// <summary>
        /// Angle of attack (degrees).
        /// </summary>
        public float AttackAngle { get; init; }

        /// <summary>
        /// The difference between the face angle and the path angle (degrees).
        /// </summary>
        public double FaceToPath => FaceToTarget - PathToTarget;
    }

    /// <summary>
    /// Swing timing metrics for a single shot.
    /// </summary>
    public class SwingData
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

    /// <summary>
    /// The complete decoded data for a single shot: identity, ball flight, club delivery, and
    /// swing timing metrics.
    /// </summary>
    public sealed class ShotData
    {
        /// <summary>
        /// The device-assigned identifier for this shot.
        /// </summary>
        public uint ShotId { get; init; }

        /// <summary>
        /// Whether this was a normal recorded shot or a practice swing.
        /// </summary>
        public ShotType ShotType { get; init; }

        /// <summary>
        /// Ball flight metrics, if the device reported them for this shot.
        /// </summary>
        public BallData? Ball { get; init; }

        /// <summary>
        /// Club delivery metrics, if the device reported them for this shot.
        /// </summary>
        public ClubData? Club { get; init; }

        /// <summary>
        /// Swing timing metrics, if the device reported them for this shot.
        /// </summary>
        public SwingData? Swing { get; init; }
    }

    /// <summary>
    /// Converts raw <see cref="Metrics"/> protobuf messages, as received from the Garmin R10,
    /// into the simpler <see cref="ShotData"/>/<see cref="BallData"/>/<see cref="ClubData"/>/
    /// <see cref="SwingData"/> shapes used throughout the rest of the app.
    /// </summary>
    public static class MetricsDecoder
    {
        /// <summary>
        /// Conversion factor from meters per second to miles per hour.
        /// </summary>
        public const float MpsToMph = 2.23694f;

        /// <summary>
        /// Decodes a raw <see cref="Metrics"/> message into a <see cref="ShotData"/> instance.
        /// </summary>
        /// <param name="m">The raw metrics message received from the device.</param>
        /// <returns>The decoded shot data.</returns>
        public static ShotData DecodeMetrics(Metrics m)
        {
            BallData? ball = null;
            if (m.BallMetrics is { } b)
            {
                float spinAxis = b.HasSpinAxis ? b.SpinAxis : 0f;
                float total = b.HasTotalSpin ? b.TotalSpin : 0f;
                float axisRad = spinAxis * MathF.PI / 180f;

                ball = new BallData
                {
                    VerticalLaunchAngle = b.HasLaunchAngle ? b.LaunchAngle : 0f,
                    HorizontalLaunchDirection = b.HasLaunchDirection ? b.LaunchDirection : 0f,
                    BallSpeed = b.HasBallSpeed ? b.BallSpeed * MpsToMph : 0f,
                    SpinAxis = spinAxis,
                    TotalSpin = total,
                    Backspin = total * MathF.Cos(axisRad),
                    SideSpin = total * MathF.Sin(axisRad),
                    SpinCalcType = b.HasSpinCalculationType
                        ? b.SpinCalculationType switch
                        {
                            BallMetrics.Types.SpinCalculationType.Ratio => SpinCalcType.Ratio,
                            BallMetrics.Types.SpinCalculationType.BallFlight => SpinCalcType.BallFlight,
                            BallMetrics.Types.SpinCalculationType.Measured => SpinCalcType.Measured,
                            _ => SpinCalcType.Other,
                        }
                        : SpinCalcType.Ratio,
                };
            }

            ClubData? club = null;
            if (m.ClubMetrics is { } c)
            {
                club = new ClubData
                {
                    ClubHeadSpeed = c.HasClubHeadSpeed ? c.ClubHeadSpeed * MpsToMph : 0f,
                    FaceToTarget = c.HasClubAngleFace ? c.ClubAngleFace : 0f,
                    PathToTarget = c.HasClubAnglePath ? c.ClubAnglePath : 0f,
                    AttackAngle = c.HasAttackAngle ? c.AttackAngle : 0f,
                };
            }

            SwingData? swing = null;
            if (m.SwingMetrics is { } s)
            {
                swing = new SwingData
                {
                    BackswingStart = s.HasBackSwingStartTime ? s.BackSwingStartTime : 0u,
                    DownswingStart = s.HasDownSwingStartTime ? s.DownSwingStartTime : 0u,
                    Impact = s.HasImpactTime ? s.ImpactTime : 0u,
                    FollowThroughEnd = s.HasFollowThroughEndTime ? s.FollowThroughEndTime : 0u,
                };
            }

            return new ShotData
            {
                ShotId = m.HasShotId ? m.ShotId : 0u,
                ShotType = m.HasShotType && m.ShotType == Metrics.Types.ShotType.Normal
                           ? ShotType.Normal : ShotType.Practice,
                Ball = ball,
                Club = club,
                Swing = swing,
            };
        }
    }
}
