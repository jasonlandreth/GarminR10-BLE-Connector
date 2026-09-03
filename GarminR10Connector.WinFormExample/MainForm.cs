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


using GarminR10Connector.ShotDataResults;
 

namespace GarminR10Connector.WinFormExample
{
  public partial class MainForm : Form
  {
    private static string degrees = "\u00B0"; // "°"
    private GarminR10Client? _r10;


    /// <summary>

    /// Initializes the form.

    /// </summary>
    public MainForm()
    {
      InitializeComponent();
    }

    private async void btnConnect_Click(object sender, System.EventArgs e)
    {
      btnConnect.Enabled = false;
      AppendLog("Connecting...");

      _r10 = new GarminR10Client
      {
        // The connector already prints its own verbose trace to the console via
        // Console.WriteLine when troubleshooting is needed - that's independent of this
        // form's log box, which only shows the higher-level Info/Error events below.
      };

      // Every event below fires on a background thread inside the connector, so each
      // handler must marshal back to the UI thread with Invoke before touching any control.

      _r10.Info += (s, args) =>
      {
        Invoke((Action)(() =>
              {
            lblStatus.Text = args.Message;
            AppendLog($"[Step] {args.Message}");
          }));
      };

      _r10.Error += (s, args) =>
      {
        Invoke((Action)(() =>
              {
            lblStatus.Text = @$"Error: {args.Message}";
            AppendLog($"[Error] {args.Message}");
          }));
      };

      _r10.Connected += (s, args) =>
      {
        Invoke((Action)(() =>
              {
            lblStatus.Text = @"Connected";
            AppendLog("[Connected]");
            btnConnect.Text = @"Connected";
          }));
      };

      _r10.Disconnected += (s, args) =>
      {
        Invoke((Action)(() =>
              {
            lblStatus.Text = @"Disconnected";
            AppendLog("[Disconnected]");
            btnConnect.Text = @"Connect to R10";
            btnConnect.Enabled = true;
          }));
      };

      _r10.BatteryUpdated += (s, args) =>
      {
        Invoke<string>(() => lblBattery.Text = @$"{args.BatteryPercent}%");
      };

      _r10.ReadyChanged += (s, args) =>
      {
        Invoke((Action)(() =>
              {
            lblReady.Text = args.Ready ? "READY" : "not ready";
            lblReady.ForeColor = args.Ready ? Color.Green : Color.Gray;
          }));
      };

      _r10.ShotReceived += (s, args) =>
      {
        Invoke(() =>
              {
            AppendLog($"[Shot] Received shot id {args.Shot.ShotId} ({args.Shot.ShotType})");
            DisplayShot((ShotData)args.Shot);
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

    private void DisplayShot(ShotData Shot)
    {
      if (Shot.Ball is { } ball)
      {
        lblBallSpeed.Text = $"Ball Speed: {ball.BallSpeed:F1} mph";
        lblVerticalLaunchAngle.Text = $"Vertical Launch Angle: {ball.VerticalLaunchAngle:F1}{degrees}";
        lblHorizontalLaunchDirection.Text = $"Horizontal Launch Dir: {ball.HorizontalLaunchDirection:F1}{degrees}";
        lblSpinAxis.Text = $"Spin Axis: {ball.SpinAxis:F1}{degrees}";
        lblTotalSpin.Text = $"Total Spin: {ball.TotalSpin:F0} rpm";
        lblBackspin.Text = $"Backspin: {ball.BackSpin:F0} rpm";
        lblSideSpin.Text = $"Sidespin: {ball.SideSpin:F0} rpm";
      }

      if (Shot.Club is { } club)
      {
        lblClubHeadSpeed.Text = $"Club Head Speed: {club.ClubHeadSpeed:F1} mph";
        lblFaceToTarget.Text = $"Face To Target: {club.FaceToTarget:F1}{degrees}";
        lblPathToTarget.Text = $"Path To Target: {club.PathToTarget:F1}{degrees}";
        lblAttackAngle.Text = $"Attack Angle: {club.AttackAngle:F1}{degrees}";
        lblFaceToPath.Text = $"Face To Path: {club.FaceToPath:F1}{degrees}";
      }

      // This is where you'd feed shot.Ball / shot.Club into your existing
      // GolfBallFlightEngine.ProcessGarminShot-style pipeline to drive the trajectory grid.
    }

    private void AppendLog(string Line)
    {
      txtLog.AppendText($"{DateTime.Now:HH:mm:ss} {Line}{Environment.NewLine}");
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs args)
    {
      _r10?.Dispose();
    }
  }
}
