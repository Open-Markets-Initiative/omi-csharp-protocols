namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnClearBookMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;ClearBookMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnClearBookMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<ClearBookMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] ClearBookMessage: short payload ({payloadLength} < {Unsafe.SizeOf<ClearBookMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(ClearBookMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] ClearBookMessage");
        Console.WriteLine($"  Reserved1 = {msg.Reserved1}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
    }
}
