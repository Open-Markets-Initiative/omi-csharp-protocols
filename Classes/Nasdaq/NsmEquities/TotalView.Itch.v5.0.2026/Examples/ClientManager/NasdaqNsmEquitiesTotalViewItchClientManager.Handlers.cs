namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientManager;

public sealed partial class NasdaqNsmEquitiesTotalViewItchClientManager
{
    partial void OnDebugPacket(DebugPacket message, int index)
    {
        Console.WriteLine($"DebugPacket {index}: DebugText {message.DebugText}");
    }
}
