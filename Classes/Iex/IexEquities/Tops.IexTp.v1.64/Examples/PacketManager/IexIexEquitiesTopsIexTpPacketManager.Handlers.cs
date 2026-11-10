namespace Iex.IexEquities.Tops.IexTp.v1_64.PacketManager;

public sealed partial class IexIexEquitiesTopsIexTpPacketManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, ulong sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: SystemEvent {message.SystemEvent}");
    }
}
