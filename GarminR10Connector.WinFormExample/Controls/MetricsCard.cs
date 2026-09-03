/*
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.ComponentModel;
using GarminR10Connector.Util;
using GarminR10Connector.WinFormExample.Controls.Rendering;

namespace GarminR10Connector.WinFormExample.Controls
{
  /// <summary>
  /// Displays a single golf metric in a rounded card with a gradient border.
  /// </summary>
  public partial class MetricsCard : UserControl
  {
    private GarminShotMetric _selectedMetric = GarminShotMetric.BallSpeed;
    private string _metricValue = "0.0";
    private string _resultUnitText = "rpm";

    private float _borderWidth = 4f;
    private int _cornerRadius = 14;

    private Color _gradientStartColor = ColorThemes.OPEN_SWING_BRIGHT_BLUE_COLOR;
    private Color _gradientEndColor = ColorThemes.OPEN_SWING_LIME_GREEN_COLOR;
    private float _gradientAngle = 35f;

    #region Properties

    /// <summary>
    /// Gets or sets the metric displayed by the card.
    /// </summary>
    [Category("Golf Metric")]
    [Description("The metric type displayed on this card.")]
    [Browsable(true)]
    [DefaultValue(typeof(GarminShotMetric), nameof(GarminShotMetric.BallSpeed))]
    public GarminShotMetric SelectedMetric
    {
      get => _selectedMetric;
      set
      {
        _selectedMetric = value;
        _lblMetricName.Text = value.GetTitle();
      }
    }

    /// <summary>
    /// Gets or sets the formatted metric value text.
    /// </summary>
    [Category("Golf Metric")]
    [Description("The current formatted value string.")]
    [Browsable(true)]
    [DefaultValue("0.0")]
    public string MetricValue
    {
      get => _metricValue;
      set
      {
        _metricValue = value;
        _lblValue.Text = value;
      }
    }

    /// <summary>
    /// Gets or sets the result type text (for example RPM or Degrees).
    /// </summary>
    [Category("Golf Metric")]
    [Description("Result Type text (e.g. RPM / Degrees).")]
    [Browsable(true)]
    [DefaultValue("rpm")]
    public string ResultUnitText
    {
      get => _resultUnitText;
      set
      {
        _resultUnitText = value;
        _lblResultUnit.Text = value;
      }
    }

    /// <summary>
    /// Gets or sets the border thickness in pixels.
    /// </summary>
    [Category("Appearance")]
    [Description("Border thickness of the card.")]
    [DefaultValue(4f)]
    public float BorderWidth
    {
      get => _borderWidth;
      set
      {
        _borderWidth = Math.Max(1f, value);
        UpdateContentPadding();
        UpdateRoundedRegion();
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the corner radius in pixels.
    /// </summary>
    [Category("Appearance")]
    [DisplayName("Corner Radius")]
    [Description("Corner radius of the card.")]
    [DefaultValue(14)]
    [Browsable(true)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [RefreshProperties(RefreshProperties.Repaint)]
    public int CornerRadius
    {
      get => _cornerRadius;
      set
      {
        int newValue = Math.Max(0, value);
        if (_cornerRadius == newValue)
        {
          return;
        }

        _cornerRadius = newValue;
        UpdateRoundedRegion();
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the gradient start color used for the border.
    /// </summary>
    [Category("Appearance")]
    [Description("Gradient start color used for border rendering.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientStartColor
    {
      get => _gradientStartColor;
      set
      {
        _gradientStartColor = value;
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the gradient end color used for the border.
    /// </summary>
    [Category("Appearance")]
    [Description("Gradient end color used for border rendering.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    public Color GradientEndColor
    {
      get => _gradientEndColor;
      set
      {
        _gradientEndColor = value;
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the gradient angle in degrees.
    /// </summary>
    [Category("Appearance")]
    [Description("Gradient angle in degrees.")]
    [DefaultValue(35f)]
    public float GradientAngle
    {
      get => _gradientAngle;
      set
      {
        _gradientAngle = value;
        Invalidate();
      }
    }

    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsCard"/> control.
    /// </summary>
    public MetricsCard()
    {
      InitializeComponent();

      DoubleBuffered = true;
      SetStyle(
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.UserPaint |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.ResizeRedraw,
          true);

      _lblMetricName.BackColor = Color.Transparent;
      _lblValue.BackColor = Color.Transparent;
      _lblResultUnit.BackColor = Color.Transparent;

      UpdateContentPadding();
      UpdateRoundedRegion();
    }

    /// <summary>
    /// Handles control resize and reapplies the rounded clipping region.
    /// </summary>
    /// <param name="e">
    /// Event data.
    /// </param>
    protected override void OnResize(System.EventArgs e)
    {
      base.OnResize(e);
      UpdateRoundedRegion();
    }

    /// <summary>
    /// Paints the control with a solid interior and gradient border.
    /// </summary>
    /// <param name="e">
    /// Paint event data.
    /// </param>
    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      RoundedBorderGradientRenderer.PaintRoundedGradientBorder(
          e.Graphics,
          ClientRectangle,
          _cornerRadius,
          _borderWidth,
          BackColor,
          _gradientStartColor,
          _gradientEndColor,
          _gradientAngle);
    }

    /// <summary>
    /// Updates control padding so child labels do not overlap the border.
    /// </summary>
    private void UpdateContentPadding()
    {
      int pad = (int)Math.Ceiling(_borderWidth);
      Padding = new Padding(pad);
    }

    /// <summary>
    /// Rebuilds and applies the rounded clipping region.
    /// </summary>
    private void UpdateRoundedRegion()
    {
      Region?.Dispose();
      Region = RoundedBorderGradientRenderer.CreateRoundedRegion(ClientRectangle, _cornerRadius);
    }
  }
}
