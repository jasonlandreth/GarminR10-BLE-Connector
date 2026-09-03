# GarminR10-BLE-Connector

A .NET library and sample applications that connect to a **Garmin Approach R10** launch monitor
over Bluetooth Low Energy (BLE) and deliver the ball, club, and swing data for every shot you hit.

No Garmin Golf app, no simulator subscription, and no cloud service required — the R10 talks
directly to your Windows machine and your code receives a strongly typed `ShotData` object each
time the ball is struck.

> This is an independent, community project. It is not affiliated with, endorsed by, or supported
> by Garmin Ltd.

---

## Features

- Connects to an already-paired **Approach R10** over BLE using `InTheHand.BluetoothLE`
- Implements the R10 protocol (COBS framing, CRC16, Protobuf messages), derived from
  [gsp-r10-adapter](https://github.com/mholow/gsp-r10-adapter)
- Performs the full device handshake, GATT subscription, and protocol setup automatically
- Retries the connection (BLE first-attempt failures are common on Windows) and auto-reconnects
- Decodes the R10's Protobuf/COBS message stream into plain C# objects
- Event-driven API — subscribe to `ShotReceived` and start collecting shots
- Reports device model, firmware, serial number, tilt, battery level, and ready state
- `log4net` logging throughout for troubleshooting
- Sample **console** and **WinForms dashboard** applications included

## Shot data captured

| Group     | Metrics                                                                                                                          |
| --------- | -------------------------------------------------------------------------------------------------------------------------------- |
| **Ball**  | Ball speed, vertical launch angle, horizontal launch direction, total spin, backspin, sidespin, spin axis, spin calculation type |
| **Club**  | Club head speed, face to target, path to target, attack angle                                                                    |
| **Swing** | Backswing start, downswing start, impact, follow-through end                                                                     |
| **Shot**  | Shot ID, shot type (normal or practice swing)                                                                                    |

## How it works

The R10 exposes a proprietary protocol over standard BLE GATT characteristics. The library
implements the whole transport stack so you don't have to:

- **Framing** — messages are length-prefixed, CRC16-checked, COBS-encoded, then split into 19-byte
  chunks to fit the BLE write size.
- **Handshake** — on connect the device assigns a per-connection header byte that must prefix every
  subsequent write.
- **Messaging** — Protobuf messages generated from `Proto/LaunchMonitor.proto` are exchanged in a
  correlated request/response cycle; the device also pushes unsolicited messages (shots, state
  changes) that are dispatched to event handlers.
- **Threading** — three background pumps (writer, reader/frame reassembler, message processor) run
  over queues so BLE notification callbacks never block.
- **Failure handling** — `Setup()` never throws. Missing GATT services, timeouts, and unexpected
  exceptions are logged and surfaced through a `SetupError` event, with a `false` return.

`GarminR10Client` is the facade over all of it: connect, subscribe, receive shots.

## Requirements

- Windows 10 version 1903 (build 10.0.19041) or newer
- [.NET 9 SDK](https://dotnet.microsoft.com/download) (projects target `net9.0-windows10.0.19041.0`, x64)
- A Bluetooth LE capable adapter
- A **Garmin Approach R10**, powered on and **already paired** through
  _Settings → Bluetooth & devices_ in Windows

## Repository layout

| Project                             | Description                                                                |
| ----------------------------------- | -------------------------------------------------------------------------- |
| `GarminR10Connector`                | The core library — BLE transport, protocol decoding, and `GarminR10Client` |
| `GarminR10Connector.TestConsole`    | Minimal console app that connects and prints each shot                     |
| `GarminR10Connector.WinFormExample` | WinForms shot dashboard with launch monitor selection                      |
| `WinFormsExample`                   | Simpler WinForms sample                                                    |

## Getting started

```powershell
git clone https://github.com/jasonlandreth/GarminR10-BLE-Connector.git
cd GarminR10-BLE-Connector
dotnet build GarminR10-BLE-Connector.sln -c Debug
```

Pair the R10 in Windows Bluetooth settings, then run the console sample:

```powershell
dotnet run --project GarminR10Connector.TestConsole
```

If your device is named something other than the factory default, pass the name as an argument:

```powershell
dotnet run --project GarminR10Connector.TestConsole -- "Approach R10"
```

## Usage

```csharp
using GarminR10Connector;

using GarminR10Client client = new();

client.Connected += (_, e) =>
    Console.WriteLine($"Connected to {e.DeviceName} (firmware {e.Firmware}, battery {e.Battery}%)");

client.ShotReceived += (_, e) =>
{
    ShotData shot = e.Shot;
    Console.WriteLine($"Shot #{shot.ShotId}");
    Console.WriteLine($"  Ball speed:  {shot.Ball?.BallSpeed:F1} mph");
    Console.WriteLine($"  Launch:      {shot.Ball?.VerticalLaunchAngle:F1}°");
    Console.WriteLine($"  Total spin:  {shot.Ball?.TotalSpin:F0} rpm");
    Console.WriteLine($"  Club speed:  {shot.Club?.ClubHeadSpeed:F1} mph");
};

client.Error += (_, e) => Console.WriteLine($"ERROR: {e.Message}");

if (!await client.ConnectAsync())
{
    Console.WriteLine("Could not connect to the launch monitor.");
    return;
}

// Keep the process alive while shots stream in.
await Task.Delay(Timeout.Infinite);
```

### `GarminR10Client` events

| Event            | Raised when                                                    |
| ---------------- | -------------------------------------------------------------- |
| `Connected`      | The handshake completed and device info is available           |
| `Info`           | A progress step occurred during connection or setup            |
| `ShotReceived`   | A shot was recorded and decoded                                |
| `BatteryUpdated` | The device reported a battery level                            |
| `ReadyChanged`   | The device's ready-to-record state changed                     |
| `Disconnected`   | The BLE link dropped (includes whether it will auto-reconnect) |
| `Error`          | A recoverable or fatal error occurred                          |

`ConnectAsync(string deviceName = "Approach R10", int maxAttempts = 5, int retryDelaySeconds = 3)`
searches the operating system's paired devices, connects, and completes the protocol setup.

## Troubleshooting

- **Connection fails on the first try.** This is normal for BLE on Windows; the client retries
  automatically. Increase `maxAttempts` if your adapter is slow.
- **Device not found.** The R10 must be paired in Windows first — the library does not perform
  pairing. Confirm the exact device name shown in Bluetooth settings.
- **The Garmin Golf app is connected.** Only one host can hold the BLE connection at a time.
  Close the mobile app or turn off Bluetooth on your phone.
- **No shots arrive.** Confirm the device is level, in range, and reporting `ReadyChanged` as
  ready. Check `log4net.config` output for protocol errors.

## Contributing

Contributions are welcome — see [CONTRIBUTING.md](.github/CONTRIBUTING.md) and the
[Code of Conduct](.github/CODE_OF_CONDUCT.md). Please report security issues privately as described in
[SECURITY.md](.github/SECURITY.md).

## Acknowledgements

This project builds directly on [**gsp-r10-adapter**](https://github.com/mholow/gsp-r10-adapter)
by [mholow](https://github.com/mholow), which reverse-engineered the Approach R10's BLE protocol —
the COBS framing, CRC handling, handshake sequence, and Protobuf message definitions. Portions of
the BLE and protocol code here are derived from that project and remain under its MIT License.
Thank you for publishing that work.

Thanks also to the wider open-source golf simulator community for documenting the R10's behavior.

## License

Copyright (c) 2026 Jason Landreth

Licensed under the [Apache License, Version 2.0](LICENSE) (`SPDX-License-Identifier: Apache-2.0`).
See [NOTICE](NOTICE) for attribution.

Some files contain code derived from MIT-licensed third-party projects; those portions remain under
the MIT License. Full details and dependency licenses are in
[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
