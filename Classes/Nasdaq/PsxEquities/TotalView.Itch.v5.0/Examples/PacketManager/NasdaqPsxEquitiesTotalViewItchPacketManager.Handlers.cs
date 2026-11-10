namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0.PacketManager;

public sealed partial class NasdaqPsxEquitiesTotalViewItchPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: StockLocate {message.StockLocate}");
    }
}
