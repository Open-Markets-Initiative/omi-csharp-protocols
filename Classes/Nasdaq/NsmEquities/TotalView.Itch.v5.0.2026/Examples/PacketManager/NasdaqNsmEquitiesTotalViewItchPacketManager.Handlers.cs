namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.PacketManager;

public sealed partial class NasdaqNsmEquitiesTotalViewItchPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: StockLocate {message.StockLocate}");
    }
}
