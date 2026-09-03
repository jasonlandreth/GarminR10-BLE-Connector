namespace GarminR10Connector.WinFormExample.Controls
{
    partial class MetricSelectorCard
    { 
            private System.ComponentModel.IContainer components = null;
            private System.Windows.Forms.Panel panelValue;
            private System.Windows.Forms.CheckBox chkSelected;

            protected override void Dispose(bool disposing)
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            panelValue = new Panel();
            chkSelected = new CheckBox();
            panelValue.SuspendLayout();
            SuspendLayout();
            // 
            // panelValue
            // 
            panelValue.BackColor = Color.Transparent;
            panelValue.Controls.Add(chkSelected);
            panelValue.Dock = DockStyle.Bottom;
            panelValue.Location = new Point(0, 75);
            panelValue.Name = "panelValue";
            panelValue.Size = new Size(130, 35);
            panelValue.TabIndex = 0;
            // 
            // chkSelected
            // 
            chkSelected.FlatStyle = FlatStyle.Flat;
            chkSelected.Location = new Point(55, 5);
            chkSelected.Name = "chkSelected";
            chkSelected.Size = new Size(20, 20);
            chkSelected.TabIndex = 0;
            chkSelected.UseVisualStyleBackColor = false;
            chkSelected.Click += Card_Click;
            // 
            // MetricSelectorCard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panelValue);
            Name = "MetricSelectorCard";
            Size = new Size(130, 110);
            panelValue.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
    }
    }