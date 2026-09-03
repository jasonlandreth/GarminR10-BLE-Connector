/*
 * SPDX-License-Identifier: Apache-2.0
 *
 * Copyright (c) 2026 Jason Landreth
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *
 * -----------------------------------------------------------------------------
 * Portions of this file are derived from gsp-r10-adapter and remain under MIT:
 *
 * MIT License
 *
 * Copyright (c) 2022 mholow
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *
 * Derived from:
 * https://github.com/mholow/gsp-r10-adapter
 */


using Google.Protobuf;
using InTheHand.Bluetooth;
using LaunchMonitor.Proto;
using static LaunchMonitor.Proto.State.Types;
using static LaunchMonitor.Proto.SubscribeResponse.Types;
using static LaunchMonitor.Proto.WakeUpResponse.Types;

namespace GarminR10Connector.Ble
{
  /// <summary>
  /// Bluetooth LE device transport for the Garmin Approach R10 (and compatible launch monitors),
  /// adding the measurement/control/status GATT characteristics and the launch-monitor-specific
  /// protobuf request/response methods (wake, status, tilt, alert subscription, shot config, tilt
  /// calibration) on top of the shared <see cref="BleProtocolDevice"/> transport.
  /// </summary>
  public class LaunchMonitorDevice : BleProtocolDevice
  {
    /// <summary>
    /// The log4net logger for this class.
    /// </summary>
    private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(LaunchMonitorDevice));

    /// <summary>
    /// Shot IDs already raised via <see cref="ShotMetrics"/>, used to filter out duplicate notifications.
    /// </summary>
    private HashSet<uint> _processedShotIDs = new HashSet<uint>();

    /// <summary>
    /// Backing field for <see cref="CurrentState"/>.
    /// </summary>
    private StateType _currentState;

    /// <summary>
    /// The device's most recently reported operating state. Setting this also updates <see cref="Ready"/>.
    /// </summary>
    public StateType CurrentState
    {
      get => _currentState;
      private set
      {
        _currentState = value;
        Ready = value == StateType.Waiting;
      }
    }

    /// <summary>
    /// The device's most recently reported tilt reading, populated during <see cref="Setup"/> and after each tilt calibration.
    /// </summary>
    public Tilt? DeviceTilt { get; private set; }

    /// <summary>
    /// Backing field for <see cref="Ready"/>.
    /// </summary>
    private bool _ready = false;

    /// <summary>
    /// Whether the device is currently ready to record a shot. Raises <see cref="ReadinessChanged"/> when it changes.
    /// </summary>
    public bool Ready
    {
      get => _ready;
      private set
      {
        bool changed = _ready != value;
        _ready = value;
        if (changed)
        {
          ReadinessChanged?.Invoke(this, new ReadinessChangedEventArgs() { Ready = value });
        }
      }
    }

    /// <summary>
    /// Whether to automatically send a wake-up request when the device reports itself asleep. Defaults to true.
    /// </summary>
    public bool AutoWake { get; set; } = true;

    /// <summary>
    /// Whether to run tilt calibration automatically as part of <see cref="Setup"/>. Defaults to true.
    /// </summary>
    public bool CalibrateTiltOnConnect { get; set; } = true;

    /// <summary>
    /// Raised whenever <see cref="Ready"/> changes.
    /// </summary>
    public event ReadinessChangedEventHandler? ReadinessChanged;

    /// <summary>
    /// Handler signature for <see cref="ReadinessChanged"/>.
    /// </summary>
    /// <param name="Sender">The device that raised the event.</param>
    /// <param name="E">The event data, carrying the new readiness state.</param>
    public delegate void ReadinessChangedEventHandler(object Sender, ReadinessChangedEventArgs E);

    /// <summary>
    /// Event data for <see cref="ReadinessChanged"/>.
    /// </summary>
    public class ReadinessChangedEventArgs : EventArgs
    {
      /// <summary>
      /// Whether the device is now ready to record a shot.
      /// </summary>
      public bool Ready { get; set; }
    }

