namespace Iex.IexEquities.Deep.IexTp.v1_06.PacketManager;

public sealed partial class IexIexEquitiesDeepIexTpPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: SystemEvent {message.SystemEvent}");
    }
}
