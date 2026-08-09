namespace Iex.IexEquities.Tops.IexTp.ConsoleDumpManager;

using global::Iex.IexEquities.Tops.IexTp;
using System.Runtime.CompilerServices;

/// <summary>
///  Implements &lt;c&gt;OnAuctionInformationMessage&lt;/c&gt; for console dumping.
/// </summary>
public sealed partial class ConsoleDumpManager
{
    /// <summary>
    ///  Dumps every field of a &lt;c&gt;AuctionInformationMessage&lt;/c&gt; to the console.
    /// </summary>
    unsafe partial void OnAuctionInformationMessage(byte* frame, int frameLength, int transportOffset, byte* payload, int payloadLength, int seq)
    {
        if (payloadLength < Unsafe.SizeOf<AuctionInformationMessage.Layout>())
        {
            Console.WriteLine($"[seq={seq}] AuctionInformationMessage: short payload ({payloadLength} < {Unsafe.SizeOf<AuctionInformationMessage.Layout>()} bytes) — skipped");
            return;
        }
        ref readonly var msg = ref *(AuctionInformationMessage.Layout*)payload;

        Console.WriteLine($"[seq={seq}] AuctionInformationMessage");
        Console.WriteLine($"  AuctionType = {msg.AuctionType}");
        Console.WriteLine($"  Timestamp = {msg.Timestamp}");
        Console.WriteLine($"  Symbol = {msg.Symbol}");
        Console.WriteLine($"  PairedShares = {msg.PairedShares}");
        Console.WriteLine($"  ReferencePrice = {msg.ReferencePrice}");
        Console.WriteLine($"  IndicativeClearingPrice = {msg.IndicativeClearingPrice}");
        Console.WriteLine($"  ImbalanceShares = {msg.ImbalanceShares}");
        Console.WriteLine($"  ImbalanceSide = {msg.ImbalanceSide}");
        Console.WriteLine($"  ExtensionNumber = {msg.ExtensionNumber}");
        Console.WriteLine($"  ScheduledAuctionTime = {msg.ScheduledAuctionTime}");
        Console.WriteLine($"  AuctionBookClearingPrice = {msg.AuctionBookClearingPrice}");
        Console.WriteLine($"  CollarReferencePrice = {msg.CollarReferencePrice}");
        Console.WriteLine($"  LowerAuctionCollar = {msg.LowerAuctionCollar}");
        Console.WriteLine($"  UpperAuctionCollar = {msg.UpperAuctionCollar}");
    }
}