    /// <summary>
    /// Raised when the device reports an error condition.
    /// </summary>
    public event ErrorEventHandler? Error;

    /// <summary>
    /// Handler signature for <see cref="Error"/>.
    /// </summary>
    /// <param name="sender">The device that raised the event.</param>
    /// <param name="E">The event data, carrying the error message and severity.</param>
    public delegate void ErrorEventHandler(object sender, ErrorEventArgs E);

    /// <summary>
    /// Event data for <see cref="Error"/>.
    /// </summary>
    public class ErrorEventArgs : EventArgs
    {
      /// <summary>
      /// A human-readable description of the error.
      /// </summary>
      public string? Message { get; set; }

      /// <summary>
      /// The severity of the error, as reported by the device or assigned locally.
      /// </summary>
      public Error.Types.Severity Severity { get; set; }
    }

    /// <summary>
    /// Raised each time a shot is recorded and its metrics are received.
    /// </summary>
    public event MetricsEventHandler? ShotMetrics;

    /// <summary>
    /// Handler signature for <see cref="ShotMetrics"/>.
    /// </summary>
    /// <param name="sender">The device that raised the event.</param>
    /// <param name="E">The event data, carrying the raw metrics.</param>
    public delegate void MetricsEventHandler(object sender, MetricsEventArgs E);

    /// <summary>
    /// Event data for <see cref="ShotMetrics"/>.
    /// </summary>
    public class MetricsEventArgs : EventArgs
    {
      /// <summary>
      /// The raw metrics reported for the shot.
      /// </summary>
      public Metrics? Metrics { get; set; }
    }

    /// <summary>
    /// Initializes the launch monitor transport for a given Bluetooth device.
    /// </summary>
    /// <param name="Device">The Bluetooth LE device to communicate with.</param>
    public LaunchMonitorDevice(BluetoothDevice Device) : base(Device)
    {
    }

