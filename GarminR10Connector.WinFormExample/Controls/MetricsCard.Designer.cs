namespace GarminR10Connector.WinFormExample.Controls
{
    sealed partial class MetricsCard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label _lblValue;
        private System.Windows.Forms.Label _lblMetricName;
        private System.Windows.Forms.Label _lblUnitType;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            _lblValue = new Label();
            _lblMetricName = new Label();
            _lblUnitType = new Label();
            checkBox = new CheckBox();
            panelBase = new Panel();
            panelBase.SuspendLayout();
            SuspendLayout();
            // 
            // _lblValue
            // 
            _lblValue.Dock = DockStyle.Fill;
            _lblValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            _lblValue.ForeColor = Color.White;
            _lblValue.Location = new Point(0, 0);
            _lblValue.Name = "_lblValue";
            _lblValue.Size = new Size(362, 228);
            _lblValue.TabIndex = 0;
            _lblValue.Text = "0.0";
            _lblValue.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblMetricName
            // 
            _lblMetricName.Dock = DockStyle.Top;
            _lblMetricName.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            _lblMetricName.ForeColor = Color.FromArgb(140, 215, 50);
            _lblMetricName.Location = new Point(0, 0);
            _lblMetricName.Name = "_lblMetricName";
            _lblMetricName.Size = new Size(362, 116);
            _lblMetricName.TabIndex = 1;
            _lblMetricName.Text = "Club Path";
            _lblMetricName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // _lblResultUnit
            // 
            _lblUnitType.Font = new Font("Segoe UI", 7.5F);
            _lblUnitType.ForeColor = Color.FromArgb(140, 155, 170);
            _lblUnitType.Location = new Point(229, 173);
            _lblUnitType.Name = "_lblUnitType";
            _lblUnitType.Size = new Size(59, 36);
            _lblUnitType.TabIndex = 2;
            _lblUnitType.Text = "rpm";
            _lblUnitType.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // checkBox
            // 
            checkBox.AutoSize = true;
            checkBox.Location = new Point(127, 256);
            checkBox.Name = "checkBox";
            checkBox.Size = new Size(82, 19);
            checkBox.TabIndex = 3;
            checkBox.Text = "checkBox1";
            checkBox.UseVisualStyleBackColor = true;
            // 
            // panelBase
            // 
            panelBase.Controls.Add(_lblMetricName);
            panelBase.Controls.Add(_lblUnitType);
            panelBase.Controls.Add(_lblValue);
            panelBase.Dock = DockStyle.Top;
            panelBase.Location = new Point(0, 0);
            panelBase.Name = "panelBase";
            panelBase.Size = new Size(362, 228);
            panelBase.TabIndex = 4;
            // 
            // MetricsCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 24, 36);
            Controls.Add(panelBase);
            Controls.Add(checkBox);
            Name = "MetricsCard";
            Size = new Size(362, 295);
            panelBase.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private CheckBox checkBox;
        private Panel panelBase;
    }
}