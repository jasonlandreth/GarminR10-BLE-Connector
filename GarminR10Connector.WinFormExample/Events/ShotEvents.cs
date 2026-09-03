using static GarminR10Connector.Ble.LaunchMonitorDevice;

namespace GarminR10Connector.WinFormExample.Events;

public class ShotEvents
{

    /// <summary>
    /// Raised each time a shot is recorded and normalized metrics are produced.
    /// </summary>
    public event ShotDataChangedEventHandler? ShotMetricsSnapshotReceived;

    /// <summary>
    /// Handler signature for <see cref="ShotMetrics"/>.
    /// </summary>
    /// <param name="sender">The device that raised the event.</param>
    /// <param name="e">The event data carrying the raw metrics.</param>
    public delegate void MetricsEventHandler(object sender, MetricsEventArgs e);

    /// <summary>
    /// Handler signature for <see cref="ShotMetricsSnapshotReceived"/>.
    /// </summary>
    /// <param name="sender">The device that raised the event.</param>
    /// <param name="e">The event data carrying normalized metrics.</param>
    public delegate void ShotDataChangedEventHandler(object sender, ShotMetricsSnapshotEventArgs e);
}