    /// <summary>
    /// Subscribes to the measurement/control/status GATT characteristics, then performs the base
    /// device setup (device info reads and handshake), then wakes the device, requests its
    /// current status and tilt, subscribes to alerts, and optionally runs tilt calibration. Never
    /// throws - any failure is logged, reported via <see cref="Error"/>, and this returns false.
    /// </summary>
    /// <returns>True if every step completed successfully; false otherwise.</returns>
    public override bool Setup()
    {
      try
      {
        Log.Debug("Subscribing to measurement service");
        GattService? measService = Device.Gatt.GetPrimaryServiceAsync(BleGattGuids.MEASUREMENT_SERVICE_UUID).WaitAsync(TimeSpan.FromSeconds(15)).Result;
        if (measService == null)
        {
          RaiseError("Measurement service was not found on the device.", global::LaunchMonitor.Proto.Error.Types.Severity.Fatal);
          return false;
        }

        GattCharacteristic? measCharacteristic = measService.GetCharacteristicAsync(BleGattGuids.MEASUREMENT_CHARACTERISTIC_UUID).WaitAsync(TimeSpan.FromSeconds(15)).Result;
        if (measCharacteristic == null)
        {
          RaiseError("Measurement characteristic was not found.", global::LaunchMonitor.Proto.Error.Types.Severity.Fatal);
          return false;
        }
        if (!measCharacteristic.StartNotificationsAsync().Wait(TimeSpan.FromSeconds(15)))
        {
          Log.Error("Error subscribing to measurement characteristic");
        }

        // Bytes that come after each shot. No idea how to parse these
        measCharacteristic.CharacteristicValueChanged += (o, E) => { };

        Log.Debug("Subscribing to control service");
        GattCharacteristic? controlPoint = measService.GetCharacteristicAsync(BleGattGuids.CONTROL_POINT_CHARACTERISTIC_UUID).WaitAsync(TimeSpan.FromSeconds(15)).Result;
        if (controlPoint == null)
        {
          RaiseError("Control point characteristic was not found.", global::LaunchMonitor.Proto.Error.Types.Severity.Fatal);
          return false;
        }
        if (!controlPoint.StartNotificationsAsync().Wait(TimeSpan.FromSeconds(15)))
        {
          Log.Error("Error subscribing to the control characteristic");
        }
        // Response to waiting device through controlPointInterface. Unused for now
        controlPoint.CharacteristicValueChanged += (o, E) => { };

        Log.Debug("Subscribing to status service");
        GattCharacteristic? statusCharacteristic = measService.GetCharacteristicAsync(BleGattGuids.STATUS_CHARACTERISTIC_UUID).WaitAsync(TimeSpan.FromSeconds(15)).Result;
        if (statusCharacteristic == null)
        {
          RaiseError("Status characteristic was not found.", global::LaunchMonitor.Proto.Error.Types.Severity.Fatal);
          return false;
        }
        if (!statusCharacteristic.StartNotificationsAsync().Wait(TimeSpan.FromSeconds(15)))
        {
          Log.Error("Error subscribing to the status characteristic");
        }
        statusCharacteristic.CharacteristicValueChanged += (o, E) =>
        {
          // Read e.Value into a local exactly once - see the note on this same pattern in
          // BleProtocolDevice.Setup(), on the device interface notifier's CharacteristicValueChanged
          // handler.
          byte[]? value = E.Value;
          if (value == null || value.Length < 3)
          {
            return;
          }

          bool isAwake = value[1] == (byte)0;
          bool isReady = value[2] == (byte)0;

          // the following is unused in favor of the status change notifications and wake control provided by the protobuf service
          // if (!isAwake)
          // {
          //   controlPoint.WriteValueWithResponseAsync(new byte[] { 0x00 }).Wait();
          // }
        };

        bool baseSetupSuccess = base.Setup();
        if (!baseSetupSuccess)
        {
          Log.Error("Error during base device setup");
          return false;
        }

        WakeDevice();
        CurrentState = StatusRequest() ?? StateType.Error;
        DeviceTilt = GetDeviceTilt();

        List<AlertStatusMessage> alertStatus = SubscribeToAlerts();
        if (alertStatus.Count == 0)
        {
          Log.Warn("Alert subscription did not return a response (device may not have acknowledged the subscribe request). Continuing anyway.");
        }

        if (CalibrateTiltOnConnect)
        {
          StartTiltCalibration();
        }

        return true;
      }
      catch (Exception ex)
      {
        Exception effective = ex;
        while (effective is AggregateException agg && agg.InnerException != null)
        {
          effective = agg.InnerException;
        }

        RaiseError($"Setup() threw {effective.GetType().Name}: {effective.Message}", global::LaunchMonitor.Proto.Error.Types.Severity.Fatal, effective);
        return false;
      }
    }

    /// <summary>
    /// Handles a device-initiated protobuf request/notification: state changes (including
    /// auto-wake on standby), error reports, shot metrics (deduplicated by shot ID), and tilt
    /// calibration completion (re-requesting the current tilt).
    /// </summary>
    /// <param name="Request">The parsed protobuf message received from the device.</param>
    public override void HandleProtobufRequest(IMessage Request)
    {
      if (Request is not WrapperProto wrapperProtoRequest)
      {
        return;
      }

      AlertDetails? notification = wrapperProtoRequest.Event?.Notification?.AlertNotification_;
      if (notification == null)
      {
        return;
      }

      if (notification.State != null)
      {
        CurrentState = notification.State.State_;
        if (notification.State.State_ == StateType.Standby)
        {
          if (AutoWake)
          {
            Log.Info("Device asleep. Sending wakeup call");
            WakeDevice();
          }
          else
          {
            Log.Warn("Device asleep. Wake device using button (or enable autowake in settings)");
          }
        }
      }

      if (notification.Error != null && notification.Error.HasCode)
      {
        Error?.Invoke(this, new ErrorEventArgs() { Message = $"{notification.Error.Code} {notification.Error.DeviceTilt}", Severity = notification.Error.Severity });
      }

      if (notification.Metrics != null)
      {
        if (!_processedShotIDs.Add(notification.Metrics.ShotId))
        {
          Log.Warn($"Received duplicate shot data {notification.Metrics.ShotId}.  Ignoring");
        }
        else
        {
          ShotMetrics?.Invoke(this, new MetricsEventArgs() { Metrics = notification.Metrics });
        }
      }

      if (notification.TiltCalibration != null)
      {
        DeviceTilt = GetDeviceTilt();
      }
    }

