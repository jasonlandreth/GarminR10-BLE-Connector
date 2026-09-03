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
using System.Threading;

log4net.Config.XmlConfigurator.Configure(new FileInfo("log4net.config"));

Console.WriteLine("=================================================");
Console.WriteLine(" Garmin Approach R10 - Bluetooth Test Console");
Console.WriteLine("=================================================");
Console.WriteLine();
Console.WriteLine("Make sure the R10 is paired through Windows Bluetooth");
Console.WriteLine("settings before continuing (Settings > Bluetooth & devices).");
Console.WriteLine();

string deviceName = "Approach R10";
if (args.Length > 0)
{
    deviceName = args[0];
    Console.WriteLine($"Using device name from command line: \"{deviceName}\"");
}
else
{
    Console.WriteLine($"Using default device name: \"{deviceName}\"");
    Console.WriteLine("(pass a different name as a command-line argument if yours differs)");
}

Console.WriteLine();
Console.WriteLine("Connecting...");

using GarminR10Client connector = new();

connector.Info += (_, e) =>
{
    Console.ForegroundColor = ConsoleColor.DarkCyan;
    Console.WriteLine($"[Step] {e.Message}");
    Console.ResetColor();
};

connector.Connected += (_, e) =>
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine();
    Console.WriteLine($"Connected to {e.DeviceName}!");
    Console.WriteLine($"  Model:    {e.Model}");
    Console.WriteLine($"  Firmware: {e.Firmware}");
    Console.WriteLine($"  Serial:   {e.Serial}");
    Console.WriteLine($"  Battery:  {e.Battery}%");
    Console.WriteLine($"  Tilt:     {connector.LaunchMonitor?.DeviceTilt}");
    Console.ResetColor();
    Console.WriteLine();
    Console.WriteLine("Waiting for shots. Address the ball and swing when ready.");
    Console.WriteLine("Press Ctrl+C to exit.");
    Console.WriteLine();
};

connector.Disconnected += (_, e) =>
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine();
    Console.WriteLine($"Disconnected from {e.DeviceName}.{(e.WillAutoReconnect ? " Reconnecting..." : "")}");
    Console.ResetColor();
};

connector.Error += (_, e) =>
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"ERROR: {e.Message}");
    Console.ResetColor();
};

connector.BatteryUpdated += (_, e) =>
{
    Console.WriteLine($"[Battery] {e.BatteryPercent}%");
};

connector.ReadyChanged += (_, e) =>
{
    Console.ForegroundColor = e.Ready ? ConsoleColor.Green : ConsoleColor.DarkGray;
    Console.WriteLine($"[Status] Device is {(e.Ready ? "READY" : "not ready")} to record a shot.");
    Console.ResetColor();
};

int shotCount = 0;
connector.ShotReceived += (_, e) =>
{
    shotCount++;
    PrintShot(shotCount, e.Shot);
};

bool connected = await connector.ConnectAsync(deviceName);
if (!connected)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine();
    Console.WriteLine("Failed to connect. Check the device name, pairing, and Bluetooth status, then try again.");
    Console.ResetColor();
    return;
}

connector.SendShotConfig();

// Keep the app running and print a periodic heartbeat reflecting the real connection state,
// so it's clear whether the app is actively listening or trying to reconnect - rather than
// optimistically showing "waiting for shot" even if the link actually dropped. Exits cleanly
// on Ctrl+C.
using CancellationTokenSource cts = new();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true; // prevent the process from being killed immediately; let cleanup run
    cts.Cancel();
};

TimeSpan heartbeatInterval = TimeSpan.FromSeconds(15);
try
{
    while (true)
    {
        await Task.Delay(heartbeatInterval, cts.Token);

        if (connector.IsConnected)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Waiting for shot... ({shotCount} recorded so far)");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Not connected - reconnect attempts are running in the background.");
        }

        Console.ResetColor();
    }
}
catch (OperationCanceledException)
{
    // Ctrl+C pressed - fall through to cleanup below.
}

Console.WriteLine();
Console.WriteLine("Shutting down...");

/// <summary>
/// Prints a single shot's Ball, Club, and Swing data to the console, with a friendly placeholder for any section the device didn't report.
/// </summary>
/// <param name="shotNumber">The sequential shot number within this session, used only for the printed header.</param>
/// <param name="shot">The decoded shot data to print.</param>
static void PrintShot(int shotNumber, ShotData shot)
{
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine($"===== Shot #{shotNumber} (device shot id {shot.ShotId}, {shot.ShotType}) =====");
    Console.ResetColor();

    if (shot.Ball is { } ball)
    {
        Console.WriteLine("Ball Data:");
        Console.WriteLine($"  Ball Speed:              {ball.BallSpeed,8:F1} mph");
        Console.WriteLine($"  Vertical Launch Angle:   {ball.VerticalLaunchAngle,8:F1} deg");
        Console.WriteLine($"  Horizontal Launch Dir:   {ball.HorizontalLaunchDirection,8:F1} deg");
        Console.WriteLine($"  Spin Axis:               {ball.SpinAxis,8:F1} deg");
        Console.WriteLine($"  Total Spin:              {ball.TotalSpin,8:F0} rpm");
        Console.WriteLine($"  Backspin:                {ball.Backspin,8:F0} rpm");
        Console.WriteLine($"  Sidespin:                {ball.SideSpin,8:F0} rpm");
        Console.WriteLine($"  Spin Calc Type:          {ball.SpinCalcType,8}");
    }
    else
    {
        Console.WriteLine("Ball Data:   (not reported for this shot)");
    }

    Console.WriteLine();

    if (shot.Club is { } club)
    {
        Console.WriteLine("Club Data:");
        Console.WriteLine($"  Club Head Speed:         {club.ClubHeadSpeed,8:F1} mph");
        Console.WriteLine($"  Face To Target:          {club.FaceToTarget,8:F1} deg");
        Console.WriteLine($"  Path To Target:          {club.PathToTarget,8:F1} deg");
        Console.WriteLine($"  Attack Angle:            {club.AttackAngle,8:F1} deg");
        Console.WriteLine($"  Face To Path:            {club.FaceToPath,8:F1} deg");
    }
    else
    {
        Console.WriteLine("Club Data:   (not reported for this shot)");
    }

    Console.WriteLine();

    if (shot.Swing is { } swing)
    {
        uint backswingDurationMs = swing.DownswingStart - swing.BackswingStart;
        uint downswingDurationMs = swing.Impact - swing.DownswingStart;

        Console.WriteLine("Swing Data:");
        Console.WriteLine($"  Backswing Start:         {swing.BackswingStart,8} ms");
        Console.WriteLine($"  Downswing Start:         {swing.DownswingStart,8} ms");
        Console.WriteLine($"  Impact:                  {swing.Impact,8} ms");
        Console.WriteLine($"  Follow-Through End:      {swing.FollowThroughEnd,8} ms");
        Console.WriteLine($"  Backswing Duration:      {backswingDurationMs,8} ms");
        Console.WriteLine($"  Downswing Duration:      {downswingDurationMs,8} ms");
    }
    else
    {
        Console.WriteLine("Swing Data:  (not reported for this shot)");
    }

    Console.WriteLine();
}
