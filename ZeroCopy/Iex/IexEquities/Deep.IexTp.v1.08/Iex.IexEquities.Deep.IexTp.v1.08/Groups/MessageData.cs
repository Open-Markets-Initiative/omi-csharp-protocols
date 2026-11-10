using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Iex.IexEquities.Deep.IexTp.v1_08;

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
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        PriceLevelBuyUpdateMessage = 8,
        /// <summary>
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        PriceLevelSellUpdateMessage = 9,
        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        TradeReportMessage = 10,
        /// <summary>
        ///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
        /// </summary>
        OfficialPriceMessage = 11,
        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        TradeBreakMessage = 12,
        /// <summary>
        ///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
        /// </summary>
        AuctionInformationMessage = 13,
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
        private readonly global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> messageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.messageTypeValue = messageTypeValue;
            this.selectedCase = (global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case)Parser.Valid(Parser.SelectMessageData(this.bytes, this.start, this.end, this.start, this.messageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Message Data that the bytes select.
        /// </summary>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case Case => this.selectedCase;

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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SecurityEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PriceLevelBuyUpdateMessage.Layout PriceLevelBuyUpdateMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PriceLevelBuyUpdateMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PriceLevelSellUpdateMessage.Layout PriceLevelSellUpdateMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PriceLevelSellUpdateMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeReportMessage.Layout TradeReportMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeReportMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OfficialPriceMessage.Layout OfficialPriceMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OfficialPriceMessage.Layout>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeBreakMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AuctionInformationMessage.Layout AuctionInformationMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AuctionInformationMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Message Data fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Value securityDirectoryMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Value tradingStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Value retailLiquidityIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Value operationalHaltStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Value shortSalePriceTestStatusMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Value securityEventMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Value priceLevelBuyUpdateMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Value priceLevelSellUpdateMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Value tradeReportMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Value officialPriceMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Value tradeBreakMessage;
            [FieldOffset(CaseStart)] internal global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Value auctionInformationMessage;
        }

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  The System Event Message is used to indicate events that apply to the market or the data feed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Value SecurityDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.securityDirectoryMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage;
            }
        }

        /// <summary>
        ///  The Trading Status Message is used to indicate the current trading status of a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Value TradingStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.tradingStatusMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage;
            }
        }

        /// <summary>
        ///  broadcasts a real-time Retail Liquidity Indicator Message each time there is an update to IEX's eligible retail liquidity interest during the trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Value RetailLiquidityIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.retailLiquidityIndicatorMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage;
            }
        }

        /// <summary>
        ///  The Exchange may suspend trading of one or more securities on IEX for operational reasons and indicates such operational halt using the Operational Halt Status Message.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Value OperationalHaltStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.operationalHaltStatusMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage;
            }
        }

        /// <summary>
        ///  The Short Sale Price Test Message is used to indicate when a short sale price test restriction is in effect for a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Value ShortSalePriceTestStatusMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.shortSalePriceTestStatusMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage;
            }
        }

        /// <summary>
        ///  The Security Event Message is used to indicate events that apply to a security
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Value SecurityEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.securityEventMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage;
            }
        }

        /// <summary>
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Value PriceLevelBuyUpdateMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.priceLevelBuyUpdateMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage;
            }
        }

        /// <summary>
        ///  Deep broadcasts a real-time Price Level Update Message each time a displayed price level on IEX is updated during the trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Value PriceLevelSellUpdateMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.priceLevelSellUpdateMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage;
            }
        }

        /// <summary>
        ///  Trade Report Messages are sent when an order on the IEX Order Book is executed in whole or in part
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Value TradeReportMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.tradeReportMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage;
            }
        }

        /// <summary>
        ///  Official Price Messages are sent for each IEX-listed security to indicate the IEX Official Opening Price and IEX Official Closing Price.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Value OfficialPriceMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.officialPriceMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage;
            }
        }

        /// <summary>
        ///  Trade Break Messages are sent when an execution on IEX is broken on that same trading day
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Value TradeBreakMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.tradeBreakMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage;
            }
        }

        /// <summary>
        ///  Broadcasts an Auction Information Message every one second between the Lock-in Time and the auction match for Opening and Closing Auctions, and during the Display Only Period for IPO, Halt, and Volatility Auctions.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Value AuctionInformationMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterTwoCases, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
                this.caseStorage.auctionInformationMessage = value;
                this.@case = global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.SystemEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Security Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SecurityDirectoryMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.SecurityDirectoryMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trading Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradingStatusMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.TradingStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Retail Liquidity Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RetailLiquidityIndicatorMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.RetailLiquidityIndicatorMessage];

        /// <summary>
        ///  The value in the bytes that selects the Operational Halt Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OperationalHaltStatusMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.OperationalHaltStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Short Sale Price Test Status Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> ShortSalePriceTestStatusMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.ShortSalePriceTestStatusMessage];

        /// <summary>
        ///  The value in the bytes that selects the Security Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SecurityEventMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.SecurityEventMessage];

        /// <summary>
        ///  The value in the bytes that selects the Price Level Buy Update Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> PriceLevelBuyUpdateMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.PriceLevelBuyUpdateMessage];

        /// <summary>
        ///  The value in the bytes that selects the Price Level Sell Update Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> PriceLevelSellUpdateMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.PriceLevelSellUpdateMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Report Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeReportMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.TradeReportMessage];

        /// <summary>
        ///  The value in the bytes that selects the Official Price Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OfficialPriceMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.OfficialPriceMessage];

        /// <summary>
        ///  The value in the bytes that selects the Trade Break Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> TradeBreakMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.TradeBreakMessage];

        /// <summary>
        ///  The value in the bytes that selects the Auction Information Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AuctionInformationMessageMessageType => [(byte)global::Iex.IexEquities.Deep.IexTp.v1_08.MessageType.AuctionInformationMessage];

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
    private const int PriceLevelBuyUpdateMessageCase = 8;
    private const int PriceLevelSellUpdateMessageCase = 9;
    private const int TradeReportMessageCase = 10;
    private const int OfficialPriceMessageCase = 11;
    private const int TradeBreakMessageCase = 12;
    private const int AuctionInformationMessageCase = 13;

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
        var cursor = new global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterCursor cursor, int start, scoped ref int messageDataWriterCaseRecord, scoped in Value value)
    {
        int messageDataWriterCase = global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Absent;
        if (value.@case is not (global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage or global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage)) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SystemEventMessage)
        {
            messageDataWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityDirectoryMessage)
        {
            messageDataWriterCase = SecurityDirectoryMessageCase;
            int securityDirectoryMessageWriting = cursor.Index;
            var securityDirectoryMessageWriteValue = value.caseStorage.securityDirectoryMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityDirectoryMessage.Write(bytes, ref cursor, securityDirectoryMessageWriting, in securityDirectoryMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradingStatusMessage)
        {
            messageDataWriterCase = TradingStatusMessageCase;
            int tradingStatusMessageWriting = cursor.Index;
            var tradingStatusMessageWriteValue = value.caseStorage.tradingStatusMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.TradingStatusMessage.Write(bytes, ref cursor, tradingStatusMessageWriting, in tradingStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.RetailLiquidityIndicatorMessage)
        {
            messageDataWriterCase = RetailLiquidityIndicatorMessageCase;
            int retailLiquidityIndicatorMessageWriting = cursor.Index;
            var retailLiquidityIndicatorMessageWriteValue = value.caseStorage.retailLiquidityIndicatorMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.RetailLiquidityIndicatorMessage.Write(bytes, ref cursor, retailLiquidityIndicatorMessageWriting, in retailLiquidityIndicatorMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OperationalHaltStatusMessage)
        {
            messageDataWriterCase = OperationalHaltStatusMessageCase;
            int operationalHaltStatusMessageWriting = cursor.Index;
            var operationalHaltStatusMessageWriteValue = value.caseStorage.operationalHaltStatusMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.OperationalHaltStatusMessage.Write(bytes, ref cursor, operationalHaltStatusMessageWriting, in operationalHaltStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.ShortSalePriceTestStatusMessage)
        {
            messageDataWriterCase = ShortSalePriceTestStatusMessageCase;
            int shortSalePriceTestStatusMessageWriting = cursor.Index;
            var shortSalePriceTestStatusMessageWriteValue = value.caseStorage.shortSalePriceTestStatusMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.ShortSalePriceTestStatusMessage.Write(bytes, ref cursor, shortSalePriceTestStatusMessageWriting, in shortSalePriceTestStatusMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.SecurityEventMessage)
        {
            messageDataWriterCase = SecurityEventMessageCase;
            int securityEventMessageWriting = cursor.Index;
            var securityEventMessageWriteValue = value.caseStorage.securityEventMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.SecurityEventMessage.Write(bytes, ref cursor, securityEventMessageWriting, in securityEventMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelBuyUpdateMessage)
        {
            messageDataWriterCase = PriceLevelBuyUpdateMessageCase;
            int priceLevelBuyUpdateMessageWriting = cursor.Index;
            var priceLevelBuyUpdateMessageWriteValue = value.caseStorage.priceLevelBuyUpdateMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelBuyUpdateMessage.Write(bytes, ref cursor, priceLevelBuyUpdateMessageWriting, in priceLevelBuyUpdateMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.PriceLevelSellUpdateMessage)
        {
            messageDataWriterCase = PriceLevelSellUpdateMessageCase;
            int priceLevelSellUpdateMessageWriting = cursor.Index;
            var priceLevelSellUpdateMessageWriteValue = value.caseStorage.priceLevelSellUpdateMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.PriceLevelSellUpdateMessage.Write(bytes, ref cursor, priceLevelSellUpdateMessageWriting, in priceLevelSellUpdateMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeReportMessage)
        {
            messageDataWriterCase = TradeReportMessageCase;
            int tradeReportMessageWriting = cursor.Index;
            var tradeReportMessageWriteValue = value.caseStorage.tradeReportMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.TradeReportMessage.Write(bytes, ref cursor, tradeReportMessageWriting, in tradeReportMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.OfficialPriceMessage)
        {
            messageDataWriterCase = OfficialPriceMessageCase;
            int officialPriceMessageWriting = cursor.Index;
            var officialPriceMessageWriteValue = value.caseStorage.officialPriceMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.OfficialPriceMessage.Write(bytes, ref cursor, officialPriceMessageWriting, in officialPriceMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.TradeBreakMessage)
        {
            messageDataWriterCase = TradeBreakMessageCase;
            int tradeBreakMessageWriting = cursor.Index;
            var tradeBreakMessageWriteValue = value.caseStorage.tradeBreakMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.TradeBreakMessage.Write(bytes, ref cursor, tradeBreakMessageWriting, in tradeBreakMessageWriteValue);
        }
        if (value.@case == global::Iex.IexEquities.Deep.IexTp.v1_08.MessageData.Case.AuctionInformationMessage)
        {
            messageDataWriterCase = AuctionInformationMessageCase;
            int auctionInformationMessageWriting = cursor.Index;
            var auctionInformationMessageWriteValue = value.caseStorage.auctionInformationMessage;
            global::Iex.IexEquities.Deep.IexTp.v1_08.AuctionInformationMessage.Write(bytes, ref cursor, auctionInformationMessageWriting, in auctionInformationMessageWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref messageDataWriterCaseRecord))
        {
            int recorded = messageDataWriterCase == global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Absent ? global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.NoCase : messageDataWriterCase;
            if (messageDataWriterCaseRecord != global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.Position.Absent && messageDataWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterRuleDisagrees, global::Iex.IexEquities.Deep.IexTp.v1_08.Parser.WriterValue);
            messageDataWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
