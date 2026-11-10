namespace Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Test;

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

    internal static void Print(ComplexStrategyDirectoryMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.StrategyId), reader.StrategyId);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.StrategyType), reader.StrategyType);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.Source), reader.Source);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.UnderlyingSymbol), reader.UnderlyingSymbol);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.NumberOfLegs), reader.NumberOfLegs);
        foreach (var entry in reader.LegInformation)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.ComplexStrategyDirectoryMessage.Reader.LegInformation));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(Payload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.ComplexStrategyDirectoryMessage));
            Print(reader.ComplexStrategyDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyOpenClosedMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.StrategyOpenClosedMessage));
            Print(reader.StrategyOpenClosedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.StrategyTradingActionMessage));
            Print(reader.StrategyTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidAndAskUpdate)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.StrategyBestBidAndAskUpdate));
            Print(reader.StrategyBestBidAndAskUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestBidUpdate)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.StrategyBestBidUpdate));
            Print(reader.StrategyBestBidUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.StrategyBestAskUpdate)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.StrategyBestAskUpdate));
            Print(reader.StrategyBestAskUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.ComplexStrategyTickerMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Reader.ComplexStrategyTickerMessage));
            Print(reader.ComplexStrategyTickerMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Message.Reader.Payload));
        Print(reader.Payload, indent + Indent);
        if (reader.Payload.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Payload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.EndOfSession));
        }
        if (reader.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader.PacketHeader));
        Print(reader.PacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.TopComboQuoteFeed.Itch.v1_0.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in ComplexStrategyTickerMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.LastPrice), layout.LastPrice);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.Size), layout.Size);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.Volume), layout.Volume);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.High), layout.High);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.Low), layout.Low);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.First), layout.First);
        Line(indent, nameof(ComplexStrategyTickerMessage.Layout.TradeCondition), layout.TradeCondition);
    }

    internal static void Print(in StrategyBestAskUpdate.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyBestAskUpdate.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.QuoteCondition), layout.QuoteCondition);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.Price), layout.Price);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.Size), layout.Size);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.CustSize), layout.CustSize);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.ProCustSize), layout.ProCustSize);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.NttSize), layout.NttSize);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.MarketSize), layout.MarketSize);
        Line(indent, nameof(StrategyBestAskUpdate.Layout.NttMarketSize), layout.NttMarketSize);
    }

    internal static void Print(in StrategyBestBidUpdate.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyBestBidUpdate.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.QuoteCondition), layout.QuoteCondition);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.Price), layout.Price);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.Size), layout.Size);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.CustSize), layout.CustSize);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.ProCustSize), layout.ProCustSize);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.NttSize), layout.NttSize);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.MarketSize), layout.MarketSize);
        Line(indent, nameof(StrategyBestBidUpdate.Layout.NttMarketSize), layout.NttMarketSize);
    }

    internal static void Print(in StrategyBestBidAndAskUpdate.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.QuoteCondition), layout.QuoteCondition);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidPrice), layout.BidPrice);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidSize), layout.BidSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidCustSize), layout.BidCustSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidProCustSize), layout.BidProCustSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidNttSize), layout.BidNttSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidMarketSize), layout.BidMarketSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.BidNttMarketSize), layout.BidNttMarketSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskPrice), layout.AskPrice);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskSize), layout.AskSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskCustSize), layout.AskCustSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskProCustSize), layout.AskProCustSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskNttSize), layout.AskNttSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskMarketSize), layout.AskMarketSize);
        Line(indent, nameof(StrategyBestBidAndAskUpdate.Layout.AskNttMarketSize), layout.AskNttMarketSize);
    }

    internal static void Print(in StrategyTradingActionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyTradingActionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyTradingActionMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyTradingActionMessage.Layout.CurrentTradingState), layout.CurrentTradingState);
    }

    internal static void Print(in StrategyOpenClosedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.OpenState), layout.OpenState);
    }

    internal static void Print(in LegInformation.Layout layout, string indent)
    {
        Line(indent, nameof(LegInformation.Layout.OptionId), layout.OptionId);
        Line(indent, nameof(LegInformation.Layout.SecuritySymbol), layout.SecuritySymbol);
        Line(indent, nameof(LegInformation.Layout.LegId), layout.LegId);
        Line(indent, nameof(LegInformation.Layout.ExpirationYear), layout.ExpirationYear);
        Line(indent, nameof(LegInformation.Layout.ExpirationMonth), layout.ExpirationMonth);
        Line(indent, nameof(LegInformation.Layout.ExpirationDay), layout.ExpirationDay);
        Line(indent, nameof(LegInformation.Layout.ExplicitStrikePrice), layout.ExplicitStrikePrice);
        Line(indent, nameof(LegInformation.Layout.OptionType), layout.OptionType);
        Line(indent, nameof(LegInformation.Layout.Side), layout.Side);
        Line(indent, nameof(LegInformation.Layout.LegRatio), layout.LegRatio);
    }

    internal static void Print(in ComplexStrategyDirectoryMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.StrategyType), layout.StrategyType);
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.Source), layout.Source);
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.UnderlyingSymbol), layout.UnderlyingSymbol);
        Line(indent, nameof(ComplexStrategyDirectoryMessage.Layout.NumberOfLegs), layout.NumberOfLegs);
    }

    internal static void Print(in SystemEventMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SystemEventMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(SystemEventMessage.Layout.EventCode), layout.EventCode);
        Line(indent, nameof(SystemEventMessage.Layout.CurrentYear), layout.CurrentYear);
        Line(indent, nameof(SystemEventMessage.Layout.CurrentMonth), layout.CurrentMonth);
        Line(indent, nameof(SystemEventMessage.Layout.CurrentDay), layout.CurrentDay);
        Line(indent, nameof(SystemEventMessage.Layout.Version), layout.Version);
        Line(indent, nameof(SystemEventMessage.Layout.SubVersion), layout.SubVersion);
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

    internal static void Print(in PacketHeader.Layout layout, string indent)
    {
        Line(indent, nameof(PacketHeader.Layout.Session), layout.Session);
        Line(indent, nameof(PacketHeader.Layout.SequenceNumber), layout.SequenceNumber);
        Line(indent, nameof(PacketHeader.Layout.MessageCount), layout.MessageCount);
    }

    internal static void Print(in Packet.Layout layout, string indent)
    {
        Line(indent, nameof(Packet.Layout.PacketHeader));
        Print(layout.PacketHeader, indent + Indent);
    }
}
