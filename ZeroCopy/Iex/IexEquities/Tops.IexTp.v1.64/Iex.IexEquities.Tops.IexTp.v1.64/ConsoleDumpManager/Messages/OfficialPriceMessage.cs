namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnOfficialPriceMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;OfficialPriceMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnOfficialPriceMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<OfficialPriceMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] OfficialPriceMessage: short payload ({payloadLength} < {Unsafe.SizeOf<OfficialPriceMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(OfficialPriceMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] OfficialPriceMessage");
        Console.WriteLine($"  PriceType = {msg.PriceType}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  OfficialPrice = {msg.OfficialPrice}");
    }
}
