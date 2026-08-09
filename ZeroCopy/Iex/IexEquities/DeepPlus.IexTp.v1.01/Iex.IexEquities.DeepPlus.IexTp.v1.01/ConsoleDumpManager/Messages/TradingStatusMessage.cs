namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnTradingStatusMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;TradingStatusMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnTradingStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<TradingStatusMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] TradingStatusMessage: short payload ({payloadLength} < {Unsafe.SizeOf<TradingStatusMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(TradingStatusMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] TradingStatusMessage");
        Console.WriteLine($"  TradingStatus = {msg.TradingStatus}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  Reason = {msg.Reason}");
    }
}
