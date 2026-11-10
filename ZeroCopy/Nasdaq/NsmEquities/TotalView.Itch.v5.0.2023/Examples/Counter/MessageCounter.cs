namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Counter;

/// <summary>
///  Counts the messages, headers, and packets of each reading, and reads none of their fields.
/// </summary>
public struct MessageCounter
{
    const string Separator = ": ";
    const string CaptureBytes = "Capture bytes";
    const string ElapsedTime = "Elapsed time";
    readonly long started;
    long onClientPacketHeaderCount;
    long onDebugPacketCount;
    long onLoginRequestPacketCount;
    long onUnsequencedDataPacketCount;
    long onClientHeartbeatCount;
    long onLogoutRequestCount;
    long onServerPacketHeaderCount;
    long onLoginAcceptedPacketCount;
    long onLoginRejectedPacketCount;
    long onSequencedDataPacketCount;
    long onServerHeartbeatCount;
    long onEndOfSessionCount;
    long onSystemEventMessageCount;
    long onStockDirectoryMessageCount;
    long onStockTradingActionMessageCount;
    long onRegShoShortSalePriceTestRestrictedIndicatorMessageCount;
    long onMarketParticipantPositionMessageCount;
    long onMwcbDeclineLevelMessageCount;
    long onMwcbStatusLevelMessageCount;
    long onIpoQuotingPeriodUpdateCount;
    long onLuldAuctionCollarMessageCount;
    long onOperationalHaltMessageCount;
    long onAddOrderNoMpidAttributionMessageCount;
    long onAddOrderWithMpidAttributionMessageCount;
    long onOrderExecutedMessageCount;
    long onOrderExecutedWithPriceMessageCount;
    long onOrderCancelMessageCount;
    long onOrderDeleteMessageCount;
    long onOrderReplaceMessageCount;
    long onNonCrossTradeMessageCount;
    long onCrossTradeMessageCount;
    long onBrokenTradeMessageCount;
    long onNetOrderImbalanceIndicatorMessageCount;
    long onRetailPriceImprovementIndicatorMessageCount;
    long onDirectListingWithCapitalRaisePriceDiscoveryMessageCount;
    long onPacketHeaderCount;
    long onHeartbeatCount;
    long onClientPacketCount;
    long onServerPacketCount;
    long onPacketCount;
    long onFileCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket.Reader clientPacket)
    {
        var clientSoupBinTcpPacketEntries = clientPacket.ClientSoupBinTcpPacket;
        while (clientSoupBinTcpPacketEntries.MoveNext())
        {
            var clientSoupBinTcpPacket = clientSoupBinTcpPacketEntries.Current;
            this.OnClientPacketHeader(in clientSoupBinTcpPacket.ClientPacketHeader);
            var clientPayload = clientSoupBinTcpPacket.ClientPayload;
            switch (clientPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(clientPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LoginRequestPacket:
                {
                    this.OnLoginRequestPacket(in clientPayload.LoginRequestPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.UnsequencedDataPacket:
                {
                    this.OnUnsequencedDataPacket(clientPayload.UnsequencedDataPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.ClientHeartbeat:
                {
                    this.OnClientHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPayload.Case.LogoutRequest:
                {
                    this.OnLogoutRequest();
                    break;
                }
            }
        }
        this.OnClientPacket(clientPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket.Reader serverPacket)
    {
        var serverSoupBinTcpPacketEntries = serverPacket.ServerSoupBinTcpPacket;
        while (serverSoupBinTcpPacketEntries.MoveNext())
        {
            var serverSoupBinTcpPacket = serverSoupBinTcpPacketEntries.Current;
            this.OnServerPacketHeader(in serverSoupBinTcpPacket.ServerPacketHeader);
            var serverPayload = serverSoupBinTcpPacket.ServerPayload;
            switch (serverPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(serverPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginAcceptedPacket:
                {
                    this.OnLoginAcceptedPacket(in serverPayload.LoginAcceptedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.LoginRejectedPacket:
                {
                    this.OnLoginRejectedPacket(in serverPayload.LoginRejectedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.SequencedDataPacket:
                {
                    var sequencedDataPacket = serverPayload.SequencedDataPacket;
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in sequencedMessage.SystemEventMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in sequencedMessage.StockDirectoryMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in sequencedMessage.StockTradingActionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in sequencedMessage.RegShoShortSalePriceTestRestrictedIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in sequencedMessage.MarketParticipantPositionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in sequencedMessage.MwcbDeclineLevelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in sequencedMessage.MwcbStatusLevelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in sequencedMessage.IpoQuotingPeriodUpdate, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in sequencedMessage.LuldAuctionCollarMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in sequencedMessage.OperationalHaltMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in sequencedMessage.AddOrderNoMpidAttributionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in sequencedMessage.AddOrderWithMpidAttributionMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in sequencedMessage.OrderExecutedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in sequencedMessage.OrderExecutedWithPriceMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in sequencedMessage.OrderCancelMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in sequencedMessage.OrderDeleteMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in sequencedMessage.OrderReplaceMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in sequencedMessage.NonCrossTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in sequencedMessage.CrossTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in sequencedMessage.BrokenTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in sequencedMessage.NetOrderImbalanceIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in sequencedMessage.RetailPriceImprovementIndicatorMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
                        {
                            this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in sequencedMessage.DirectListingWithCapitalRaisePriceDiscoveryMessage, null);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, null);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.ServerHeartbeat:
                {
                    this.OnServerHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPayload.Case.EndOfSession:
                {
                    this.VisitEndOfSession();
                    break;
                }
            }
        }
        this.OnServerPacket(serverPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.EndOfSession:
            {
                this.VisitEndOfSession();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in payload.MwcbStatusLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in payload.IpoQuotingPeriodUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in payload.AddOrderWithMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in payload.NonCrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in payload.RetailPriceImprovementIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Payload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
                        {
                            this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in payload.DirectListingWithCapitalRaisePriceDiscoveryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                    }
                }
                break;
            }
        }
        this.OnPacket(packet);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.File.Reader file)
    {
        var fileMessageEntries = file.FileMessage;
        while (fileMessageEntries.MoveNext())
        {
            var fileMessage = fileMessageEntries.Current;
            this.VisitFileMessage(fileMessage);
        }
        this.OnFile(file);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader debugPacket)
    {
        this.OnDebugPacket(debugPacket);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitEndOfSession()
    {
        this.OnEndOfSession();
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout systemEventMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout stockDirectoryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout stockTradingActionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, global::System.UInt128? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout operationalHaltMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout orderExecutedMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout orderCancelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout orderDeleteMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout orderReplaceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout nonCrossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout crossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout brokenTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout systemEventMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout stockDirectoryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout stockTradingActionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout operationalHaltMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout orderExecutedMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout orderCancelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout orderDeleteMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout orderReplaceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout nonCrossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout crossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout brokenTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFileMessage(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FileMessage.Reader fileMessage)
    {
        var filePayload = fileMessage.FilePayload;
        switch (filePayload.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.SystemEventMessage:
            {
                this.VisitSystemEventMessage(in filePayload.SystemEventMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockDirectoryMessage:
            {
                this.VisitStockDirectoryMessage(in filePayload.StockDirectoryMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.StockTradingActionMessage:
            {
                this.VisitStockTradingActionMessage(in filePayload.StockTradingActionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
            {
                this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in filePayload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MarketParticipantPositionMessage:
            {
                this.VisitMarketParticipantPositionMessage(in filePayload.MarketParticipantPositionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbDeclineLevelMessage:
            {
                this.VisitMwcbDeclineLevelMessage(in filePayload.MwcbDeclineLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.MwcbStatusLevelMessage:
            {
                this.VisitMwcbStatusLevelMessage(in filePayload.MwcbStatusLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.IpoQuotingPeriodUpdate:
            {
                this.VisitIpoQuotingPeriodUpdate(in filePayload.IpoQuotingPeriodUpdate, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.LuldAuctionCollarMessage:
            {
                this.VisitLuldAuctionCollarMessage(in filePayload.LuldAuctionCollarMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OperationalHaltMessage:
            {
                this.VisitOperationalHaltMessage(in filePayload.OperationalHaltMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderNoMpidAttributionMessage:
            {
                this.VisitAddOrderNoMpidAttributionMessage(in filePayload.AddOrderNoMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.AddOrderWithMpidAttributionMessage:
            {
                this.VisitAddOrderWithMpidAttributionMessage(in filePayload.AddOrderWithMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedMessage:
            {
                this.VisitOrderExecutedMessage(in filePayload.OrderExecutedMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderExecutedWithPriceMessage:
            {
                this.VisitOrderExecutedWithPriceMessage(in filePayload.OrderExecutedWithPriceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderCancelMessage:
            {
                this.VisitOrderCancelMessage(in filePayload.OrderCancelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderDeleteMessage:
            {
                this.VisitOrderDeleteMessage(in filePayload.OrderDeleteMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.OrderReplaceMessage:
            {
                this.VisitOrderReplaceMessage(in filePayload.OrderReplaceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NonCrossTradeMessage:
            {
                this.VisitNonCrossTradeMessage(in filePayload.NonCrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.CrossTradeMessage:
            {
                this.VisitCrossTradeMessage(in filePayload.CrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.BrokenTradeMessage:
            {
                this.VisitBrokenTradeMessage(in filePayload.BrokenTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.NetOrderImbalanceIndicatorMessage:
            {
                this.VisitNetOrderImbalanceIndicatorMessage(in filePayload.NetOrderImbalanceIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.RetailPriceImprovementIndicatorMessage:
            {
                this.VisitRetailPriceImprovementIndicatorMessage(in filePayload.RetailPriceImprovementIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.FilePayload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
            {
                this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in filePayload.DirectListingWithCapitalRaisePriceDiscoveryMessage, in fileMessage.MessageHeader, null);
                break;
            }
        }
    }

    /// <summary>
    ///  Called with each Client Packet Header of a reading.
    /// </summary>
    public void OnClientPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacketHeader.Layout value) => this.onClientPacketHeaderCount++;

    /// <summary>
    ///  Called with each Debug Packet of a reading.
    /// </summary>
    public void OnDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DebugPacket.Reader value) => this.onDebugPacketCount++;

    /// <summary>
    ///  Called with each Login Request Packet of a reading.
    /// </summary>
    public void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRequestPacket.Layout value) => this.onLoginRequestPacketCount++;

    /// <summary>
    ///  Called with each Unsequenced Data Packet of a reading.
    /// </summary>
    public void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.UnsequencedDataPacket.Reader value) => this.onUnsequencedDataPacketCount++;

    /// <summary>
    ///  Called with each Client Heartbeat of a reading.
    /// </summary>
    public void OnClientHeartbeat() => this.onClientHeartbeatCount++;

    /// <summary>
    ///  Called with each Logout Request of a reading.
    /// </summary>
    public void OnLogoutRequest() => this.onLogoutRequestCount++;

    /// <summary>
    ///  Called with each Server Packet Header of a reading.
    /// </summary>
    public void OnServerPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacketHeader.Layout value) => this.onServerPacketHeaderCount++;

    /// <summary>
    ///  Called with each Login Accepted Packet of a reading.
    /// </summary>
    public void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginAcceptedPacket.Layout value) => this.onLoginAcceptedPacketCount++;

    /// <summary>
    ///  Called with each Login Rejected Packet of a reading.
    /// </summary>
    public void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LoginRejectedPacket.Layout value) => this.onLoginRejectedPacketCount++;

    /// <summary>
    ///  Called with each Sequenced Data Packet of a reading.
    /// </summary>
    public void OnSequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedDataPacket.Reader value, global::System.UInt128? sequenceNumber) => this.onSequencedDataPacketCount++;

    /// <summary>
    ///  Called with each Server Heartbeat of a reading.
    /// </summary>
    public void OnServerHeartbeat() => this.onServerHeartbeatCount++;

    /// <summary>
    ///  Called with each End Of Session of a reading.
    /// </summary>
    public void OnEndOfSession() => this.onEndOfSessionCount++;

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    public void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    public void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onStockDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    public void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onStockTradingActionMessageCount++;

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    public void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onRegShoShortSalePriceTestRestrictedIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    public void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onMarketParticipantPositionMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    public void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onMwcbDeclineLevelMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Status Level Message of a reading.
    /// </summary>
    public void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onMwcbStatusLevelMessageCount++;

    /// <summary>
    ///  Called with each Ipo Quoting Period Update of a reading.
    /// </summary>
    public void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout value, global::System.UInt128? sequenceNumber) => this.onIpoQuotingPeriodUpdateCount++;

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    public void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onLuldAuctionCollarMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    public void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOperationalHaltMessageCount++;

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onAddOrderNoMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Add Order With Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onAddOrderWithMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    public void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOrderExecutedMessageCount++;

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    public void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOrderExecutedWithPriceMessageCount++;

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    public void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOrderCancelMessageCount++;

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    public void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOrderDeleteMessageCount++;

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    public void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onOrderReplaceMessageCount++;

    /// <summary>
    ///  Called with each Non Cross Trade Message of a reading.
    /// </summary>
    public void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onNonCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    public void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    public void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onBrokenTradeMessageCount++;

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    public void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onNetOrderImbalanceIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Retail Price Improvement Indicator Message of a reading.
    /// </summary>
    public void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onRetailPriceImprovementIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Direct Listing With Capital Raise Price Discovery Message of a reading.
    /// </summary>
    public void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onDirectListingWithCapitalRaisePriceDiscoveryMessageCount++;

    /// <summary>
    ///  Called with each Packet Header of a reading.
    /// </summary>
    public void OnPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.PacketHeader.Layout value) => this.onPacketHeaderCount++;

    /// <summary>
    ///  Called with each Heartbeat of a reading.
    /// </summary>
    public void OnHeartbeat() => this.onHeartbeatCount++;

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    public void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    public void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onStockDirectoryMessageCount++;

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    public void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onStockTradingActionMessageCount++;

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    public void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onRegShoShortSalePriceTestRestrictedIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    public void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onMarketParticipantPositionMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    public void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onMwcbDeclineLevelMessageCount++;

    /// <summary>
    ///  Called with each Mwcb Status Level Message of a reading.
    /// </summary>
    public void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onMwcbStatusLevelMessageCount++;

    /// <summary>
    ///  Called with each Ipo Quoting Period Update of a reading.
    /// </summary>
    public void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onIpoQuotingPeriodUpdateCount++;

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    public void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onLuldAuctionCollarMessageCount++;

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    public void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOperationalHaltMessageCount++;

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onAddOrderNoMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Add Order With Mpid Attribution Message of a reading.
    /// </summary>
    public void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onAddOrderWithMpidAttributionMessageCount++;

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    public void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOrderExecutedMessageCount++;

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    public void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOrderExecutedWithPriceMessageCount++;

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    public void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOrderCancelMessageCount++;

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    public void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOrderDeleteMessageCount++;

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    public void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onOrderReplaceMessageCount++;

    /// <summary>
    ///  Called with each Non Cross Trade Message of a reading.
    /// </summary>
    public void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onNonCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    public void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onCrossTradeMessageCount++;

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    public void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onBrokenTradeMessageCount++;

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    public void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onNetOrderImbalanceIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Retail Price Improvement Indicator Message of a reading.
    /// </summary>
    public void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onRetailPriceImprovementIndicatorMessageCount++;

    /// <summary>
    ///  Called with each Direct Listing With Capital Raise Price Discovery Message of a reading.
    /// </summary>
    public void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout value, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MessageHeader.Layout header, ulong? sequenceNumber) => this.onDirectListingWithCapitalRaisePriceDiscoveryMessageCount++;

    /// <summary>
    ///  Called with each Client Packet of a reading.
    /// </summary>
    public void OnClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ClientPacket.Reader value) => this.onClientPacketCount++;

    /// <summary>
    ///  Called with each Server Packet of a reading.
    /// </summary>
    public void OnServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.ServerPacket.Reader value) => this.onServerPacketCount++;

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    public void OnPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Packet.Reader value) => this.onPacketCount++;

    /// <summary>
    ///  Called with each File of a reading.
    /// </summary>
    public void OnFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.File.Reader value) => this.onFileCount++;

    /// <summary>
    ///  Writes the counts of messages, headers and packets, the capture file size and the elapsed time to the console.
    /// </summary>
    /// <param name="captureBytes">
    ///  The number of bytes of the capture file.
    /// </param>
    public readonly void WriteReport(long captureBytes)
    {
        var elapsed = global::System.Diagnostics.Stopwatch.GetElapsedTime(this.started);
        Console.WriteLine($"{nameof(OnClientPacketHeader)}{Separator}{this.onClientPacketHeaderCount}");
        Console.WriteLine($"{nameof(OnDebugPacket)}{Separator}{this.onDebugPacketCount}");
        Console.WriteLine($"{nameof(OnLoginRequestPacket)}{Separator}{this.onLoginRequestPacketCount}");
        Console.WriteLine($"{nameof(OnUnsequencedDataPacket)}{Separator}{this.onUnsequencedDataPacketCount}");
        Console.WriteLine($"{nameof(OnClientHeartbeat)}{Separator}{this.onClientHeartbeatCount}");
        Console.WriteLine($"{nameof(OnLogoutRequest)}{Separator}{this.onLogoutRequestCount}");
        Console.WriteLine($"{nameof(OnServerPacketHeader)}{Separator}{this.onServerPacketHeaderCount}");
        Console.WriteLine($"{nameof(OnLoginAcceptedPacket)}{Separator}{this.onLoginAcceptedPacketCount}");
        Console.WriteLine($"{nameof(OnLoginRejectedPacket)}{Separator}{this.onLoginRejectedPacketCount}");
        Console.WriteLine($"{nameof(OnSequencedDataPacket)}{Separator}{this.onSequencedDataPacketCount}");
        Console.WriteLine($"{nameof(OnServerHeartbeat)}{Separator}{this.onServerHeartbeatCount}");
        Console.WriteLine($"{nameof(OnEndOfSession)}{Separator}{this.onEndOfSessionCount}");
        Console.WriteLine($"{nameof(OnSystemEventMessage)}{Separator}{this.onSystemEventMessageCount}");
        Console.WriteLine($"{nameof(OnStockDirectoryMessage)}{Separator}{this.onStockDirectoryMessageCount}");
        Console.WriteLine($"{nameof(OnStockTradingActionMessage)}{Separator}{this.onStockTradingActionMessageCount}");
        Console.WriteLine($"{nameof(OnRegShoShortSalePriceTestRestrictedIndicatorMessage)}{Separator}{this.onRegShoShortSalePriceTestRestrictedIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnMarketParticipantPositionMessage)}{Separator}{this.onMarketParticipantPositionMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbDeclineLevelMessage)}{Separator}{this.onMwcbDeclineLevelMessageCount}");
        Console.WriteLine($"{nameof(OnMwcbStatusLevelMessage)}{Separator}{this.onMwcbStatusLevelMessageCount}");
        Console.WriteLine($"{nameof(OnIpoQuotingPeriodUpdate)}{Separator}{this.onIpoQuotingPeriodUpdateCount}");
        Console.WriteLine($"{nameof(OnLuldAuctionCollarMessage)}{Separator}{this.onLuldAuctionCollarMessageCount}");
        Console.WriteLine($"{nameof(OnOperationalHaltMessage)}{Separator}{this.onOperationalHaltMessageCount}");
        Console.WriteLine($"{nameof(OnAddOrderNoMpidAttributionMessage)}{Separator}{this.onAddOrderNoMpidAttributionMessageCount}");
        Console.WriteLine($"{nameof(OnAddOrderWithMpidAttributionMessage)}{Separator}{this.onAddOrderWithMpidAttributionMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedMessage)}{Separator}{this.onOrderExecutedMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedWithPriceMessage)}{Separator}{this.onOrderExecutedWithPriceMessageCount}");
        Console.WriteLine($"{nameof(OnOrderCancelMessage)}{Separator}{this.onOrderCancelMessageCount}");
        Console.WriteLine($"{nameof(OnOrderDeleteMessage)}{Separator}{this.onOrderDeleteMessageCount}");
        Console.WriteLine($"{nameof(OnOrderReplaceMessage)}{Separator}{this.onOrderReplaceMessageCount}");
        Console.WriteLine($"{nameof(OnNonCrossTradeMessage)}{Separator}{this.onNonCrossTradeMessageCount}");
        Console.WriteLine($"{nameof(OnCrossTradeMessage)}{Separator}{this.onCrossTradeMessageCount}");
        Console.WriteLine($"{nameof(OnBrokenTradeMessage)}{Separator}{this.onBrokenTradeMessageCount}");
        Console.WriteLine($"{nameof(OnNetOrderImbalanceIndicatorMessage)}{Separator}{this.onNetOrderImbalanceIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnRetailPriceImprovementIndicatorMessage)}{Separator}{this.onRetailPriceImprovementIndicatorMessageCount}");
        Console.WriteLine($"{nameof(OnDirectListingWithCapitalRaisePriceDiscoveryMessage)}{Separator}{this.onDirectListingWithCapitalRaisePriceDiscoveryMessageCount}");
        Console.WriteLine($"{nameof(OnPacketHeader)}{Separator}{this.onPacketHeaderCount}");
        Console.WriteLine($"{nameof(OnHeartbeat)}{Separator}{this.onHeartbeatCount}");
        Console.WriteLine($"{nameof(OnClientPacket)}{Separator}{this.onClientPacketCount}");
        Console.WriteLine($"{nameof(OnServerPacket)}{Separator}{this.onServerPacketCount}");
        Console.WriteLine($"{nameof(OnPacket)}{Separator}{this.onPacketCount}");
        Console.WriteLine($"{nameof(OnFile)}{Separator}{this.onFileCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
