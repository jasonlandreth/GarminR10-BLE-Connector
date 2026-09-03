/*
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the ""License"");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an ""AS IS"" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
namespace GarminR10Connector.WinFormExample
{
  partial class LaunchMonitorSelectorForm
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelFlightScope = new Panel();
            panelGarmin = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            panel7 = new Panel();
            panel8 = new Panel();
            panel9 = new Panel();
            panel10 = new Panel();
            panel11 = new Panel();
            panel12 = new Panel();
            SuspendLayout();
            // 
            // panelFlightScope
            // 
            panelFlightScope.Location = new Point(76, 44);
            panelFlightScope.Name = "panelFlightScope";
            panelFlightScope.Size = new Size(344, 100);
            panelFlightScope.TabIndex = 0;
            // 
            // panelGarmin
            // 
            panelGarmin.Location = new Point(76, 169);
            panelGarmin.Name = "panelGarmin";
            panelGarmin.Size = new Size(344, 100);
            panelGarmin.TabIndex = 1;
            // 
            // panel3
            // 
            panel3.Location = new Point(76, 390);
            panel3.Name = "panel3";
            panel3.Size = new Size(344, 100);
            panel3.TabIndex = 1;
            // 
            // panel4
            // 
            panel4.Location = new Point(76, 275);
            panel4.Name = "panel4";
            panel4.Size = new Size(344, 100);
            panel4.TabIndex = 1;
            // 
            // panel5
            // 
            panel5.Location = new Point(446, 275);
            panel5.Name = "panel5";
            panel5.Size = new Size(344, 100);
            panel5.TabIndex = 3;
            // 
            // panel6
            // 
            panel6.Location = new Point(446, 390);
            panel6.Name = "panel6";
            panel6.Size = new Size(344, 100);
            panel6.TabIndex = 4;
            // 
            // panel7
            // 
            panel7.Location = new Point(446, 169);
            panel7.Name = "panel7";
            panel7.Size = new Size(344, 100);
            panel7.TabIndex = 5;
            // 
            // panel8
            // 
            panel8.Location = new Point(446, 44);
            panel8.Name = "panel8";
            panel8.Size = new Size(344, 100);
            panel8.TabIndex = 2;
            // 
            // panel9
            // 
            panel9.Location = new Point(816, 275);
            panel9.Name = "panel9";
            panel9.Size = new Size(344, 100);
            panel9.TabIndex = 7;
            // 
            // panel10
            // 
            panel10.Location = new Point(816, 390);
            panel10.Name = "panel10";
            panel10.Size = new Size(344, 100);
            panel10.TabIndex = 8;
            panel10.DoubleClick += panel_DoubleClick;
            // 
            // panel11
            // 
            panel11.Location = new Point(816, 169);
            panel11.Name = "panel11";
            panel11.Size = new Size(344, 100);
            panel11.TabIndex = 9;
            // 
            // panel12
            // 
            panel12.Location = new Point(816, 44);
            panel12.Name = "panel12";
            panel12.Size = new Size(344, 100);
            panel12.TabIndex = 6;
            // 
            // LaunchMonitorSelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            BackColor = SystemColors.ControlDarkDark;
            ClientSize = new Size(1394, 577);
            ControlBox = false;
            Controls.Add(panel9);
            Controls.Add(panel10);
            Controls.Add(panel11);
            Controls.Add(panel12);
            Controls.Add(panel5);
            Controls.Add(panel6);
            Controls.Add(panel7);
            Controls.Add(panel8);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panelGarmin);
            Controls.Add(panelFlightScope);
            FormBorderStyle = FormBorderStyle.None;
            Name = "LaunchMonitorSelectorForm";
            Text = "Launch Monitor Selector";
            ResumeLayout(false);
        }

        #endregion
        private Panel panelFlightScope;
    private Panel panelGarmin;
    private Panel panel3;
    private Panel panel4;
    private Panel panel5;
    private Panel panel6;
    private Panel panel7;
    private Panel panel8;
    private Panel panel9;
    private Panel panel10;
    private Panel panel11;
    private Panel panel12;
    private Panel panel1;
  }
}
