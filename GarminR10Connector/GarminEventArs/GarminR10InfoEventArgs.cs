namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.Info"/>.
/// </summary>
public sealed class GarminR10InfoEventArgs : EventArgs
{
    /// <summary>
    /// A human-readable description of the step in progress.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Initializes the event args with the info message.
    /// </summary>
    /// <param name="Message">A human-readable description of the step in progress.</param>
    public GarminR10InfoEventArgs(string Message)
    {
        this.Message = Message;
    }
}