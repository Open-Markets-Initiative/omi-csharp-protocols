namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Viewer;

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

    internal static void Print(FilePayload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.StockDirectoryMessage));
            Print(reader.StockDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.StockTradingActionMessage));
            Print(reader.StockTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.RegShoShortSalePriceTestRestrictedIndicatorMessage));
            Print(reader.RegShoShortSalePriceTestRestrictedIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MarketParticipantPositionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.MarketParticipantPositionMessage));
            Print(reader.MarketParticipantPositionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbDeclineLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.MwcbDeclineLevelMessage));
            Print(reader.MwcbDeclineLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbStatusLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.MwcbStatusLevelMessage));
            Print(reader.MwcbStatusLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.IpoQuotingPeriodUpdate)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.IpoQuotingPeriodUpdate));
            Print(reader.IpoQuotingPeriodUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.LuldAuctionCollarMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.LuldAuctionCollarMessage));
            Print(reader.LuldAuctionCollarMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OperationalHaltMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OperationalHaltMessage));
            Print(reader.OperationalHaltMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderNoMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.AddOrderNoMpidAttributionMessage));
            Print(reader.AddOrderNoMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderWithMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.AddOrderWithMpidAttributionMessage));
            Print(reader.AddOrderWithMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OrderExecutedMessage));
            Print(reader.OrderExecutedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedWithPriceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OrderExecutedWithPriceMessage));
            Print(reader.OrderExecutedWithPriceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderCancelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OrderCancelMessage));
            Print(reader.OrderCancelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderDeleteMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OrderDeleteMessage));
            Print(reader.OrderDeleteMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderReplaceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.OrderReplaceMessage));
            Print(reader.OrderReplaceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NonCrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.NonCrossTradeMessage));
            Print(reader.NonCrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.CrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.CrossTradeMessage));
            Print(reader.CrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.BrokenTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.BrokenTradeMessage));
            Print(reader.BrokenTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NetOrderImbalanceIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.NetOrderImbalanceIndicatorMessage));
            Print(reader.NetOrderImbalanceIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RetailPriceImprovementIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.RetailPriceImprovementIndicatorMessage));
            Print(reader.RetailPriceImprovementIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Reader.DirectListingWithCapitalRaisePriceDiscoveryMessage));
            Print(reader.DirectListingWithCapitalRaisePriceDiscoveryMessage, indent + Indent);
        }
    }

    internal static void Print(FileMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage.Reader.FilePayload));
        Print(reader.FilePayload, indent + Indent);
        if (reader.FilePayload.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(File.Reader reader, string indent)
    {
        foreach (var entry in reader.FileMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.File.Reader.FileMessage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(Payload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.StockDirectoryMessage));
            Print(reader.StockDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.StockTradingActionMessage));
            Print(reader.StockTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.RegShoShortSalePriceTestRestrictedIndicatorMessage));
            Print(reader.RegShoShortSalePriceTestRestrictedIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MarketParticipantPositionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.MarketParticipantPositionMessage));
            Print(reader.MarketParticipantPositionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbDeclineLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.MwcbDeclineLevelMessage));
            Print(reader.MwcbDeclineLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbStatusLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.MwcbStatusLevelMessage));
            Print(reader.MwcbStatusLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.IpoQuotingPeriodUpdate)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.IpoQuotingPeriodUpdate));
            Print(reader.IpoQuotingPeriodUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.LuldAuctionCollarMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.LuldAuctionCollarMessage));
            Print(reader.LuldAuctionCollarMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OperationalHaltMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OperationalHaltMessage));
            Print(reader.OperationalHaltMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderNoMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.AddOrderNoMpidAttributionMessage));
            Print(reader.AddOrderNoMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderWithMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.AddOrderWithMpidAttributionMessage));
            Print(reader.AddOrderWithMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OrderExecutedMessage));
            Print(reader.OrderExecutedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedWithPriceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OrderExecutedWithPriceMessage));
            Print(reader.OrderExecutedWithPriceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderCancelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OrderCancelMessage));
            Print(reader.OrderCancelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderDeleteMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OrderDeleteMessage));
            Print(reader.OrderDeleteMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderReplaceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.OrderReplaceMessage));
            Print(reader.OrderReplaceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NonCrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.NonCrossTradeMessage));
            Print(reader.NonCrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.CrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.CrossTradeMessage));
            Print(reader.CrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.BrokenTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.BrokenTradeMessage));
            Print(reader.BrokenTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NetOrderImbalanceIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.NetOrderImbalanceIndicatorMessage));
            Print(reader.NetOrderImbalanceIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RetailPriceImprovementIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.RetailPriceImprovementIndicatorMessage));
            Print(reader.RetailPriceImprovementIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Reader.DirectListingWithCapitalRaisePriceDiscoveryMessage));
            Print(reader.DirectListingWithCapitalRaisePriceDiscoveryMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Message.Reader.Payload));
        Print(reader.Payload, indent + Indent);
        if (reader.Payload.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.EndOfSession));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader.PacketHeader));
        Print(reader.PacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(SequencedMessage.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.StockDirectoryMessage));
            Print(reader.StockDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.StockTradingActionMessage));
            Print(reader.StockTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.RegShoShortSalePriceTestRestrictedIndicatorMessage));
            Print(reader.RegShoShortSalePriceTestRestrictedIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.MarketParticipantPositionMessage));
            Print(reader.MarketParticipantPositionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.MwcbDeclineLevelMessage));
            Print(reader.MwcbDeclineLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.MwcbStatusLevelMessage));
            Print(reader.MwcbStatusLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.IpoQuotingPeriodUpdate));
            Print(reader.IpoQuotingPeriodUpdate, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.LuldAuctionCollarMessage));
            Print(reader.LuldAuctionCollarMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OperationalHaltMessage));
            Print(reader.OperationalHaltMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.AddOrderNoMpidAttributionMessage));
            Print(reader.AddOrderNoMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.AddOrderWithMpidAttributionMessage));
            Print(reader.AddOrderWithMpidAttributionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OrderExecutedMessage));
            Print(reader.OrderExecutedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OrderExecutedWithPriceMessage));
            Print(reader.OrderExecutedWithPriceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OrderCancelMessage));
            Print(reader.OrderCancelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OrderDeleteMessage));
            Print(reader.OrderDeleteMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.OrderReplaceMessage));
            Print(reader.OrderReplaceMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.NonCrossTradeMessage));
            Print(reader.NonCrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.CrossTradeMessage));
            Print(reader.CrossTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.BrokenTradeMessage));
            Print(reader.BrokenTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.NetOrderImbalanceIndicatorMessage));
            Print(reader.NetOrderImbalanceIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.RetailPriceImprovementIndicatorMessage));
            Print(reader.RetailPriceImprovementIndicatorMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Reader.DirectListingWithCapitalRaisePriceDiscoveryMessage));
            Print(reader.DirectListingWithCapitalRaisePriceDiscoveryMessage, indent + Indent);
        }
    }

    internal static void Print(SequencedDataPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader.SequencedMessageType), reader.SequencedMessageType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader.SequencedMessage));
        Print(reader.SequencedMessage, indent + Indent);
        if (reader.SequencedMessage.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(DebugPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader.DebugText), Convert.ToHexString(reader.DebugText));
    }

    internal static void Print(ServerPayload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Reader.DebugPacket));
            Print(reader.DebugPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Reader.LoginAcceptedPacket));
            Print(reader.LoginAcceptedPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Reader.LoginRejectedPacket));
            Print(reader.LoginRejectedPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Reader.SequencedDataPacket));
            Print(reader.SequencedDataPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession));
        }
    }

    internal static void Print(ServerSoupBinTcpPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerSoupBinTcpPacket.Reader.ServerPacketHeader));
        Print(reader.ServerPacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerSoupBinTcpPacket.Reader.ServerPayload));
        Print(reader.ServerPayload, indent + Indent);
        if (reader.ServerPayload.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ServerPacket.Reader reader, string indent)
    {
        foreach (var entry in reader.ServerSoupBinTcpPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket.Reader.ServerSoupBinTcpPacket));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(UnsequencedDataPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader.UnsequencedMessageType), reader.UnsequencedMessageType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader.UnsequencedMessage), Convert.ToHexString(reader.UnsequencedMessage));
    }

    internal static void Print(ClientPayload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Reader.DebugPacket));
            Print(reader.DebugPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Reader.LoginRequestPacket));
            Print(reader.LoginRequestPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Reader.UnsequencedDataPacket));
            Print(reader.UnsequencedDataPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest));
        }
    }

    internal static void Print(ClientSoupBinTcpPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientSoupBinTcpPacket.Reader.ClientPacketHeader));
        Print(reader.ClientPacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientSoupBinTcpPacket.Reader.ClientPayload));
        Print(reader.ClientPayload, indent + Indent);
        if (reader.ClientPayload.Case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ClientPacket.Reader reader, string indent)
    {
        foreach (var entry in reader.ClientSoupBinTcpPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket.Reader.ClientSoupBinTcpPacket));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(in DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout layout, string indent)
    {
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.OpenEligibilityStatus), layout.OpenEligibilityStatus);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.MinimumAllowablePrice), layout.MinimumAllowablePrice);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.MaximumAllowablePrice), layout.MaximumAllowablePrice);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.NearExecutionPrice), layout.NearExecutionPrice);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.NearExecutionTime), layout.NearExecutionTime);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.LowerPriceRangeCollar), layout.LowerPriceRangeCollar);
        Line(indent, nameof(DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout.UpperPriceRangeCollar), layout.UpperPriceRangeCollar);
    }

    internal static void Print(in RetailPriceImprovementIndicatorMessage.Layout layout, string indent)
    {
        Line(indent, nameof(RetailPriceImprovementIndicatorMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(RetailPriceImprovementIndicatorMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(RetailPriceImprovementIndicatorMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(RetailPriceImprovementIndicatorMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(RetailPriceImprovementIndicatorMessage.Layout.InterestFlag), layout.InterestFlag);
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

    internal static void Print(in NonCrossTradeMessage.Layout layout, string indent)
    {
        Line(indent, nameof(NonCrossTradeMessage.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(NonCrossTradeMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(NonCrossTradeMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(NonCrossTradeMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(NonCrossTradeMessage.Layout.BuySellIndicator), layout.BuySellIndicator);
        Line(indent, nameof(NonCrossTradeMessage.Layout.Shares), layout.Shares);
        Line(indent, nameof(NonCrossTradeMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(NonCrossTradeMessage.Layout.Price), layout.Price);
        Line(indent, nameof(NonCrossTradeMessage.Layout.MatchNumber), layout.MatchNumber);
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
        Line(indent, nameof(OrderCancelMessage.Layout.CanceledShares), layout.CanceledShares);
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

    internal static void Print(in IpoQuotingPeriodUpdate.Layout layout, string indent)
    {
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.StockLocate), layout.StockLocate);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.Stock), layout.Stock);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.IpoQuotationReleaseTime), layout.IpoQuotationReleaseTime);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.IpoQuotationReleaseQualifier), layout.IpoQuotationReleaseQualifier);
        Line(indent, nameof(IpoQuotingPeriodUpdate.Layout.IpoPrice), layout.IpoPrice);
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
        Line(indent, nameof(StockTradingActionMessage.Layout.ReasonCode), layout.ReasonCode);
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

    internal static void Print(in FileMessage.Layout layout, string indent)
    {
        Line(indent, nameof(FileMessage.Layout.MessageHeader));
        Print(layout.MessageHeader, indent + Indent);
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

    internal static void Print(in SequencedDataPacket.Layout layout, string indent)
    {
        Line(indent, nameof(SequencedDataPacket.Layout.SequencedMessageType), layout.SequencedMessageType);
    }

    internal static void Print(in LoginRejectedPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginRejectedPacket.Layout.RejectReasonCode), layout.RejectReasonCode);
    }

    internal static void Print(in LoginAcceptedPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginAcceptedPacket.Layout.AcceptedSession), layout.AcceptedSession);
        Line(indent, nameof(LoginAcceptedPacket.Layout.AcceptedSequenceNumber), layout.AcceptedSequenceNumber);
    }

    internal static void Print(in ServerPacketHeader.Layout layout, string indent)
    {
        Line(indent, nameof(ServerPacketHeader.Layout.PacketLength), layout.PacketLength);
        Line(indent, nameof(ServerPacketHeader.Layout.ServerPacketType), layout.ServerPacketType);
    }

    internal static void Print(in ServerSoupBinTcpPacket.Layout layout, string indent)
    {
        Line(indent, nameof(ServerSoupBinTcpPacket.Layout.ServerPacketHeader));
        Print(layout.ServerPacketHeader, indent + Indent);
    }

    internal static void Print(in UnsequencedDataPacket.Layout layout, string indent)
    {
        Line(indent, nameof(UnsequencedDataPacket.Layout.UnsequencedMessageType), layout.UnsequencedMessageType);
    }

    internal static void Print(in LoginRequestPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginRequestPacket.Layout.Username), layout.Username);
        Line(indent, nameof(LoginRequestPacket.Layout.Password), layout.Password);
        Line(indent, nameof(LoginRequestPacket.Layout.RequestedSession), layout.RequestedSession);
        Line(indent, nameof(LoginRequestPacket.Layout.RequestedSequenceNumber), layout.RequestedSequenceNumber);
    }

    internal static void Print(in ClientPacketHeader.Layout layout, string indent)
    {
        Line(indent, nameof(ClientPacketHeader.Layout.PacketLength), layout.PacketLength);
        Line(indent, nameof(ClientPacketHeader.Layout.ClientPacketType), layout.ClientPacketType);
    }

    internal static void Print(in ClientSoupBinTcpPacket.Layout layout, string indent)
    {
        Line(indent, nameof(ClientSoupBinTcpPacket.Layout.ClientPacketHeader));
        Print(layout.ClientPacketHeader, indent + Indent);
    }
}
