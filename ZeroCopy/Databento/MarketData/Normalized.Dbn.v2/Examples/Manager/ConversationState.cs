namespace Databento.MarketData.Normalized.Dbn.v2.Manager;

/// <summary>
///  Keeps the values of the conversation that a later packet reads.
/// </summary>
public sealed class ConversationState
{
    /// <summary>
    ///  Gets the last Ts Out that the conversation gave, or null before it gives one.
    /// </summary>
    public byte? TsOut { get; private set; }

    /// <summary>
    ///  Keeps the values of the conversation that the File gives.
    /// </summary>
    public void Apply(global::Databento.MarketData.Normalized.Dbn.v2.File.Reader file)
    {
        this.TsOut = (byte)file.Metadata.TsOut.Decode();
    }

    /// <summary>
    ///  Keeps the values of the conversation that the Gateway Stream gives.
    /// </summary>
    public void Apply(global::Databento.MarketData.Normalized.Dbn.v2.GatewayStream.Reader gatewayStream)
    {
        this.TsOut = (byte)gatewayStream.Metadata.TsOut.Decode();
    }

    /// <summary>
    ///  Clears the values that the conversation gave.
    /// </summary>
    public void Reset()
    {
        this.TsOut = null;
    }
}
