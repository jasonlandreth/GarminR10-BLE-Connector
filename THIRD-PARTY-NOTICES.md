# Third-Party Notices

This project incorporates material from the projects listed below.

---

## gsp-r10-adapter

Portions of this project are derived from **gsp-r10-adapter** by mholow, which reverse-engineered
the Garmin Approach R10 BLE protocol. That work is licensed under the MIT License and those
portions remain under the MIT License.

Source: https://github.com/mholow/gsp-r10-adapter

Files in this repository containing derived code (each carries the MIT notice in its header):

- `GarminR10Connector/GarminR10Client.cs`
- `GarminR10Connector/Ble/BleBaseDevice.cs`
- `GarminR10Connector/Ble/BleProtocolDevice.cs`
- `GarminR10Connector/Ble/LaunchMonitorDevice.cs`
- `GarminR10Connector/Proto/LaunchMonitor.proto`
- `GarminR10Connector/Util/Bytes.cs`
- `GarminR10Connector/Util/CobsCodec.cs`
- `GarminR10Connector/Util/Crc.cs`

```
MIT License

Copyright (c) 2022 mholow

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

---

## NuGet package dependencies

These are referenced as unmodified binary packages and are not redistributed in source form:

| Package                                                        | License      |
| -------------------------------------------------------------- | ------------ |
| [InTheHand.BluetoothLE](https://github.com/inthehand/32feet)   | MIT          |
| [Google.Protobuf](https://github.com/protocolbuffers/protobuf) | BSD-3-Clause |
| [Grpc.Tools](https://github.com/grpc/grpc)                     | Apache-2.0   |
| [log4net](https://github.com/apache/logging-log4net)           | Apache-2.0   |
