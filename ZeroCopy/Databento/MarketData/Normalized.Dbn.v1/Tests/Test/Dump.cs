namespace Databento.MarketData.Normalized.Dbn.v1.Test;

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

    internal static void Print(ControlMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.ControlMessage.Reader.ControlFields), Convert.ToHexString(reader.ControlFields));
    }

    internal static void Print(ClientStream.Reader reader, string indent)
    {
        foreach (var entry in reader.ControlMessage)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.ClientStream.Reader.ControlMessage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(Mbp10Msg.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Price), reader.Price);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Size), reader.Size);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Action), reader.Action);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Side), reader.Side);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Flags), reader.Flags);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Depth), reader.Depth);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.TsRecv), reader.TsRecv);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.TsInDelta), reader.TsInDelta);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.Sequence), reader.Sequence);
        foreach (var entry in reader.BidAskPair)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Reader.BidAskPair));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(RecordBody.Reader reader, string indent)
    {
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.TradeMsg));
            Print(reader.TradeMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.Mbp1Msg));
            Print(reader.Mbp1Msg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.Mbp10Msg));
            Print(reader.Mbp10Msg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.OhlcvMsg));
            Print(reader.OhlcvMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.StatusMsg));
            Print(reader.StatusMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.InstrumentDefMsg));
            Print(reader.InstrumentDefMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.ImbalanceMsg));
            Print(reader.ImbalanceMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.ErrorMsg));
            Print(reader.ErrorMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.SymbolMappingMsg));
            Print(reader.SymbolMappingMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.SystemMsg));
            Print(reader.SystemMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.StatMsg));
            Print(reader.StatMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.MboMsg));
            Print(reader.MboMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.Cmbp1Msg));
            Print(reader.Cmbp1Msg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.CbboMsg));
            Print(reader.CbboMsg, indent + Indent);
        }
        if (reader.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Reader.BboMsg));
            Print(reader.BboMsg, indent + Indent);
        }
    }

    internal static void Print(Record.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader.RecordHeader));
        Print(reader.RecordHeader, indent + Indent);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader.RecordBody));
        Print(reader.RecordBody, indent + Indent);
        if (reader.RecordBody.Case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.None)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.None), Convert.ToHexString(reader.Bytes));
        }
        if (reader.TsOutTime is { } tsOutTimeValue)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Record.Reader.TsOutTime), tsOutTimeValue);
        }
    }

    internal static void Print(SymbolMapping.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMapping.Reader.RawSymbolIn), reader.RawSymbolIn);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMapping.Reader.IntervalCount), reader.IntervalCount);
        foreach (var entry in reader.MappingInterval)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.SymbolMapping.Reader.MappingInterval));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(Metadata.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Magic), reader.Magic);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Version), reader.Version);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.MetadataLength), reader.MetadataLength);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Dataset), reader.Dataset);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Schema), reader.Schema);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Start), reader.Start);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.End), reader.End);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Limit), reader.Limit);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.MetadataReserved8), reader.MetadataReserved8);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.StypeIn), reader.StypeIn);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.StypeOut), reader.StypeOut);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.TsOut), reader.TsOut);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.MetadataReserved), reader.MetadataReserved);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.SchemaDefinitionLength), reader.SchemaDefinitionLength);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.SchemaDefinition), Convert.ToHexString(reader.SchemaDefinition));
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.SymbolsCount), reader.SymbolsCount);
        foreach (var entry in reader.Symbol)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.Symbol), entry);
        }
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.PartialCount), reader.PartialCount);
        foreach (var entry in reader.PartialSymbol)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.PartialSymbol), entry);
        }
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.NotFoundCount), reader.NotFoundCount);
        foreach (var entry in reader.NotFoundSymbol)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.NotFoundSymbol), entry);
        }
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.MappingsCount), reader.MappingsCount);
        foreach (var entry in reader.SymbolMapping)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Metadata.Reader.SymbolMapping));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(AuthenticationResponse.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.AuthenticationResponse.Reader.ControlFields), Convert.ToHexString(reader.ControlFields));
    }

    internal static void Print(ChallengeRequest.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.ChallengeRequest.Reader.ControlFields), Convert.ToHexString(reader.ControlFields));
    }

    internal static void Print(Greeting.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.Greeting.Reader.ControlFields), Convert.ToHexString(reader.ControlFields));
    }

    internal static void Print(GatewayStream.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.Greeting));
        Print(reader.Greeting, indent + Indent);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.ChallengeRequest));
        Print(reader.ChallengeRequest, indent + Indent);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.AuthenticationResponse));
        Print(reader.AuthenticationResponse, indent + Indent);
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.Metadata));
        Print(reader.Metadata, indent + Indent);
        foreach (var entry in reader.Record)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.GatewayStream.Reader.Record));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(File.Reader reader, string indent)
    {
        Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.File.Reader.Metadata));
        Print(reader.Metadata, indent + Indent);
        foreach (var entry in reader.Record)
        {
            Line(indent, nameof(global::Databento.MarketData.Normalized.Dbn.v1.File.Reader.Record));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(in BidAskPair.Layout layout, string indent)
    {
        Line(indent, nameof(BidAskPair.Layout.BidPx), layout.BidPx);
        Line(indent, nameof(BidAskPair.Layout.AskPx), layout.AskPx);
        Line(indent, nameof(BidAskPair.Layout.BidSz), layout.BidSz);
        Line(indent, nameof(BidAskPair.Layout.AskSz), layout.AskSz);
        Line(indent, nameof(BidAskPair.Layout.BidCt), layout.BidCt);
        Line(indent, nameof(BidAskPair.Layout.AskCt), layout.AskCt);
    }

    internal static void Print(in BboMsg.Layout layout, string indent)
    {
        Line(indent, nameof(BboMsg.Layout.Price), layout.Price);
        Line(indent, nameof(BboMsg.Layout.Size), layout.Size);
        Line(indent, nameof(BboMsg.Layout.Reserved1), layout.Reserved1);
        Line(indent, nameof(BboMsg.Layout.Side), layout.Side);
        Line(indent, nameof(BboMsg.Layout.Flags), layout.Flags);
        Line(indent, nameof(BboMsg.Layout.SecondReserved1), layout.SecondReserved1);
        Line(indent, nameof(BboMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(BboMsg.Layout.Reserved4), layout.Reserved4);
        Line(indent, nameof(BboMsg.Layout.Sequence), layout.Sequence);
        Line(indent, nameof(BboMsg.Layout.BidAskPair));
        Print(layout.BidAskPair, indent + Indent);
    }

    internal static void Print(in ConsolidatedBidAskPair.Layout layout, string indent)
    {
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.BidPx), layout.BidPx);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.AskPx), layout.AskPx);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.BidSz), layout.BidSz);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.AskSz), layout.AskSz);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.BidPb), layout.BidPb);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.Reserved2), layout.Reserved2);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.AskPb), layout.AskPb);
        Line(indent, nameof(ConsolidatedBidAskPair.Layout.SecondReserved2), layout.SecondReserved2);
    }

    internal static void Print(in CbboMsg.Layout layout, string indent)
    {
        Line(indent, nameof(CbboMsg.Layout.Price), layout.Price);
        Line(indent, nameof(CbboMsg.Layout.Size), layout.Size);
        Line(indent, nameof(CbboMsg.Layout.Reserved1), layout.Reserved1);
        Line(indent, nameof(CbboMsg.Layout.Side), layout.Side);
        Line(indent, nameof(CbboMsg.Layout.Flags), layout.Flags);
        Line(indent, nameof(CbboMsg.Layout.SecondReserved1), layout.SecondReserved1);
        Line(indent, nameof(CbboMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(CbboMsg.Layout.Reserved8), layout.Reserved8);
        Line(indent, nameof(CbboMsg.Layout.ConsolidatedBidAskPair));
        Print(layout.ConsolidatedBidAskPair, indent + Indent);
    }

    internal static void Print(in Cmbp1Msg.Layout layout, string indent)
    {
        Line(indent, nameof(Cmbp1Msg.Layout.Price), layout.Price);
        Line(indent, nameof(Cmbp1Msg.Layout.Size), layout.Size);
        Line(indent, nameof(Cmbp1Msg.Layout.Action), layout.Action);
        Line(indent, nameof(Cmbp1Msg.Layout.Side), layout.Side);
        Line(indent, nameof(Cmbp1Msg.Layout.Flags), layout.Flags);
        Line(indent, nameof(Cmbp1Msg.Layout.Reserved1), layout.Reserved1);
        Line(indent, nameof(Cmbp1Msg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(Cmbp1Msg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(Cmbp1Msg.Layout.Reserved4), layout.Reserved4);
        Line(indent, nameof(Cmbp1Msg.Layout.ConsolidatedBidAskPair));
        Print(layout.ConsolidatedBidAskPair, indent + Indent);
    }

    internal static void Print(in MboMsg.Layout layout, string indent)
    {
        Line(indent, nameof(MboMsg.Layout.OrderId), layout.OrderId);
        Line(indent, nameof(MboMsg.Layout.Price), layout.Price);
        Line(indent, nameof(MboMsg.Layout.Size), layout.Size);
        Line(indent, nameof(MboMsg.Layout.Flags), layout.Flags);
        Line(indent, nameof(MboMsg.Layout.ChannelId), layout.ChannelId);
        Line(indent, nameof(MboMsg.Layout.Action), layout.Action);
        Line(indent, nameof(MboMsg.Layout.Side), layout.Side);
        Line(indent, nameof(MboMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(MboMsg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(MboMsg.Layout.Sequence), layout.Sequence);
    }

    internal static void Print(in StatMsg.Layout layout, string indent)
    {
        Line(indent, nameof(StatMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(StatMsg.Layout.TsRef), layout.TsRef);
        Line(indent, nameof(StatMsg.Layout.Price), layout.Price);
        Line(indent, nameof(StatMsg.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(StatMsg.Layout.Sequence), layout.Sequence);
        Line(indent, nameof(StatMsg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(StatMsg.Layout.StatType), layout.StatType);
        Line(indent, nameof(StatMsg.Layout.Channel), layout.Channel);
        Line(indent, nameof(StatMsg.Layout.UpdateAction), layout.UpdateAction);
        Line(indent, nameof(StatMsg.Layout.StatFlags), layout.StatFlags);
        Line(indent, nameof(StatMsg.Layout.Reserved6), layout.Reserved6);
    }

    internal static void Print(in SystemMsg.Layout layout, string indent)
    {
        Line(indent, nameof(SystemMsg.Layout.Msg), layout.Msg);
    }

    internal static void Print(in SymbolMappingMsg.Layout layout, string indent)
    {
        Line(indent, nameof(SymbolMappingMsg.Layout.StypeInSymbol), layout.StypeInSymbol);
        Line(indent, nameof(SymbolMappingMsg.Layout.StypeOutSymbol), layout.StypeOutSymbol);
        Line(indent, nameof(SymbolMappingMsg.Layout.Dummy4), layout.Dummy4);
        Line(indent, nameof(SymbolMappingMsg.Layout.StartTs), layout.StartTs);
        Line(indent, nameof(SymbolMappingMsg.Layout.EndTs), layout.EndTs);
    }

    internal static void Print(in ErrorMsg.Layout layout, string indent)
    {
        Line(indent, nameof(ErrorMsg.Layout.Err), layout.Err);
    }

    internal static void Print(in ImbalanceMsg.Layout layout, string indent)
    {
        Line(indent, nameof(ImbalanceMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(ImbalanceMsg.Layout.RefPrice), layout.RefPrice);
        Line(indent, nameof(ImbalanceMsg.Layout.AuctionTime), layout.AuctionTime);
        Line(indent, nameof(ImbalanceMsg.Layout.ContBookClrPrice), layout.ContBookClrPrice);
        Line(indent, nameof(ImbalanceMsg.Layout.AuctInterestClrPrice), layout.AuctInterestClrPrice);
        Line(indent, nameof(ImbalanceMsg.Layout.SsrFillingPrice), layout.SsrFillingPrice);
        Line(indent, nameof(ImbalanceMsg.Layout.IndMatchPrice), layout.IndMatchPrice);
        Line(indent, nameof(ImbalanceMsg.Layout.UpperCollar), layout.UpperCollar);
        Line(indent, nameof(ImbalanceMsg.Layout.LowerCollar), layout.LowerCollar);
        Line(indent, nameof(ImbalanceMsg.Layout.PairedQty), layout.PairedQty);
        Line(indent, nameof(ImbalanceMsg.Layout.TotalImbalanceQty), layout.TotalImbalanceQty);
        Line(indent, nameof(ImbalanceMsg.Layout.MarketImbalanceQty), layout.MarketImbalanceQty);
        Line(indent, nameof(ImbalanceMsg.Layout.UnpairedQty), layout.UnpairedQty);
        Line(indent, nameof(ImbalanceMsg.Layout.AuctionType), layout.AuctionType);
        Line(indent, nameof(ImbalanceMsg.Layout.Side), layout.Side);
        Line(indent, nameof(ImbalanceMsg.Layout.AuctionStatus), layout.AuctionStatus);
        Line(indent, nameof(ImbalanceMsg.Layout.FreezeStatus), layout.FreezeStatus);
        Line(indent, nameof(ImbalanceMsg.Layout.NumExtensions), layout.NumExtensions);
        Line(indent, nameof(ImbalanceMsg.Layout.UnpairedSide), layout.UnpairedSide);
        Line(indent, nameof(ImbalanceMsg.Layout.SignificantImbalance), layout.SignificantImbalance);
        Line(indent, nameof(ImbalanceMsg.Layout.Reserved1), layout.Reserved1);
    }

    internal static void Print(in InstrumentDefMsg.Layout layout, string indent)
    {
        Line(indent, nameof(InstrumentDefMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinPriceIncrement), layout.MinPriceIncrement);
        Line(indent, nameof(InstrumentDefMsg.Layout.DisplayFactor), layout.DisplayFactor);
        Line(indent, nameof(InstrumentDefMsg.Layout.Expiration), layout.Expiration);
        Line(indent, nameof(InstrumentDefMsg.Layout.Activation), layout.Activation);
        Line(indent, nameof(InstrumentDefMsg.Layout.HighLimitPrice), layout.HighLimitPrice);
        Line(indent, nameof(InstrumentDefMsg.Layout.LowLimitPrice), layout.LowLimitPrice);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaxPriceVariation), layout.MaxPriceVariation);
        Line(indent, nameof(InstrumentDefMsg.Layout.TradingReferencePrice), layout.TradingReferencePrice);
        Line(indent, nameof(InstrumentDefMsg.Layout.UnitOfMeasureQty), layout.UnitOfMeasureQty);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinPriceIncrementAmount), layout.MinPriceIncrementAmount);
        Line(indent, nameof(InstrumentDefMsg.Layout.PriceRatio), layout.PriceRatio);
        Line(indent, nameof(InstrumentDefMsg.Layout.InstAttribValue), layout.InstAttribValue);
        Line(indent, nameof(InstrumentDefMsg.Layout.UnderlyingId), layout.UnderlyingId);
        Line(indent, nameof(InstrumentDefMsg.Layout.RawInstrumentId), layout.RawInstrumentId);
        Line(indent, nameof(InstrumentDefMsg.Layout.MarketDepthImplied), layout.MarketDepthImplied);
        Line(indent, nameof(InstrumentDefMsg.Layout.MarketDepth), layout.MarketDepth);
        Line(indent, nameof(InstrumentDefMsg.Layout.MarketSegmentId), layout.MarketSegmentId);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaxTradeVol), layout.MaxTradeVol);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinLotSize), layout.MinLotSize);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinLotSizeBlock), layout.MinLotSizeBlock);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinLotSizeRoundLot), layout.MinLotSizeRoundLot);
        Line(indent, nameof(InstrumentDefMsg.Layout.MinTradeVol), layout.MinTradeVol);
        Line(indent, nameof(InstrumentDefMsg.Layout.Reserved4), layout.Reserved4);
        Line(indent, nameof(InstrumentDefMsg.Layout.ContractMultiplier), layout.ContractMultiplier);
        Line(indent, nameof(InstrumentDefMsg.Layout.DecayQuantity), layout.DecayQuantity);
        Line(indent, nameof(InstrumentDefMsg.Layout.OriginalContractSize), layout.OriginalContractSize);
        Line(indent, nameof(InstrumentDefMsg.Layout.SecondReserved4), layout.SecondReserved4);
        Line(indent, nameof(InstrumentDefMsg.Layout.TradingReferenceDate), layout.TradingReferenceDate);
        Line(indent, nameof(InstrumentDefMsg.Layout.ApplId), layout.ApplId);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaturityYear), layout.MaturityYear);
        Line(indent, nameof(InstrumentDefMsg.Layout.DecayStartDate), layout.DecayStartDate);
        Line(indent, nameof(InstrumentDefMsg.Layout.Channel), layout.Channel);
        Line(indent, nameof(InstrumentDefMsg.Layout.Currency), layout.Currency);
        Line(indent, nameof(InstrumentDefMsg.Layout.SettlCurrency), layout.SettlCurrency);
        Line(indent, nameof(InstrumentDefMsg.Layout.Secsubtype), layout.Secsubtype);
        Line(indent, nameof(InstrumentDefMsg.Layout.RawSymbol), layout.RawSymbol);
        Line(indent, nameof(InstrumentDefMsg.Layout.Group), layout.Group);
        Line(indent, nameof(InstrumentDefMsg.Layout.Exchange), layout.Exchange);
        Line(indent, nameof(InstrumentDefMsg.Layout.Asset), layout.Asset);
        Line(indent, nameof(InstrumentDefMsg.Layout.Cfi), layout.Cfi);
        Line(indent, nameof(InstrumentDefMsg.Layout.SecurityType), layout.SecurityType);
        Line(indent, nameof(InstrumentDefMsg.Layout.UnitOfMeasure), layout.UnitOfMeasure);
        Line(indent, nameof(InstrumentDefMsg.Layout.Underlying), layout.Underlying);
        Line(indent, nameof(InstrumentDefMsg.Layout.StrikePriceCurrency), layout.StrikePriceCurrency);
        Line(indent, nameof(InstrumentDefMsg.Layout.InstrumentClass), layout.InstrumentClass);
        Line(indent, nameof(InstrumentDefMsg.Layout.Reserved2), layout.Reserved2);
        Line(indent, nameof(InstrumentDefMsg.Layout.StrikePrice), layout.StrikePrice);
        Line(indent, nameof(InstrumentDefMsg.Layout.Reserved6), layout.Reserved6);
        Line(indent, nameof(InstrumentDefMsg.Layout.MatchAlgorithm), layout.MatchAlgorithm);
        Line(indent, nameof(InstrumentDefMsg.Layout.MdSecurityTradingStatus), layout.MdSecurityTradingStatus);
        Line(indent, nameof(InstrumentDefMsg.Layout.MainFraction), layout.MainFraction);
        Line(indent, nameof(InstrumentDefMsg.Layout.PriceDisplayFormat), layout.PriceDisplayFormat);
        Line(indent, nameof(InstrumentDefMsg.Layout.SettlPriceType), layout.SettlPriceType);
        Line(indent, nameof(InstrumentDefMsg.Layout.SubFraction), layout.SubFraction);
        Line(indent, nameof(InstrumentDefMsg.Layout.UnderlyingProduct), layout.UnderlyingProduct);
        Line(indent, nameof(InstrumentDefMsg.Layout.SecurityUpdateAction), layout.SecurityUpdateAction);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaturityMonth), layout.MaturityMonth);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaturityDay), layout.MaturityDay);
        Line(indent, nameof(InstrumentDefMsg.Layout.MaturityWeek), layout.MaturityWeek);
        Line(indent, nameof(InstrumentDefMsg.Layout.UserDefinedInstrument), layout.UserDefinedInstrument);
        Line(indent, nameof(InstrumentDefMsg.Layout.ContractMultiplierUnit), layout.ContractMultiplierUnit);
        Line(indent, nameof(InstrumentDefMsg.Layout.FlowScheduleType), layout.FlowScheduleType);
        Line(indent, nameof(InstrumentDefMsg.Layout.TickRule), layout.TickRule);
        Line(indent, nameof(InstrumentDefMsg.Layout.Dummy3), layout.Dummy3);
    }

    internal static void Print(in StatusMsg.Layout layout, string indent)
    {
        Line(indent, nameof(StatusMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(StatusMsg.Layout.StatusAction), layout.StatusAction);
        Line(indent, nameof(StatusMsg.Layout.StatusReason), layout.StatusReason);
        Line(indent, nameof(StatusMsg.Layout.TradingEvent), layout.TradingEvent);
        Line(indent, nameof(StatusMsg.Layout.IsTrading), layout.IsTrading);
        Line(indent, nameof(StatusMsg.Layout.IsQuoting), layout.IsQuoting);
        Line(indent, nameof(StatusMsg.Layout.IsShortSellRestricted), layout.IsShortSellRestricted);
        Line(indent, nameof(StatusMsg.Layout.Reserved7), layout.Reserved7);
    }

    internal static void Print(in OhlcvMsg.Layout layout, string indent)
    {
        Line(indent, nameof(OhlcvMsg.Layout.Open), layout.Open);
        Line(indent, nameof(OhlcvMsg.Layout.High), layout.High);
        Line(indent, nameof(OhlcvMsg.Layout.Low), layout.Low);
        Line(indent, nameof(OhlcvMsg.Layout.Close), layout.Close);
        Line(indent, nameof(OhlcvMsg.Layout.Volume), layout.Volume);
    }

    internal static void Print(in Mbp10Msg.Layout layout, string indent)
    {
        Line(indent, nameof(Mbp10Msg.Layout.Price), layout.Price);
        Line(indent, nameof(Mbp10Msg.Layout.Size), layout.Size);
        Line(indent, nameof(Mbp10Msg.Layout.Action), layout.Action);
        Line(indent, nameof(Mbp10Msg.Layout.Side), layout.Side);
        Line(indent, nameof(Mbp10Msg.Layout.Flags), layout.Flags);
        Line(indent, nameof(Mbp10Msg.Layout.Depth), layout.Depth);
        Line(indent, nameof(Mbp10Msg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(Mbp10Msg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(Mbp10Msg.Layout.Sequence), layout.Sequence);
    }

    internal static void Print(in Mbp1Msg.Layout layout, string indent)
    {
        Line(indent, nameof(Mbp1Msg.Layout.Price), layout.Price);
        Line(indent, nameof(Mbp1Msg.Layout.Size), layout.Size);
        Line(indent, nameof(Mbp1Msg.Layout.Action), layout.Action);
        Line(indent, nameof(Mbp1Msg.Layout.Side), layout.Side);
        Line(indent, nameof(Mbp1Msg.Layout.Flags), layout.Flags);
        Line(indent, nameof(Mbp1Msg.Layout.Depth), layout.Depth);
        Line(indent, nameof(Mbp1Msg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(Mbp1Msg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(Mbp1Msg.Layout.Sequence), layout.Sequence);
        Line(indent, nameof(Mbp1Msg.Layout.BidAskPair));
        Print(layout.BidAskPair, indent + Indent);
    }

    internal static void Print(in TradeMsg.Layout layout, string indent)
    {
        Line(indent, nameof(TradeMsg.Layout.Price), layout.Price);
        Line(indent, nameof(TradeMsg.Layout.Size), layout.Size);
        Line(indent, nameof(TradeMsg.Layout.Action), layout.Action);
        Line(indent, nameof(TradeMsg.Layout.Side), layout.Side);
        Line(indent, nameof(TradeMsg.Layout.Flags), layout.Flags);
        Line(indent, nameof(TradeMsg.Layout.Depth), layout.Depth);
        Line(indent, nameof(TradeMsg.Layout.TsRecv), layout.TsRecv);
        Line(indent, nameof(TradeMsg.Layout.TsInDelta), layout.TsInDelta);
        Line(indent, nameof(TradeMsg.Layout.Sequence), layout.Sequence);
    }

    internal static void Print(in RecordHeader.Layout layout, string indent)
    {
        Line(indent, nameof(RecordHeader.Layout.RecordLength), layout.RecordLength);
        Line(indent, nameof(RecordHeader.Layout.Rtype), layout.Rtype);
        Line(indent, nameof(RecordHeader.Layout.PublisherId), layout.PublisherId);
        Line(indent, nameof(RecordHeader.Layout.InstrumentId), layout.InstrumentId);
        Line(indent, nameof(RecordHeader.Layout.TsEvent), layout.TsEvent);
    }

    internal static void Print(in Record.Layout layout, string indent)
    {
        Line(indent, nameof(Record.Layout.RecordHeader));
        Print(layout.RecordHeader, indent + Indent);
    }

    internal static void Print(in MappingInterval.Layout layout, string indent)
    {
        Line(indent, nameof(MappingInterval.Layout.StartDate), layout.StartDate);
        Line(indent, nameof(MappingInterval.Layout.EndDate), layout.EndDate);
        Line(indent, nameof(MappingInterval.Layout.MappedSymbol), layout.MappedSymbol);
    }

    internal static void Print(in SymbolMapping.Layout layout, string indent)
    {
        Line(indent, nameof(SymbolMapping.Layout.RawSymbolIn), layout.RawSymbolIn);
        Line(indent, nameof(SymbolMapping.Layout.IntervalCount), layout.IntervalCount);
    }

    internal static void Print(in Metadata.Layout layout, string indent)
    {
        Line(indent, nameof(Metadata.Layout.Magic), layout.Magic);
        Line(indent, nameof(Metadata.Layout.Version), layout.Version);
        Line(indent, nameof(Metadata.Layout.MetadataLength), layout.MetadataLength);
        Line(indent, nameof(Metadata.Layout.Dataset), layout.Dataset);
        Line(indent, nameof(Metadata.Layout.Schema), layout.Schema);
        Line(indent, nameof(Metadata.Layout.Start), layout.Start);
        Line(indent, nameof(Metadata.Layout.End), layout.End);
        Line(indent, nameof(Metadata.Layout.Limit), layout.Limit);
        Line(indent, nameof(Metadata.Layout.MetadataReserved8), layout.MetadataReserved8);
        Line(indent, nameof(Metadata.Layout.StypeIn), layout.StypeIn);
        Line(indent, nameof(Metadata.Layout.StypeOut), layout.StypeOut);
        Line(indent, nameof(Metadata.Layout.TsOut), layout.TsOut);
        Line(indent, nameof(Metadata.Layout.MetadataReserved), layout.MetadataReserved);
        Line(indent, nameof(Metadata.Layout.SchemaDefinitionLength), layout.SchemaDefinitionLength);
    }

    internal static void Print(in Metadata.SymbolsCountLayout layout, string indent)
    {
        Line(indent, nameof(Metadata.SymbolsCountLayout.SymbolsCount), layout.SymbolsCount);
    }

    internal static void Print(in Metadata.PartialCountLayout layout, string indent)
    {
        Line(indent, nameof(Metadata.PartialCountLayout.PartialCount), layout.PartialCount);
    }

    internal static void Print(in Metadata.NotFoundCountLayout layout, string indent)
    {
        Line(indent, nameof(Metadata.NotFoundCountLayout.NotFoundCount), layout.NotFoundCount);
    }

    internal static void Print(in Metadata.MappingsCountLayout layout, string indent)
    {
        Line(indent, nameof(Metadata.MappingsCountLayout.MappingsCount), layout.MappingsCount);
    }
}
