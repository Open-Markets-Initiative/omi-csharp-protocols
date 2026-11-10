namespace Iex.IexEquities.Tops.IexTp.v1_56.Test;

/// <summary>
///  Writes the fields of messages, headers, and packets to the console, one line for each field.
/// </summary>
internal static class Dump
{
    /// <summary>
    ///  The indent of the fields under the element that holds them.
    /// </summary>
    internal const string Indent = "  ";

    /// <summary>
    ///  The text between the name of a field and its value.
    /// </summary>
    internal const string Separator = " = ";

    /// <summary>
    ///  Writes a name on its own line.
    /// </summary>
    internal static void Line(string indent, string name) => Console.WriteLine($"{indent}{name}");

    /// <summary>
    ///  Writes a name and its value on one line.
    /// </summary>
    internal static void Line(string indent, string name, object? value) => Console.WriteLine($"{indent}{name}{Separator}{value}");

    internal static void Print(MessageData.Reader reader, string indent)
    {
        if (reader.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.QuoteUpdateMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Reader.QuoteUpdateMessage));
            Print(reader.QuoteUpdateMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeReportMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Reader.TradeReportMessage));
            Print(reader.TradeReportMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.TradeBreakMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Reader.TradeBreakMessage));
            Print(reader.TradeBreakMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Message.Reader.MessageData));
        Print(reader.MessageData, indent + Indent);
        if (reader.MessageData.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.None)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.MessageData.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader.IextpHeader));
        Print(reader.IextpHeader, indent + Indent);
        Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.None)
        {
            Line(indent, nameof(global::Iex.IexEquities.Tops.IexTp.v1_56.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in TradeBreakMessage.Layout layout, string indent)
    {
        Line(indent, nameof(TradeBreakMessage.Layout.SaleConditionFlags), layout.SaleConditionFlags);
        Line(indent, nameof(TradeBreakMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradeBreakMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(TradeBreakMessage.Layout.Size), layout.Size);
        Line(indent, nameof(TradeBreakMessage.Layout.Price), layout.Price);
        Line(indent, nameof(TradeBreakMessage.Layout.TradeId), layout.TradeId);
        Line(indent, nameof(TradeBreakMessage.Layout.Reserved4), layout.Reserved4);
    }

    internal static void Print(in TradeReportMessage.Layout layout, string indent)
    {
        Line(indent, nameof(TradeReportMessage.Layout.SaleConditionFlags), layout.SaleConditionFlags);
        Line(indent, nameof(TradeReportMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradeReportMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(TradeReportMessage.Layout.Size), layout.Size);
        Line(indent, nameof(TradeReportMessage.Layout.Price), layout.Price);
        Line(indent, nameof(TradeReportMessage.Layout.TradeId), layout.TradeId);
        Line(indent, nameof(TradeReportMessage.Layout.Reserved4), layout.Reserved4);
    }

    internal static void Print(in QuoteUpdateMessage.Layout layout, string indent)
    {
        Line(indent, nameof(QuoteUpdateMessage.Layout.QuoteUpdateFlags), layout.QuoteUpdateFlags);
        Line(indent, nameof(QuoteUpdateMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(QuoteUpdateMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(QuoteUpdateMessage.Layout.BidSize), layout.BidSize);
        Line(indent, nameof(QuoteUpdateMessage.Layout.BidPrice), layout.BidPrice);
        Line(indent, nameof(QuoteUpdateMessage.Layout.AskPrice), layout.AskPrice);
        Line(indent, nameof(QuoteUpdateMessage.Layout.AskSize), layout.AskSize);
    }

    internal static void Print(in MessageHeader.Layout layout, string indent)
    {
        Line(indent, nameof(MessageHeader.Layout.MessageLength), layout.MessageLength);
        Line(indent, nameof(MessageHeader.Layout.MessageType), layout.MessageType);
    }

    internal static void Print(in Message.Layout layout, string indent)
    {
        Line(indent, nameof(Message.Layout.MessageHeader));
        Print(layout.MessageHeader, indent + Indent);
    }

    internal static void Print(in IextpHeader.Layout layout, string indent)
    {
        Line(indent, nameof(IextpHeader.Layout.Version), layout.Version);
        Line(indent, nameof(IextpHeader.Layout.Reserved), layout.Reserved);
        Line(indent, nameof(IextpHeader.Layout.MessageProtocolId), layout.MessageProtocolId);
        Line(indent, nameof(IextpHeader.Layout.ChannelId), layout.ChannelId);
        Line(indent, nameof(IextpHeader.Layout.SessionId), layout.SessionId);
        Line(indent, nameof(IextpHeader.Layout.PayloadLength), layout.PayloadLength);
        Line(indent, nameof(IextpHeader.Layout.MessageCount), layout.MessageCount);
        Line(indent, nameof(IextpHeader.Layout.StreamOffset), layout.StreamOffset);
        Line(indent, nameof(IextpHeader.Layout.FirstMessageSequenceNumber), layout.FirstMessageSequenceNumber);
        Line(indent, nameof(IextpHeader.Layout.SendTime), layout.SendTime);
    }

    internal static void Print(in Packet.Layout layout, string indent)
    {
        Line(indent, nameof(Packet.Layout.IextpHeader));
        Print(layout.IextpHeader, indent + Indent);
    }
}
