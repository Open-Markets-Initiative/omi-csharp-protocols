using System;
using System.Buffers.Binary;
namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Iterator;
internal static class FlowSide
{
    private const int NoDecoder = 0;
    private const int OneDecoder = 1;

    internal enum Outcome { Failed, Waiting, Recognized }

    internal static bool Infer(ReadOnlySpan<byte> bytes, out global::Omi.Pcap.TcpSide side)
    {
        side = global::Omi.Pcap.TcpSide.Unknown;
        var decoders = NoDecoder;
        var waiting = false;
        switch (ClientPacketCapture.Attempt(bytes))
        {
            case Outcome.Recognized:
                side = global::Omi.Pcap.TcpSide.Client;
                decoders++;
                break;
            case Outcome.Waiting:
                waiting = true;
                break;
        }
        switch (ServerPacketCapture.Attempt(bytes))
        {
            case Outcome.Recognized:
                side = global::Omi.Pcap.TcpSide.Server;
                decoders++;
                break;
            case Outcome.Waiting:
                waiting = true;
                break;
        }
        if (decoders == NoDecoder && waiting) return false;
        if (decoders != OneDecoder) side = global::Omi.Pcap.TcpSide.Unknown;
        return true;
    }
}
