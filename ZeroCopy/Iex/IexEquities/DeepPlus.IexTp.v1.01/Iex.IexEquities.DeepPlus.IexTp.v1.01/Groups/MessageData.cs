using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

/// <summary>
///  Branch
/// </summary>
public static class MessageData
{
    /// <summary>
    ///  Identifies the case of the Message Data that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Message Data.
        /// </summary>
        None = 0,
        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        SystemEventMessage = 1,
        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        SecurityDirectoryMessage = 2,
        /// <summary>
        ///  The Trading Status Message is used to indicate the current trading status of a security.
        /// </summary>
        TradingStatusMessage = 3,
        /// <summary>
        ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
        /// </summary>
        RetailLiquidityIndicatorMessage = 4,
        /// <summary>
        ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
        /// </summary>
        OperationalHaltStatusMessage = 5,
        /// <summary>
        ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
        /// </summary>
        ShortSalePriceTestStatusMessage = 6,
        /// <summary>
        ///  The Security Event Message is used to indicate events that apply to a security
        /// </summary>
        SecurityEventMessage = 7,
        /// <summary>
        ///  A displayed order that has been added to the IEX Book
        /// </summary>
        AddOrderMessage = 8,
        /// <summary>
        ///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
        /// </summary>
        OrderModifyMessage = 9,
        /// <summary>
        ///  A displayed order that was removed from the IEX Book
        /// </summary>
        OrderDeleteMessage = 10,
        /// <summary>
        ///  A displayed order that was executed against
        /// </summary>
        OrderExecutedMessage = 11,
        /// <summary>
        ///  A non-displayed order on the book that executed against another non-displayed order on the book
        /// </summary>
        TradeMessage = 12,
        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        TradeBreakMessage = 13,
        /// <summary>
        ///  This message is used to indicate that the IEX Book for a symbol has been cleared of all orders
        /// </summary>
        ClearBookMessage = 14,
    }

    /// <summary>
    ///  Reads the Message Data in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly ReadOnlySpan<byte> messageTypeValue;
        private readonly global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case)Parser.Valid(Parser.SelectMessageData(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Message Data that the bytes select.
        /// </summary>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Message Data.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureMessageData(this.bytes, this.start, this.end, this.start, this.messageTypeValue))];

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemEventMessage.Layout SystemEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SecurityDirectoryMessage.Layout SecurityDirectoryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SecurityDirectoryMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Trading Status Message is used to indicate the current trading status of a security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradingStatusMessage.Layout TradingStatusMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradingStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradingStatusMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RetailLiquidityIndicatorMessage.Layout RetailLiquidityIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.RetailLiquidityIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RetailLiquidityIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OperationalHaltStatusMessage.Layout OperationalHaltStatusMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OperationalHaltStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OperationalHaltStatusMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ShortSalePriceTestStatusMessage.Layout ShortSalePriceTestStatusMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ShortSalePriceTestStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ShortSalePriceTestStatusMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Security Event Message is used to indicate events that apply to a security
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SecurityEventMessage.Layout SecurityEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SecurityEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A displayed order that has been added to the IEX Book
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AddOrderMessage.Layout AddOrderMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.AddOrderMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AddOrderMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderModifyMessage.Layout OrderModifyMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderModifyMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderModifyMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A displayed order that was removed from the IEX Book
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderDeleteMessage.Layout OrderDeleteMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderDeleteMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderDeleteMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A displayed order that was executed against
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderExecutedMessage.Layout OrderExecutedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderExecutedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderExecutedMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A non-displayed order on the book that executed against another non-displayed order on the book
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeMessage.Layout TradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeBreakMessage.Layout TradeBreakMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeBreakMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeBreakMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is used to indicate that the IEX Book for a symbol has been cleared of all orders
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ClearBookMessage.Layout ClearBookMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ClearBookMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ClearBookMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Message Data fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityDirectoryMessage.Value securityDirectoryMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradingStatusMessage.Value tradingStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.RetailLiquidityIndicatorMessage.Value retailLiquidityIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OperationalHaltStatusMessage.Value operationalHaltStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusMessage.Value shortSalePriceTestStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityEventMessage.Value securityEventMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.AddOrderMessage.Value addOrderMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderModifyMessage.Value orderModifyMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderDeleteMessage.Value orderDeleteMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderExecutedMessage.Value orderExecutedMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeMessage.Value tradeMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeBreakMessage.Value tradeBreakMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ClearBookMessage.Value clearBookMessage;
        }

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityDirectoryMessage.Value SecurityDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.securityDirectoryMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityDirectoryMessage;
            }
        }

        /// <summary>
        ///  The Trading Status Message is used to indicate the current trading status of a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradingStatusMessage.Value TradingStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.tradingStatusMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradingStatusMessage;
            }
        }

        /// <summary>
        ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.RetailLiquidityIndicatorMessage.Value RetailLiquidityIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.retailLiquidityIndicatorMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.RetailLiquidityIndicatorMessage;
            }
        }

        /// <summary>
        ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OperationalHaltStatusMessage.Value OperationalHaltStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.operationalHaltStatusMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OperationalHaltStatusMessage;
            }
        }

        /// <summary>
        ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusMessage.Value ShortSalePriceTestStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.shortSalePriceTestStatusMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ShortSalePriceTestStatusMessage;
            }
        }

        /// <summary>
        ///  The Security Event Message is used to indicate events that apply to a security
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityEventMessage.Value SecurityEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.securityEventMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityEventMessage;
            }
        }

        /// <summary>
        ///  A displayed order that has been added to the IEX Book
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.AddOrderMessage.Value AddOrderMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.addOrderMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.AddOrderMessage;
            }
        }

        /// <summary>
        ///  A displayed order that had its Price, Size, or Priority component changed as a result of user or system action
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderModifyMessage.Value OrderModifyMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.orderModifyMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderModifyMessage;
            }
        }

        /// <summary>
        ///  A displayed order that was removed from the IEX Book
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderDeleteMessage.Value OrderDeleteMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.orderDeleteMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderDeleteMessage;
            }
        }

        /// <summary>
        ///  A displayed order that was executed against
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderExecutedMessage.Value OrderExecutedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.orderExecutedMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderExecutedMessage;
            }
        }

        /// <summary>
        ///  A non-displayed order on the book that executed against another non-displayed order on the book
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeMessage.Value TradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.tradeMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeMessage;
            }
        }

        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeBreakMessage.Value TradeBreakMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.tradeBreakMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeBreakMessage;
            }
        }

        /// <summary>
        ///  This message is used to indicate that the IEX Book for a symbol has been cleared of all orders
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ClearBookMessage.Value ClearBookMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterTwoCases, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
                this.caseStorage.clearBookMessage = value;
                this.@case = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ClearBookMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Security Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SecurityDirectoryMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.SecurityDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trading Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradingStatusMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.TradingStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Retail Liquidity Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RetailLiquidityIndicatorMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.RetailLiquidityIndicatorMessage];

        /// <summary>
        ///  The value in the bytes that selects the Operational Halt Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OperationalHaltStatusMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.OperationalHaltStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Short Sale Price Test Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ShortSalePriceTestStatusMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.ShortSalePriceTestStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Security Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SecurityEventMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.SecurityEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Add Order Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AddOrderMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.AddOrderMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Modify Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderModifyMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.OrderModifyMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Delete Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderDeleteMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.OrderDeleteMessage];

        /// <summary>
        ///  The value in the bytes that selects the Order Executed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.OrderExecutedMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.TradeMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Break Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeBreakMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.TradeBreakMessage];

        /// <summary>
        ///  The value in the bytes that selects the Clear Book Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ClearBookMessageMessageType => [(byte)global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageType.ClearBookMessage];

        /// <summary>
        ///  Creates the value of the Message Data, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int SystemEventMessageCase = 1;
    private const int SecurityDirectoryMessageCase = 2;
    private const int TradingStatusMessageCase = 3;
    private const int RetailLiquidityIndicatorMessageCase = 4;
    private const int OperationalHaltStatusMessageCase = 5;
    private const int ShortSalePriceTestStatusMessageCase = 6;
    private const int SecurityEventMessageCase = 7;
    private const int AddOrderMessageCase = 8;
    private const int OrderModifyMessageCase = 9;
    private const int OrderDeleteMessageCase = 10;
    private const int OrderExecutedMessageCase = 11;
    private const int TradeMessageCase = 12;
    private const int TradeBreakMessageCase = 13;
    private const int ClearBookMessageCase = 14;

    /// <summary>
    ///  Writes the Message Data to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Message Data.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterCursor cursor, int start, scoped ref int messageDataWriterCaseRecord, scoped in Value value)
    {
        int messageDataWriterCase = global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent;
        if (value.@case is not (global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SystemEventMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityDirectoryMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradingStatusMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.RetailLiquidityIndicatorMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OperationalHaltStatusMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ShortSalePriceTestStatusMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityEventMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.AddOrderMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderModifyMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderDeleteMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderExecutedMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeBreakMessage or global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ClearBookMessage)) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SystemEventMessage)
        {
            messageDataWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityDirectoryMessage)
        {
            messageDataWriterCase = SecurityDirectoryMessageCase;
            int securityDirectoryMessageWriting = cursor.Index;
            var securityDirectoryMessageWriteValue = value.caseStorage.securityDirectoryMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityDirectoryMessage.Write(bytes, ref cursor, securityDirectoryMessageWriting, in securityDirectoryMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradingStatusMessage)
        {
            messageDataWriterCase = TradingStatusMessageCase;
            int tradingStatusMessageWriting = cursor.Index;
            var tradingStatusMessageWriteValue = value.caseStorage.tradingStatusMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradingStatusMessage.Write(bytes, ref cursor, tradingStatusMessageWriting, in tradingStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.RetailLiquidityIndicatorMessage)
        {
            messageDataWriterCase = RetailLiquidityIndicatorMessageCase;
            int retailLiquidityIndicatorMessageWriting = cursor.Index;
            var retailLiquidityIndicatorMessageWriteValue = value.caseStorage.retailLiquidityIndicatorMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.RetailLiquidityIndicatorMessage.Write(bytes, ref cursor, retailLiquidityIndicatorMessageWriting, in retailLiquidityIndicatorMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OperationalHaltStatusMessage)
        {
            messageDataWriterCase = OperationalHaltStatusMessageCase;
            int operationalHaltStatusMessageWriting = cursor.Index;
            var operationalHaltStatusMessageWriteValue = value.caseStorage.operationalHaltStatusMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OperationalHaltStatusMessage.Write(bytes, ref cursor, operationalHaltStatusMessageWriting, in operationalHaltStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ShortSalePriceTestStatusMessage)
        {
            messageDataWriterCase = ShortSalePriceTestStatusMessageCase;
            int shortSalePriceTestStatusMessageWriting = cursor.Index;
            var shortSalePriceTestStatusMessageWriteValue = value.caseStorage.shortSalePriceTestStatusMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ShortSalePriceTestStatusMessage.Write(bytes, ref cursor, shortSalePriceTestStatusMessageWriting, in shortSalePriceTestStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.SecurityEventMessage)
        {
            messageDataWriterCase = SecurityEventMessageCase;
            int securityEventMessageWriting = cursor.Index;
            var securityEventMessageWriteValue = value.caseStorage.securityEventMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.SecurityEventMessage.Write(bytes, ref cursor, securityEventMessageWriting, in securityEventMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.AddOrderMessage)
        {
            messageDataWriterCase = AddOrderMessageCase;
            int addOrderMessageWriting = cursor.Index;
            var addOrderMessageWriteValue = value.caseStorage.addOrderMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.AddOrderMessage.Write(bytes, ref cursor, addOrderMessageWriting, in addOrderMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderModifyMessage)
        {
            messageDataWriterCase = OrderModifyMessageCase;
            int orderModifyMessageWriting = cursor.Index;
            var orderModifyMessageWriteValue = value.caseStorage.orderModifyMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderModifyMessage.Write(bytes, ref cursor, orderModifyMessageWriting, in orderModifyMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderDeleteMessage)
        {
            messageDataWriterCase = OrderDeleteMessageCase;
            int orderDeleteMessageWriting = cursor.Index;
            var orderDeleteMessageWriteValue = value.caseStorage.orderDeleteMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderDeleteMessage.Write(bytes, ref cursor, orderDeleteMessageWriting, in orderDeleteMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.OrderExecutedMessage)
        {
            messageDataWriterCase = OrderExecutedMessageCase;
            int orderExecutedMessageWriting = cursor.Index;
            var orderExecutedMessageWriteValue = value.caseStorage.orderExecutedMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.OrderExecutedMessage.Write(bytes, ref cursor, orderExecutedMessageWriting, in orderExecutedMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeMessage)
        {
            messageDataWriterCase = TradeMessageCase;
            int tradeMessageWriting = cursor.Index;
            var tradeMessageWriteValue = value.caseStorage.tradeMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeMessage.Write(bytes, ref cursor, tradeMessageWriting, in tradeMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.TradeBreakMessage)
        {
            messageDataWriterCase = TradeBreakMessageCase;
            int tradeBreakMessageWriting = cursor.Index;
            var tradeBreakMessageWriteValue = value.caseStorage.tradeBreakMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.TradeBreakMessage.Write(bytes, ref cursor, tradeBreakMessageWriting, in tradeBreakMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.MessageData.Case.ClearBookMessage)
        {
            messageDataWriterCase = ClearBookMessageCase;
            int clearBookMessageWriting = cursor.Index;
            var clearBookMessageWriteValue = value.caseStorage.clearBookMessage;
            global::Iex.IexEquities.DeepPlus.IexTp.v1_01.ClearBookMessage.Write(bytes, ref cursor, clearBookMessageWriting, in clearBookMessageWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageDataWriterCaseRecord))
        {
            int recorded = messageDataWriterCase == global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent ? global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.NoCase : messageDataWriterCase;
            if (messageDataWriterCaseRecord != global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.Position.Absent && messageDataWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterRuleDisagrees, global::Iex.IexEquities.DeepPlus.IexTp.v1_01.Parser.WriterValue);
            messageDataWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
