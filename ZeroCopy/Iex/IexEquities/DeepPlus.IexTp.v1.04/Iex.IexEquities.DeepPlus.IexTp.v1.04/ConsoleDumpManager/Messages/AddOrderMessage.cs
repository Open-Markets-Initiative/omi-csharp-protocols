namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnAddOrderMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;AddOrderMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnAddOrderMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<AddOrderMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] AddOrderMessage: short payload ({payloadLength} < {Unsafe.SizeOf<AddOrderMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(AddOrderMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] AddOrderMessage");
        Console.WriteLine($"  Side = {msg.Side}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  OrderId = {msg.OrderId}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
    }
}
