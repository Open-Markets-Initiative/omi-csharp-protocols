namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.PacketManager;

public sealed partial class NasdaqIseOptionsTopComboQuoteFeedItchPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: Timestamp {message.Timestamp}");
    }
}
