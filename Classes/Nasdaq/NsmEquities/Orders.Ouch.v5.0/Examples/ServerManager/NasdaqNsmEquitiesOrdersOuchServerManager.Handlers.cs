namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerManager;

public sealed partial class NasdaqNsmEquitiesOrdersOuchServerManager
{
    partial void OnSystemEventMessage(SystemEventMessage message, global::System.UInt128? sequence)
    {
        Console.WriteLine($"SystemEventMessage {sequence}: Timestamp {message.Timestamp}");
    }
}
