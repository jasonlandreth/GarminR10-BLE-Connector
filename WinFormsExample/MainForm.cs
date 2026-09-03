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

using GarminR10Connector;

namespace WinFormsExample
{
    /// <summary>
    /// Minimal example form demonstrating how to consume <see cref="GarminR10Client"/> from a
    /// WinForms application: connecting, subscribing to its events, and marshaling each event
    /// back onto the UI thread with <see cref="Control.Invoke(Delegate)"/>, since every event on
    /// the connector fires from a background thread.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// The connector instance for the currently connected (or connecting) device, if any.
        /// </summary>
        private GarminR10Client? _r10;

        /// <summary>Initializes the form.</summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Creates the connector, subscribes to its events, and starts connecting to the R10.
        /// </summary>
        /// <param name="sender">The Connect button.</param>
        /// <param name="e">Unused event arguments.</param>
        private async void btnConnect_Click(object sender, EventArgs e)
        {
            btnConnect.Enabled = false;
            AppendLog("Connecting...");

            // Logging is handled by log4net (configured in Program.cs via log4net.config) - it
            // writes to both the console and a rolling log file. That's independent of this
            // form's log box, which only shows the higher-level Info/Error events below.
            _r10 = new GarminR10Client();

            // Every event below fires on a background thread inside the connector, so each
            // handler must marshal back to the UI thread with Invoke before touching any control.

            _r10.Info += (s, args) =>
            {
                Invoke(() =>
                {
                    lblStatus.Text = args.Message;
                    AppendLog($"[Step] {args.Message}");
                });
            };

            _r10.Error += (s, args) =>
            {
                Invoke(() =>
                {
                    lblStatus.Text = $"Error: {args.Message}";
                    AppendLog($"[Error] {args.Message}");
                });
            };

            _r10.Connected += (s, args) =>
            {
                Invoke(() =>
                {
                    lblStatus.Text = "Connected";
                    AppendLog("[Connected]");
                    btnConnect.Text = "Connected";
                });
            };

            _r10.Disconnected += (s, args) =>
            {
                Invoke(() =>
                {
                    lblStatus.Text = "Disconnected";
                    AppendLog("[Disconnected]");
                    btnConnect.Text = "Connect to R10";
                    btnConnect.Enabled = true;
                });
            };

            _r10.BatteryUpdated += (s, args) =>
            {
                Invoke(() => lblBattery.Text = $"{args.BatteryPercent}%");
            };

            _r10.ReadyChanged += (s, args) =>
            {
                Invoke(() =>
                {
                    lblReady.Text = args.Ready ? "READY" : "not ready";
                    lblReady.ForeColor = args.Ready ? Color.Green : Color.Gray;
                });
            };

            _r10.ShotReceived += (s, args) =>
            {
                Invoke(() =>
                {
                    AppendLog($"[Shot] Received shot id {args.Shot.ShotId} ({args.Shot.ShotType})");
                    DisplayShot(args.Shot);
                });
            };

            //bool connected = await _r10.ConnectAsync("Approach R10");
            bool connected = await Task.Run(() => _r10.ConnectAsync("Approach R10"));
            if (connected)
            {
                _r10.SendShotConfig();
                AppendLog("Shot config sent. Waiting for shots.");
            }
            else
            {
                btnConnect.Enabled = true;
            }
        }

        /// <summary>
        /// Updates the Ball Data and Club Data label groups with a shot's values, leaving either group unchanged if the device didn't report it.
        /// </summary>
        /// <param name="shot">The decoded shot data to display.</param>
        private void DisplayShot(ShotData shot)
        {
            if (shot.Ball is { } ball)
            {
                lblBallSpeed.Text = $"Ball Speed: {ball.BallSpeed:F1} mph";
                lblVerticalLaunchAngle.Text = $"Vertical Launch Angle: {ball.VerticalLaunchAngle:F1}\u00B0";
                lblHorizontalLaunchDirection.Text = $"Horizontal Launch Dir: {ball.HorizontalLaunchDirection:F1}\u00B0";
                lblSpinAxis.Text = $"Spin Axis: {ball.SpinAxis:F1}\u00B0";
                lblTotalSpin.Text = $"Total Spin: {ball.TotalSpin:F0} rpm";
                lblBackspin.Text = $"Backspin: {ball.Backspin:F0} rpm";
                lblSideSpin.Text = $"Sidespin: {ball.SideSpin:F0} rpm";
            }

            if (shot.Club is { } club)
            {
                lblClubHeadSpeed.Text = $"Club Head Speed: {club.ClubHeadSpeed:F1} mph";
                lblFaceToTarget.Text = $"Face To Target: {club.FaceToTarget:F1}\u00B0";
                lblPathToTarget.Text = $"Path To Target: {club.PathToTarget:F1}\u00B0";
                lblAttackAngle.Text = $"Attack Angle: {club.AttackAngle:F1}\u00B0";
                lblFaceToPath.Text = $"Face To Path: {club.FaceToPath:F1}\u00B0";
            }

            // This is where you'd feed shot.Ball / shot.Club into your existing
            // GolfBallFlightEngine.ProcessGarminShot-style pipeline to drive the trajectory grid.
        }

        /// <summary>
        /// Appends a timestamped line to the log text box.
        /// </summary>
        /// <param name="line">The line of text to append.</param>
        private void AppendLog(string line)
        {
            txtLog.AppendText($"{DateTime.Now:HH:mm:ss} {line}{Environment.NewLine}");
        }

        /// <summary>
        /// Disposes the connector when the form closes.
        /// </summary>
        /// <param name="sender">The form.</param>
        /// <param name="e">Unused event arguments.</param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _r10?.Dispose();
        }
    }
}
