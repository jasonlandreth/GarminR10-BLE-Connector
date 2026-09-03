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
  partial class SelectorForm
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
            this.panelBlueTees = new Panel();
            panelFlightScope = new Panel();
            panelBlueTees = new Panel();
            SuspendLayout();
            // 
            // panelBlueTees
            // 
            this.panelBlueTees.BackgroundImage = Properties.Resources.blue_tee_rain_maker_logo;
            this.panelBlueTees.BackgroundImageLayout = ImageLayout.Stretch;
            this.panelBlueTees.Location = new Point(33, 101);
            this.panelBlueTees.Name = "panelBlueTees";
            this.panelBlueTees.Size = new Size(341, 100);
            this.panelBlueTees.TabIndex = 0;
            this.panelBlueTees.Tag = "Blue Tees";
            this.panelBlueTees.DoubleClick += this.Panel_DoubleClick;
            // 
            // panelFlightScope
            // 
            panelFlightScope.BackgroundImage = Properties.Resources.flight_scope_logo;
            panelFlightScope.BackgroundImageLayout = ImageLayout.Stretch;
            panelFlightScope.Location = new Point(33, 222);
            panelFlightScope.Name = "panelFlightScope";
            panelFlightScope.Size = new Size(341, 100);
            panelFlightScope.TabIndex = 1;
            panelFlightScope.Tag = "Flight Scope";
            panelFlightScope.DoubleClick += Panel_DoubleClick;
            // 
            // panel1
            // 
            panelBlueTees.BackgroundImageLayout = ImageLayout.Stretch;
            panelBlueTees.Location = new Point(389, 101);
            panelBlueTees.Name = "panelBlueTees";
            panelBlueTees.Size = new Size(341, 100);
            panelBlueTees.TabIndex = 2;
            panelBlueTees.Tag = "Blue Tees";
            // 
            // SelectorForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panelBlueTees);
            Controls.Add(panelFlightScope);
            Controls.Add(this.panelBlueTees);
            Name = "SelectorForm";
            Text = "SelectorForm";
            ResumeLayout(false);
        }

        #endregion
        private Panel panelFlightScope;
        private Panel panelBlueTees;
    }
}
