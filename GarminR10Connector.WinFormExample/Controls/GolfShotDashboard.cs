
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

using System.ComponentModel; 
using System.Drawing.Drawing2D; 

namespace GarminR10Connector.WinFormExample.Controls
{
    public partial class GolfShotDashboard : UserControl
    {
        private bool _isLeftHanded = false;
        private GarminShotData _currentShot = new GarminShotData();

        // Event fired whenever shot data is updated
        public event EventHandler<GarminShotData> OnShotChanged;

        public GolfShotDashboard()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);

            PopulateDefaultMetrics();
        }

        #region Properties

        [Category("Golf Simulator Settings")]
        [Description("Inverts directional graphics and metrics for left-handed golfers.")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool IsLeftHanded
        {
            get => _isLeftHanded;
            set
            {
                if (_isLeftHanded != value)
                {
                    _isLeftHanded = value;
                    ApplyHandednessInversion();
                    UpdateAllCardValues();
                    Invalidate(); // Redraw 3D Ball with flipped Spin Axis
                }
            }
        }

        [Category("Golf Simulator Data")]
        [Browsable(false)]
        public GarminShotData CurrentShot => _currentShot;

        #endregion

        #region Public Methods

        /// <summary>
        /// Pass a new GarminShotData object to update all metric cards and refresh the 3D ball rotation.
        /// </summary>
        public void UpdateShotData(GarminShotData shot)
        {
            _currentShot = shot ?? throw new ArgumentNullException(nameof(shot));

            UpdateAllCardValues();

            // Raise the OnShotChanged event
            OnShotChanged?.Invoke(this, _currentShot);

            // Redraw graphics (golf ball spin axis, trajectories)
            Invalidate();
        }

        #endregion

        #region Dashboard Setup & Value Formatting

        private void PopulateDefaultMetrics()
        {
            _pnlCards.SuspendLayout();
            _pnlCards.Controls.Clear();

            // Default set of Awesome Golf metrics
            GarminShotMetric[] defaultMetrics = new GarminShotMetric[]
            {
                GarminShotMetric.BallSpeed,
                GarminShotMetric.ClubHeadSpeed,
                GarminShotMetric.CarryDistance,
                GarminShotMetric.TotalDistance,
                GarminShotMetric.VerticalLaunchAngle,
                GarminShotMetric.HorizontalLaunchAngle,
                GarminShotMetric.SpinRate,
                GarminShotMetric.BackSpin,
                GarminShotMetric.SideSpin,
                GarminShotMetric.SpinAxis,
                GarminShotMetric.ClubPath,
                GarminShotMetric.FaceToTarget,
                GarminShotMetric.SmashFactor
            };

            foreach (var metric in defaultMetrics)
            {
                var card = new MetricSelectorCard
                {
                    SelectedMetric = metric,
                    SubtitleText = "Imperial",
                    Margin = new Padding(6)
                };

                _pnlCards.Controls.Add(card);
            }

            _pnlCards.ResumeLayout(true);
        }

        private void UpdateAllCardValues()
        {
            _pnlCards.SuspendLayout();
            foreach (Control ctrl in _pnlCards.Controls)
            {
                if (ctrl is MetricSelectorCard card)
                {
                    card.MetricValue = FormatMetricValue(card.SelectedMetric, _currentShot);
                }
            }
            _pnlCards.ResumeLayout(true);
        }

        private string FormatMetricValue(GarminShotMetric metric, GarminShotData shot)
        {
            // Invert lateral signs if in Left-Handed mode
            double sideMultiplier = _isLeftHanded ? -1.0 : 1.0;

            switch (metric)
            {
                case GarminShotMetric.BallSpeed: return $"{shot.BallSpeed:F1} mph";
                case GarminShotMetric.ClubHeadSpeed: return $"{shot.ClubSpeed:F1} mph";
                case GarminShotMetric.CarryDistance: return $"{shot.CarryDistance:F0} yds";
                case GarminShotMetric.TotalDistance: return $"{shot.TotalDistance:F0} yds";
                case GarminShotMetric.RollDistance: return $"{shot.RollDistance:F0} yds";
                case GarminShotMetric.SmashFactor: return $"{shot.SmashFactor:F2}";
                case GarminShotMetric.VerticalLaunchAngle: return $"{shot.LaunchAngle:F1}°";
                case GarminShotMetric.HorizontalLaunchAngle: return $"{(shot.HorizontalLaunchAngle * sideMultiplier):F1}°";
                case GarminShotMetric.SpinRate: return $"{shot.SpinRate:F0} rpm";
                case GarminShotMetric.BackSpin: return $"{shot.BackSpin:F0} rpm";
                case GarminShotMetric.SideSpin: return $"{(shot.SideSpin * sideMultiplier):F0} rpm";
                case GarminShotMetric.SpinAxis: return $"{(shot.SpinAxis * sideMultiplier):F1}°";
                case GarminShotMetric.ApexHeight: return $"{shot.ApexHeight:F0} ft";
                case GarminShotMetric.FlightTime: return $"{shot.FlightTime:F1} s";
                case GarminShotMetric.LateralOffline: return $"{(shot.LateralOffline * sideMultiplier):F1} yds";
                case GarminShotMetric.ClubPath: return $"{(shot.ClubPath * sideMultiplier):F1}°";
                case GarminShotMetric.FaceToTarget: return $"{(shot.FaceAngle * sideMultiplier):F1}°";
                case GarminShotMetric.FaceToPath: return $"{(shot.FaceToPath * sideMultiplier):F1}°";
                case GarminShotMetric.AttackAngle: return $"{shot.AttackAngle:F1}°";
                case GarminShotMetric.DynamicLoft: return $"{shot.DynamicLoft:F1}°";
                default: return "0.0";
            }
        }

        private void ApplyHandednessInversion()
        {
            // Re-order card layout for left-handed view symmetry
            _pnlCards.RightToLeft = _isLeftHanded ? RightToLeft.Yes : RightToLeft.No;
        }

        #endregion

        #region Custom Graphics & Rotated 3D Golf Ball

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Canvas Dimensions for top preview area
            float centerX = this.Width / 2f;
            float centerY = (this.Height - _pnlCards.Height) / 2f;

            if (centerY > 30)
            {
                DrawRotated3DGolfBall(g, new PointF(centerX, centerY), 40f);
            }
        }

        private void DrawRotated3DGolfBall(Graphics g, PointF center, float radius)
        {
            GraphicsState state = g.Save();

            g.TranslateTransform(center.X, center.Y);

            // Invert spin axis rotation angle if left-handed mode is set
            float effectiveSpinAxis = (float)(_isLeftHanded ? -_currentShot.SpinAxis : _currentShot.SpinAxis);

            // 1. Rotate Matrix to match Shot's Spin Axis
            g.RotateTransform(effectiveSpinAxis);

            // 2. Base Sphere Fill with Shading Gradient
            RectangleF ballBounds = new RectangleF(-radius, -radius, radius * 2, radius * 2);
            using (PathGradientBrush sphereBrush = GetSphereBrush(ballBounds))
            {
                g.FillEllipse(sphereBrush, ballBounds);
            }

            // 3. Render Equatorial Seam / Trajectory Ring based on Launch Angle
            using (Pen seamPen = new Pen(Color.FromArgb(180, 6, 182, 212), 2f)) // OpenSwing Electric Cyan
            {
                seamPen.DashStyle = DashStyle.Dash;

                float pitchHeight = radius * (float)Math.Sin(_currentShot.LaunchAngle * Math.PI / 180.0);
                if (Math.Abs(pitchHeight) < 3f) pitchHeight = 3f;

                g.DrawEllipse(seamPen, -radius + 4, -pitchHeight / 2f, (radius - 4) * 2, pitchHeight);
            }

            // 4. Outer Rim Highlight
            using (Pen rimPen = new Pen(Color.FromArgb(160, 255, 255, 255), 1.5f))
            {
                g.DrawEllipse(rimPen, ballBounds);
            }

            g.Restore(state);
        }

        private PathGradientBrush GetSphereBrush(RectangleF rect)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(rect);

            PathGradientBrush brush = new PathGradientBrush(path)
            {
                CenterPoint = new PointF(rect.X + rect.Width * 0.35f, rect.Y + rect.Height * 0.35f),
                CenterColor = Color.White,
                SurroundColors = new Color[] { Color.FromArgb(140, 155, 170) }
            };

            return brush;
        }

        #endregion
    }
}