namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnOrderDeleteMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;OrderDeleteMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnOrderDeleteMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<OrderDeleteMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] OrderDeleteMessage: short payload ({payloadLength} < {Unsafe.SizeOf<OrderDeleteMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(OrderDeleteMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] OrderDeleteMessage");
        Console.WriteLine($"  Reserved1 = {msg.Reserved1}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  OrderIdReference = {msg.OrderIdReference}");
    }
}
