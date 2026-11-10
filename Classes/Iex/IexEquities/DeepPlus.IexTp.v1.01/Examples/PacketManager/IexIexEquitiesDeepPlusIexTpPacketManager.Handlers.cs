namespace Iex.IexEquities.DeepPlus.IexTp.v1_01.PacketManager;

public sealed partial class IexIexEquitiesDeepPlusIexTpPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: SystemEvent {message.SystemEvent}");
    }
}
