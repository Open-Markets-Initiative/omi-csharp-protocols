namespace Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Viewer;

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
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.StockDirectoryMessage));
            Print(reader.StockDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.StockTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.StockTradingActionMessage));
            Print(reader.StockTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.RegShoRestrictionMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.RegShoRestrictionMessage));
            Print(reader.RegShoRestrictionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbDeclineLevelMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.MwcbDeclineLevelMessage));
            Print(reader.MwcbDeclineLevelMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.MwcbBreachMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.MwcbBreachMessage));
            Print(reader.MwcbBreachMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.OperationalHaltMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.OperationalHaltMessage));
            Print(reader.OperationalHaltMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.BboQuotationMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.BboQuotationMessage));
            Print(reader.BboQuotationMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.PriceImprovementMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.PriceImprovementMessage));
            Print(reader.PriceImprovementMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.IpoQuotingPeriodUpdateMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Reader.IpoQuotingPeriodUpdateMessage));
            Print(reader.IpoQuotingPeriodUpdateMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Message.Reader.Payload));
        Print(reader.Payload, indent + Indent);
        if (reader.Payload.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Payload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.EndOfSession));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader.PacketHeader));
        Print(reader.PacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Qbbo.Itch.v2_1.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in IpoQuotingPeriodUpdateMessage.Layout layout, string indent)
    {
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.Tracking), layout.Tracking);
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.IpoQuotationReleaseTime), layout.IpoQuotationReleaseTime);
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.IpoQuotationReleaseQualifier), layout.IpoQuotationReleaseQualifier);
        Line(indent, nameof(IpoQuotingPeriodUpdateMessage.Layout.IpoPrice), layout.IpoPrice);
    }

    internal static void Print(in PriceImprovementMessage.Layout layout, string indent)
    {
        Line(indent, nameof(PriceImprovementMessage.Layout.Tracking), layout.Tracking);
        Line(indent, nameof(PriceImprovementMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(PriceImprovementMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(PriceImprovementMessage.Layout.InterestFlag), layout.InterestFlag);
    }

    internal static void Print(in BboQuotationMessage.Layout layout, string indent)
    {
        Line(indent, nameof(BboQuotationMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(BboQuotationMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(BboQuotationMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(BboQuotationMessage.Layout.SecurityClass), layout.SecurityClass);
        Line(indent, nameof(BboQuotationMessage.Layout.BestBidPrice), layout.BestBidPrice);
        Line(indent, nameof(BboQuotationMessage.Layout.BestBidSize), layout.BestBidSize);
        Line(indent, nameof(BboQuotationMessage.Layout.BestOfferPrice), layout.BestOfferPrice);
        Line(indent, nameof(BboQuotationMessage.Layout.BestOfferSize), layout.BestOfferSize);
    }

    internal static void Print(in OperationalHaltMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OperationalHaltMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(OperationalHaltMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(OperationalHaltMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(OperationalHaltMessage.Layout.MarketCode), layout.MarketCode);
        Line(indent, nameof(OperationalHaltMessage.Layout.OperationalHaltAction), layout.OperationalHaltAction);
    }

    internal static void Print(in MwcbBreachMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MwcbBreachMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(MwcbBreachMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(MwcbBreachMessage.Layout.BreachedLevel), layout.BreachedLevel);
    }

    internal static void Print(in MwcbDeclineLevelMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level1), layout.Level1);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level2), layout.Level2);
        Line(indent, nameof(MwcbDeclineLevelMessage.Layout.Level3), layout.Level3);
    }

    internal static void Print(in RegShoRestrictionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(RegShoRestrictionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(RegShoRestrictionMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(RegShoRestrictionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(RegShoRestrictionMessage.Layout.RegShoAction), layout.RegShoAction);
    }

    internal static void Print(in StockTradingActionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StockTradingActionMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(StockTradingActionMessage.Layout.TimeStamp), layout.TimeStamp);
        Line(indent, nameof(StockTradingActionMessage.Layout.Stock), layout.Stock);
        Line(indent, nameof(StockTradingActionMessage.Layout.SecurityClass), layout.SecurityClass);
        Line(indent, nameof(StockTradingActionMessage.Layout.CurrentTradingState), layout.CurrentTradingState);
        Line(indent, nameof(StockTradingActionMessage.Layout.Reason), layout.Reason);
    }

    internal static void Print(in StockDirectoryMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StockDirectoryMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(StockDirectoryMessage.Layout.TimeStamp), layout.TimeStamp);
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
        Line(indent, nameof(SystemEventMessage.Layout.TrackingNumber), layout.TrackingNumber);
        Line(indent, nameof(SystemEventMessage.Layout.TimeStamp), layout.TimeStamp);
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