    /// <summary>
    /// Logs an error at a severity-appropriate level and raises <see cref="Error"/> with it.
    /// </summary>
    /// <param name="Message">A human-readable description of the error.</param>
    /// <param name="Severity">The severity to report.</param>
    /// <param name="Exception">The exception that caused the error, if any.</param>
    private void RaiseError(string Message, global::LaunchMonitor.Proto.Error.Types.Severity Severity, Exception? Exception = null)
    {
      if (Exception != null)
      {
        Log.Error(Message, Exception);
      }
      else if (Severity == global::LaunchMonitor.Proto.Error.Types.Severity.Fatal)
      {
        Log.Error(Message);
      }
      else
      {
        Log.Warn(Message);
      }

      Error?.Invoke(this, new ErrorEventArgs() { Message = Message, Severity = Severity });
    }

    /// <summary>
    /// Requests the device's current tilt reading.
    /// </summary>
    /// <returns>The tilt reading, or null if the request failed or timed out.</returns>
    public Tilt? GetDeviceTilt()
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto() { Service = new LaunchMonitorService() { TiltRequest = new TiltRequest() } }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        Tilt? tilt = wrapperProtoResponse.Service?.TiltResponse?.Tilt;
        if (tilt == null)
        {
          RaiseError("GetDeviceTilt: response did not contain a tilt reading.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        }
        return tilt;
      }

      RaiseError("GetDeviceTilt: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return null;
    }

    /// <summary>
    /// Sends a wake-up request to the device.
    /// </summary>
    /// <returns>The response status, or null if the request failed or timed out.</returns>
    public ResponseStatus? WakeDevice()
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto() { Service = new LaunchMonitorService() { WakeUpRequest = new WakeUpRequest() } }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        ResponseStatus? status = wrapperProtoResponse.Service?.WakeUpResponse?.Status;
        if (status == null)
        {
          RaiseError("WakeDevice: response did not contain a status.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        }
        return status;
      }

      RaiseError("WakeDevice: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return null;
    }

    /// <summary>
    /// Requests the device's current operating state.
    /// </summary>
    /// <returns>The device's current state, or null if the request failed or timed out.</returns>
    public StateType? StatusRequest()
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto() { Service = new LaunchMonitorService() { StatusRequest = new StatusRequest() } }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        if (wrapperProtoResponse.Service?.StatusResponse?.State != null)
        {
          return wrapperProtoResponse.Service.StatusResponse.State.State_;
        }

        RaiseError("StatusRequest: response did not contain a state.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        return null;
      }

      RaiseError("StatusRequest: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return null;
    }

    /// <summary>
    /// Subscribes to launch monitor alert notifications from the device.
    /// </summary>
    /// <returns>The device's current alert status list, or an empty list if the request failed, timed out, or the response was empty.</returns>
    public List<AlertStatusMessage> SubscribeToAlerts()
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto()
        {
          Event = new EventSharing()
          {
            SubscribeRequest = new SubscribeRequest()
            {
              Alerts = { new List<AlertMessage>() { new AlertMessage() { Type = AlertNotification.Types.AlertType.LaunchMonitor } } }
            }
          }
        }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        IEnumerable<AlertStatusMessage>? alertStatus = wrapperProtoResponse.Event?.SubscribeRespose?.AlertStatus;
        if (alertStatus != null)
        {
          return alertStatus.ToList();
        }

        RaiseError("SubscribeToAlerts: response did not contain alert status.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        return new List<AlertStatusMessage>();
      }

      RaiseError("SubscribeToAlerts: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return new List<AlertStatusMessage>();
    }

    /// <summary>
    /// Sends the device's environmental shot configuration, used by its own internal calculations.
    /// </summary>
    /// <param name="Temperature">Ambient temperature, in Fahrenheit.</param>
    /// <param name="Humidity">Relative humidity, 0-1.</param>
    /// <param name="Altitude">Altitude, in meters.</param>
    /// <param name="AirDensity">Relative air density, 1.0 = sea level standard.</param>
    /// <param name="TeeRange">Distance from the device to the tee/ball, in meters.</param>
    /// <returns>True if the device acknowledged the configuration with a success flag; false otherwise (including on timeout).</returns>
    public bool ShotConfig(float Temperature, float Humidity, float Altitude, float AirDensity, float TeeRange)
    {
      IMessage? resp = SendProtobufRequest(new WrapperProto()
      {
        Service = new LaunchMonitorService()
        {
          ShotConfigRequest = new ShotConfigRequest()
          {
            Temperature = Temperature,
            Humidity = Humidity,
            Altitude = Altitude,
            AirDensity = AirDensity,
            TeeRange = TeeRange
          }
        }
      });

      if (resp is WrapperProto wrapperProtoResponse)
      {
        bool? success = wrapperProtoResponse.Service?.ShotConfigResponse?.Success;
        if (success == null)
        {
          RaiseError("ShotConfig: response did not contain a success flag.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
          return false;
        }
        return success.Value;
      }

      RaiseError("ShotConfig: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return false;
    }

    /// <summary>
    /// Requests that the device reset its tilt calibration.
    /// </summary>
    /// <param name="ShouldReset">Whether to actually perform the reset. Defaults to true.</param>
    /// <returns>The reset status, or null if the request failed or timed out.</returns>
    public ResetTiltCalibrationResponse.Types.Status? ResetTiltCalibration(bool ShouldReset = true)
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto() { Service = new LaunchMonitorService() { ResetTiltCalRequest = new ResetTiltCalibrationRequest() { ShouldReset = ShouldReset } } }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        ResetTiltCalibrationResponse.Types.Status? status = wrapperProtoResponse.Service?.ResetTiltCalResponse?.Status;
        if (status == null)
        {
          RaiseError("ResetTiltCalibration: response did not contain a status.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        }
        return status;
      }

      RaiseError("ResetTiltCalibration: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return null;
    }

    /// <summary>
    /// Requests that the device start tilt calibration.
    /// </summary>
    /// <param name="ShouldReset">Unused by the current request shape; retained for API compatibility.</param>
    /// <returns>The calibration status, or null if the request failed or timed out.</returns>
    public StartTiltCalibrationResponse.Types.CalibrationStatus? StartTiltCalibration(bool ShouldReset = true)
    {
      IMessage? resp = SendProtobufRequest(
        new WrapperProto() { Service = new LaunchMonitorService() { StartTiltCalRequest = new StartTiltCalibrationRequest() } }
      );

      if (resp is WrapperProto wrapperProtoResponse)
      {
        StartTiltCalibrationResponse.Types.CalibrationStatus? status = wrapperProtoResponse.Service?.StartTiltCalResponse?.Status;
        if (status == null)
        {
          RaiseError("StartTiltCalibration: response did not contain a status.", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
        }
        return status;
      }

      RaiseError("StartTiltCalibration: no response received (request may have timed out).", global::LaunchMonitor.Proto.Error.Types.Severity.Warning);
      return null;
    }

    /// <summary>
    /// Unsubscribes all event handlers before disposing the base transport.
    /// </summary>
    /// <param name="Disposing">True if called from <see cref="IDisposable.Dispose"/>; false if called from a finalizer.</param>
    protected override void Dispose(bool Disposing)
    {
      foreach (var d in ReadinessChanged?.GetInvocationList() ?? Array.Empty<Delegate>())
      {
        ReadinessChanged -= (d as ReadinessChangedEventHandler);
      }

      foreach (var d in Error?.GetInvocationList() ?? Array.Empty<Delegate>())
      {
        Error -= (d as ErrorEventHandler);
      }

      foreach (var d in ShotMetrics?.GetInvocationList() ?? Array.Empty<Delegate>())
      {
        ShotMetrics -= (d as MetricsEventHandler);
      }

      base.Dispose(Disposing);
    }
  }
}
