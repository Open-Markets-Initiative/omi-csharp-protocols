namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerManager;

public sealed partial class NasdaqNsmEquitiesTotalViewItchServerManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, global::System.UInt128? sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: StockLocate {message.StockLocate}");
    }
}
