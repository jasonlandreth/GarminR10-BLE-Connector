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

namespace WinFormsExample
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblStatusCaption = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblBatteryCaption = new System.Windows.Forms.Label();
            this.lblBattery = new System.Windows.Forms.Label();
            this.lblReadyCaption = new System.Windows.Forms.Label();
            this.lblReady = new System.Windows.Forms.Label();
            this.grpBallData = new System.Windows.Forms.GroupBox();
            this.lblBallSpeed = new System.Windows.Forms.Label();
            this.lblVerticalLaunchAngle = new System.Windows.Forms.Label();
            this.lblHorizontalLaunchDirection = new System.Windows.Forms.Label();
            this.lblSpinAxis = new System.Windows.Forms.Label();
            this.lblTotalSpin = new System.Windows.Forms.Label();
            this.lblBackspin = new System.Windows.Forms.Label();
            this.lblSideSpin = new System.Windows.Forms.Label();
            this.grpClubData = new System.Windows.Forms.GroupBox();
            this.lblClubHeadSpeed = new System.Windows.Forms.Label();
            this.lblFaceToTarget = new System.Windows.Forms.Label();
            this.lblPathToTarget = new System.Windows.Forms.Label();
            this.lblAttackAngle = new System.Windows.Forms.Label();
            this.lblFaceToPath = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.lblLogCaption = new System.Windows.Forms.Label();
            this.grpBallData.SuspendLayout();
            this.grpClubData.SuspendLayout();
            this.SuspendLayout();
            //
            // btnConnect
            //
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(160, 30);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "Connect to R10";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            //
            // lblStatusCaption
            //
            this.lblStatusCaption.AutoSize = true;
            this.lblStatusCaption.Location = new System.Drawing.Point(12, 55);
            this.lblStatusCaption.Name = "lblStatusCaption";
            this.lblStatusCaption.Size = new System.Drawing.Size(45, 15);
            this.lblStatusCaption.Text = "Status:";
            //
            // lblStatus
            //
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(90, 55);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(80, 15);
            this.lblStatus.Text = "Not connected";
            //
            // lblBatteryCaption
            //
            this.lblBatteryCaption.AutoSize = true;
            this.lblBatteryCaption.Location = new System.Drawing.Point(12, 78);
            this.lblBatteryCaption.Name = "lblBatteryCaption";
            this.lblBatteryCaption.Size = new System.Drawing.Size(52, 15);
            this.lblBatteryCaption.Text = "Battery:";
            //
            // lblBattery
            //
            this.lblBattery.AutoSize = true;
            this.lblBattery.Location = new System.Drawing.Point(90, 78);
            this.lblBattery.Name = "lblBattery";
            this.lblBattery.Size = new System.Drawing.Size(24, 15);
            this.lblBattery.Text = "--";
            //
            // lblReadyCaption
            //
            this.lblReadyCaption.AutoSize = true;
            this.lblReadyCaption.Location = new System.Drawing.Point(12, 101);
            this.lblReadyCaption.Name = "lblReadyCaption";
            this.lblReadyCaption.Size = new System.Drawing.Size(48, 15);
            this.lblReadyCaption.Text = "Ready:";
            //
            // lblReady
            //
            this.lblReady.AutoSize = true;
            this.lblReady.Location = new System.Drawing.Point(90, 101);
            this.lblReady.Name = "lblReady";
            this.lblReady.Size = new System.Drawing.Size(24, 15);
            this.lblReady.Text = "--";
            //
            // grpBallData
            //
            this.grpBallData.Controls.Add(this.lblBallSpeed);
            this.grpBallData.Controls.Add(this.lblVerticalLaunchAngle);
            this.grpBallData.Controls.Add(this.lblHorizontalLaunchDirection);
            this.grpBallData.Controls.Add(this.lblSpinAxis);
            this.grpBallData.Controls.Add(this.lblTotalSpin);
            this.grpBallData.Controls.Add(this.lblBackspin);
            this.grpBallData.Controls.Add(this.lblSideSpin);
            this.grpBallData.Location = new System.Drawing.Point(12, 130);
            this.grpBallData.Name = "grpBallData";
            this.grpBallData.Size = new System.Drawing.Size(320, 190);
            this.grpBallData.TabIndex = 1;
            this.grpBallData.TabStop = false;
            this.grpBallData.Text = "Ball Data";
            //
            // lblBallSpeed
            //
            this.lblBallSpeed.AutoSize = true;
            this.lblBallSpeed.Location = new System.Drawing.Point(15, 25);
            this.lblBallSpeed.Name = "lblBallSpeed";
            this.lblBallSpeed.Size = new System.Drawing.Size(100, 15);
            this.lblBallSpeed.Text = "Ball Speed: --";
            //
            // lblVerticalLaunchAngle
            //
            this.lblVerticalLaunchAngle.AutoSize = true;
            this.lblVerticalLaunchAngle.Location = new System.Drawing.Point(15, 50);
            this.lblVerticalLaunchAngle.Name = "lblVerticalLaunchAngle";
            this.lblVerticalLaunchAngle.Size = new System.Drawing.Size(150, 15);
            this.lblVerticalLaunchAngle.Text = "Vertical Launch Angle: --";
            //
            // lblHorizontalLaunchDirection
            //
            this.lblHorizontalLaunchDirection.AutoSize = true;
            this.lblHorizontalLaunchDirection.Location = new System.Drawing.Point(15, 75);
            this.lblHorizontalLaunchDirection.Name = "lblHorizontalLaunchDirection";
            this.lblHorizontalLaunchDirection.Size = new System.Drawing.Size(170, 15);
            this.lblHorizontalLaunchDirection.Text = "Horizontal Launch Dir: --";
            //
            // lblSpinAxis
            //
            this.lblSpinAxis.AutoSize = true;
            this.lblSpinAxis.Location = new System.Drawing.Point(15, 100);
            this.lblSpinAxis.Name = "lblSpinAxis";
            this.lblSpinAxis.Size = new System.Drawing.Size(90, 15);
            this.lblSpinAxis.Text = "Spin Axis: --";
            //
            // lblTotalSpin
            //
            this.lblTotalSpin.AutoSize = true;
            this.lblTotalSpin.Location = new System.Drawing.Point(15, 125);
            this.lblTotalSpin.Name = "lblTotalSpin";
            this.lblTotalSpin.Size = new System.Drawing.Size(95, 15);
            this.lblTotalSpin.Text = "Total Spin: --";
            //
            // lblBackspin
            //
            this.lblBackspin.AutoSize = true;
            this.lblBackspin.Location = new System.Drawing.Point(15, 150);
            this.lblBackspin.Name = "lblBackspin";
            this.lblBackspin.Size = new System.Drawing.Size(90, 15);
            this.lblBackspin.Text = "Backspin: --";
            //
            // lblSideSpin
            //
            this.lblSideSpin.AutoSize = true;
            this.lblSideSpin.Location = new System.Drawing.Point(15, 165);
            this.lblSideSpin.Name = "lblSideSpin";
            this.lblSideSpin.Size = new System.Drawing.Size(90, 15);
            this.lblSideSpin.Text = "Sidespin: --";
            //
            // grpClubData
            //
            this.grpClubData.Controls.Add(this.lblClubHeadSpeed);
            this.grpClubData.Controls.Add(this.lblFaceToTarget);
            this.grpClubData.Controls.Add(this.lblPathToTarget);
            this.grpClubData.Controls.Add(this.lblAttackAngle);
            this.grpClubData.Controls.Add(this.lblFaceToPath);
            this.grpClubData.Location = new System.Drawing.Point(344, 130);
            this.grpClubData.Name = "grpClubData";
            this.grpClubData.Size = new System.Drawing.Size(320, 190);
            this.grpClubData.TabIndex = 2;
            this.grpClubData.TabStop = false;
            this.grpClubData.Text = "Club Data";
            //
            // lblClubHeadSpeed
            //
            this.lblClubHeadSpeed.AutoSize = true;
            this.lblClubHeadSpeed.Location = new System.Drawing.Point(15, 25);
            this.lblClubHeadSpeed.Name = "lblClubHeadSpeed";
            this.lblClubHeadSpeed.Size = new System.Drawing.Size(120, 15);
            this.lblClubHeadSpeed.Text = "Club Head Speed: --";
            //
            // lblFaceToTarget
            //
            this.lblFaceToTarget.AutoSize = true;
            this.lblFaceToTarget.Location = new System.Drawing.Point(15, 50);
            this.lblFaceToTarget.Name = "lblFaceToTarget";
            this.lblFaceToTarget.Size = new System.Drawing.Size(120, 15);
            this.lblFaceToTarget.Text = "Face To Target: --";
            //
            // lblPathToTarget
            //
            this.lblPathToTarget.AutoSize = true;
            this.lblPathToTarget.Location = new System.Drawing.Point(15, 75);
            this.lblPathToTarget.Name = "lblPathToTarget";
            this.lblPathToTarget.Size = new System.Drawing.Size(120, 15);
            this.lblPathToTarget.Text = "Path To Target: --";
            //
            // lblAttackAngle
            //
            this.lblAttackAngle.AutoSize = true;
            this.lblAttackAngle.Location = new System.Drawing.Point(15, 100);
            this.lblAttackAngle.Name = "lblAttackAngle";
            this.lblAttackAngle.Size = new System.Drawing.Size(110, 15);
            this.lblAttackAngle.Text = "Attack Angle: --";
            //
            // lblFaceToPath
            //
            this.lblFaceToPath.AutoSize = true;
            this.lblFaceToPath.Location = new System.Drawing.Point(15, 125);
            this.lblFaceToPath.Name = "lblFaceToPath";
            this.lblFaceToPath.Size = new System.Drawing.Size(110, 15);
            this.lblFaceToPath.Text = "Face To Path: --";
            //
            // lblLogCaption
            //
            this.lblLogCaption.AutoSize = true;
            this.lblLogCaption.Location = new System.Drawing.Point(12, 330);
            this.lblLogCaption.Name = "lblLogCaption";
            this.lblLogCaption.Size = new System.Drawing.Size(28, 15);
            this.lblLogCaption.Text = "Log:";
            //
            // txtLog
            //
            this.txtLog.BackColor = System.Drawing.Color.Black;
            this.txtLog.ForeColor = System.Drawing.Color.Lime;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.25F);
            this.txtLog.Location = new System.Drawing.Point(12, 350);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(652, 200);
            this.txtLog.TabIndex = 3;
            //
            // MainForm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 561);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblLogCaption);
            this.Controls.Add(this.grpClubData);
            this.Controls.Add(this.grpBallData);
            this.Controls.Add(this.lblReady);
            this.Controls.Add(this.lblReadyCaption);
            this.Controls.Add(this.lblBattery);
            this.Controls.Add(this.lblBatteryCaption);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.lblStatusCaption);
            this.Controls.Add(this.btnConnect);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "Garmin R10 Connector Example";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.grpBallData.ResumeLayout(false);
            this.grpBallData.PerformLayout();
            this.grpClubData.ResumeLayout(false);
            this.grpClubData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblStatusCaption;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblBatteryCaption;
        private System.Windows.Forms.Label lblBattery;
        private System.Windows.Forms.Label lblReadyCaption;
        private System.Windows.Forms.Label lblReady;
        private System.Windows.Forms.GroupBox grpBallData;
        private System.Windows.Forms.Label lblBallSpeed;
        private System.Windows.Forms.Label lblVerticalLaunchAngle;
        private System.Windows.Forms.Label lblHorizontalLaunchDirection;
        private System.Windows.Forms.Label lblSpinAxis;
        private System.Windows.Forms.Label lblTotalSpin;
        private System.Windows.Forms.Label lblBackspin;
        private System.Windows.Forms.Label lblSideSpin;
        private System.Windows.Forms.GroupBox grpClubData;
        private System.Windows.Forms.Label lblClubHeadSpeed;
        private System.Windows.Forms.Label lblFaceToTarget;
        private System.Windows.Forms.Label lblPathToTarget;
        private System.Windows.Forms.Label lblAttackAngle;
        private System.Windows.Forms.Label lblFaceToPath;
        private System.Windows.Forms.Label lblLogCaption;
        private System.Windows.Forms.TextBox txtLog;
    }
}
