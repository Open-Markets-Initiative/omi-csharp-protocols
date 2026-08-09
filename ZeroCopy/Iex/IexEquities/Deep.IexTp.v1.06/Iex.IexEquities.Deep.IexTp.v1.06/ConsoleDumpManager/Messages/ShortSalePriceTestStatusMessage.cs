namespace Iex.IexEquities.Deep.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Deep.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnShortSalePriceTestStatusMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;ShortSalePriceTestStatusMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnShortSalePriceTestStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<ShortSalePriceTestStatusMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] ShortSalePriceTestStatusMessage: short payload ({payloadLength} < {Unsafe.SizeOf<ShortSalePriceTestStatusMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(ShortSalePriceTestStatusMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] ShortSalePriceTestStatusMessage");
        Console.WriteLine($"  ShortSalePriceTestStatus = {msg.ShortSalePriceTestStatus}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Detail = {msg.Detail}");
    }
}
