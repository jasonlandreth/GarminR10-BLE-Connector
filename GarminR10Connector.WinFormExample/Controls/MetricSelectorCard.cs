
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
using GarminR10Connector.Util;

namespace GarminR10Connector.WinFormExample.Controls
{

    public partial class MetricSelectorCard : UserControl
    {
        private static string FONT_NAME = "Segoe UI";
        private GarminShotMetric _selectedMetric = GarminShotMetric.BackSpin;
        private string _titleText = "BACK SPIN";
        private string _subtitleText = "rpm";
        private string _metricValue = "2,450 rpm";
        private string _resultUnitText = "rpm";
        private bool _isChecked = false;
        private bool _showCheckBox = true;

        // OpenSwing Theme Color Palette
        private Color _cardBgTop = Color.FromArgb(18, 26, 36);
        private Color _cardBgBottom = Color.FromArgb(8, 12, 18);
        private Color _checkedColor = Color.FromArgb(132, 204, 22);        // OpenSwing Lime Green
        private Color _uncheckedBorderColor = Color.FromArgb(6, 182, 212); // OpenSwing Electric Cyan
        private Color _uncheckedBgColor = Color.FromArgb(15, 23, 32);

        public event EventHandler CheckedChanged;

        public MetricSelectorCard()
        {
            InitializeComponent();

            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.Size = new Size(130, 110);
            this.Cursor = Cursors.Hand;

            chkSelected.Click += Card_Click;
            this.Click += Card_Click;

            UpdateCheckboxAppearance();
        }

        #region Designer & Field Properties

        [Category("Awesome Golf Data")]
        [Description("Select the launch monitor metric from the Awesome Golf suite.")]
        [Browsable(true)]
        [DefaultValue(AwesomeGolfMetric.BackSpin)]
        public GarminShotMetric SelectedMetric
        {
            get => _selectedMetric;
            set
            {
                _selectedMetric = value;
                _titleText = value.GetTitle();
                Invalidate();
            }
        }

        [Category("Metric Selector Data")]
        [Description("The metric name displayed at the top.")]
        [Browsable(true)]
        [DefaultValue("Imperial")]
        public string TitleText
        {
            get => _titleText;
            set { _titleText = value; Invalidate(); }
        }

        [Category("Metric Selector Data")]
        [Description("The unit type displayed below the title.")]
        [Browsable(true)]
        [DefaultValue("Imperial")]
        public string SubtitleText
        {
            get => _subtitleText;
            set { _subtitleText = value; Invalidate(); }
        }

        [Category("Metric Selector Data")]
        [Description("The numerical shot value to display inside the tile.")]
        [Browsable(true)]
        [DefaultValue("2,450 rpm")]
        public string MetricValue
        {
            get => _metricValue;
            set { _metricValue = value; Invalidate(); }
        }

        [Category("Metric Selector Data")]
        [Description("Unit label displayed in lower-right of panelValue when CheckBox is hidden (e.g. mph, rpm, deg).")]
        [Browsable(true)]
        [DefaultValue("rpm")]
        public string ResultUnitText

        {
            get => _resultUnitText;
            set
            {
                _resultUnitText = value;
                panelValue.Invalidate();
            }
        }

        [Category("Metric Selector Data")]
        [Description("Show or hide the selection checkbox inside panelValue.")]
        [Browsable(true)]
        [DefaultValue(true)]
        public bool ShowCheckBox
        {
            get => _showCheckBox;
            set
            {
                _showCheckBox = value;
                chkSelected.Visible = value;
                panelValue.Invalidate();
            }
        }


