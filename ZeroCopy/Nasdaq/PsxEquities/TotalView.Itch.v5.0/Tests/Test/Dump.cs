namespace Nasdaq.PsxEquities.TotalView.Itch.v5_0.Test;

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

    internal static void Print(Payload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.StockDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.StockDirectoryMessage));
            Print(reader.StockDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.StockTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.StockTradingActionMessage));
            Print(reader.StockTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.RegShoShortSalePriceTestRestrictedIndicatorMessage));
            Print(reader.RegShoShortSalePriceTestRestrictedIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MarketParticipantPositionMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.MarketParticipantPositionMessage));
            Print(reader.MarketParticipantPositionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbDeclineLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.MwcbDeclineLevelMessage));
            Print(reader.MwcbDeclineLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.MwcbStatusLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.MwcbStatusLevelMessage));
            Print(reader.MwcbStatusLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.LuldAuctionCollarMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.LuldAuctionCollarMessage));
            Print(reader.LuldAuctionCollarMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OperationalHaltMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OperationalHaltMessage));
            Print(reader.OperationalHaltMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderNoMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.AddOrderNoMpidAttributionMessage));
            Print(reader.AddOrderNoMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.AddOrderWithMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.AddOrderWithMpidAttributionMessage));
            Print(reader.AddOrderWithMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OrderExecutedMessage));
            Print(reader.OrderExecutedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderExecutedWithPriceMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OrderExecutedWithPriceMessage));
            Print(reader.OrderExecutedWithPriceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderCancelMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OrderCancelMessage));
            Print(reader.OrderCancelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderDeleteMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OrderDeleteMessage));
            Print(reader.OrderDeleteMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.OrderReplaceMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.OrderReplaceMessage));
            Print(reader.OrderReplaceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.TradeMessageNonCross)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.TradeMessageNonCross));
            Print(reader.TradeMessageNonCross, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.CrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.CrossTradeMessage));
            Print(reader.CrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.BrokenTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.BrokenTradeMessage));
            Print(reader.BrokenTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.NetOrderImbalanceIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Reader.NetOrderImbalanceIndicatorMessage));
            Print(reader.NetOrderImbalanceIndicatorMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Message.Reader.Payload));
        Print(reader.Payload, indent + Indent);
        if (reader.Payload.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Payload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.EndOfSession));
        }
        if (reader.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Packet.Reader.PacketHeader));
        Print(reader.PacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.PsxEquities.TotalView.Itch.v5_0.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in NetOrderImbalanceIndicatorMessage.Layout layout, string indent)
    {
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.PairedShares), layout.PairedShares);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.ImbalanceShares), layout.ImbalanceShares);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.ImbalanceDirection), layout.ImbalanceDirection);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.FarPrice), layout.FarPrice);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.NearPrice), layout.NearPrice);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.CurrentReferencePrice), layout.CurrentReferencePrice);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.CrossType), layout.CrossType);
        Line(indent, nameof(NetOrderImbalanceIndicatorMessage.Layout.PriceVariationIndicator), layout.PriceVariationIndicator);
    }

    internal static void Print(in BrokenTradeMessage.Layout layout, string indent)
    {
        Line(indent, nameof(BrokenTradeMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(BrokenTradeMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(BrokenTradeMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(BrokenTradeMessage.Layout.MatchNumber), layout.MatchNumber);
    }

    internal static void Print(in CrossTradeMessage.Layout layout, string indent)
    {
        Line(indent, nameof(CrossTradeMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(CrossTradeMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(CrossTradeMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(CrossTradeMessage.Layout.CrossShares), layout.CrossShares);
        Line(indent, nameof(CrossTradeMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(CrossTradeMessage.Layout.CrossPrice), layout.CrossPrice);
        Line(indent, nameof(CrossTradeMessage.Layout.MatchNumber), layout.MatchNumber);
        Line(indent, nameof(CrossTradeMessage.Layout.CrossType), layout.CrossType);
    }

    internal static void Print(in TradeMessageNonCross.Layout layout, string indent)
    {
        Line(indent, nameof(TradeMessageNonCross.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(TradeMessageNonCross.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(TradeMessageNonCross.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(TradeMessageNonCross.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(TradeMessageNonCross.Layout.BuySellIndicator), layout.BuySellIndicator);
        Line(indent, nameof(TradeMessageNonCross.Layout.Shares), layout.Shares);
        Line(indent, nameof(TradeMessageNonCross.Layout.Stock), layout.Stock);
        Line(indent, nameof(TradeMessageNonCross.Layout.Price), layout.Price);
        Line(indent, nameof(TradeMessageNonCross.Layout.MatchNumber), layout.MatchNumber);
    }

    internal static void Print(in OrderReplaceMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderReplaceMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OrderReplaceMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OrderReplaceMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderReplaceMessage.Layout.OriginalOrderReferenceNumber), layout.OriginalOrderReferenceNumber);
        Line(indent, nameof(OrderReplaceMessage.Layout.NewOrderReferenceNumber), layout.NewOrderReferenceNumber);
        Line(indent, nameof(OrderReplaceMessage.Layout.Shares), layout.Shares);
        Line(indent, nameof(OrderReplaceMessage.Layout.Price), layout.Price);
    }

    internal static void Print(in OrderDeleteMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderDeleteMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OrderDeleteMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OrderDeleteMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderDeleteMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
    }

    internal static void Print(in OrderCancelMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderCancelMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OrderCancelMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OrderCancelMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderCancelMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(OrderCancelMessage.Layout.CancelledShares), layout.CancelledShares);
    }

    internal static void Print(in OrderExecutedWithPriceMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.ExecutedShares), layout.ExecutedShares);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.MatchNumber), layout.MatchNumber);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.Printable), layout.Printable);
        Line(indent, nameof(OrderExecutedWithPriceMessage.Layout.ExecutionPrice), layout.ExecutionPrice);
    }

    internal static void Print(in OrderExecutedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderExecutedMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OrderExecutedMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OrderExecutedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderExecutedMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(OrderExecutedMessage.Layout.ExecutedShares), layout.ExecutedShares);
        Line(indent, nameof(OrderExecutedMessage.Layout.MatchNumber), layout.MatchNumber);
    }

    internal static void Print(in AddOrderWithMpidAttributionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.BuySellIndicator), layout.BuySellIndicator);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.Shares), layout.Shares);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.Price), layout.Price);
        Line(indent, nameof(AddOrderWithMpidAttributionMessage.Layout.Attribution), layout.Attribution);
    }

    internal static void Print(in AddOrderNoMpidAttributionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.BuySellIndicator), layout.BuySellIndicator);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.Shares), layout.Shares);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(AddOrderNoMpidAttributionMessage.Layout.Price), layout.Price);
    }

    internal static void Print(in OperationalHaltMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OperationalHaltMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(OperationalHaltMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OperationalHaltMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OperationalHaltMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(OperationalHaltMessage.Layout.MarketCode), layout.MarketCode);
        Line(indent, nameof(OperationalHaltMessage.Layout.OperationalHaltAction), layout.OperationalHaltAction);
    }

    internal static void Print(in LuldAuctionCollarMessage.Layout layout, string indent)
    {
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.AuctionCollarReferencePrice), layout.AuctionCollarReferencePrice);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.UpperAuctionCollarPrice), layout.UpperAuctionCollarPrice);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.LowerAuctionCollarPrice), layout.LowerAuctionCollarPrice);
        Line(indent, nameof(LuldAuctionCollarMessage.Layout.AuctionCollarExtension), layout.AuctionCollarExtension);
    }

    internal static void Print(in MwcbStatusLevelMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MwcbStatusLevelMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(MwcbStatusLevelMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(MwcbStatusLevelMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(MwcbStatusLevelMessage.Layout.BreachedLevel), layout.BreachedLevel);
    }

    internal static void Print(in MwcbDeclineLevelMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level1), layout.Level1);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level2), layout.Level2);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level3), layout.Level3);
    }

    internal static void Print(in MarketParticipantPositionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.Mpid), layout.Mpid);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.PrimaryMarketMaker), layout.PrimaryMarketMaker);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.MarketMakerMode), layout.MarketMakerMode);
        Line(indent, nameof(MarketParticipantPositionMessage.Layout.MarketParticipantState), layout.MarketParticipantState);
    }

    internal static void Print(in RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout layout, string indent)
    {
        Line(indent, nameof(RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.LocateCode), layout.LocateCode);
        Line(indent, nameof(RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout.RegShoAction), layout.RegShoAction);
    }

    internal static void Print(in StockTradingActionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StockTradingActionMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(StockTradingActionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(StockTradingActionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StockTradingActionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(StockTradingActionMessage.Layout.TradingState), layout.TradingState);
        Line(indent, nameof(StockTradingActionMessage.Layout.Reserved), layout.Reserved);
        Line(indent, nameof(StockTradingActionMessage.Layout.Reason), layout.Reason);
    }

    internal static void Print(in StockDirectoryMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StockDirectoryMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(StockDirectoryMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(StockDirectoryMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StockDirectoryMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(StockDirectoryMessage.Layout.MarketCategory), layout.MarketCategory);
        Line(indent, nameof(StockDirectoryMessage.Layout.FinancialStatusIndicator), layout.FinancialStatusIndicator);
        Line(indent, nameof(StockDirectoryMessage.Layout.RoundLotSize), layout.RoundLotSize);
        Line(indent, nameof(StockDirectoryMessage.Layout.RoundLotsOnly), layout.RoundLotsOnly);
        Line(indent, nameof(StockDirectoryMessage.Layout.IssueClassification), layout.IssueClassification);
        Line(indent, nameof(StockDirectoryMessage.Layout.IssueSubType), layout.IssueSubType);
        Line(indent, nameof(StockDirectoryMessage.Layout.Authenticity), layout.Authenticity);
        Line(indent, nameof(StockDirectoryMessage.Layout.ShortSaleThresholdIndicator), layout.ShortSaleThresholdIndicator);
        Line(indent, nameof(StockDirectoryMessage.Layout.IpoFlag), layout.IpoFlag);
        Line(indent, nameof(StockDirectoryMessage.Layout.LuldReferencePriceTier), layout.LuldReferencePriceTier);
        Line(indent, nameof(StockDirectoryMessage.Layout.EtpFlag), layout.EtpFlag);
        Line(indent, nameof(StockDirectoryMessage.Layout.EtpLeverageFactor), layout.EtpLeverageFactor);
        Line(indent, nameof(StockDirectoryMessage.Layout.InverseIndicator), layout.InverseIndicator);
    }

    internal static void Print(in SystemEventMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SystemEventMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(SystemEventMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(SystemEventMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(SystemEventMessage.Layout.EventCode), layout.EventCode);
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
