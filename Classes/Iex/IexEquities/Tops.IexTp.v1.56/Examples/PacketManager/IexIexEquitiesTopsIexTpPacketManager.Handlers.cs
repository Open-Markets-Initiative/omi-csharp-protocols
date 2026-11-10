namespace Iex.IexEquities.Tops.IexTp.v1_56.PacketManager;

public sealed partial class IexIexEquitiesTopsIexTpPacketManager
{
    partial void OnQuoteUpdateMessage(QuoteUpdateMessage message, ulong sequence)
    {
        Console.WriteLine($"QuoteUpdateMessage {sequence}: QuoteUpdateFlags {message.QuoteUpdateFlags}");
    }
}
