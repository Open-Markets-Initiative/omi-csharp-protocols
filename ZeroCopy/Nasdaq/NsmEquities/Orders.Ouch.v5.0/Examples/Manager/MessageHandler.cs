namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Manager;

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
    public void VisitClientPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader clientPacket)
    {
        var clientSoupBinTcpPacketEntries = clientPacket.ClientSoupBinTcpPacket;
        while (clientSoupBinTcpPacketEntries.MoveNext())
        {
            var clientSoupBinTcpPacket = clientSoupBinTcpPacketEntries.Current;
            this.OnClientPacketHeader(in clientSoupBinTcpPacket.ClientPacketHeader);
            var clientPayload = clientSoupBinTcpPacket.ClientPayload;
            switch (clientPayload.Case)
            {
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(clientPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LoginRequestPacket:
                {
                    this.OnLoginRequestPacket(in clientPayload.LoginRequestPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.UnsequencedDataPacket:
                {
                    var unsequencedDataPacket = clientPayload.UnsequencedDataPacket;
                    var unsequencedMessage = unsequencedDataPacket.UnsequencedMessage;
                    switch (unsequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage:
                        {
                            this.OnEnterOrderMessage(unsequencedMessage.EnterOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage:
                        {
                            this.OnReplaceOrderMessage(unsequencedMessage.ReplaceOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage:
                        {
                            this.OnCancelOrderMessage(unsequencedMessage.CancelOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage:
                        {
                            this.OnModifyOrderMessage(unsequencedMessage.ModifyOrderMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage:
                        {
                            this.OnAccountQueryMessage(unsequencedMessage.AccountQueryMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage:
                        {
                            this.OnMassCancelRequestMessage(unsequencedMessage.MassCancelRequestMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage:
                        {
                            this.OnDisableOrderEntryRequestMessage(unsequencedMessage.DisableOrderEntryRequestMessage);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage:
                        {
                            this.OnEnableOrderEntryRequestMessage(unsequencedMessage.EnableOrderEntryRequestMessage);
                            break;
                        }
                    }
                    this.OnUnsequencedDataPacket(unsequencedDataPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.ClientHeartbeat:
                {
                    this.OnClientHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LogoutRequest:
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
    public void VisitServerPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader serverPacket)
    {
        var serverSoupBinTcpPacketEntries = serverPacket.ServerSoupBinTcpPacket;
        while (serverSoupBinTcpPacketEntries.MoveNext())
        {
            var serverSoupBinTcpPacket = serverSoupBinTcpPacketEntries.Current;
            this.OnServerPacketHeader(in serverSoupBinTcpPacket.ServerPacketHeader);
            var serverPayload = serverSoupBinTcpPacket.ServerPayload;
            switch (serverPayload.Case)
            {
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.DebugPacket:
                {
                    this.VisitDebugPacket(serverPayload.DebugPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginAcceptedPacket:
                {
                    this.ConversationState.Apply(in serverPayload.LoginAcceptedPacket);
                    this.OnLoginAcceptedPacket(in serverPayload.LoginAcceptedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginRejectedPacket:
                {
                    this.OnLoginRejectedPacket(in serverPayload.LoginRejectedPacket);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.SequencedDataPacket:
                {
                    var sequencedDataPacket = serverPayload.SequencedDataPacket;
                    this.ConversationState.CountSequencedDataPacket();
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in sequencedMessage.SystemEventMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage:
                        {
                            this.OnOrderAcceptedMessage(sequencedMessage.OrderAcceptedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage:
                        {
                            this.OnReplacedMessage(sequencedMessage.ReplacedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage:
                        {
                            this.OnCanceledMessage(sequencedMessage.CanceledMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage:
                        {
                            this.OnAiqCanceledMessage(sequencedMessage.AiqCanceledMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(sequencedMessage.OrderExecutedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(sequencedMessage.BrokenTradeMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage:
                        {
                            this.OnRejectedOrderMessage(sequencedMessage.RejectedOrderMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage:
                        {
                            this.OnCancelPendingMessage(sequencedMessage.CancelPendingMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage:
                        {
                            this.OnCancelRejectMessage(sequencedMessage.CancelRejectMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage:
                        {
                            this.OnOrderPriorityUpdateMessage(sequencedMessage.OrderPriorityUpdateMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage:
                        {
                            this.OnOrderModifiedMessage(sequencedMessage.OrderModifiedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage:
                        {
                            this.OnOrderRestatedMessage(sequencedMessage.OrderRestatedMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage:
                        {
                            this.OnAccountQueryResponseMessage(sequencedMessage.AccountQueryResponseMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage:
                        {
                            this.OnMassCancelResponseMessage(sequencedMessage.MassCancelResponseMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage:
                        {
                            this.OnDisableOrderEntryResponseMessage(sequencedMessage.DisableOrderEntryResponseMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage:
                        {
                            this.OnEnableOrderEntryResponseMessage(sequencedMessage.EnableOrderEntryResponseMessage, this.ConversationState.SequencedDataPacketNumber);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, this.ConversationState.SequencedDataPacketNumber);
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.ServerHeartbeat:
                {
                    this.OnServerHeartbeat();
                    break;
                }
                case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.EndOfSession:
                {
                    this.OnEndOfSession();
                    break;
                }
            }
        }
        this.OnServerPacket(serverPacket);
    }

    /// <summary>
    ///  Visits the member and each message and header that it holds, in wire order.
    /// </summary>
    public void VisitDebugPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader debugPacket)
    {
        this.OnDebugPacket(debugPacket);
    }

    /// <summary>
    ///  Called with each Client Packet Header of a reading.
    /// </summary>
    partial void OnClientPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout value);

    /// <summary>
    ///  Called with each Debug Packet of a reading.
    /// </summary>
    partial void OnDebugPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader message);

    /// <summary>
    ///  Called with each Login Request Packet of a reading.
    /// </summary>
    partial void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout message);

    /// <summary>
    ///  Called with each Unsequenced Data Packet of a reading.
    /// </summary>
    partial void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader message);

    /// <summary>
    ///  Called with each Client Heartbeat of a reading.
    /// </summary>
    partial void OnClientHeartbeat();

    /// <summary>
    ///  Called with each Logout Request of a reading.
    /// </summary>
    partial void OnLogoutRequest();

    /// <summary>
    ///  Called with each Enter Order Message of a reading.
    /// </summary>
    partial void OnEnterOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader message);

    /// <summary>
    ///  Called with each Replace Order Message of a reading.
    /// </summary>
    partial void OnReplaceOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader message);

    /// <summary>
    ///  Called with each Cancel Order Message of a reading.
    /// </summary>
    partial void OnCancelOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader message);

    /// <summary>
    ///  Called with each Modify Order Message of a reading.
    /// </summary>
    partial void OnModifyOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader message);

    /// <summary>
    ///  Called with each Account Query Message of a reading.
    /// </summary>
    partial void OnAccountQueryMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader message);

    /// <summary>
    ///  Called with each Mass Cancel Request Message of a reading.
    /// </summary>
    partial void OnMassCancelRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader message);

    /// <summary>
    ///  Called with each Disable Order Entry Request Message of a reading.
    /// </summary>
    partial void OnDisableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader message);

    /// <summary>
    ///  Called with each Enable Order Entry Request Message of a reading.
    /// </summary>
    partial void OnEnableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader message);

    /// <summary>
    ///  Called with each Server Packet Header of a reading.
    /// </summary>
    partial void OnServerPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout value);

    /// <summary>
    ///  Called with each Login Accepted Packet of a reading.
    /// </summary>
    partial void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout message);

    /// <summary>
    ///  Called with each Login Rejected Packet of a reading.
    /// </summary>
    partial void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout message);

    /// <summary>
    ///  Called with each Sequenced Data Packet of a reading.
    /// </summary>
    partial void OnSequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Server Heartbeat of a reading.
    /// </summary>
    partial void OnServerHeartbeat();

    /// <summary>
    ///  Called with each End Of Session of a reading.
    /// </summary>
    partial void OnEndOfSession();

    /// <summary>
    ///  Called with each System Event Message of a reading.
    /// </summary>
    partial void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Accepted Message of a reading.
    /// </summary>
    partial void OnOrderAcceptedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Replaced Message of a reading.
    /// </summary>
    partial void OnReplacedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Canceled Message of a reading.
    /// </summary>
    partial void OnCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Aiq Canceled Message of a reading.
    /// </summary>
    partial void OnAiqCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    partial void OnOrderExecutedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    partial void OnBrokenTradeMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Rejected Order Message of a reading.
    /// </summary>
    partial void OnRejectedOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Cancel Pending Message of a reading.
    /// </summary>
    partial void OnCancelPendingMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Cancel Reject Message of a reading.
    /// </summary>
    partial void OnCancelRejectMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Priority Update Message of a reading.
    /// </summary>
    partial void OnOrderPriorityUpdateMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Modified Message of a reading.
    /// </summary>
    partial void OnOrderModifiedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Order Restated Message of a reading.
    /// </summary>
    partial void OnOrderRestatedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Account Query Response Message of a reading.
    /// </summary>
    partial void OnAccountQueryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Mass Cancel Response Message of a reading.
    /// </summary>
    partial void OnMassCancelResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Disable Order Entry Response Message of a reading.
    /// </summary>
    partial void OnDisableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Enable Order Entry Response Message of a reading.
    /// </summary>
    partial void OnEnableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader message, global::System.UInt128? sequenceNumber);

    /// <summary>
    ///  Called with each Client Packet of a reading.
    /// </summary>
    partial void OnClientPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader value);

    /// <summary>
    ///  Called with each Server Packet of a reading.
    /// </summary>
    partial void OnServerPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader value);
}