        [Category("Metric Selector Data")]
        [Description("Whether the card's checkbox is checked.")]
        [Browsable(true)]
        [DefaultValue(false)]
        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    chkSelected.Checked = value;
                    UpdateCheckboxAppearance();
                    CheckedChanged?.Invoke(this, System.EventArgs.Empty);
                    Invalidate();
                }
            }
        }

        #endregion

   
  

        private void Card_Click(object sender, System.EventArgs e)
        {
            if (_showCheckBox)
            {
                IsChecked = !IsChecked;
            }
        }


        private void UpdateCheckboxAppearance()
        {
            if (_isChecked)
            {
                chkSelected.BackColor = _checkedColor;
                chkSelected.FlatAppearance.BorderColor = _checkedColor;
                chkSelected.FlatAppearance.BorderSize = 1;
            }
            else
            {
                chkSelected.BackColor = _uncheckedBgColor;
                chkSelected.FlatAppearance.BorderColor = _uncheckedBorderColor;
                chkSelected.FlatAppearance.BorderSize = 2;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            int width = this.Width;
            int height = this.Height;
            int cornerRadius = 10;

            if (width < 20 || height < 20) return;

            // Master Alpha: 255 (Full brightness) when checked, 100 (~40% opacity grayed out) when unchecked
            int masterAlpha = _isChecked ? 255 : 100;
            Rectangle rect = new Rectangle(0, 0, width - 1, height - 1);

            // 1. Metallic Card Background
            Color bgTopWithAlpha = Color.FromArgb((int)(_cardBgTop.A * (masterAlpha / 255f)), _cardBgTop);
            Color bgBottomWithAlpha = Color.FromArgb((int)(_cardBgBottom.A * (masterAlpha / 255f)), _cardBgBottom);

            using (GraphicsPath path = GetRoundedRectanglePath(rect, cornerRadius))
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(rect, bgTopWithAlpha, bgBottomWithAlpha, LinearGradientMode.Vertical))
            {
                g.FillPath(bgBrush, path);

                // Top Cyan Glow Border
                int glowAlpha = _isChecked ? 60 : 25;
                using (GraphicsPath topGlowPath = new GraphicsPath())
                {
                    topGlowPath.AddArc(1, 1, cornerRadius * 2, cornerRadius * 2, 180, 90);
                    topGlowPath.AddLine(cornerRadius, 1, width - cornerRadius, 1);
                    topGlowPath.AddArc(width - 1 - cornerRadius * 2, 1, cornerRadius * 2, cornerRadius * 2, 270, 90);

                    using (Pen glowPen = new Pen(Color.FromArgb(glowAlpha, _uncheckedBorderColor), 1.5f))
                    {
                        g.DrawPath(glowPen, topGlowPath);
                    }
                }

                // Card Edge Border
                int borderAlpha = _isChecked ? 50 : 20;
                using (Pen borderPen = new Pen(Color.FromArgb(borderAlpha, 255, 255, 255), 1f))
                {
                    g.DrawPath(borderPen, path);
                }
            }

            // 2. Drag Handle
            int handleAlpha = _isChecked ? 160 : 60;
            using (Pen handlePen = new Pen(Color.FromArgb(handleAlpha, 148, 163, 184), 2f))
            {
                float handleX2 = width - 12f;
                float handleX1 = handleX2 - 16f;
                g.DrawLine(handlePen, handleX1, 12f, handleX2, 12f);
                g.DrawLine(handlePen, handleX1, 17f, handleX2, 17f);
            }

            // 3. Metric Title Text ("BACK SPIN", etc.)
            int titleAlpha = _isChecked ? 255 : 110;
            using (Font titleFont = new Font(FONT_NAME, 8.5f, FontStyle.Bold))
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(titleAlpha, 255, 255, 255)))
            {
                SizeF titleSize = g.MeasureString(_titleText, titleFont);
                g.DrawString(_titleText, titleFont, titleBrush, (width - titleSize.Width) / 2f, 22f);
            }

            // 4. Metric Numerical Value ("2,450 rpm")
            using (Font valFont = new Font(FONT_NAME, 10f, FontStyle.Bold))
            using (Brush valBrush = new SolidBrush(Color.FromArgb(titleAlpha, 6, 182, 212))) // OpenSwing Electric Cyan
            {
                SizeF valSize = g.MeasureString(_metricValue, valFont);
                g.DrawString(_metricValue, valFont, valBrush, (width - valSize.Width) / 2f, 40f);
            }

            // 5. Subtitle Text ("Imperial")
            int subAlpha = _isChecked ? 160 : 70;
            using (Font subFont = new Font(FONT_NAME, 7.5f, FontStyle.Regular))
            using (Brush subBrush = new SolidBrush(Color.FromArgb(subAlpha, 148, 163, 184)))
            {
                SizeF subSize = g.MeasureString(_subtitleText, subFont);
                g.DrawString(_subtitleText, subFont, subBrush, (width - subSize.Width) / 2f, 60f);
            }
        }

        private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;

            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}