namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnSecurityEventMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;SecurityEventMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnSecurityEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<SecurityEventMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] SecurityEventMessage: short payload ({payloadLength} < {Unsafe.SizeOf<SecurityEventMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(SecurityEventMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] SecurityEventMessage");
        Console.WriteLine($"  SecurityEvent = {msg.SecurityEvent}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
    }
}
