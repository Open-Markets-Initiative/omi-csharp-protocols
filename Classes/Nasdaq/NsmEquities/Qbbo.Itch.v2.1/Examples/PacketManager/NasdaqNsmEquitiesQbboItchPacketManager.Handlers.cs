namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1.PacketManager;

public sealed partial class NasdaqNsmEquitiesQbboItchPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: TrackingNumber {message.TrackingNumber}");
    }
}
