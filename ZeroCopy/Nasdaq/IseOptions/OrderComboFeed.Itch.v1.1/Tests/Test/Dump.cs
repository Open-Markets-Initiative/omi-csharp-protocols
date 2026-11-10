namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Test;

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

    internal static void Print(ComplexStrategyAuctionMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.StrategyId), reader.StrategyId);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionId), reader.AuctionId);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OrderType), reader.OrderType);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Size), reader.Size);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.ExecFlag), reader.ExecFlag);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OrderCapacity), reader.OrderCapacity);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Scope), reader.Scope);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.OwnerId), reader.OwnerId);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Giveup), reader.Giveup);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.Cmta), reader.Cmta);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionEvent), reader.AuctionEvent);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionType), reader.AuctionType);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.NumberOfResponses), reader.NumberOfResponses);
        foreach (var entry in reader.AuctionResponse)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyAuctionMessage.Reader.AuctionResponse));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(ComplexStrategyDirectoryMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.StrategyId), reader.StrategyId);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.StrategyType), reader.StrategyType);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.Source), reader.Source);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.UnderlyingSymbol), reader.UnderlyingSymbol);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.NumberOfLegs), reader.NumberOfLegs);
        foreach (var entry in reader.LegInformation)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.ComplexStrategyDirectoryMessage.Reader.LegInformation));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(Payload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyDirectoryMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.ComplexStrategyDirectoryMessage));
            Print(reader.ComplexStrategyDirectoryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyTradingActionMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.StrategyTradingActionMessage));
            Print(reader.StrategyTradingActionMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.StrategyOpenClosedMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.StrategyOpenClosedMessage));
            Print(reader.StrategyOpenClosedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyOrderOnBookMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.ComplexStrategyOrderOnBookMessage));
            Print(reader.ComplexStrategyOrderOnBookMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.ComplexStrategyAuctionMessage)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Reader.ComplexStrategyAuctionMessage));
            Print(reader.ComplexStrategyAuctionMessage, indent + Indent);
        }
    }

    internal static void Print(Message.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Message.Reader.MessageHeader));
        Print(reader.MessageHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Message.Reader.Payload));
        Print(reader.Payload, indent + Indent);
        if (reader.Payload.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Payload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(Messages.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Heartbeat)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Heartbeat));
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.EndOfSession));
        }
        if (reader.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.Message)
        {
            foreach (var entry in reader.Message)
            {
                Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Reader.Message));
                Print(entry, indent + Indent);
            }
        }
    }

    internal static void Print(Packet.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader.PacketHeader));
        Print(reader.PacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Packet.Reader.Messages));
        Print(reader.Messages, indent + Indent);
        if (reader.Messages.Case == global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Messages.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(in AuctionResponse.Layout layout, string indent)
    {
        Line(indent, nameof(AuctionResponse.Layout.ResponsePrice), layout.ResponsePrice);
        Line(indent, nameof(AuctionResponse.Layout.ResponseSize), layout.ResponseSize);
    }

    internal static void Print(in ComplexStrategyAuctionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.AuctionId), layout.AuctionId);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.OrderType), layout.OrderType);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Side), layout.Side);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Price), layout.Price);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Size), layout.Size);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.ExecFlag), layout.ExecFlag);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.OrderCapacity), layout.OrderCapacity);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Scope), layout.Scope);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.OwnerId), layout.OwnerId);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Giveup), layout.Giveup);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.Cmta), layout.Cmta);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.AuctionEvent), layout.AuctionEvent);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.AuctionType), layout.AuctionType);
        Line(indent, nameof(ComplexStrategyAuctionMessage.Layout.NumberOfResponses), layout.NumberOfResponses);
    }

    internal static void Print(in ComplexStrategyOrderOnBookMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.OrderType), layout.OrderType);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Side), layout.Side);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Price), layout.Price);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Size), layout.Size);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.ExecFlag), layout.ExecFlag);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.OrderCapacity), layout.OrderCapacity);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Scope), layout.Scope);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.OwnerId), layout.OwnerId);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Giveup), layout.Giveup);
        Line(indent, nameof(ComplexStrategyOrderOnBookMessage.Layout.Cmta), layout.Cmta);
    }

    internal static void Print(in StrategyOpenClosedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyOpenClosedMessage.Layout.OpenState), layout.OpenState);
    }

    internal static void Print(in StrategyTradingActionMessage.Layout layout, string indent)
    {
        Line(indent, nameof(StrategyTradingActionMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(StrategyTradingActionMessage.Layout.StrategyId), layout.StrategyId);
        Line(indent, nameof(StrategyTradingActionMessage.Layout.CurrentTradingState), layout.CurrentTradingState);
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
        Line(indent, nameof(LegInformation.Layout.LegSide), layout.LegSide);
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
