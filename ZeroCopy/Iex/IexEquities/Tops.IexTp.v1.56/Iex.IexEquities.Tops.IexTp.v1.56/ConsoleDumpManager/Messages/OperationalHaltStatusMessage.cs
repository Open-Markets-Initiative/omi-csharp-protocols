namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnOperationalHaltStatusMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;OperationalHaltStatusMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnOperationalHaltStatusMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<OperationalHaltStatusMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] OperationalHaltStatusMessage: short payload ({payloadLength} < {Unsafe.SizeOf<OperationalHaltStatusMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(OperationalHaltStatusMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] OperationalHaltStatusMessage");
        Console.WriteLine($"  OperationalHaltStatus = {msg.OperationalHaltStatus}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
    }
}
