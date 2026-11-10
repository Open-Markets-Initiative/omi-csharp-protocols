namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientManager;

public sealed partial class NasdaqNsmEquitiesOrdersOuchClientManager
{
    partial void OnEnterOrderMessage(EnterOrderMessage message, int index)
    {
        Console.WriteLine($"EnterOrderMessage {index}: UserRefNum {message.UserRefNum}");
    }
}
