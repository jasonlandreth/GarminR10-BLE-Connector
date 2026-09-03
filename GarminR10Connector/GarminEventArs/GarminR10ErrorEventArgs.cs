namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.Error"/>.
/// </summary>
public sealed class GarminR10ErrorEventArgs : EventArgs
{
    /// <summary>
    /// A human-readable description of the error.
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// Initializes the event args with the error message.
    /// </summary>
    /// <param name="Message">A human-readable description of the error.</param>
    public GarminR10ErrorEventArgs(string Message)
    {
        this.Message = Message;
    }
}