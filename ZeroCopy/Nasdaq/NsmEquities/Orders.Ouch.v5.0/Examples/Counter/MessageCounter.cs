namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Counter;

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
    long onEnterOrderMessageCount;
    long onReplaceOrderMessageCount;
    long onCancelOrderMessageCount;
    long onModifyOrderMessageCount;
    long onAccountQueryMessageCount;
    long onMassCancelRequestMessageCount;
    long onDisableOrderEntryRequestMessageCount;
    long onEnableOrderEntryRequestMessageCount;
    long onServerPacketHeaderCount;
    long onLoginAcceptedPacketCount;
    long onLoginRejectedPacketCount;
    long onSequencedDataPacketCount;
    long onServerHeartbeatCount;
    long onEndOfSessionCount;
    long onSystemEventMessageCount;
    long onOrderAcceptedMessageCount;
    long onReplacedMessageCount;
    long onCanceledMessageCount;
    long onAiqCanceledMessageCount;
    long onOrderExecutedMessageCount;
    long onBrokenTradeMessageCount;
    long onRejectedOrderMessageCount;
    long onCancelPendingMessageCount;
    long onCancelRejectMessageCount;
    long onOrderPriorityUpdateMessageCount;
    long onOrderModifiedMessageCount;
    long onOrderRestatedMessageCount;
    long onAccountQueryResponseMessageCount;
    long onMassCancelResponseMessageCount;
    long onDisableOrderEntryResponseMessageCount;
    long onEnableOrderEntryResponseMessageCount;
    long onClientPacketCount;
    long onServerPacketCount;

    /// <summary>
    ///  Starts the clock for the elapsed time.
    /// </summary>
    public MessageCounter() => this.started = global::System.Diagnostics.Stopwatch.GetTimestamp();

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
                    var sequencedMessage = sequencedDataPacket.SequencedMessage;
                    switch (sequencedMessage.Case)
                    {
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage:
                        {
                            this.OnSystemEventMessage(in sequencedMessage.SystemEventMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage:
                        {
                            this.OnOrderAcceptedMessage(sequencedMessage.OrderAcceptedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage:
                        {
                            this.OnReplacedMessage(sequencedMessage.ReplacedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage:
                        {
                            this.OnCanceledMessage(sequencedMessage.CanceledMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage:
                        {
                            this.OnAiqCanceledMessage(sequencedMessage.AiqCanceledMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage:
                        {
                            this.OnOrderExecutedMessage(sequencedMessage.OrderExecutedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage:
                        {
                            this.OnBrokenTradeMessage(sequencedMessage.BrokenTradeMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage:
                        {
                            this.OnRejectedOrderMessage(sequencedMessage.RejectedOrderMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage:
                        {
                            this.OnCancelPendingMessage(sequencedMessage.CancelPendingMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage:
                        {
                            this.OnCancelRejectMessage(sequencedMessage.CancelRejectMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage:
                        {
                            this.OnOrderPriorityUpdateMessage(sequencedMessage.OrderPriorityUpdateMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage:
                        {
                            this.OnOrderModifiedMessage(sequencedMessage.OrderModifiedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage:
                        {
                            this.OnOrderRestatedMessage(sequencedMessage.OrderRestatedMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage:
                        {
                            this.OnAccountQueryResponseMessage(sequencedMessage.AccountQueryResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage:
                        {
                            this.OnMassCancelResponseMessage(sequencedMessage.MassCancelResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage:
                        {
                            this.OnDisableOrderEntryResponseMessage(sequencedMessage.DisableOrderEntryResponseMessage, null);
                            break;
                        }
                        case global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage:
                        {
                            this.OnEnableOrderEntryResponseMessage(sequencedMessage.EnableOrderEntryResponseMessage, null);
                            break;
                        }
                    }
                    this.OnSequencedDataPacket(sequencedDataPacket, null);
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
    public void OnClientPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacketHeader.Layout value) => this.onClientPacketHeaderCount++;

    /// <summary>
    ///  Called with each Debug Packet of a reading.
    /// </summary>
    public void OnDebugPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader value) => this.onDebugPacketCount++;

    /// <summary>
    ///  Called with each Login Request Packet of a reading.
    /// </summary>
    public void OnLoginRequestPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRequestPacket.Layout value) => this.onLoginRequestPacketCount++;

    /// <summary>
    ///  Called with each Unsequenced Data Packet of a reading.
    /// </summary>
    public void OnUnsequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader value) => this.onUnsequencedDataPacketCount++;

    /// <summary>
    ///  Called with each Client Heartbeat of a reading.
    /// </summary>
    public void OnClientHeartbeat() => this.onClientHeartbeatCount++;

    /// <summary>
    ///  Called with each Logout Request of a reading.
    /// </summary>
    public void OnLogoutRequest() => this.onLogoutRequestCount++;

    /// <summary>
    ///  Called with each Enter Order Message of a reading.
    /// </summary>
    public void OnEnterOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader value) => this.onEnterOrderMessageCount++;

    /// <summary>
    ///  Called with each Replace Order Message of a reading.
    /// </summary>
    public void OnReplaceOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader value) => this.onReplaceOrderMessageCount++;

    /// <summary>
    ///  Called with each Cancel Order Message of a reading.
    /// </summary>
    public void OnCancelOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader value) => this.onCancelOrderMessageCount++;

    /// <summary>
    ///  Called with each Modify Order Message of a reading.
    /// </summary>
    public void OnModifyOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader value) => this.onModifyOrderMessageCount++;

    /// <summary>
    ///  Called with each Account Query Message of a reading.
    /// </summary>
    public void OnAccountQueryMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader value) => this.onAccountQueryMessageCount++;

    /// <summary>
    ///  Called with each Mass Cancel Request Message of a reading.
    /// </summary>
    public void OnMassCancelRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader value) => this.onMassCancelRequestMessageCount++;

    /// <summary>
    ///  Called with each Disable Order Entry Request Message of a reading.
    /// </summary>
    public void OnDisableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader value) => this.onDisableOrderEntryRequestMessageCount++;

    /// <summary>
    ///  Called with each Enable Order Entry Request Message of a reading.
    /// </summary>
    public void OnEnableOrderEntryRequestMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader value) => this.onEnableOrderEntryRequestMessageCount++;

    /// <summary>
    ///  Called with each Server Packet Header of a reading.
    /// </summary>
    public void OnServerPacketHeader(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacketHeader.Layout value) => this.onServerPacketHeaderCount++;

    /// <summary>
    ///  Called with each Login Accepted Packet of a reading.
    /// </summary>
    public void OnLoginAcceptedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginAcceptedPacket.Layout value) => this.onLoginAcceptedPacketCount++;

    /// <summary>
    ///  Called with each Login Rejected Packet of a reading.
    /// </summary>
    public void OnLoginRejectedPacket(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LoginRejectedPacket.Layout value) => this.onLoginRejectedPacketCount++;

    /// <summary>
    ///  Called with each Sequenced Data Packet of a reading.
    /// </summary>
    public void OnSequencedDataPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader value, global::System.UInt128? sequenceNumber) => this.onSequencedDataPacketCount++;

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
    public void OnSystemEventMessage(in global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Layout value, global::System.UInt128? sequenceNumber) => this.onSystemEventMessageCount++;

    /// <summary>
    ///  Called with each Order Accepted Message of a reading.
    /// </summary>
    public void OnOrderAcceptedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onOrderAcceptedMessageCount++;

    /// <summary>
    ///  Called with each Replaced Message of a reading.
    /// </summary>
    public void OnReplacedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onReplacedMessageCount++;

    /// <summary>
    ///  Called with each Canceled Message of a reading.
    /// </summary>
    public void OnCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onCanceledMessageCount++;

    /// <summary>
    ///  Called with each Aiq Canceled Message of a reading.
    /// </summary>
    public void OnAiqCanceledMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onAiqCanceledMessageCount++;

    /// <summary>
    ///  Called with each Order Executed Message of a reading.
    /// </summary>
    public void OnOrderExecutedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onOrderExecutedMessageCount++;

    /// <summary>
    ///  Called with each Broken Trade Message of a reading.
    /// </summary>
    public void OnBrokenTradeMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onBrokenTradeMessageCount++;

    /// <summary>
    ///  Called with each Rejected Order Message of a reading.
    /// </summary>
    public void OnRejectedOrderMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onRejectedOrderMessageCount++;

    /// <summary>
    ///  Called with each Cancel Pending Message of a reading.
    /// </summary>
    public void OnCancelPendingMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onCancelPendingMessageCount++;

    /// <summary>
    ///  Called with each Cancel Reject Message of a reading.
    /// </summary>
    public void OnCancelRejectMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onCancelRejectMessageCount++;

    /// <summary>
    ///  Called with each Order Priority Update Message of a reading.
    /// </summary>
    public void OnOrderPriorityUpdateMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onOrderPriorityUpdateMessageCount++;

    /// <summary>
    ///  Called with each Order Modified Message of a reading.
    /// </summary>
    public void OnOrderModifiedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onOrderModifiedMessageCount++;

    /// <summary>
    ///  Called with each Order Restated Message of a reading.
    /// </summary>
    public void OnOrderRestatedMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onOrderRestatedMessageCount++;

    /// <summary>
    ///  Called with each Account Query Response Message of a reading.
    /// </summary>
    public void OnAccountQueryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onAccountQueryResponseMessageCount++;

    /// <summary>
    ///  Called with each Mass Cancel Response Message of a reading.
    /// </summary>
    public void OnMassCancelResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onMassCancelResponseMessageCount++;

    /// <summary>
    ///  Called with each Disable Order Entry Response Message of a reading.
    /// </summary>
    public void OnDisableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onDisableOrderEntryResponseMessageCount++;

    /// <summary>
    ///  Called with each Enable Order Entry Response Message of a reading.
    /// </summary>
    public void OnEnableOrderEntryResponseMessage(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader value, global::System.UInt128? sequenceNumber) => this.onEnableOrderEntryResponseMessageCount++;

    /// <summary>
    ///  Called with each Client Packet of a reading.
    /// </summary>
    public void OnClientPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader value) => this.onClientPacketCount++;

    /// <summary>
    ///  Called with each Server Packet of a reading.
    /// </summary>
    public void OnServerPacket(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader value) => this.onServerPacketCount++;

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
        Console.WriteLine($"{nameof(OnEnterOrderMessage)}{Separator}{this.onEnterOrderMessageCount}");
        Console.WriteLine($"{nameof(OnReplaceOrderMessage)}{Separator}{this.onReplaceOrderMessageCount}");
        Console.WriteLine($"{nameof(OnCancelOrderMessage)}{Separator}{this.onCancelOrderMessageCount}");
        Console.WriteLine($"{nameof(OnModifyOrderMessage)}{Separator}{this.onModifyOrderMessageCount}");
        Console.WriteLine($"{nameof(OnAccountQueryMessage)}{Separator}{this.onAccountQueryMessageCount}");
        Console.WriteLine($"{nameof(OnMassCancelRequestMessage)}{Separator}{this.onMassCancelRequestMessageCount}");
        Console.WriteLine($"{nameof(OnDisableOrderEntryRequestMessage)}{Separator}{this.onDisableOrderEntryRequestMessageCount}");
        Console.WriteLine($"{nameof(OnEnableOrderEntryRequestMessage)}{Separator}{this.onEnableOrderEntryRequestMessageCount}");
        Console.WriteLine($"{nameof(OnServerPacketHeader)}{Separator}{this.onServerPacketHeaderCount}");
        Console.WriteLine($"{nameof(OnLoginAcceptedPacket)}{Separator}{this.onLoginAcceptedPacketCount}");
        Console.WriteLine($"{nameof(OnLoginRejectedPacket)}{Separator}{this.onLoginRejectedPacketCount}");
        Console.WriteLine($"{nameof(OnSequencedDataPacket)}{Separator}{this.onSequencedDataPacketCount}");
        Console.WriteLine($"{nameof(OnServerHeartbeat)}{Separator}{this.onServerHeartbeatCount}");
        Console.WriteLine($"{nameof(OnEndOfSession)}{Separator}{this.onEndOfSessionCount}");
        Console.WriteLine($"{nameof(OnSystemEventMessage)}{Separator}{this.onSystemEventMessageCount}");
        Console.WriteLine($"{nameof(OnOrderAcceptedMessage)}{Separator}{this.onOrderAcceptedMessageCount}");
        Console.WriteLine($"{nameof(OnReplacedMessage)}{Separator}{this.onReplacedMessageCount}");
        Console.WriteLine($"{nameof(OnCanceledMessage)}{Separator}{this.onCanceledMessageCount}");
        Console.WriteLine($"{nameof(OnAiqCanceledMessage)}{Separator}{this.onAiqCanceledMessageCount}");
        Console.WriteLine($"{nameof(OnOrderExecutedMessage)}{Separator}{this.onOrderExecutedMessageCount}");
        Console.WriteLine($"{nameof(OnBrokenTradeMessage)}{Separator}{this.onBrokenTradeMessageCount}");
        Console.WriteLine($"{nameof(OnRejectedOrderMessage)}{Separator}{this.onRejectedOrderMessageCount}");
        Console.WriteLine($"{nameof(OnCancelPendingMessage)}{Separator}{this.onCancelPendingMessageCount}");
        Console.WriteLine($"{nameof(OnCancelRejectMessage)}{Separator}{this.onCancelRejectMessageCount}");
        Console.WriteLine($"{nameof(OnOrderPriorityUpdateMessage)}{Separator}{this.onOrderPriorityUpdateMessageCount}");
        Console.WriteLine($"{nameof(OnOrderModifiedMessage)}{Separator}{this.onOrderModifiedMessageCount}");
        Console.WriteLine($"{nameof(OnOrderRestatedMessage)}{Separator}{this.onOrderRestatedMessageCount}");
        Console.WriteLine($"{nameof(OnAccountQueryResponseMessage)}{Separator}{this.onAccountQueryResponseMessageCount}");
        Console.WriteLine($"{nameof(OnMassCancelResponseMessage)}{Separator}{this.onMassCancelResponseMessageCount}");
        Console.WriteLine($"{nameof(OnDisableOrderEntryResponseMessage)}{Separator}{this.onDisableOrderEntryResponseMessageCount}");
        Console.WriteLine($"{nameof(OnEnableOrderEntryResponseMessage)}{Separator}{this.onEnableOrderEntryResponseMessageCount}");
        Console.WriteLine($"{nameof(OnClientPacket)}{Separator}{this.onClientPacketCount}");
        Console.WriteLine($"{nameof(OnServerPacket)}{Separator}{this.onServerPacketCount}");
        Console.WriteLine($"{CaptureBytes}{Separator}{captureBytes}");
        Console.WriteLine($"{ElapsedTime}{Separator}{elapsed}");
    }
}
