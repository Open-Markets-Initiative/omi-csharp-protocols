using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The sequenced (server to client) message carried by the packet, opaque bytes unless an application source dispatches it
/// </summary>
public static class SequencedMessage
{
    /// <summary>
    ///  Identifies the case of the Sequenced Message that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Sequenced Message.
        /// </summary>
        None = 0,
        /// <summary>
        ///  System Event Messages signal events that affect the entire NASDAQ system.
        /// </summary>
        SystemEventMessage = 1,
        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Enter Order Message. The data fields from the Enter Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields.
        /// </summary>
        OrderAcceptedMessage = 2,
        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Replace Order Message. The data fields from the Replace Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields. You will receive one and only one of these two for each replacement.
        /// </summary>
        ReplacedMessage = 3,
        /// <summary>
        ///  A Canceled Message informs you that an order has been reduced or canceled. This could be acknowledging a Cancel Order Message, or it could be the result of the order timing out or being canceled automatically.
        /// </summary>
        CanceledMessage = 4,
        /// <summary>
        ///  AIQ Canceled Message
        /// </summary>
        AiqCanceledMessage = 5,
        /// <summary>
        ///  An Executed Order Message informs you that all or part of an order has been executed.
        /// </summary>
        OrderExecutedMessage = 6,
        /// <summary>
        ///  A Broken Trade Message informs you that an execution has been broken. The trade is no longer good and will not clear. The reason for the break is given.
        /// </summary>
        BrokenTradeMessage = 7,
        /// <summary>
        ///  A Rejected Message may be sent in response to an Enter Order Message or Replace Order Message if the order or replace cannot be accepted at this time. The reason for the rejection is given.
        /// </summary>
        RejectedOrderMessage = 8,
        /// <summary>
        ///  A Cancel Pending Message is sent in response to a cancel request for a cross order during a pre-cross late period signifying that it cannot be canceled at this time, but any unexecuted portion of this order will automatically be canceled immediately after the cross completes.
        /// </summary>
        CancelPendingMessage = 9,
        /// <summary>
        ///  A Cancel Reject Message is sent in response to a partial cancel request (with non-zero “intended order size”) for a cross order during a pre-cross late period signifying that it cannot be partially canceled at this time. No automatic cancel will be scheduled for this order. Clients could repeat their request for any unexecuted portion of the order after the cross completes.
        /// </summary>
        CancelRejectMessage = 10,
        /// <summary>
        ///  A Priority Update Message is sent whenever priority of the order has been changed by the system.
        /// </summary>
        OrderPriorityUpdateMessage = 11,
        /// <summary>
        ///  An Order Modified Message is sent in response to an order modify request.
        /// </summary>
        OrderModifiedMessage = 12,
        /// <summary>
        ///  The Order Restated Message is sent to indicate that the system has modified an order as part of its order management.
        /// </summary>
        OrderRestatedMessage = 13,
        /// <summary>
        ///  The Account Query Response message in sent in response to an Account Query Request to indicate the next available UserRefNum that can be used to identify new transactions.
        /// </summary>
        AccountQueryResponseMessage = 14,
        /// <summary>
        ///  This message acknowledges the receipt of a valid Mass Cancel Request message. The data fields from the Mass Cancel Request message are echoed back in this message.
        /// </summary>
        MassCancelResponseMessage = 15,
        /// <summary>
        ///  This message acknowledges the receipt of the Disable Order Entry Request message. The data fields from the Disable Order Entry Request message are echoed back in this message.
        /// </summary>
        DisableOrderEntryResponseMessage = 16,
        /// <summary>
        ///  This message acknowledges the receipt of the Enable Order Entry Request message. The data fields from the Enable Order Entry Request message are echoed back in this message.
        /// </summary>
        EnableOrderEntryResponseMessage = 17,
    }

    /// <summary>
    ///  Reads the Sequenced Message in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> sequencedMessageTypeValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> sequencedMessageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.sequencedMessageTypeValue = sequencedMessageTypeValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case)Parser.Valid(Parser.SelectSequencedMessage(this.bytes, this.start, this.end, this.start, this.sequencedMessageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Sequenced Message that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Sequenced Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  System Event Messages signal events that affect the entire NASDAQ system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemEventMessage.Layout SystemEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Enter Order Message. The data fields from the Enter Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public OrderAcceptedMessage.Reader OrderAcceptedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int orderAcceptedMessageReaderPosition = this.start;
                return new(this.bytes, orderAcceptedMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Replace Order Message. The data fields from the Replace Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields. You will receive one and only one of these two for each replacement.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ReplacedMessage.Reader ReplacedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int replacedMessageReaderPosition = this.start;
                return new(this.bytes, replacedMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Canceled Message informs you that an order has been reduced or canceled. This could be acknowledging a Cancel Order Message, or it could be the result of the order timing out or being canceled automatically.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public CanceledMessage.Reader CanceledMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int canceledMessageReaderPosition = this.start;
                return new(this.bytes, canceledMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  AIQ Canceled Message
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public AiqCanceledMessage.Reader AiqCanceledMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int aiqCanceledMessageReaderPosition = this.start;
                return new(this.bytes, aiqCanceledMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  An Executed Order Message informs you that all or part of an order has been executed.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public OrderExecutedMessage.Reader OrderExecutedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int orderExecutedMessageReaderPosition = this.start;
                return new(this.bytes, orderExecutedMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Broken Trade Message informs you that an execution has been broken. The trade is no longer good and will not clear. The reason for the break is given.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public BrokenTradeMessage.Reader BrokenTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int brokenTradeMessageReaderPosition = this.start;
                return new(this.bytes, brokenTradeMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Rejected Message may be sent in response to an Enter Order Message or Replace Order Message if the order or replace cannot be accepted at this time. The reason for the rejection is given.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public RejectedOrderMessage.Reader RejectedOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int rejectedOrderMessageReaderPosition = this.start;
                return new(this.bytes, rejectedOrderMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Cancel Pending Message is sent in response to a cancel request for a cross order during a pre-cross late period signifying that it cannot be canceled at this time, but any unexecuted portion of this order will automatically be canceled immediately after the cross completes.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public CancelPendingMessage.Reader CancelPendingMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int cancelPendingMessageReaderPosition = this.start;
                return new(this.bytes, cancelPendingMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Cancel Reject Message is sent in response to a partial cancel request (with non-zero “intended order size”) for a cross order during a pre-cross late period signifying that it cannot be partially canceled at this time. No automatic cancel will be scheduled for this order. Clients could repeat their request for any unexecuted portion of the order after the cross completes.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public CancelRejectMessage.Reader CancelRejectMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int cancelRejectMessageReaderPosition = this.start;
                return new(this.bytes, cancelRejectMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  A Priority Update Message is sent whenever priority of the order has been changed by the system.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public OrderPriorityUpdateMessage.Reader OrderPriorityUpdateMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int orderPriorityUpdateMessageReaderPosition = this.start;
                return new(this.bytes, orderPriorityUpdateMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  An Order Modified Message is sent in response to an order modify request.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public OrderModifiedMessage.Reader OrderModifiedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int orderModifiedMessageReaderPosition = this.start;
                return new(this.bytes, orderModifiedMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Order Restated Message is sent to indicate that the system has modified an order as part of its order management.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public OrderRestatedMessage.Reader OrderRestatedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int orderRestatedMessageReaderPosition = this.start;
                return new(this.bytes, orderRestatedMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Account Query Response message in sent in response to an Account Query Request to indicate the next available UserRefNum that can be used to identify new transactions.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public AccountQueryResponseMessage.Reader AccountQueryResponseMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int accountQueryResponseMessageReaderPosition = this.start;
                return new(this.bytes, accountQueryResponseMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of a valid Mass Cancel Request message. The data fields from the Mass Cancel Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public MassCancelResponseMessage.Reader MassCancelResponseMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int massCancelResponseMessageReaderPosition = this.start;
                return new(this.bytes, massCancelResponseMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of the Disable Order Entry Request message. The data fields from the Disable Order Entry Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public DisableOrderEntryResponseMessage.Reader DisableOrderEntryResponseMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int disableOrderEntryResponseMessageReaderPosition = this.start;
                return new(this.bytes, disableOrderEntryResponseMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of the Enable Order Entry Request message. The data fields from the Enable Order Entry Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public EnableOrderEntryResponseMessage.Reader EnableOrderEntryResponseMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int enableOrderEntryResponseMessageReaderPosition = this.start;
                return new(this.bytes, enableOrderEntryResponseMessageReaderPosition, this.end);
            }
        }
    }

    /// <summary>
    ///  The Sequenced Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case @case;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Value orderAcceptedMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Value replacedMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Value canceledMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Value aiqCanceledMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Value orderExecutedMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Value brokenTradeMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Value rejectedOrderMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Value cancelPendingMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Value cancelRejectMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Value orderPriorityUpdateMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Value orderModifiedMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Value orderRestatedMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Value accountQueryResponseMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Value massCancelResponseMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Value disableOrderEntryResponseMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Value enableOrderEntryResponseMessage;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Value systemEventMessage;
        }

        /// <summary>
        ///  System Event Messages signal events that affect the entire NASDAQ system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Enter Order Message. The data fields from the Enter Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Value OrderAcceptedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.orderAcceptedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage;
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt and acceptance of a valid Replace Order Message. The data fields from the Replace Order Message are echoed back in this message. Note that the accepted values may differ from the entered values for some fields. You will receive one and only one of these two for each replacement.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Value ReplacedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.replacedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage;
            }
        }

        /// <summary>
        ///  A Canceled Message informs you that an order has been reduced or canceled. This could be acknowledging a Cancel Order Message, or it could be the result of the order timing out or being canceled automatically.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Value CanceledMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.canceledMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage;
            }
        }

        /// <summary>
        ///  AIQ Canceled Message
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Value AiqCanceledMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.aiqCanceledMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage;
            }
        }

        /// <summary>
        ///  An Executed Order Message informs you that all or part of an order has been executed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Value OrderExecutedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.orderExecutedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage;
            }
        }

        /// <summary>
        ///  A Broken Trade Message informs you that an execution has been broken. The trade is no longer good and will not clear. The reason for the break is given.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Value BrokenTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.brokenTradeMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage;
            }
        }

        /// <summary>
        ///  A Rejected Message may be sent in response to an Enter Order Message or Replace Order Message if the order or replace cannot be accepted at this time. The reason for the rejection is given.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Value RejectedOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.rejectedOrderMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage;
            }
        }

        /// <summary>
        ///  A Cancel Pending Message is sent in response to a cancel request for a cross order during a pre-cross late period signifying that it cannot be canceled at this time, but any unexecuted portion of this order will automatically be canceled immediately after the cross completes.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Value CancelPendingMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.cancelPendingMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage;
            }
        }

        /// <summary>
        ///  A Cancel Reject Message is sent in response to a partial cancel request (with non-zero “intended order size”) for a cross order during a pre-cross late period signifying that it cannot be partially canceled at this time. No automatic cancel will be scheduled for this order. Clients could repeat their request for any unexecuted portion of the order after the cross completes.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Value CancelRejectMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.cancelRejectMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage;
            }
        }

        /// <summary>
        ///  A Priority Update Message is sent whenever priority of the order has been changed by the system.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Value OrderPriorityUpdateMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.orderPriorityUpdateMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage;
            }
        }

        /// <summary>
        ///  An Order Modified Message is sent in response to an order modify request.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Value OrderModifiedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.orderModifiedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage;
            }
        }

        /// <summary>
        ///  The Order Restated Message is sent to indicate that the system has modified an order as part of its order management.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Value OrderRestatedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.orderRestatedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage;
            }
        }

        /// <summary>
        ///  The Account Query Response message in sent in response to an Account Query Request to indicate the next available UserRefNum that can be used to identify new transactions.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Value AccountQueryResponseMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.accountQueryResponseMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage;
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of a valid Mass Cancel Request message. The data fields from the Mass Cancel Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Value MassCancelResponseMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.massCancelResponseMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage;
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of the Disable Order Entry Request message. The data fields from the Disable Order Entry Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Value DisableOrderEntryResponseMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.disableOrderEntryResponseMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage;
            }
        }

        /// <summary>
        ///  This message acknowledges the receipt of the Enable Order Entry Request message. The data fields from the Enable Order Entry Request message are echoed back in this message.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Value EnableOrderEntryResponseMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.enableOrderEntryResponseMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Accepted Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderAcceptedMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.OrderAcceptedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Replaced Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ReplacedMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.ReplacedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Canceled Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CanceledMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.CanceledMessage];

        /// <summary>
        ///  The value in the bytes that selects the Aiq Canceled Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AiqCanceledMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.AiqCanceledMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Executed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.OrderExecutedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Broken Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> BrokenTradeMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.BrokenTradeMessage];

        /// <summary>
        ///  The value in the bytes that selects the Rejected Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RejectedOrderMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.RejectedOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Cancel Pending Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CancelPendingMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.CancelPendingMessage];

        /// <summary>
        ///  The value in the bytes that selects the Cancel Reject Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CancelRejectMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.CancelRejectMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Priority Update Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderPriorityUpdateMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.OrderPriorityUpdateMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Modified Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderModifiedMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.OrderModifiedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Restated Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderRestatedMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.OrderRestatedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Account Query Response Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AccountQueryResponseMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.AccountQueryResponseMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mass Cancel Response Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MassCancelResponseMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.MassCancelResponseMessage];

        /// <summary>
        ///  The value in the bytes that selects the Disable Order Entry Response Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> DisableOrderEntryResponseMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.DisableOrderEntryResponseMessage];

        /// <summary>
        ///  The value in the bytes that selects the Enable Order Entry Response Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> EnableOrderEntryResponseMessageSequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessageType.EnableOrderEntryResponseMessage];

        /// <summary>
        ///  Creates the value of the Sequenced Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int packetLengthDependencyWriteDistanceFromSequencedMessage = -4;
    private const int SystemEventMessageCase = 1;
    private const int OrderAcceptedMessageCase = 2;
    private const int ReplacedMessageCase = 3;
    private const int CanceledMessageCase = 4;
    private const int AiqCanceledMessageCase = 5;
    private const int OrderExecutedMessageCase = 6;
    private const int BrokenTradeMessageCase = 7;
    private const int RejectedOrderMessageCase = 8;
    private const int CancelPendingMessageCase = 9;
    private const int CancelRejectMessageCase = 10;
    private const int OrderPriorityUpdateMessageCase = 11;
    private const int OrderModifiedMessageCase = 12;
    private const int OrderRestatedMessageCase = 13;
    private const int AccountQueryResponseMessageCase = 14;
    private const int MassCancelResponseMessageCase = 15;
    private const int DisableOrderEntryResponseMessageCase = 16;
    private const int EnableOrderEntryResponseMessageCase = 17;

    /// <summary>
    ///  Writes the Sequenced Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Sequenced Message.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped ref int sequencedMessageWriterCaseRecord, scoped in Value value)
    {
        int sequencedMessageWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        cursor.SequencedDataPacket_SequencedMessage_EndFrozen = false;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage)
        {
            sequencedMessageWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage)
        {
            sequencedMessageWriterCase = OrderAcceptedMessageCase;
            int orderAcceptedMessageWriting = cursor.Index;
            var orderAcceptedMessageWriteValue = value.orderAcceptedMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Write(bytes, ref cursor, orderAcceptedMessageWriting, in orderAcceptedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage)
        {
            sequencedMessageWriterCase = ReplacedMessageCase;
            int replacedMessageWriting = cursor.Index;
            var replacedMessageWriteValue = value.replacedMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Write(bytes, ref cursor, replacedMessageWriting, in replacedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage)
        {
            sequencedMessageWriterCase = CanceledMessageCase;
            int canceledMessageWriting = cursor.Index;
            var canceledMessageWriteValue = value.canceledMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Write(bytes, ref cursor, canceledMessageWriting, in canceledMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage)
        {
            sequencedMessageWriterCase = AiqCanceledMessageCase;
            int aiqCanceledMessageWriting = cursor.Index;
            var aiqCanceledMessageWriteValue = value.aiqCanceledMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Write(bytes, ref cursor, aiqCanceledMessageWriting, in aiqCanceledMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage)
        {
            sequencedMessageWriterCase = OrderExecutedMessageCase;
            int orderExecutedMessageWriting = cursor.Index;
            var orderExecutedMessageWriteValue = value.orderExecutedMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Write(bytes, ref cursor, orderExecutedMessageWriting, in orderExecutedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage)
        {
            sequencedMessageWriterCase = BrokenTradeMessageCase;
            int brokenTradeMessageWriting = cursor.Index;
            var brokenTradeMessageWriteValue = value.brokenTradeMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Write(bytes, ref cursor, brokenTradeMessageWriting, in brokenTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage)
        {
            sequencedMessageWriterCase = RejectedOrderMessageCase;
            int rejectedOrderMessageWriting = cursor.Index;
            var rejectedOrderMessageWriteValue = value.rejectedOrderMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Write(bytes, ref cursor, rejectedOrderMessageWriting, in rejectedOrderMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage)
        {
            sequencedMessageWriterCase = CancelPendingMessageCase;
            int cancelPendingMessageWriting = cursor.Index;
            var cancelPendingMessageWriteValue = value.cancelPendingMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Write(bytes, ref cursor, cancelPendingMessageWriting, in cancelPendingMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage)
        {
            sequencedMessageWriterCase = CancelRejectMessageCase;
            int cancelRejectMessageWriting = cursor.Index;
            var cancelRejectMessageWriteValue = value.cancelRejectMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Write(bytes, ref cursor, cancelRejectMessageWriting, in cancelRejectMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage)
        {
            sequencedMessageWriterCase = OrderPriorityUpdateMessageCase;
            int orderPriorityUpdateMessageWriting = cursor.Index;
            var orderPriorityUpdateMessageWriteValue = value.orderPriorityUpdateMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Write(bytes, ref cursor, orderPriorityUpdateMessageWriting, in orderPriorityUpdateMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage)
        {
            sequencedMessageWriterCase = OrderModifiedMessageCase;
            int orderModifiedMessageWriting = cursor.Index;
            var orderModifiedMessageWriteValue = value.orderModifiedMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Write(bytes, ref cursor, orderModifiedMessageWriting, in orderModifiedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage)
        {
            sequencedMessageWriterCase = OrderRestatedMessageCase;
            int orderRestatedMessageWriting = cursor.Index;
            var orderRestatedMessageWriteValue = value.orderRestatedMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Write(bytes, ref cursor, orderRestatedMessageWriting, in orderRestatedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage)
        {
            sequencedMessageWriterCase = AccountQueryResponseMessageCase;
            int accountQueryResponseMessageWriting = cursor.Index;
            var accountQueryResponseMessageWriteValue = value.accountQueryResponseMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Write(bytes, ref cursor, accountQueryResponseMessageWriting, in accountQueryResponseMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage)
        {
            sequencedMessageWriterCase = MassCancelResponseMessageCase;
            int massCancelResponseMessageWriting = cursor.Index;
            var massCancelResponseMessageWriteValue = value.massCancelResponseMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Write(bytes, ref cursor, massCancelResponseMessageWriting, in massCancelResponseMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage)
        {
            sequencedMessageWriterCase = DisableOrderEntryResponseMessageCase;
            int disableOrderEntryResponseMessageWriting = cursor.Index;
            var disableOrderEntryResponseMessageWriteValue = value.disableOrderEntryResponseMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Write(bytes, ref cursor, disableOrderEntryResponseMessageWriting, in disableOrderEntryResponseMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage)
        {
            sequencedMessageWriterCase = EnableOrderEntryResponseMessageCase;
            int enableOrderEntryResponseMessageWriting = cursor.Index;
            var enableOrderEntryResponseMessageWriteValue = value.enableOrderEntryResponseMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Write(bytes, ref cursor, enableOrderEntryResponseMessageWriting, in enableOrderEntryResponseMessageWriteValue);
        }
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SequencedMessageWriter.SequencedMessageSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + packetLengthDependencyWriteDistanceFromSequencedMessage, ref packetLengthDependencyWriteUsed, start, cursor.Index);
        if (cursor.SequencedDataPacket_SequencedMessage_EndFrozen && cursor.Index != cursor.SequencedDataPacket_SequencedMessage_End) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref sequencedMessageWriterCaseRecord))
        {
            int recorded = sequencedMessageWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : sequencedMessageWriterCase;
            if (sequencedMessageWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && sequencedMessageWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            sequencedMessageWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
