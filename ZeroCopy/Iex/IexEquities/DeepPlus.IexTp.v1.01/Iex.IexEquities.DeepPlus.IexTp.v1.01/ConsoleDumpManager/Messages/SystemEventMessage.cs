namespace Iex.IexEquities.DeepPlus.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.DeepPlus.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnSystemEventMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;SystemEventMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnSystemEventMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<SystemEventMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] SystemEventMessage: short payload ({payloadLength} < {Unsafe.SizeOf<SystemEventMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(SystemEventMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] SystemEventMessage");
        Console.WriteLine($"  SystemEvent = {msg.SystemEvent}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
    }
}
