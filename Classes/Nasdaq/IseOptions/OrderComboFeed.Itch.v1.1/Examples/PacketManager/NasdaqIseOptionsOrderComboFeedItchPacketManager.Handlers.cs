namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.PacketManager;

public sealed partial class NasdaqIseOptionsOrderComboFeedItchPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: Timestamp {message.Timestamp}");
    }
}
