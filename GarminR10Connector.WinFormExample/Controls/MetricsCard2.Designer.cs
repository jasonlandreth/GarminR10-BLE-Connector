namespace GarminR10Connector.WinFormExample.Controls
{
    partial class MetricsCard2
    {
        private System.ComponentModel.IContainer components = null;

        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Label lblUnit;
        private System.Windows.Forms.Label lblMetricName;
        private System.Windows.Forms.Panel _pnlValueHost;

        private void InitializeComponent()
        {
            _pnlValueHost = new Panel();
            panelBottom = new Panel();
            lblUnit = new Label();
            lblValue = new Label();
            lblMetricName = new Label();
            _pnlValueHost.SuspendLayout();
            SuspendLayout();
            // 
            // _pnlValueHost
            // 
            _pnlValueHost.BackColor = Color.Transparent;
            _pnlValueHost.Controls.Add(panelBottom);
            _pnlValueHost.Controls.Add(lblUnit);
            _pnlValueHost.Controls.Add(lblValue);
            _pnlValueHost.Dock = DockStyle.Fill;
            _pnlValueHost.Location = new Point(0, 87);
            _pnlValueHost.Name = "_pnlValueHost";
            _pnlValueHost.Size = new Size(336, 170);
            _pnlValueHost.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Location = new Point(0, 150);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(336, 20);
            panelBottom.TabIndex = 2;
            // 
            // lblUnit
            // 
            lblUnit.AutoSize = true;
            lblUnit.BackColor = Color.Transparent;
            lblUnit.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblUnit.ForeColor = Color.LightGray;
            lblUnit.Location = new Point(200, 51);
            lblUnit.Name = "lblUnit";
            lblUnit.Size = new Size(63, 19);
            lblUnit.TabIndex = 1;
            lblUnit.Text = "degrees";
            lblUnit.FontChanged += lblUnit_FontChanged;
            lblUnit.TextChanged += lblUnit_TextChanged;
            // 
            // lblValue
            // 
            lblValue.AutoSize = true;
            lblValue.BackColor = Color.Transparent;
            lblValue.Font = new Font("Segoe UI", 45F, FontStyle.Bold);
            lblValue.ForeColor = Color.White;
            lblValue.Location = new Point(104, 0);
            lblValue.Name = "lblValue";
            lblValue.Size = new Size(121, 81);
            lblValue.TabIndex = 0;
            lblValue.Text = "0.0";
            lblValue.TextAlign = ContentAlignment.MiddleCenter;
            lblValue.FontChanged += lblValue_FontChanged;
            lblValue.TextChanged += lblValue_TextChanged;
            // 
            // lblMetricName
            // 
            lblMetricName.Dock = DockStyle.Top;
            lblMetricName.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblMetricName.ForeColor = Color.FromArgb(140, 215, 50);
            lblMetricName.Location = new Point(0, 0);
            lblMetricName.Name = "lblMetricName";
            lblMetricName.Size = new Size(336, 87);
            lblMetricName.TabIndex = 2;
            lblMetricName.Text = "Club Path";
            lblMetricName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // MetricsCard2
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(18, 24, 36);
            Controls.Add(_pnlValueHost);
            Controls.Add(lblMetricName);
            Name = "MetricsCard2";
            Size = new Size(336, 257);
            _pnlValueHost.ResumeLayout(false);
            _pnlValueHost.PerformLayout();
            ResumeLayout(false);
        }

        private Panel panelBottom;
    }
}