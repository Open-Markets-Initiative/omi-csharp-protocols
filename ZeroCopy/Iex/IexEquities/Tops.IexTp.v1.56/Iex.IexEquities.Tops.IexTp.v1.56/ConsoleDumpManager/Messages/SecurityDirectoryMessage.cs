namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnSecurityDirectoryMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;SecurityDirectoryMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnSecurityDirectoryMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<SecurityDirectoryMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] SecurityDirectoryMessage: short payload ({payloadLength} < {Unsafe.SizeOf<SecurityDirectoryMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(SecurityDirectoryMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] SecurityDirectoryMessage");
        Console.WriteLine($"  SecurityDirectoryFlags = {msg.SecurityDirectoryFlags}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  RoundLotSize = {msg.RoundLotSize}");
        Console.WriteLine($"  AdjustedPocPrice = {msg.AdjustedPocPrice}");
        Console.WriteLine($"  LuldTier = {msg.LuldTier}");
    }
}
