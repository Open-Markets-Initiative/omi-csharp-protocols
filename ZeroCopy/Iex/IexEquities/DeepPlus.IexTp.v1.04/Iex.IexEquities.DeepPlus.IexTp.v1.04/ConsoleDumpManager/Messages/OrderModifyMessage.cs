namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnOrderModifyMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;OrderModifyMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnOrderModifyMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<OrderModifyMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] OrderModifyMessage: short payload ({payloadLength} < {Unsafe.SizeOf<OrderModifyMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(OrderModifyMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] OrderModifyMessage");
        Console.WriteLine($"  ModifyFlags = {msg.ModifyFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  OrderIdReference = {msg.OrderIdReference}");
        Console.WriteLine($"  Size = {msg.Size}");
        Console.WriteLine($"  Price = {msg.Price}");
    }
}
