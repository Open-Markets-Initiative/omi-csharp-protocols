namespace Iex.IexEquities.Deep.IexTp.v1_08.Test;

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
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.SecurityDirectoryMessage));
            Print(reader.SecurityDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.TradingStatusMessage));
            Print(reader.TradingStatusMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.RetailLiquidityIndicatorMessage));
            Print(reader.RetailLiquidityIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.OperationalHaltStatusMessage));
            Print(reader.OperationalHaltStatusMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.ShortSalePriceTestStatusMessage));
            Print(reader.ShortSalePriceTestStatusMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.SecurityEventMessage));
            Print(reader.SecurityEventMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.PriceLevelBuyUpdateMessage));
            Print(reader.PriceLevelBuyUpdateMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.PriceLevelSellUpdateMessage));
            Print(reader.PriceLevelSellUpdateMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.TradeReportMessage));
            Print(reader.TradeReportMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.OfficialPriceMessage));
            Print(reader.OfficialPriceMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.TradeBreakMessage));
            Print(reader.TradeBreakMessage, indent + Indent);
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Reader.AuctionInformationMessage));
            Print(reader.AuctionInformationMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Message.Reader.MessageData));
        Print(reader.MessageData, indent + Indent);
        if (reader.MessageData.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.None)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader.IextpHeader));
        Print(reader.IextpHeader, indent + Indent);
        Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.None)
        {
            Line(indent, nameof(global::Iex.IexEquities.Deep.IexTp.v1_08.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in AuctionInformationMessage.Layout layout, string indent)
    {
        Line(indent, nameof(AuctionInformationMessage.Layout.AuctionType), layout.AuctionType);
        Line(indent, nameof(AuctionInformationMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(AuctionInformationMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(AuctionInformationMessage.Layout.PairedShares), layout.PairedShares);
        Line(indent, nameof(AuctionInformationMessage.Layout.ReferencePrice), layout.ReferencePrice);
        Line(indent, nameof(AuctionInformationMessage.Layout.IndicativeClearingPrice), layout.IndicativeClearingPrice);
        Line(indent, nameof(AuctionInformationMessage.Layout.ImbalanceShares), layout.ImbalanceShares);
        Line(indent, nameof(AuctionInformationMessage.Layout.ImbalanceSide), layout.ImbalanceSide);
        Line(indent, nameof(AuctionInformationMessage.Layout.ExtensionNumber), layout.ExtensionNumber);
        Line(indent, nameof(AuctionInformationMessage.Layout.ScheduledAuctionTime), layout.ScheduledAuctionTime);
        Line(indent, nameof(AuctionInformationMessage.Layout.AuctionBookClearingPrice), layout.AuctionBookClearingPrice);
        Line(indent, nameof(AuctionInformationMessage.Layout.CollarReferencePrice), layout.CollarReferencePrice);
        Line(indent, nameof(AuctionInformationMessage.Layout.LowerAuctionCollar), layout.LowerAuctionCollar);
        Line(indent, nameof(AuctionInformationMessage.Layout.UpperAuctionCollar), layout.UpperAuctionCollar);
    }

    internal static void Print(in TradeBreakMessage.Layout layout, string indent)
    {
        Line(indent, nameof(TradeBreakMessage.Layout.SaleConditionFlags), layout.SaleConditionFlags);
        Line(indent, nameof(TradeBreakMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradeBreakMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(TradeBreakMessage.Layout.Size), layout.Size);
        Line(indent, nameof(TradeBreakMessage.Layout.Price), layout.Price);
        Line(indent, nameof(TradeBreakMessage.Layout.TradeId), layout.TradeId);
    }

    internal static void Print(in OfficialPriceMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OfficialPriceMessage.Layout.PriceType), layout.PriceType);
        Line(indent, nameof(OfficialPriceMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OfficialPriceMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(OfficialPriceMessage.Layout.OfficialPrice), layout.OfficialPrice);
    }

    internal static void Print(in TradeReportMessage.Layout layout, string indent)
    {
        Line(indent, nameof(TradeReportMessage.Layout.SaleConditionFlags), layout.SaleConditionFlags);
        Line(indent, nameof(TradeReportMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradeReportMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(TradeReportMessage.Layout.Size), layout.Size);
        Line(indent, nameof(TradeReportMessage.Layout.Price), layout.Price);
        Line(indent, nameof(TradeReportMessage.Layout.TradeId), layout.TradeId);
    }

    internal static void Print(in PriceLevelSellUpdateMessage.Layout layout, string indent)
    {
        Line(indent, nameof(PriceLevelSellUpdateMessage.Layout.EventFlags), layout.EventFlags);
        Line(indent, nameof(PriceLevelSellUpdateMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(PriceLevelSellUpdateMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(PriceLevelSellUpdateMessage.Layout.Size), layout.Size);
        Line(indent, nameof(PriceLevelSellUpdateMessage.Layout.Price), layout.Price);
    }

    internal static void Print(in PriceLevelBuyUpdateMessage.Layout layout, string indent)
    {
        Line(indent, nameof(PriceLevelBuyUpdateMessage.Layout.EventFlags), layout.EventFlags);
        Line(indent, nameof(PriceLevelBuyUpdateMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(PriceLevelBuyUpdateMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(PriceLevelBuyUpdateMessage.Layout.Size), layout.Size);
        Line(indent, nameof(PriceLevelBuyUpdateMessage.Layout.Price), layout.Price);
    }

    internal static void Print(in SecurityEventMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SecurityEventMessage.Layout.SecurityEvent), layout.SecurityEvent);
        Line(indent, nameof(SecurityEventMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(SecurityEventMessage.Layout.Symbol), layout.Symbol);
    }

    internal static void Print(in ShortSalePriceTestStatusMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ShortSalePriceTestStatusMessage.Layout.ShortSalePriceTestStatus), layout.ShortSalePriceTestStatus);
        Line(indent, nameof(ShortSalePriceTestStatusMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ShortSalePriceTestStatusMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(ShortSalePriceTestStatusMessage.Layout.Detail), layout.Detail);
    }

    internal static void Print(in OperationalHaltStatusMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OperationalHaltStatusMessage.Layout.OperationalHaltStatus), layout.OperationalHaltStatus);
        Line(indent, nameof(OperationalHaltStatusMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OperationalHaltStatusMessage.Layout.Symbol), layout.Symbol);
    }

    internal static void Print(in RetailLiquidityIndicatorMessage.Layout layout, string indent)
    {
        Line(indent, nameof(RetailLiquidityIndicatorMessage.Layout.RetailLiquidityIndicator), layout.RetailLiquidityIndicator);
        Line(indent, nameof(RetailLiquidityIndicatorMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(RetailLiquidityIndicatorMessage.Layout.Symbol), layout.Symbol);
    }

    internal static void Print(in TradingStatusMessage.Layout layout, string indent)
    {
        Line(indent, nameof(TradingStatusMessage.Layout.TradingStatus), layout.TradingStatus);
        Line(indent, nameof(TradingStatusMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradingStatusMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(TradingStatusMessage.Layout.Reason), layout.Reason);
    }

    internal static void Print(in SecurityDirectoryMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SecurityDirectoryMessage.Layout.SecurityDirectoryFlags), layout.SecurityDirectoryFlags);
        Line(indent, nameof(SecurityDirectoryMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(SecurityDirectoryMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(SecurityDirectoryMessage.Layout.RoundLotSize), layout.RoundLotSize);
        Line(indent, nameof(SecurityDirectoryMessage.Layout.AdjustedPocPrice), layout.AdjustedPocPrice);
        Line(indent, nameof(SecurityDirectoryMessage.Layout.LuldTier), layout.LuldTier);
    }

    internal static void Print(in SystemEventMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SystemEventMessage.Layout.SystemEvent), layout.SystemEvent);
        Line(indent, nameof(SystemEventMessage.Layout.Timestamp), layout.Timestamp);
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
