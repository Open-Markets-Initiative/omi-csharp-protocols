using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023;

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
        ///  The system event message type is used to signal a market or data feed handler event
        /// </summary>
        SystemEventMessage = 1,
        /// <summary>
        ///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
        /// </summary>
        StockDirectoryMessage = 2,
        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community
        /// </summary>
        StockTradingActionMessage = 3,
        /// <summary>
        ///  Nasaq processes orders based on the most Reg SHO Restriction status value
        /// </summary>
        RegShoShortSalePriceTestRestrictedIndicatorMessage = 4,
        /// <summary>
        ///  Nasdaq will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue
        /// </summary>
        MarketParticipantPositionMessage = 5,
        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        MwcbDeclineLevelMessage = 6,
        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        MwcbStatusLevelMessage = 7,
        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        IpoQuotingPeriodUpdate = 8,
        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD trading pause
        /// </summary>
        LuldAuctionCollarMessage = 9,
        /// <summary>
        ///  An Operational Halt means that there has been an interruption of service on the identified security impacting only the designated Market Center
        /// </summary>
        OperationalHaltMessage = 10,
        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq system
        /// </summary>
        AddOrderNoMpidAttributionMessage = 11,
        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq system
        /// </summary>
        AddOrderWithMpidAttributionMessage = 12,
        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part
        /// </summary>
        OrderExecutedMessage = 13,
        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price
        /// </summary>
        OrderExecutedWithPriceMessage = 14,
        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        OrderCancelMessage = 15,
        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled
        /// </summary>
        OrderDeleteMessage = 16,
        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced
        /// </summary>
        OrderReplaceMessage = 17,
        /// <summary>
        ///  Trade Messages should be included in NASDAQ time-and-sales displays as well as volume and other market statistics
        /// </summary>
        NonCrossTradeMessage = 18,
        /// <summary>
        ///  Cross Trade message indicates that Nasdaq has completed its cross process for a specific security
        /// </summary>
        CrossTradeMessage = 19,
        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq is broken
        /// </summary>
        BrokenTradeMessage = 20,
        /// <summary>
        ///  NASDAQ disseminates NOII data
        /// </summary>
        NetOrderImbalanceIndicatorMessage = 21,
        /// <summary>
        ///  Identifies a retail interest indication of the Bid, Ask or both the Bid and Ask for NASDAQ-listed securities.
        /// </summary>
        RetailPriceImprovementIndicatorMessage = 22,
        /// <summary>
        ///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
        /// </summary>
        DirectListingWithCapitalRaisePriceDiscoveryMessage = 23,
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
        private readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, ReadOnlySpan<byte> sequencedMessageTypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.sequencedMessageTypeValue = sequencedMessageTypeValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case)Parser.Valid(Parser.SelectSequencedMessage(this.bytes, this.start, this.end, this.start, this.sequencedMessageTypeValue));
        }

        /// <summary>
        ///  Gets the case of the Sequenced Message that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Sequenced Message.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..this.end];

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemEventMessage.Layout SystemEventMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemEventMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockDirectoryMessage.Layout StockDirectoryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockDirectoryMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StockTradingActionMessage.Layout StockTradingActionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StockTradingActionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasaq processes orders based on the most Reg SHO Restriction status value
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout RegShoShortSalePriceTestRestrictedIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RegShoShortSalePriceTestRestrictedIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Nasdaq will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MarketParticipantPositionMessage.Layout MarketParticipantPositionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MarketParticipantPositionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MwcbDeclineLevelMessage.Layout MwcbDeclineLevelMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbDeclineLevelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MwcbStatusLevelMessage.Layout MwcbStatusLevelMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MwcbStatusLevelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly IpoQuotingPeriodUpdate.Layout IpoQuotingPeriodUpdate
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<IpoQuotingPeriodUpdate.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD trading pause
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LuldAuctionCollarMessage.Layout LuldAuctionCollarMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LuldAuctionCollarMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  An Operational Halt means that there has been an interruption of service on the identified security impacting only the designated Market Center
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OperationalHaltMessage.Layout OperationalHaltMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OperationalHaltMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq system
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AddOrderNoMpidAttributionMessage.Layout AddOrderNoMpidAttributionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AddOrderNoMpidAttributionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq system
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AddOrderWithMpidAttributionMessage.Layout AddOrderWithMpidAttributionMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AddOrderWithMpidAttributionMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderExecutedMessage.Layout OrderExecutedMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderExecutedMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderExecutedWithPriceMessage.Layout OrderExecutedWithPriceMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderExecutedWithPriceMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderCancelMessage.Layout OrderCancelMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderCancelMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderDeleteMessage.Layout OrderDeleteMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderDeleteMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OrderReplaceMessage.Layout OrderReplaceMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OrderReplaceMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Trade Messages should be included in NASDAQ time-and-sales displays as well as volume and other market statistics
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly NonCrossTradeMessage.Layout NonCrossTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NonCrossTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Cross Trade message indicates that Nasdaq has completed its cross process for a specific security
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly CrossTradeMessage.Layout CrossTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<CrossTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq is broken
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly BrokenTradeMessage.Layout BrokenTradeMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BrokenTradeMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  NASDAQ disseminates NOII data
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly NetOrderImbalanceIndicatorMessage.Layout NetOrderImbalanceIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<NetOrderImbalanceIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Identifies a retail interest indication of the Bid, Ask or both the Bid and Ask for NASDAQ-listed securities.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RetailPriceImprovementIndicatorMessage.Layout RetailPriceImprovementIndicatorMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RetailPriceImprovementIndicatorMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout DirectListingWithCapitalRaisePriceDiscoveryMessage
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DirectListingWithCapitalRaisePriceDiscoveryMessage.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Sequenced Message fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Value systemEventMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Value stockDirectoryMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Value stockTradingActionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Value regShoShortSalePriceTestRestrictedIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Value marketParticipantPositionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Value mwcbDeclineLevelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Value mwcbStatusLevelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Value ipoQuotingPeriodUpdate;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Value luldAuctionCollarMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Value operationalHaltMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Value addOrderNoMpidAttributionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Value addOrderWithMpidAttributionMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Value orderExecutedMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Value orderExecutedWithPriceMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Value orderCancelMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Value orderDeleteMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Value orderReplaceMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Value nonCrossTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Value crossTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Value brokenTradeMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Value netOrderImbalanceIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Value retailPriceImprovementIndicatorMessage;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Value directListingWithCapitalRaisePriceDiscoveryMessage;
        }

        /// <summary>
        ///  The system event message type is used to signal a market or data feed handler event
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Value SystemEventMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.systemEventMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage;
            }
        }

        /// <summary>
        ///  Market data redistributors should process this message to populate the Financial Status Indicator (required display field) and the Market Category (recommended display field) for NASDAQ-listed issues.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Value StockDirectoryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.stockDirectoryMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage;
            }
        }

        /// <summary>
        ///  Nasdaq uses this administrative message to indicate the current trading status of a security to the trading community
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Value StockTradingActionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.stockTradingActionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage;
            }
        }

        /// <summary>
        ///  Nasaq processes orders based on the most Reg SHO Restriction status value
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Value RegShoShortSalePriceTestRestrictedIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.regShoShortSalePriceTestRestrictedIndicatorMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage;
            }
        }

        /// <summary>
        ///  Nasdaq will send out this message only if Nasdaq Operations changes the status of a market participant firm in an issue
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Value MarketParticipantPositionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.marketParticipantPositionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients what the daily MWCB breach points are set to for the current trading day.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Value MwcbDeclineLevelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.mwcbDeclineLevelMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage;
            }
        }

        /// <summary>
        ///  Informs data recipients when a MWCB has breached one of the established levels
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Value MwcbStatusLevelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.mwcbStatusLevelMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage;
            }
        }

        /// <summary>
        ///  Indicates the anticipated IPO quotation release time of a security.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Value IpoQuotingPeriodUpdate
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.ipoQuotingPeriodUpdate = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate;
            }
        }

        /// <summary>
        ///  Indicates the auction collar thresholds within which a paused security can reopen following a LULD trading pause
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Value LuldAuctionCollarMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.luldAuctionCollarMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage;
            }
        }

        /// <summary>
        ///  An Operational Halt means that there has been an interruption of service on the identified security impacting only the designated Market Center
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Value OperationalHaltMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.operationalHaltMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage;
            }
        }

        /// <summary>
        ///  This message will be generated for unattributed orders accepted by the Nasdaq system
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Value AddOrderNoMpidAttributionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.addOrderNoMpidAttributionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage;
            }
        }

        /// <summary>
        ///  This message will be generated for attributed orders and quotations accepted by the Nasdaq system
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Value AddOrderWithMpidAttributionMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.addOrderWithMpidAttributionMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Value OrderExecutedMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.orderExecutedMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is executed in whole or in part at a price different from the initial display price
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Value OrderExecutedWithPriceMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.orderExecutedWithPriceMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is modified as a result of a partial cancellation
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Value OrderCancelMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.orderCancelMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book is being cancelled
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Value OrderDeleteMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.orderDeleteMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage;
            }
        }

        /// <summary>
        ///  This message is sent whenever an order on the book has been cancel-replaced
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Value OrderReplaceMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.orderReplaceMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage;
            }
        }

        /// <summary>
        ///  Trade Messages should be included in NASDAQ time-and-sales displays as well as volume and other market statistics
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Value NonCrossTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.nonCrossTradeMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage;
            }
        }

        /// <summary>
        ///  Cross Trade message indicates that Nasdaq has completed its cross process for a specific security
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Value CrossTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.crossTradeMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage;
            }
        }

        /// <summary>
        ///  The Broken Trade Message is sent whenever an execution on Nasdaq is broken
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Value BrokenTradeMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.brokenTradeMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage;
            }
        }

        /// <summary>
        ///  NASDAQ disseminates NOII data
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Value NetOrderImbalanceIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.netOrderImbalanceIndicatorMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage;
            }
        }

        /// <summary>
        ///  Identifies a retail interest indication of the Bid, Ask or both the Bid and Ask for NASDAQ-listed securities.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Value RetailPriceImprovementIndicatorMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.retailPriceImprovementIndicatorMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage;
            }
        }

        /// <summary>
        ///  This message is disseminated only for Direct Listing with Capital Raise (DLCR) securities
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Value DirectListingWithCapitalRaisePriceDiscoveryMessage
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
                this.caseStorage.directListingWithCapitalRaisePriceDiscoveryMessage = value;
                this.@case = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the System Event Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> SystemEventMessageSequencedMessageType => "S"u8;

        /// <summary>
        ///  The value in the bytes that selects the Stock Directory Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockDirectoryMessageSequencedMessageType => "R"u8;

        /// <summary>
        ///  The value in the bytes that selects the Stock Trading Action Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> StockTradingActionMessageSequencedMessageType => "H"u8;

        /// <summary>
        ///  The value in the bytes that selects the Reg Sho Short Sale Price Test Restricted Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RegShoShortSalePriceTestRestrictedIndicatorMessageSequencedMessageType => "Y"u8;

        /// <summary>
        ///  The value in the bytes that selects the Market Participant Position Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MarketParticipantPositionMessageSequencedMessageType => "L"u8;

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Decline Level Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbDeclineLevelMessageSequencedMessageType => "V"u8;

        /// <summary>
        ///  The value in the bytes that selects the Mwcb Status Level Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> MwcbStatusLevelMessageSequencedMessageType => "W"u8;

        /// <summary>
        ///  The value in the bytes that selects the Ipo Quoting Period Update.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> IpoQuotingPeriodUpdateSequencedMessageType => "K"u8;

        /// <summary>
        ///  The value in the bytes that selects the Luld Auction Collar Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> LuldAuctionCollarMessageSequencedMessageType => "J"u8;

        /// <summary>
        ///  The value in the bytes that selects the Operational Halt Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OperationalHaltMessageSequencedMessageType => "h"u8;

        /// <summary>
        ///  The value in the bytes that selects the Add Order No Mpid Attribution Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AddOrderNoMpidAttributionMessageSequencedMessageType => "A"u8;

        /// <summary>
        ///  The value in the bytes that selects the Add Order With Mpid Attribution Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> AddOrderWithMpidAttributionMessageSequencedMessageType => "F"u8;

        /// <summary>
        ///  The value in the bytes that selects the Order Executed Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedMessageSequencedMessageType => "E"u8;

        /// <summary>
        ///  The value in the bytes that selects the Order Executed With Price Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderExecutedWithPriceMessageSequencedMessageType => "C"u8;

        /// <summary>
        ///  The value in the bytes that selects the Order Cancel Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderCancelMessageSequencedMessageType => "X"u8;

        /// <summary>
        ///  The value in the bytes that selects the Order Delete Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderDeleteMessageSequencedMessageType => "D"u8;

        /// <summary>
        ///  The value in the bytes that selects the Order Replace Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> OrderReplaceMessageSequencedMessageType => "U"u8;

        /// <summary>
        ///  The value in the bytes that selects the Non Cross Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> NonCrossTradeMessageSequencedMessageType => "P"u8;

        /// <summary>
        ///  The value in the bytes that selects the Cross Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> CrossTradeMessageSequencedMessageType => "Q"u8;

        /// <summary>
        ///  The value in the bytes that selects the Broken Trade Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> BrokenTradeMessageSequencedMessageType => "B"u8;

        /// <summary>
        ///  The value in the bytes that selects the Net Order Imbalance Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> NetOrderImbalanceIndicatorMessageSequencedMessageType => "I"u8;

        /// <summary>
        ///  The value in the bytes that selects the Retail Price Improvement Indicator Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> RetailPriceImprovementIndicatorMessageSequencedMessageType => "N"u8;

        /// <summary>
        ///  The value in the bytes that selects the Direct Listing With Capital Raise Price Discovery Message.
        /// </summary>
        public static global::System.ReadOnlySpan<byte> DirectListingWithCapitalRaisePriceDiscoveryMessageSequencedMessageType => "O"u8;

        /// <summary>
        ///  Creates the value of the Sequenced Message, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int packetLengthDependencyWriteDistanceFromSequencedMessage = -4;
    private const int SystemEventMessageCase = 1;
    private const int StockDirectoryMessageCase = 2;
    private const int StockTradingActionMessageCase = 3;
    private const int RegShoShortSalePriceTestRestrictedIndicatorMessageCase = 4;
    private const int MarketParticipantPositionMessageCase = 5;
    private const int MwcbDeclineLevelMessageCase = 6;
    private const int MwcbStatusLevelMessageCase = 7;
    private const int IpoQuotingPeriodUpdateCase = 8;
    private const int LuldAuctionCollarMessageCase = 9;
    private const int OperationalHaltMessageCase = 10;
    private const int AddOrderNoMpidAttributionMessageCase = 11;
    private const int AddOrderWithMpidAttributionMessageCase = 12;
    private const int OrderExecutedMessageCase = 13;
    private const int OrderExecutedWithPriceMessageCase = 14;
    private const int OrderCancelMessageCase = 15;
    private const int OrderDeleteMessageCase = 16;
    private const int OrderReplaceMessageCase = 17;
    private const int NonCrossTradeMessageCase = 18;
    private const int CrossTradeMessageCase = 19;
    private const int BrokenTradeMessageCase = 20;
    private const int NetOrderImbalanceIndicatorMessageCase = 21;
    private const int RetailPriceImprovementIndicatorMessageCase = 22;
    private const int DirectListingWithCapitalRaisePriceDiscoveryMessageCase = 23;

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
        var cursor = new global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<bool>(), ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterCursor cursor, int start, scoped ref bool packetLengthDependencyWriteUsed, scoped ref int sequencedMessageWriterCaseRecord, scoped in Value value)
    {
        int sequencedMessageWriterCase = global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage or global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.SystemEventMessage)
        {
            sequencedMessageWriterCase = SystemEventMessageCase;
            int systemEventMessageWriting = cursor.Index;
            var systemEventMessageWriteValue = value.caseStorage.systemEventMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SystemEventMessage.Write(bytes, ref cursor, systemEventMessageWriting, in systemEventMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockDirectoryMessage)
        {
            sequencedMessageWriterCase = StockDirectoryMessageCase;
            int stockDirectoryMessageWriting = cursor.Index;
            var stockDirectoryMessageWriteValue = value.caseStorage.stockDirectoryMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockDirectoryMessage.Write(bytes, ref cursor, stockDirectoryMessageWriting, in stockDirectoryMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.StockTradingActionMessage)
        {
            sequencedMessageWriterCase = StockTradingActionMessageCase;
            int stockTradingActionMessageWriting = cursor.Index;
            var stockTradingActionMessageWriteValue = value.caseStorage.stockTradingActionMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.StockTradingActionMessage.Write(bytes, ref cursor, stockTradingActionMessageWriting, in stockTradingActionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RegShoShortSalePriceTestRestrictedIndicatorMessage)
        {
            sequencedMessageWriterCase = RegShoShortSalePriceTestRestrictedIndicatorMessageCase;
            int regShoShortSalePriceTestRestrictedIndicatorMessageWriting = cursor.Index;
            var regShoShortSalePriceTestRestrictedIndicatorMessageWriteValue = value.caseStorage.regShoShortSalePriceTestRestrictedIndicatorMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RegShoShortSalePriceTestRestrictedIndicatorMessage.Write(bytes, ref cursor, regShoShortSalePriceTestRestrictedIndicatorMessageWriting, in regShoShortSalePriceTestRestrictedIndicatorMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MarketParticipantPositionMessage)
        {
            sequencedMessageWriterCase = MarketParticipantPositionMessageCase;
            int marketParticipantPositionMessageWriting = cursor.Index;
            var marketParticipantPositionMessageWriteValue = value.caseStorage.marketParticipantPositionMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MarketParticipantPositionMessage.Write(bytes, ref cursor, marketParticipantPositionMessageWriting, in marketParticipantPositionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbDeclineLevelMessage)
        {
            sequencedMessageWriterCase = MwcbDeclineLevelMessageCase;
            int mwcbDeclineLevelMessageWriting = cursor.Index;
            var mwcbDeclineLevelMessageWriteValue = value.caseStorage.mwcbDeclineLevelMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbDeclineLevelMessage.Write(bytes, ref cursor, mwcbDeclineLevelMessageWriting, in mwcbDeclineLevelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.MwcbStatusLevelMessage)
        {
            sequencedMessageWriterCase = MwcbStatusLevelMessageCase;
            int mwcbStatusLevelMessageWriting = cursor.Index;
            var mwcbStatusLevelMessageWriteValue = value.caseStorage.mwcbStatusLevelMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.MwcbStatusLevelMessage.Write(bytes, ref cursor, mwcbStatusLevelMessageWriting, in mwcbStatusLevelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.IpoQuotingPeriodUpdate)
        {
            sequencedMessageWriterCase = IpoQuotingPeriodUpdateCase;
            int ipoQuotingPeriodUpdateWriting = cursor.Index;
            var ipoQuotingPeriodUpdateWriteValue = value.caseStorage.ipoQuotingPeriodUpdate;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.IpoQuotingPeriodUpdate.Write(bytes, ref cursor, ipoQuotingPeriodUpdateWriting, in ipoQuotingPeriodUpdateWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.LuldAuctionCollarMessage)
        {
            sequencedMessageWriterCase = LuldAuctionCollarMessageCase;
            int luldAuctionCollarMessageWriting = cursor.Index;
            var luldAuctionCollarMessageWriteValue = value.caseStorage.luldAuctionCollarMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.LuldAuctionCollarMessage.Write(bytes, ref cursor, luldAuctionCollarMessageWriting, in luldAuctionCollarMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OperationalHaltMessage)
        {
            sequencedMessageWriterCase = OperationalHaltMessageCase;
            int operationalHaltMessageWriting = cursor.Index;
            var operationalHaltMessageWriteValue = value.caseStorage.operationalHaltMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OperationalHaltMessage.Write(bytes, ref cursor, operationalHaltMessageWriting, in operationalHaltMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderNoMpidAttributionMessage)
        {
            sequencedMessageWriterCase = AddOrderNoMpidAttributionMessageCase;
            int addOrderNoMpidAttributionMessageWriting = cursor.Index;
            var addOrderNoMpidAttributionMessageWriteValue = value.caseStorage.addOrderNoMpidAttributionMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderNoMpidAttributionMessage.Write(bytes, ref cursor, addOrderNoMpidAttributionMessageWriting, in addOrderNoMpidAttributionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.AddOrderWithMpidAttributionMessage)
        {
            sequencedMessageWriterCase = AddOrderWithMpidAttributionMessageCase;
            int addOrderWithMpidAttributionMessageWriting = cursor.Index;
            var addOrderWithMpidAttributionMessageWriteValue = value.caseStorage.addOrderWithMpidAttributionMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.AddOrderWithMpidAttributionMessage.Write(bytes, ref cursor, addOrderWithMpidAttributionMessageWriting, in addOrderWithMpidAttributionMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedMessage)
        {
            sequencedMessageWriterCase = OrderExecutedMessageCase;
            int orderExecutedMessageWriting = cursor.Index;
            var orderExecutedMessageWriteValue = value.caseStorage.orderExecutedMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedMessage.Write(bytes, ref cursor, orderExecutedMessageWriting, in orderExecutedMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderExecutedWithPriceMessage)
        {
            sequencedMessageWriterCase = OrderExecutedWithPriceMessageCase;
            int orderExecutedWithPriceMessageWriting = cursor.Index;
            var orderExecutedWithPriceMessageWriteValue = value.caseStorage.orderExecutedWithPriceMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderExecutedWithPriceMessage.Write(bytes, ref cursor, orderExecutedWithPriceMessageWriting, in orderExecutedWithPriceMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderCancelMessage)
        {
            sequencedMessageWriterCase = OrderCancelMessageCase;
            int orderCancelMessageWriting = cursor.Index;
            var orderCancelMessageWriteValue = value.caseStorage.orderCancelMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderCancelMessage.Write(bytes, ref cursor, orderCancelMessageWriting, in orderCancelMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderDeleteMessage)
        {
            sequencedMessageWriterCase = OrderDeleteMessageCase;
            int orderDeleteMessageWriting = cursor.Index;
            var orderDeleteMessageWriteValue = value.caseStorage.orderDeleteMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderDeleteMessage.Write(bytes, ref cursor, orderDeleteMessageWriting, in orderDeleteMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.OrderReplaceMessage)
        {
            sequencedMessageWriterCase = OrderReplaceMessageCase;
            int orderReplaceMessageWriting = cursor.Index;
            var orderReplaceMessageWriteValue = value.caseStorage.orderReplaceMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.OrderReplaceMessage.Write(bytes, ref cursor, orderReplaceMessageWriting, in orderReplaceMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NonCrossTradeMessage)
        {
            sequencedMessageWriterCase = NonCrossTradeMessageCase;
            int nonCrossTradeMessageWriting = cursor.Index;
            var nonCrossTradeMessageWriteValue = value.caseStorage.nonCrossTradeMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NonCrossTradeMessage.Write(bytes, ref cursor, nonCrossTradeMessageWriting, in nonCrossTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.CrossTradeMessage)
        {
            sequencedMessageWriterCase = CrossTradeMessageCase;
            int crossTradeMessageWriting = cursor.Index;
            var crossTradeMessageWriteValue = value.caseStorage.crossTradeMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.CrossTradeMessage.Write(bytes, ref cursor, crossTradeMessageWriting, in crossTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.BrokenTradeMessage)
        {
            sequencedMessageWriterCase = BrokenTradeMessageCase;
            int brokenTradeMessageWriting = cursor.Index;
            var brokenTradeMessageWriteValue = value.caseStorage.brokenTradeMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.BrokenTradeMessage.Write(bytes, ref cursor, brokenTradeMessageWriting, in brokenTradeMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.NetOrderImbalanceIndicatorMessage)
        {
            sequencedMessageWriterCase = NetOrderImbalanceIndicatorMessageCase;
            int netOrderImbalanceIndicatorMessageWriting = cursor.Index;
            var netOrderImbalanceIndicatorMessageWriteValue = value.caseStorage.netOrderImbalanceIndicatorMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.NetOrderImbalanceIndicatorMessage.Write(bytes, ref cursor, netOrderImbalanceIndicatorMessageWriting, in netOrderImbalanceIndicatorMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.RetailPriceImprovementIndicatorMessage)
        {
            sequencedMessageWriterCase = RetailPriceImprovementIndicatorMessageCase;
            int retailPriceImprovementIndicatorMessageWriting = cursor.Index;
            var retailPriceImprovementIndicatorMessageWriteValue = value.caseStorage.retailPriceImprovementIndicatorMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.RetailPriceImprovementIndicatorMessage.Write(bytes, ref cursor, retailPriceImprovementIndicatorMessageWriting, in retailPriceImprovementIndicatorMessageWriteValue);
        }
        if (value.@case == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.SequencedMessage.Case.DirectListingWithCapitalRaisePriceDiscoveryMessage)
        {
            sequencedMessageWriterCase = DirectListingWithCapitalRaisePriceDiscoveryMessageCase;
            int directListingWithCapitalRaisePriceDiscoveryMessageWriting = cursor.Index;
            var directListingWithCapitalRaisePriceDiscoveryMessageWriteValue = value.caseStorage.directListingWithCapitalRaisePriceDiscoveryMessage;
            global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.DirectListingWithCapitalRaisePriceDiscoveryMessage.Write(bytes, ref cursor, directListingWithCapitalRaisePriceDiscoveryMessageWriting, in directListingWithCapitalRaisePriceDiscoveryMessageWriteValue);
        }
        global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.SequencedMessageWriter.SequencedMessageSizeRule0(bytes, ref cursor, start < global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Minimum ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent : start + packetLengthDependencyWriteDistanceFromSequencedMessage, ref packetLengthDependencyWriteUsed, start, cursor.Index);
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref sequencedMessageWriterCaseRecord))
        {
            int recorded = sequencedMessageWriterCase == global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent ? global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.NoCase : sequencedMessageWriterCase;
            if (sequencedMessageWriterCaseRecord != global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.Position.Absent && sequencedMessageWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.TotalView.Itch.v5_0_2023.Parser.WriterValue);
            sequencedMessageWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
