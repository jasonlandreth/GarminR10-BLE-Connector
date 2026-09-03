namespace GarminR10Connector.WinFormExample.Events;

public class ShotChangedEventArgs(AwesomeGolfMetric Metric, string Value) : EventArgs
{
    public AwesomeGolfMetric Metric { get; } = Metric;
    public string Value { get; } = Value;
}