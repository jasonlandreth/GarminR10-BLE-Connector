namespace GarminR10Connector.WinFormExample.EventArgs;

public class ShotChangedEventArgs(AwesomeGolfMetric Metric, string Value) : System.EventArgs
{
    public AwesomeGolfMetric Metric { get; } = Metric;
    public string Value { get; } = Value;
}