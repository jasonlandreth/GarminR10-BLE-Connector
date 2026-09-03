using GarminR10Connector.ShotDataResults;

namespace GarminR10Connector.GarminEventArs;

/// <summary>
/// Event data for <see cref="GarminR10Client.ShotReceived"/>.
/// </summary>
public sealed class GarminR10ShotEventArgs : EventArgs
{
    /// <summary>
    /// The decoded shot data.
    /// </summary>
    public ShotData Shot { get; }

    /// <summary>
    /// Initializes the event args with the decoded shot.
    /// </summary>
    /// <param name="Shot">The decoded shot data.</param>
    public GarminR10ShotEventArgs(ShotData Shot)
    {
        this.Shot = Shot;
    }
}