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
using System.Drawing.Drawing2D;
using System.Linq;
using GarminR10Connector.Util;
using GarminR10Connector.WinFormExample.Controls.Rendering;

namespace GarminR10Connector.WinFormExample.Controls
{
  /// <summary>
  /// Displays a single golf metric in a rounded card with a gradient border.
  /// </summary>
  public partial class MetricsCard2 : UserControl
  {
    private GarminShotMetric _selectedMetric = GarminShotMetric.BallSpeed;
    private string _metricValue = "50";
    private string _valueUnitText = "mph";

    private float _borderWidth = 4f;
    private int _cornerRadius = 14;

    private Color _gradientStartColor = ColorThemes.OPEN_SWING_BRIGHT_BLUE_COLOR;
    private Color _gradientEndColor = ColorThemes.OPEN_SWING_LIME_GREEN_COLOR;
    private float _gradientAngle = 35f;

    private float _valueFontPercent = 0.25f;

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
        lblMetricName.Text = value.GetTitle();
        ValueUnitText = _selectedMetric.GetResultUnit();
      }
    }

    /// <summary>
    /// Gets or sets the formatted metric value text.
    /// </summary>
    [Category("Golf Metric")]
    [Description("The current formatted value string.")]
    [Browsable(true)]
    [DefaultValue("50")]
    public string MetricValue
    {
      get => _metricValue;
      set
      {
        _metricValue = value;
        lblValue.Text = value;
        LayoutValueAndUnit();
      }
    }

    /// <summary>
    /// Gets or sets the unit text shown at the bottom-right of the value text.
    /// </summary>
    [Category("Golf Metric")]
    [Description("Small unit text (e.g. rpm, mph, °) displayed near the value text.")]
    [Browsable(true)]
    [DefaultValue("mph")]
    public string ValueUnitText
    {
      get => _valueUnitText;
      set
      {
        _valueUnitText = value;
        lblUnit.Text = value;
        LayoutValueAndUnit();
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
        LayoutValueAndUnit();
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
        LayoutValueAndUnit();
        Invalidate();
      }
    }

    /// <summary>
    /// Gets or sets the gradient start color used for border rendering.
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
    /// Gets or sets the gradient end color used for border rendering.
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

    /// <summary>
    /// Gets or sets value font size as a percentage of control height (0.50 = 50%).
    /// </summary>
    [Category("Appearance")]
    [Description("Value font size as a percentage of control height (0.50 = 50%).")]
    [DefaultValue(0.50f)]
    [Browsable(true)]
    public float ValueFontPercent
    {
      get => _valueFontPercent;
      set
      {
        _valueFontPercent = Math.Clamp(value, 0.01f, 1.0f);
        ApplyScaledFonts();
        Invalidate();
      }
    }




    #endregion

    /// <summary>
    /// Initializes a new instance of the <see cref="MetricsCard2"/> control.
    /// </summary>
    public MetricsCard2()
    {
      InitializeComponent();

      DoubleBuffered = true;
      SetStyle(
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.UserPaint |
          ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.ResizeRedraw,
          true);

      lblMetricName.BackColor = Color.Transparent;
      lblValue.BackColor = Color.Transparent;
      lblUnit.BackColor = Color.Transparent;

      lblValue.AutoSize = true;
      lblValue.Dock = DockStyle.None;

      lblUnit.AutoSize = true;
      lblUnit.Dock = DockStyle.None;

      // Unit is fixed at 9pt and not user-configurable.
      lblUnit.Font = new Font(lblUnit.Font.FontFamily, 9f, FontStyle.Bold);

      lblMetricName.Text = _selectedMetric.GetTitle();
      lblValue.Text = _metricValue;
      lblUnit.Text = _valueUnitText;

      // keep unit anchored whenever value moves/resizes/text changes
      lblValue.LocationChanged += (_, _) => AnchorUnitToValue();
      lblValue.SizeChanged += (_, _) => AnchorUnitToValue();
      lblValue.TextChanged += (_, _) => AnchorUnitToValue();
      lblValue.FontChanged += (_, _) => AnchorUnitToValue();
      lblUnit.TextChanged += (_, _) => AnchorUnitToValue();
      lblUnit.FontChanged += (_, _) => AnchorUnitToValue();

      AnchorUnitToValue();
      UpdateContentPadding();
      UpdateRoundedRegion();
      ApplyScaledFonts();
      LayoutValueAndUnit();
    }



    /// <summary>
    /// Applies scaled font size for the value label and fixed 9pt for the unit label.
    /// </summary>
    private void ApplyScaledFonts()
    {
      float valueSize = Math.Max(8f, ClientSize.Height * _valueFontPercent);
      lblValue.Font = new Font(lblValue.Font.FontFamily, valueSize, FontStyle.Bold);
      lblUnit.Font = new Font(lblUnit.Font.FontFamily, 9f, FontStyle.Bold);

      LayoutValueAndUnit();
    }
    /// <summary>
    /// Handles control resize and reapplies region and centered layout.
    /// </summary>
    /// <param name="e">
    /// Event data.
    /// </param>
    protected override void OnResize(System.EventArgs e)
    {
      base.OnResize(e);
      UpdateRoundedRegion();
      ApplyScaledFonts();
      LayoutValueAndUnit();
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
    /// Paints the bottom panel with a horizontal blue-to-green gradient.
    /// </summary>
    /// <param name="sender">
    /// Event sender.
    /// </param>
    /// <param name="e">
    /// Paint event data.
    /// </param>
    private void PanelBottom_Paint(object? sender, PaintEventArgs e)
    {
      // if (sender is not Panel panel)
      // {
      //     return;
      // }
      //
      // Rectangle rect = panel.ClientRectangle;
      // if (rect.Width <= 0 || rect.Height <= 0)
      // {
      //     return;
      // }
      //
      // using LinearGradientBrush brush = new(
      //     rect,
      //     ColorThemes.OPEN_SWING_BRIGHT_BLUE_COLOR,
      //     ColorThemes.OPEN_SWING_LIME_GREEN_COLOR,
      //     0f);
      //
      // e.Graphics.FillRectangle(brush, rect);
    }
    /// <summary>
    /// Updates control padding so child controls do not overlap the border.
    /// </summary>
    private void UpdateContentPadding()
    {
      int pad = (int)Math.Ceiling(_borderWidth);
      Padding = new Padding(pad);
    }

    /// <summary>
    /// Rebuilds and applies the rounded clipping region for the control.
    /// </summary>
    private void UpdateRoundedRegion()
    {
      Region?.Dispose();
      Region = RoundedBorderGradientRenderer.CreateRoundedRegion(ClientRectangle, _cornerRadius);
    }

    /// <summary>
    /// Centers the value/unit pair and keeps unit text at the value's bottom-right.
    /// </summary>
    private void LayoutValueAndUnit()
    {
      const int gap = 4;

      Rectangle area = _pnlValueHost.ClientRectangle;

      // Keep value above the bottom strip.
      if (panelBottom.Visible)
      {
        area.Height -= panelBottom.Height;
      }

      if (area.Width <= 0 || area.Height <= 0)
      {
        return;
      }

      lblValue.AutoSize = true;
      lblUnit.AutoSize = true;

      Size valueSize = lblValue.PreferredSize;
      Size unitSize = lblUnit.PreferredSize;

      // Center VALUE only.
      int valueX = area.Left + (area.Width - valueSize.Width) / 2;
      int valueY = area.Top + (area.Height - valueSize.Height) / 2;

      lblValue.Location = new Point(valueX, valueY);

      // Attach unit to value's bottom-right.
      int unitX = lblValue.Right + gap;
      int unitY = lblValue.Bottom - unitSize.Height;

      // Prevent clipping on the right.
      if (unitX + unitSize.Width > area.Right)
      {
        unitX = area.Right - unitSize.Width;
      }

      lblUnit.Location = new Point(unitX, unitY);
    }

    /// <summary>
    /// Attaches unit label to the bottom-right of the value label.
    /// </summary>
    private void AnchorUnitToValue()
    {
      const int gap = -10;

      lblUnit.AutoSize = true;
      lblUnit.Dock = DockStyle.None;

      int x = lblValue.Right + gap;
      int y = lblValue.Bottom - lblUnit.Height - 15;

      lblUnit.Location = new Point(x, y);
    }

    /// <summary>
    /// Handles designer-wired value text changed event.
    /// </summary>
    private void lblValue_TextChanged(object sender, System.EventArgs e) => LayoutValueAndUnit();

    /// <summary>
    /// Handles designer-wired value font changed event.
    /// </summary>
    private void lblValue_FontChanged(object sender, System.EventArgs e) => LayoutValueAndUnit();

    /// <summary>
    /// Handles designer-wired unit text changed event.
    /// </summary>
    private void lblUnit_TextChanged(object sender, System.EventArgs e) => LayoutValueAndUnit();

    /// <summary>
    /// Handles designer-wired unit font changed event.
    /// </summary>
    private void lblUnit_FontChanged(object sender, System.EventArgs e) => LayoutValueAndUnit();
  }
}
