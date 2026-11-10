using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  The unsequenced (client to server) message carried by the packet, opaque bytes unless an application source dispatches it
/// </summary>
public static class UnsequencedMessage
{
    /// <summary>
    ///  Identifies the case of the Unsequenced Message that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Unsequenced Message.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Enter Order Message
        /// </summary>
        EnterOrderMessage = 1,
        /// <summary>
        ///  The Replace Order Message allows you to cancel and replace an existing order in a single message. This is more efficient than canceling an existing order and immediately succeeding it with a new order. Replacing an order always gives it a new timestamp for its time priority on the book. If you wish you simply partially cancel an order and retain its time priority, send a Cancel Order Message instead.
        /// </summary>
        ReplaceOrderMessage = 2,
        /// <summary>
        ///  The Cancel Order Message is used to request that an order be canceled or reduced. In the Cancel Order Message, you must specify the new "intended order size" for the order. The "intended order size" is the maximum number of shares that can be executed in total after the cancel is applied.
        /// </summary>
        CancelOrderMessage = 3,
        /// <summary>
        ///  The Modify Order Message is used to request modifications that will not affect order priority on the book. Since priority of the order does not change, allowed modifications are restricted. Increasing share amount is not allowed and requests to do so will be ignored.
        /// </summary>
        ModifyOrderMessage = 4,
        /// <summary>
        ///  The Account Query Request message can be used when recovering state to request the next available UserRefNum that can be used for identifying new transactions.
        /// </summary>
        AccountQueryMessage = 5,
        /// <summary>
        ///  The Mass Cancel Request message allows firms to initiate the cancellation of one or more orders that are currently booked at the exchange if they match the criteria specified in the request. If no optional criteria are specified then all orders for the specified firm on the account will be cancelled. Optional criteria can be specified in the Appendage field.
        /// </summary>
        MassCancelRequestMessage = 6,
        /// <summary>
        ///  The Disable Order Entry Request allows firms the ability to block submissions of new orders on a particular account.
        /// </summary>
        DisableOrderEntryRequestMessage = 7,
        /// <summary>
        ///  The Enable Order Entry Request allows firms the ability to unblock submission of new orders on a particular account.
        /// </summary>
        EnableOrderEntryRequestMessage = 8,
    }

    /// <summary>
    ///  Reads the Unsequenced Message in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> unsequencedMessageTypeValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> unsequencedMessageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.unsequencedMessageTypeValue = unsequencedMessageTypeValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case)Parser.Valid(Parser.SelectUnsequencedMessage(this.bytes, this.start, this.end, this.start, this.unsequencedMessageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Unsequenced Message that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Unsequenced Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  Enter Order Message
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public EnterOrderMessage.Reader EnterOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int enterOrderMessageReaderPosition = this.start;
                return new(this.bytes, enterOrderMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Replace Order Message allows you to cancel and replace an existing order in a single message. This is more efficient than canceling an existing order and immediately succeeding it with a new order. Replacing an order always gives it a new timestamp for its time priority on the book. If you wish you simply partially cancel an order and retain its time priority, send a Cancel Order Message instead.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ReplaceOrderMessage.Reader ReplaceOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int replaceOrderMessageReaderPosition = this.start;
                return new(this.bytes, replaceOrderMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Cancel Order Message is used to request that an order be canceled or reduced. In the Cancel Order Message, you must specify the new "intended order size" for the order. The "intended order size" is the maximum number of shares that can be executed in total after the cancel is applied.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public CancelOrderMessage.Reader CancelOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int cancelOrderMessageReaderPosition = this.start;
                return new(this.bytes, cancelOrderMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Modify Order Message is used to request modifications that will not affect order priority on the book. Since priority of the order does not change, allowed modifications are restricted. Increasing share amount is not allowed and requests to do so will be ignored.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ModifyOrderMessage.Reader ModifyOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int modifyOrderMessageReaderPosition = this.start;
                return new(this.bytes, modifyOrderMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Account Query Request message can be used when recovering state to request the next available UserRefNum that can be used for identifying new transactions.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public AccountQueryMessage.Reader AccountQueryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int accountQueryMessageReaderPosition = this.start;
                return new(this.bytes, accountQueryMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Mass Cancel Request message allows firms to initiate the cancellation of one or more orders that are currently booked at the exchange if they match the criteria specified in the request. If no optional criteria are specified then all orders for the specified firm on the account will be cancelled. Optional criteria can be specified in the Appendage field.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public MassCancelRequestMessage.Reader MassCancelRequestMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int massCancelRequestMessageReaderPosition = this.start;
                return new(this.bytes, massCancelRequestMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Disable Order Entry Request allows firms the ability to block submissions of new orders on a particular account.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public DisableOrderEntryRequestMessage.Reader DisableOrderEntryRequestMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int disableOrderEntryRequestMessageReaderPosition = this.start;
                return new(this.bytes, disableOrderEntryRequestMessageReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  The Enable Order Entry Request allows firms the ability to unblock submission of new orders on a particular account.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public EnableOrderEntryRequestMessage.Reader EnableOrderEntryRequestMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int enableOrderEntryRequestMessageReaderPosition = this.start;
                return new(this.bytes, enableOrderEntryRequestMessageReaderPosition, this.end);
            }
        }
    }

    /// <summary>
    ///  The Unsequenced Message fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case @case;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Value enterOrderMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Value replaceOrderMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Value cancelOrderMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Value modifyOrderMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Value accountQueryMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Value massCancelRequestMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Value disableOrderEntryRequestMessage;
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Value enableOrderEntryRequestMessage;

        /// <summary>
        ///  Enter Order Message
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Value EnterOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.enterOrderMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage;
            }
        }

        /// <summary>
        ///  The Replace Order Message allows you to cancel and replace an existing order in a single message. This is more efficient than canceling an existing order and immediately succeeding it with a new order. Replacing an order always gives it a new timestamp for its time priority on the book. If you wish you simply partially cancel an order and retain its time priority, send a Cancel Order Message instead.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Value ReplaceOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.replaceOrderMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage;
            }
        }

        /// <summary>
        ///  The Cancel Order Message is used to request that an order be canceled or reduced. In the Cancel Order Message, you must specify the new "intended order size" for the order. The "intended order size" is the maximum number of shares that can be executed in total after the cancel is applied.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Value CancelOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.cancelOrderMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage;
            }
        }

        /// <summary>
        ///  The Modify Order Message is used to request modifications that will not affect order priority on the book. Since priority of the order does not change, allowed modifications are restricted. Increasing share amount is not allowed and requests to do so will be ignored.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Value ModifyOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.modifyOrderMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage;
            }
        }

        /// <summary>
        ///  The Account Query Request message can be used when recovering state to request the next available UserRefNum that can be used for identifying new transactions.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Value AccountQueryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.accountQueryMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage;
            }
        }

        /// <summary>
        ///  The Mass Cancel Request message allows firms to initiate the cancellation of one or more orders that are currently booked at the exchange if they match the criteria specified in the request. If no optional criteria are specified then all orders for the specified firm on the account will be cancelled. Optional criteria can be specified in the Appendage field.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Value MassCancelRequestMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.massCancelRequestMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage;
            }
        }

        /// <summary>
        ///  The Disable Order Entry Request allows firms the ability to block submissions of new orders on a particular account.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Value DisableOrderEntryRequestMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.disableOrderEntryRequestMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage;
            }
        }

        /// <summary>
        ///  The Enable Order Entry Request allows firms the ability to unblock submission of new orders on a particular account.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Value EnableOrderEntryRequestMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.enableOrderEntryRequestMessage = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Enter Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> EnterOrderMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.EnterOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Replace Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ReplaceOrderMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.ReplaceOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Cancel Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CancelOrderMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.CancelOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Modify Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ModifyOrderMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.ModifyOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Account Query Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AccountQueryMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.AccountQueryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Mass Cancel Request Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MassCancelRequestMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.MassCancelRequestMessage];

        /// <summary>
        ///  The value in the bytes that selects the Disable Order Entry Request Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> DisableOrderEntryRequestMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.DisableOrderEntryRequestMessage];

        /// <summary>
        ///  The value in the bytes that selects the Enable Order Entry Request Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> EnableOrderEntryRequestMessageUnsequencedMessageType => [(byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessageType.EnableOrderEntryRequestMessage];

        /// <summary>
        ///  Creates the value of the Unsequenced Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int packetLengthDependencyWriteDistanceFromUnsequencedMessage = -4;
    private const int EnterOrderMessageCase = 1;
    private const int ReplaceOrderMessageCase = 2;
    private const int CancelOrderMessageCase = 3;
    private const int ModifyOrderMessageCase = 4;
    private const int AccountQueryMessageCase = 5;
    private const int MassCancelRequestMessageCase = 6;
    private const int DisableOrderEntryRequestMessageCase = 7;
    private const int EnableOrderEntryRequestMessageCase = 8;

    /// <summary>
    ///  Writes the Unsequenced Message to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Unsequenced Message.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped ref int unsequencedMessageWriterCaseRecord, scoped in Value value)
    {
        int unsequencedMessageWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen = false;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage)
        {
            unsequencedMessageWriterCase = EnterOrderMessageCase;
            int enterOrderMessageWriting = cursor.Index;
            var enterOrderMessageWriteValue = value.enterOrderMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Write(bytes, ref cursor, enterOrderMessageWriting, in enterOrderMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage)
        {
            unsequencedMessageWriterCase = ReplaceOrderMessageCase;
            int replaceOrderMessageWriting = cursor.Index;
            var replaceOrderMessageWriteValue = value.replaceOrderMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Write(bytes, ref cursor, replaceOrderMessageWriting, in replaceOrderMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage)
        {
            unsequencedMessageWriterCase = CancelOrderMessageCase;
            int cancelOrderMessageWriting = cursor.Index;
            var cancelOrderMessageWriteValue = value.cancelOrderMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Write(bytes, ref cursor, cancelOrderMessageWriting, in cancelOrderMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage)
        {
            unsequencedMessageWriterCase = ModifyOrderMessageCase;
            int modifyOrderMessageWriting = cursor.Index;
            var modifyOrderMessageWriteValue = value.modifyOrderMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Write(bytes, ref cursor, modifyOrderMessageWriting, in modifyOrderMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage)
        {
            unsequencedMessageWriterCase = AccountQueryMessageCase;
            int accountQueryMessageWriting = cursor.Index;
            var accountQueryMessageWriteValue = value.accountQueryMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Write(bytes, ref cursor, accountQueryMessageWriting, in accountQueryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage)
        {
            unsequencedMessageWriterCase = MassCancelRequestMessageCase;
            int massCancelRequestMessageWriting = cursor.Index;
            var massCancelRequestMessageWriteValue = value.massCancelRequestMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Write(bytes, ref cursor, massCancelRequestMessageWriting, in massCancelRequestMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage)
        {
            unsequencedMessageWriterCase = DisableOrderEntryRequestMessageCase;
            int disableOrderEntryRequestMessageWriting = cursor.Index;
            var disableOrderEntryRequestMessageWriteValue = value.disableOrderEntryRequestMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Write(bytes, ref cursor, disableOrderEntryRequestMessageWriting, in disableOrderEntryRequestMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage)
        {
            unsequencedMessageWriterCase = EnableOrderEntryRequestMessageCase;
            int enableOrderEntryRequestMessageWriting = cursor.Index;
            var enableOrderEntryRequestMessageWriteValue = value.enableOrderEntryRequestMessage;
            global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Write(bytes, ref cursor, enableOrderEntryRequestMessageWriting, in enableOrderEntryRequestMessageWriteValue);
        }
        global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.UnsequencedMessageWriter.UnsequencedMessageSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent : start + packetLengthDependencyWriteDistanceFromUnsequencedMessage, ref packetLengthDependencyWriteUsed, start, cursor.Index);
        if (cursor.UnsequencedDataPacket_UnsequencedMessage_EndFrozen && cursor.Index != cursor.UnsequencedDataPacket_UnsequencedMessage_End) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref unsequencedMessageWriterCaseRecord))
        {
            int recorded = unsequencedMessageWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : unsequencedMessageWriterCase;
            if (unsequencedMessageWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && unsequencedMessageWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            unsequencedMessageWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
