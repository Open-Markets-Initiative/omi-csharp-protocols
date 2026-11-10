namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Manager;

/// <summary>
///  Passes each message, header, and packet of a reading to the method of the same name.
/// </summary>
public sealed partial class MessageHandler
{
    /// <summary>
    ///  Gets or sets the values of the conversation that a later packet reads.
    /// </summary>
    public ConversationState ConversationState { get; set; } = new();

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacket.Reader clientPacket)
    {
        var clientSoupBinTcpPacketEntries = clientPacket.ClientSoupBinTcpPacket;
        while (clientSoupBinTcpPacketEntries.MoveNext())
        {
            var clientSoupBinTcpPacket = clientSoupBinTcpPacketEntries.Current;
            this.OnClientPacketHeader(in clientSoupBinTcpPacket.ClientPacketHeader);
            var clientPayload = clientSoupBinTcpPacket.ClientPayload;
            switch (clientPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(clientPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Case.LoginRequestPacket:
                {
                    this.OnLoginRequestPacket(in clientPayload.LoginRequestPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Case.UnsequencedDataPacket:
                {
                    this.OnUnsequencedDataPacket(clientPayload.UnsequencedDataPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Case.ClientHeartbeatPacket:
                {
                    this.OnClientHeartbeatPacket();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPayload.Case.LogoutRequestPacket:
                {
                    this.OnLogoutRequestPacket();
                    break;
                }
            }
        }
        this.OnClientPacket(clientPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPacket.Reader serverPacket)
    {
        var serverSoupBinTcpPacketEntries = serverPacket.ServerSoupBinTcpPacket;
        while (serverSoupBinTcpPacketEntries.MoveNext())
        {
            var serverSoupBinTcpPacket = serverSoupBinTcpPacketEntries.Current;
            this.OnServerPacketHeader(in serverSoupBinTcpPacket.ServerPacketHeader);
            var serverPayload = serverSoupBinTcpPacket.ServerPayload;
            switch (serverPayload.Case)
            {
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(serverPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.LoginAcceptedPacket:
                {
                    this.ConversationState.Apply(in serverPayload.LoginAcceptedPacket);
                    this.OnLoginAcceptedPacket(in serverPayload.LoginAcceptedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.LoginRejectedPacket:
                {
                    this.OnLoginRejectedPacket(in serverPayload.LoginRejectedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.SequencedDataPacket:
                {
                    var sequencedDataPacket = serverPayload.SequencedDataPacket;
                    this.ConversationState.CountSequencedDataPacket();
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in sequencedMessage.SystemEventMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in sequencedMessage.StockDirectoryMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in sequencedMessage.StockTradingActionMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in sequencedMessage.RegShoShortSalePriceTestRestrictedIndicatorMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in sequencedMessage.MarketParticipantPositionMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in sequencedMessage.MwcbDeclineLevelMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in sequencedMessage.MwcbStatusLevelMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in sequencedMessage.IpoQuotingPeriodUpdate, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in sequencedMessage.LuldAuctionCollarMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in sequencedMessage.OperationalHaltMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in sequencedMessage.AddOrderNoMpidAttributionMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in sequencedMessage.AddOrderWithMpidAttributionMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in sequencedMessage.OrderExecutedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in sequencedMessage.OrderExecutedWithPriceMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in sequencedMessage.OrderCancelMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in sequencedMessage.OrderDeleteMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in sequencedMessage.OrderReplaceMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in sequencedMessage.NonCrossTradeMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in sequencedMessage.CrossTradeMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in sequencedMessage.BrokenTradeMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in sequencedMessage.NetOrderImbalanceIndicatorMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in sequencedMessage.RetailPriceImprovementIndicatorMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
                        {
                            this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in sequencedMessage.DirectListingWithCapitalRaisePriceDiscoveryMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, this.ConversationState.SequencedDataPacketNumber);
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.ServerHeartbeatPacket:
                {
                    this.OnServerHeartbeatPacket();
                    break;
                }
                case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPayload.Case.EndOfSessionPacket:
                {
                    this.OnEndOfSessionPacket();
                    break;
                }
            }
        }
        this.OnServerPacket(serverPacket);
    }

    /// <summary>
    ///  Visits the packet and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Packet.Reader packet)
    {
        this.OnPacketHeader(in packet.PacketHeader);
        var messages = packet.Messages;
        switch (messages.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Messages.Case.Heartbeat:
            {
                this.OnHeartbeat();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Messages.Case.EndOfSession:
            {
                this.OnEndOfSession();
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Messages.Case.Message:
            {
                var messageEntries = messages.Message;
                while (messageEntries.MoveNext())
                {
                    var message = messageEntries.Current;
                    var payload = message.Payload;
                    switch (payload.Case)
                    {
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.SystemEventMessage:
                        {
                            this.VisitSystemEventMessage(in payload.SystemEventMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.StockDirectoryMessage:
                        {
                            this.VisitStockDirectoryMessage(in payload.StockDirectoryMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.StockTradingActionMessage:
                        {
                            this.VisitStockTradingActionMessage(in payload.StockTradingActionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
                        {
                            this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in payload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.MarketParticipantPositionMessage:
                        {
                            this.VisitMarketParticipantPositionMessage(in payload.MarketParticipantPositionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.MwcbDeclineLevelMessage:
                        {
                            this.VisitMwcbDeclineLevelMessage(in payload.MwcbDeclineLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.MwcbStatusLevelMessage:
                        {
                            this.VisitMwcbStatusLevelMessage(in payload.MwcbStatusLevelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.IpoQuotingPeriodUpdate:
                        {
                            this.VisitIpoQuotingPeriodUpdate(in payload.IpoQuotingPeriodUpdate, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.LuldAuctionCollarMessage:
                        {
                            this.VisitLuldAuctionCollarMessage(in payload.LuldAuctionCollarMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OperationalHaltMessage:
                        {
                            this.VisitOperationalHaltMessage(in payload.OperationalHaltMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.AddOrderNoMpidAttributionMessage:
                        {
                            this.VisitAddOrderNoMpidAttributionMessage(in payload.AddOrderNoMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.AddOrderWithMpidAttributionMessage:
                        {
                            this.VisitAddOrderWithMpidAttributionMessage(in payload.AddOrderWithMpidAttributionMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OrderExecutedMessage:
                        {
                            this.VisitOrderExecutedMessage(in payload.OrderExecutedMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OrderExecutedWithPriceMessage:
                        {
                            this.VisitOrderExecutedWithPriceMessage(in payload.OrderExecutedWithPriceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OrderCancelMessage:
                        {
                            this.VisitOrderCancelMessage(in payload.OrderCancelMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OrderDeleteMessage:
                        {
                            this.VisitOrderDeleteMessage(in payload.OrderDeleteMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.OrderReplaceMessage:
                        {
                            this.VisitOrderReplaceMessage(in payload.OrderReplaceMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.NonCrossTradeMessage:
                        {
                            this.VisitNonCrossTradeMessage(in payload.NonCrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.CrossTradeMessage:
                        {
                            this.VisitCrossTradeMessage(in payload.CrossTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.BrokenTradeMessage:
                        {
                            this.VisitBrokenTradeMessage(in payload.BrokenTradeMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.NetOrderImbalanceIndicatorMessage:
                        {
                            this.VisitNetOrderImbalanceIndicatorMessage(in payload.NetOrderImbalanceIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.RetailPriceImprovementIndicatorMessage:
                        {
                            this.VisitRetailPriceImprovementIndicatorMessage(in payload.RetailPriceImprovementIndicatorMessage, in message.MessageHeader, message.SequenceNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Payload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
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
    public void VisitFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.File.Reader file)
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
    public void VisitDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DebugPacket.Reader debugPacket)
    {
        this.OnDebugPacket(debugPacket);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SystemEventMessage.Layout systemEventMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockDirectoryMessage.Layout stockDirectoryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockTradingActionMessage.Layout stockTradingActionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, global::System.UInt128? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OperationalHaltMessage.Layout operationalHaltMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedMessage.Layout orderExecutedMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderCancelMessage.Layout orderCancelMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderDeleteMessage.Layout orderDeleteMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderReplaceMessage.Layout orderReplaceMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NonCrossTradeMessage.Layout nonCrossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.CrossTradeMessage.Layout crossTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.BrokenTradeMessage.Layout brokenTradeMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, global::System.UInt128? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SystemEventMessage.Layout systemEventMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnSystemEventMessage(in systemEventMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockDirectoryMessage.Layout stockDirectoryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockDirectoryMessage(in stockDirectoryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockTradingActionMessage.Layout stockTradingActionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnStockTradingActionMessage(in stockTradingActionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout regShoShortSalePriceTestRestrictedIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in regShoShortSalePriceTestRestrictedIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketParticipantPositionMessage.Layout marketParticipantPositionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMarketParticipantPositionMessage(in marketParticipantPositionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbDeclineLevelMessage.Layout mwcbDeclineLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbDeclineLevelMessage(in mwcbDeclineLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbStatusLevelMessage.Layout mwcbStatusLevelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnMwcbStatusLevelMessage(in mwcbStatusLevelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotingPeriodUpdate.Layout ipoQuotingPeriodUpdate, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnIpoQuotingPeriodUpdate(in ipoQuotingPeriodUpdate, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldAuctionCollarMessage.Layout luldAuctionCollarMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnLuldAuctionCollarMessage(in luldAuctionCollarMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OperationalHaltMessage.Layout operationalHaltMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOperationalHaltMessage(in operationalHaltMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderNoMpidAttributionMessage.Layout addOrderNoMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderNoMpidAttributionMessage(in addOrderNoMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderWithMpidAttributionMessage.Layout addOrderWithMpidAttributionMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnAddOrderWithMpidAttributionMessage(in addOrderWithMpidAttributionMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedMessage.Layout orderExecutedMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedMessage(in orderExecutedMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedWithPriceMessage.Layout orderExecutedWithPriceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderExecutedWithPriceMessage(in orderExecutedWithPriceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderCancelMessage.Layout orderCancelMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderCancelMessage(in orderCancelMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderDeleteMessage.Layout orderDeleteMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderDeleteMessage(in orderDeleteMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderReplaceMessage.Layout orderReplaceMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnOrderReplaceMessage(in orderReplaceMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NonCrossTradeMessage.Layout nonCrossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNonCrossTradeMessage(in nonCrossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.CrossTradeMessage.Layout crossTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnCrossTradeMessage(in crossTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.BrokenTradeMessage.Layout brokenTradeMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnBrokenTradeMessage(in brokenTradeMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NetOrderImbalanceIndicatorMessage.Layout netOrderImbalanceIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnNetOrderImbalanceIndicatorMessage(in netOrderImbalanceIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RetailPriceImprovementIndicatorMessage.Layout retailPriceImprovementIndicatorMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnRetailPriceImprovementIndicatorMessage(in retailPriceImprovementIndicatorMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout directListingWithCapitalRaisePriceDiscoveryMessage, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout messageHeader, ulong? sequenceNumber)
    {
        this.OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in directListingWithCapitalRaisePriceDiscoveryMessage, in messageHeader, sequenceNumber);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitFileMessage(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FileMessage.Reader fileMessage)
    {
        var filePayload = fileMessage.FilePayload;
        switch (filePayload.Case)
        {
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.SystemEventMessage:
            {
                this.VisitSystemEventMessage(in filePayload.SystemEventMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.StockDirectoryMessage:
            {
                this.VisitStockDirectoryMessage(in filePayload.StockDirectoryMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.StockTradingActionMessage:
            {
                this.VisitStockTradingActionMessage(in filePayload.StockTradingActionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage:
            {
                this.VisitRegShoShortSalePriceTestRestrictedIndicatorMessage(in filePayload.RegShoShortSalePriceTestRestrictedIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.MarketParticipantPositionMessage:
            {
                this.VisitMarketParticipantPositionMessage(in filePayload.MarketParticipantPositionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.MwcbDeclineLevelMessage:
            {
                this.VisitMwcbDeclineLevelMessage(in filePayload.MwcbDeclineLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.MwcbStatusLevelMessage:
            {
                this.VisitMwcbStatusLevelMessage(in filePayload.MwcbStatusLevelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.IpoQuotingPeriodUpdate:
            {
                this.VisitIpoQuotingPeriodUpdate(in filePayload.IpoQuotingPeriodUpdate, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.LuldAuctionCollarMessage:
            {
                this.VisitLuldAuctionCollarMessage(in filePayload.LuldAuctionCollarMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OperationalHaltMessage:
            {
                this.VisitOperationalHaltMessage(in filePayload.OperationalHaltMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.AddOrderNoMpidAttributionMessage:
            {
                this.VisitAddOrderNoMpidAttributionMessage(in filePayload.AddOrderNoMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.AddOrderWithMpidAttributionMessage:
            {
                this.VisitAddOrderWithMpidAttributionMessage(in filePayload.AddOrderWithMpidAttributionMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OrderExecutedMessage:
            {
                this.VisitOrderExecutedMessage(in filePayload.OrderExecutedMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OrderExecutedWithPriceMessage:
            {
                this.VisitOrderExecutedWithPriceMessage(in filePayload.OrderExecutedWithPriceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OrderCancelMessage:
            {
                this.VisitOrderCancelMessage(in filePayload.OrderCancelMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OrderDeleteMessage:
            {
                this.VisitOrderDeleteMessage(in filePayload.OrderDeleteMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.OrderReplaceMessage:
            {
                this.VisitOrderReplaceMessage(in filePayload.OrderReplaceMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.NonCrossTradeMessage:
            {
                this.VisitNonCrossTradeMessage(in filePayload.NonCrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.CrossTradeMessage:
            {
                this.VisitCrossTradeMessage(in filePayload.CrossTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.BrokenTradeMessage:
            {
                this.VisitBrokenTradeMessage(in filePayload.BrokenTradeMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.NetOrderImbalanceIndicatorMessage:
            {
                this.VisitNetOrderImbalanceIndicatorMessage(in filePayload.NetOrderImbalanceIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.RetailPriceImprovementIndicatorMessage:
            {
                this.VisitRetailPriceImprovementIndicatorMessage(in filePayload.RetailPriceImprovementIndicatorMessage, in fileMessage.MessageHeader, null);
                break;
            }
            case global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.FilePayload.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage:
            {
                this.VisitDirectListingWithCapitalRaisePriceDiscoveryMessage(in filePayload.DirectListingWithCapitalRaisePriceDiscoveryMessage, in fileMessage.MessageHeader, null);
                break;
            }
        }
    }

    /// <summary>
    ///  Called with each Client Packet Header of a reading.
    /// </summary>
    partial void OnClientPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacketHeader.Layout value);

    /// <summary>
    ///  Called with each Debug Packet of a reading.
    /// </summary>
    partial void OnDebugPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DebugPacket.Reader message);

    /// <summary>
    ///  Called with each Login Request Packet of a reading.
    /// </summary>
    partial void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LoginRequestPacket.Layout message);

    /// <summary>
    ///  Called with each Unsequenced Data Packet of a reading.
    /// </summary>
    partial void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.UnsequencedDataPacket.Reader message);

    /// <summary>
    ///  Called with each Client Heartbeat Packet of a reading.
    /// </summary>
    partial void OnClientHeartbeatPacket();

    /// <summary>
    ///  Called with each Logout Request Packet of a reading.
    /// </summary>
    partial void OnLogoutRequestPacket();

    /// <summary>
    ///  Called with each Server Packet Header of a reading.
    /// </summary>
    partial void OnServerPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPacketHeader.Layout value);

    /// <summary>
    ///  Called with each Login Accepted Packet of a reading.
    /// </summary>
    partial void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LoginAcceptedPacket.Layout message);

    /// <summary>
    ///  Called with each Login Rejected Packet of a reading.
    /// </summary>
    partial void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LoginRejectedPacket.Layout message);

    /// <summary>
    ///  Called with each Sequenced Data Packet of a reading.
    /// </summary>
    partial void OnSequencedDataPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SequencedDataPacket.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Server Heartbeat Packet of a reading.
    /// </summary>
    partial void OnServerHeartbeatPacket();

    /// <summary>
    ///  Called with each End Of Session Packet of a reading.
    /// </summary>
    partial void OnEndOfSessionPacket();

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SystemEventMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    partial void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockDirectoryMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    partial void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockTradingActionMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    partial void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    partial void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketParticipantPositionMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    partial void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbDeclineLevelMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Status Level Message of a reading.
    /// </summary>
    partial void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbStatusLevelMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Ipo Quoting Period Update of a reading.
    /// </summary>
    partial void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotingPeriodUpdate.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    partial void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldAuctionCollarMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    partial void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OperationalHaltMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderNoMpidAttributionMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Add Order With Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderWithMpidAttributionMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    partial void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    partial void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedWithPriceMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    partial void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderCancelMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    partial void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderDeleteMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    partial void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderReplaceMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Non Cross Trade Message of a reading.
    /// </summary>
    partial void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NonCrossTradeMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    partial void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.CrossTradeMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    partial void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.BrokenTradeMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    partial void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NetOrderImbalanceIndicatorMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Retail Price Improvement Indicator Message of a reading.
    /// </summary>
    partial void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RetailPriceImprovementIndicatorMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Direct Listing With Capital Raise Price Discovery Message of a reading.
    /// </summary>
    partial void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Packet Header of a reading.
    /// </summary>
    partial void OnPacketHeader(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.PacketHeader.Layout value);

    /// <summary>
    ///  Called with each Heartbeat of a reading.
    /// </summary>
    partial void OnHeartbeat();

    /// <summary>
    ///  Called with each End Of Session of a reading.
    /// </summary>
    partial void OnEndOfSession();

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.SystemEventMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Stock Directory Message of a reading.
    /// </summary>
    partial void OnStockDirectoryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockDirectoryMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Stock Trading Action Message of a reading.
    /// </summary>
    partial void OnStockTradingActionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.StockTradingActionMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Reg Sho Short Sale Price Test Restricted Indicator Message of a reading.
    /// </summary>
    partial void OnRegShoShortSalePriceTestRestrictedIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Market Participant Position Message of a reading.
    /// </summary>
    partial void OnMarketParticipantPositionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MarketParticipantPositionMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Decline Level Message of a reading.
    /// </summary>
    partial void OnMwcbDeclineLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbDeclineLevelMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Mwcb Status Level Message of a reading.
    /// </summary>
    partial void OnMwcbStatusLevelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MwcbStatusLevelMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Ipo Quoting Period Update of a reading.
    /// </summary>
    partial void OnIpoQuotingPeriodUpdate(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.IpoQuotingPeriodUpdate.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Luld Auction Collar Message of a reading.
    /// </summary>
    partial void OnLuldAuctionCollarMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.LuldAuctionCollarMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Operational Halt Message of a reading.
    /// </summary>
    partial void OnOperationalHaltMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OperationalHaltMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Add Order No Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderNoMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderNoMpidAttributionMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Add Order With Mpid Attribution Message of a reading.
    /// </summary>
    partial void OnAddOrderWithMpidAttributionMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.AddOrderWithMpidAttributionMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    partial void OnOrderExecutedMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed With Price Message of a reading.
    /// </summary>
    partial void OnOrderExecutedWithPriceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderExecutedWithPriceMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Order Cancel Message of a reading.
    /// </summary>
    partial void OnOrderCancelMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderCancelMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Order Delete Message of a reading.
    /// </summary>
    partial void OnOrderDeleteMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderDeleteMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Order Replace Message of a reading.
    /// </summary>
    partial void OnOrderReplaceMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.OrderReplaceMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Non Cross Trade Message of a reading.
    /// </summary>
    partial void OnNonCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NonCrossTradeMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Cross Trade Message of a reading.
    /// </summary>
    partial void OnCrossTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.CrossTradeMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    partial void OnBrokenTradeMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.BrokenTradeMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Net Order Imbalance Indicator Message of a reading.
    /// </summary>
    partial void OnNetOrderImbalanceIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.NetOrderImbalanceIndicatorMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Retail Price Improvement Indicator Message of a reading.
    /// </summary>
    partial void OnRetailPriceImprovementIndicatorMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.RetailPriceImprovementIndicatorMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Direct Listing With Capital Raise Price Discovery Message of a reading.
    /// </summary>
    partial void OnDirectListingWithCapitalRaisePriceDiscoveryMessage(in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout message, in global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.MessageHeader.Layout header, ulong? sequenceNumber);

    /// <summary>
    ///  Called with each Client Packet of a reading.
    /// </summary>
    partial void OnClientPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ClientPacket.Reader value);

    /// <summary>
    ///  Called with each Server Packet of a reading.
    /// </summary>
    partial void OnServerPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.ServerPacket.Reader value);

    /// <summary>
    ///  Called with each Packet of a reading.
    /// </summary>
    partial void OnPacket(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.Packet.Reader value);

    /// <summary>
    ///  Called with each File of a reading.
    /// </summary>
    partial void OnFile(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2026.File.Reader value);
}
