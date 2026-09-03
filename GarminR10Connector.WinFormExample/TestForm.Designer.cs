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
  partial class TestForm
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
      metricsCard21 = new GarminR10Connector.WinFormExample.Controls.MetricsCard2();
      SuspendLayout();
      //
      // metricsCard21
      //
      metricsCard21.BackColor = Color.FromArgb(18, 24, 36);
      metricsCard21.GradientEndColor = Color.FromArgb(140, 215, 50);
      metricsCard21.GradientStartColor = Color.FromArgb(9, 159, 235);
      metricsCard21.Location = new Point(377, 178);
      metricsCard21.Name = "metricsCard21";
      metricsCard21.Padding = new Padding(4);
      metricsCard21.Size = new Size(336, 257);
      metricsCard21.TabIndex = 0;
      metricsCard21.ValueFontPercent = 0.25F;
      //
      // TestForm
      //
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(800, 450);
      Controls.Add(metricsCard21);
      Name = "TestForm";
      Text = "TestForm";
      ResumeLayout(false);
    }

    #endregion
    private Controls.MetricSelectorCard metricSelectorCard1;
    private Controls.MetricsCard2 metricsCard21;
  }
}
