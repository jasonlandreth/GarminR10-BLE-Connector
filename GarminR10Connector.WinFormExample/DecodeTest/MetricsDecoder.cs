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

using GarminR10Connector.WinFormExample.DecodeTest;
using LaunchMonitor.Proto;

namespace GarminR10Connector.WinFormExample.DecodeTest;

/// <summary>
/// Default <see cref="IShotMetricsDecoder"/> implementation. Converts raw <see cref="Metrics"/>
/// protobuf messages, as received from the Garmin R10, into the simpler <see cref="ShotDataTest"/>/
/// <see cref="BallDataTest"/>/<see cref="ClubDataTest"/>/<see cref="SwingDataTest  "/> shapes used throughout
/// the rest of the app.
/// </summary>
public sealed class MetricsDecoder : IShotMetricsDecoder
{
    /// <summary>
    /// Conversion factor from meters per second to miles per hour.
    /// </summary>
    public const float MpsToMph = 2.23694f;

    /// <summary>
    /// Decodes a raw <see cref="Metrics"/> message into a <see cref="ShotData"/> instance.
    /// </summary>
    /// <param name="Metrics">The raw metrics message received from the device.</param>
    /// <returns>The decoded shot data.</returns>
    public ShotDataTest Decode(Metrics Metrics)
    {
        BallDataTest? ball = null;
        if (Metrics.BallMetrics is { } b)
        {
            float spinAxis = b.HasSpinAxis ? b.SpinAxis : 0f;
            float total = b.HasTotalSpin ? b.TotalSpin : 0f;
            float axisRad = spinAxis * MathF.PI / 180f;

            ball = new BallDataTest
            {
                VerticalLaunchAngle = b.HasLaunchAngle ? b.LaunchAngle : 0f,
                HorizontalLaunchDirection = b.HasLaunchDirection ? b.LaunchDirection : 0f,
                BallSpeed = b.HasBallSpeed ? b.BallSpeed * MpsToMph : 0f,
                SpinAxis = spinAxis,
                TotalSpin = total,
                BackSpin = total * MathF.Cos(axisRad),
                SideSpin = total * MathF.Sin(axisRad),
                SpinCalcType = b.HasSpinCalculationType
                  ? b.SpinCalculationType switch
                  {
                      BallMetrics.Types.SpinCalculationType.Ratio => SpinCalcTypeTest.Ratio,
                      BallMetrics.Types.SpinCalculationType.BallFlight => SpinCalcTypeTest.BallFlight,
                      BallMetrics.Types.SpinCalculationType.Measured => SpinCalcTypeTest.Measured,
                      _ => SpinCalcTypeTest.Other,
                  }
                  : SpinCalcTypeTest.Ratio,
            };
        }

        ClubDataTest? club = null;
        if (Metrics.ClubMetrics is { } c)
        {
            club = new ClubDataTest
            {
                ClubHeadSpeed = c.HasClubHeadSpeed ? c.ClubHeadSpeed * MpsToMph : 0f,
                FaceToTarget = c.HasClubAngleFace ? c.ClubAngleFace : 0f,
                PathToTarget = c.HasClubAnglePath ? c.ClubAnglePath : 0f,
                AttackAngle = c.HasAttackAngle ? c.AttackAngle : 0f,
            };
        }

        SwingDataTest? swing = null;
        if (Metrics.SwingMetrics is { } s)
        {
            swing = new SwingDataTest
            {
                BackswingStart = s.HasBackSwingStartTime ? s.BackSwingStartTime : 0u,
                DownswingStart = s.HasDownSwingStartTime ? s.DownSwingStartTime : 0u,
                Impact = s.HasImpactTime ? s.ImpactTime : 0u,
                FollowThroughEnd = s.HasFollowThroughEndTime ? s.FollowThroughEndTime : 0u,
            };
        }

        return new ShotDataTest
        {
            ShotId = Metrics.HasShotId ? Metrics.ShotId : 0u,
            ShotType = Metrics.HasShotType && Metrics.ShotType == Metrics.Types.ShotType.Normal
              ? ShotTypeTest.Normal : ShotTypeTest.Practice,
            Ball = ball,
            Club = club,
            Swing = swing,
        };
    }
}
