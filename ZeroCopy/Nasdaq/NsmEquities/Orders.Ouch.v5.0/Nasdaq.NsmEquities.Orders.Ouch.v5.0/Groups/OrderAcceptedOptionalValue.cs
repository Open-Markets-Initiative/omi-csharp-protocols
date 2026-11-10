using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class OrderAcceptedOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Order Accepted Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Order Accepted Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        Firm = 1,
        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        MinQty = 2,
        /// <summary>
        ///  CustomerType
        /// </summary>
        CustomerType = 3,
        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        MaxFloor = 4,
        /// <summary>
        ///  PriceType
        /// </summary>
        PriceType = 5,
        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        PegOffset = 6,
        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        DiscretionPrice = 7,
        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        DiscretionPriceType = 8,
        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        DiscretionPegOffset = 9,
        /// <summary>
        ///  PostOnly
        /// </summary>
        PostOnly = 10,
        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        RandomReserves = 11,
        /// <summary>
        ///  Route
        /// </summary>
        Route = 12,
        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        ExpireTime = 13,
        /// <summary>
        ///  TradeNow
        /// </summary>
        TradeNow = 14,
        /// <summary>
        ///  HandleInst
        /// </summary>
        HandleInst = 15,
        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        BboWeightIndicator = 16,
        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        GroupId = 17,
        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        SharesLocated = 18,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 19,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy = 20,
        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        AiqGroupId = 21,
    }

    /// <summary>
    ///  Reads the Order Accepted Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte orderAcceptedOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte orderAcceptedOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.orderAcceptedOptionalFieldValue = orderAcceptedOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case)Parser.Valid(Parser.SelectOrderAcceptedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderAcceptedOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Order Accepted Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Order Accepted Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureOrderAcceptedOptionalValue(this.bytes, this.start, this.end, this.start, this.orderAcceptedOptionalFieldValue))];

        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly Firm Firm
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Firm>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MinQty MinQty
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MinQty>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  CustomerType
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly CustomerTypeField CustomerType
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<CustomerTypeField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MaxFloor MaxFloor
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MaxFloor>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  PriceType
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PriceTypeField PriceType
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PriceTypeField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PegOffset PegOffset
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PegOffset>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DiscretionPrice DiscretionPrice
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DiscretionPrice>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DiscretionPriceTypeField DiscretionPriceType
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DiscretionPriceTypeField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly DiscretionPegOffset DiscretionPegOffset
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DiscretionPegOffset>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  PostOnly
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly PostOnlyField PostOnly
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PostOnlyField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly RandomReserves RandomReserves
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RandomReserves>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Route
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly Route Route
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Route>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ExpireTime ExpireTime
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ExpireTime>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  TradeNow
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeNowField TradeNow
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeNowField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  HandleInst
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly HandleInstField HandleInst
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<HandleInstField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly BboWeightIndicatorField BboWeightIndicator
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BboWeightIndicatorField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly GroupId GroupId
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<GroupId>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SharesLocatedField SharesLocated
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SharesLocatedField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly UserRefIdx UserRefIdx
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AiqStrategyField AiqStrategy
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AiqStrategyField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly AiqGroupId AiqGroupId
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AiqGroupId>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Order Accepted Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm firm;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty minQty;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CustomerTypeField customerType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor maxFloor;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField priceType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset pegOffset;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice discretionPrice;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField discretionPriceType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset discretionPegOffset;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField postOnly;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves randomReserves;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Route route;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime expireTime;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField tradeNow;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField handleInst;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BboWeightIndicatorField bboWeightIndicator;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId groupId;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField sharesLocated;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField aiqStrategy;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId aiqGroupId;
        }

        /// <summary>
        ///  This field should contain all caps. Firm identifier for the order entry firm. One logical OUCH account can potentially enter orders for multiple firms in a Service Bureau configuration.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm Firm
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.firm = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm;
            }
        }

        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty MinQty
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.minQty = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty;
            }
        }

        /// <summary>
        ///  CustomerType
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CustomerTypeField CustomerType
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.customerType = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType;
            }
        }

        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor MaxFloor
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.maxFloor = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor;
            }
        }

        /// <summary>
        ///  PriceType
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField PriceType
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.priceType = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType;
            }
        }

        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset PegOffset
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.pegOffset = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset;
            }
        }

        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice DiscretionPrice
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.discretionPrice = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice;
            }
        }

        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField DiscretionPriceType
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.discretionPriceType = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType;
            }
        }

        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset DiscretionPegOffset
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.discretionPegOffset = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset;
            }
        }

        /// <summary>
        ///  PostOnly
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField PostOnly
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.postOnly = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly;
            }
        }

        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves RandomReserves
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.randomReserves = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves;
            }
        }

        /// <summary>
        ///  Route
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Route Route
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.route = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route;
            }
        }

        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime ExpireTime
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.expireTime = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime;
            }
        }

        /// <summary>
        ///  TradeNow
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField TradeNow
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.tradeNow = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow;
            }
        }

        /// <summary>
        ///  HandleInst
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField HandleInst
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.handleInst = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst;
            }
        }

        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BboWeightIndicatorField BboWeightIndicator
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.bboWeightIndicator = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator;
            }
        }

        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId GroupId
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.groupId = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId;
            }
        }

        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField SharesLocated
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.sharesLocated = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated;
            }
        }

        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx UserRefIdx
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.userRefIdx = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField AiqStrategy
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.aiqStrategy = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy;
            }
        }

        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId AiqGroupId
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.aiqGroupId = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Firm.
        /// </summary>
        public const byte FirmOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.Firm;

        /// <summary>
        ///  The value in the bytes that selects the Min Qty.
        /// </summary>
        public const byte MinQtyOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.MinQty;

        /// <summary>
        ///  The value in the bytes that selects the Customer Type.
        /// </summary>
        public const byte CustomerTypeOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.CustomerType;

        /// <summary>
        ///  The value in the bytes that selects the Max Floor.
        /// </summary>
        public const byte MaxFloorOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.MaxFloor;

        /// <summary>
        ///  The value in the bytes that selects the Price Type.
        /// </summary>
        public const byte PriceTypeOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.PriceType;

        /// <summary>
        ///  The value in the bytes that selects the Peg Offset.
        /// </summary>
        public const byte PegOffsetOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.PegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price.
        /// </summary>
        public const byte DiscretionPriceOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.DiscretionPrice;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price Type.
        /// </summary>
        public const byte DiscretionPriceTypeOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.DiscretionPriceType;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Peg Offset.
        /// </summary>
        public const byte DiscretionPegOffsetOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.DiscretionPegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Post Only.
        /// </summary>
        public const byte PostOnlyOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.PostOnly;

        /// <summary>
        ///  The value in the bytes that selects the Random Reserves.
        /// </summary>
        public const byte RandomReservesOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.RandomReserves;

        /// <summary>
        ///  The value in the bytes that selects the Route.
        /// </summary>
        public const byte RouteOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.Route;

        /// <summary>
        ///  The value in the bytes that selects the Expire Time.
        /// </summary>
        public const byte ExpireTimeOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.ExpireTime;

        /// <summary>
        ///  The value in the bytes that selects the Trade Now.
        /// </summary>
        public const byte TradeNowOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.TradeNow;

        /// <summary>
        ///  The value in the bytes that selects the Handle Inst.
        /// </summary>
        public const byte HandleInstOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.HandleInst;

        /// <summary>
        ///  The value in the bytes that selects the Bbo Weight Indicator.
        /// </summary>
        public const byte BboWeightIndicatorOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.BboWeightIndicator;

        /// <summary>
        ///  The value in the bytes that selects the Group Id.
        /// </summary>
        public const byte GroupIdOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.GroupId;

        /// <summary>
        ///  The value in the bytes that selects the Shares Located.
        /// </summary>
        public const byte SharesLocatedOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.SharesLocated;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Strategy.
        /// </summary>
        public const byte AiqStrategyOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.AiqStrategy;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Group Id.
        /// </summary>
        public const byte AiqGroupIdOrderAcceptedOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalField.AiqGroupId;

        /// <summary>
        ///  Creates the value of the Order Accepted Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int OrderAcceptedOptionalValueFirmCase = 1;
    private const int OrderAcceptedOptionalValueMinQtyCase = 2;
    private const int CustomerTypeCase = 3;
    private const int OrderAcceptedOptionalValueMaxFloorCase = 4;
    private const int OrderAcceptedOptionalValuePriceTypeCase = 5;
    private const int PegOffsetCase = 6;
    private const int DiscretionPriceCase = 7;
    private const int DiscretionPriceTypeCase = 8;
    private const int DiscretionPegOffsetCase = 9;
    private const int OrderAcceptedOptionalValuePostOnlyCase = 10;
    private const int RandomReservesCase = 11;
    private const int RouteCase = 12;
    private const int OrderAcceptedOptionalValueExpireTimeCase = 13;
    private const int OrderAcceptedOptionalValueTradeNowCase = 14;
    private const int OrderAcceptedOptionalValueHandleInstCase = 15;
    private const int OrderAcceptedOptionalValueBboWeightIndicatorCase = 16;
    private const int OrderAcceptedOptionalValueGroupIdCase = 17;
    private const int OrderAcceptedOptionalValueSharesLocatedCase = 18;
    private const int OrderAcceptedOptionalValueUserRefIdxCase = 19;
    private const int OrderAcceptedOptionalValueAiqStrategyCase = 20;
    private const int OrderAcceptedOptionalValueAiqGroupIdCase = 21;

    /// <summary>
    ///  Writes the Order Accepted Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Order Accepted Optional Value.
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
        return Write(destination, ref cursor, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int orderAcceptedOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int orderAcceptedOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueFirmCase;
            int orderAcceptedOptionalValueFirmWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm>(bytes[orderAcceptedOptionalValueFirmWriterPosition..]) = value.caseStorage.firm;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueMinQtyCase;
            int orderAcceptedOptionalValueMinQtyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty>(bytes[orderAcceptedOptionalValueMinQtyWriterPosition..]) = value.caseStorage.minQty;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCustomerType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = CustomerTypeCase;
            int customerTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CustomerTypeField>(bytes[customerTypeWriterPosition..]) = value.caseStorage.customerType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCustomerType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueMaxFloorCase;
            int orderAcceptedOptionalValueMaxFloorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor>(bytes[orderAcceptedOptionalValueMaxFloorWriterPosition..]) = value.caseStorage.maxFloor;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValuePriceTypeCase;
            int orderAcceptedOptionalValuePriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField>(bytes[orderAcceptedOptionalValuePriceTypeWriterPosition..]) = value.caseStorage.priceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = PegOffsetCase;
            int pegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset>(bytes[pegOffsetWriterPosition..]) = value.caseStorage.pegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = DiscretionPriceCase;
            int discretionPriceWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice>(bytes[discretionPriceWriterPosition..]) = value.caseStorage.discretionPrice;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = DiscretionPriceTypeCase;
            int discretionPriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField>(bytes[discretionPriceTypeWriterPosition..]) = value.caseStorage.discretionPriceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = DiscretionPegOffsetCase;
            int discretionPegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset>(bytes[discretionPegOffsetWriterPosition..]) = value.caseStorage.discretionPegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValuePostOnlyCase;
            int orderAcceptedOptionalValuePostOnlyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField>(bytes[orderAcceptedOptionalValuePostOnlyWriterPosition..]) = value.caseStorage.postOnly;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = RandomReservesCase;
            int randomReservesWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves>(bytes[randomReservesWriterPosition..]) = value.caseStorage.randomReserves;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRoute > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = RouteCase;
            int routeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Route>(bytes[routeWriterPosition..]) = value.caseStorage.route;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRoute;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueExpireTimeCase;
            int orderAcceptedOptionalValueExpireTimeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime>(bytes[orderAcceptedOptionalValueExpireTimeWriterPosition..]) = value.caseStorage.expireTime;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueTradeNowCase;
            int orderAcceptedOptionalValueTradeNowWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField>(bytes[orderAcceptedOptionalValueTradeNowWriterPosition..]) = value.caseStorage.tradeNow;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueHandleInstCase;
            int orderAcceptedOptionalValueHandleInstWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField>(bytes[orderAcceptedOptionalValueHandleInstWriterPosition..]) = value.caseStorage.handleInst;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeBboWeightIndicator > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueBboWeightIndicatorCase;
            int orderAcceptedOptionalValueBboWeightIndicatorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BboWeightIndicatorField>(bytes[orderAcceptedOptionalValueBboWeightIndicatorWriterPosition..]) = value.caseStorage.bboWeightIndicator;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeBboWeightIndicator;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueGroupIdCase;
            int orderAcceptedOptionalValueGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId>(bytes[orderAcceptedOptionalValueGroupIdWriterPosition..]) = value.caseStorage.groupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueSharesLocatedCase;
            int orderAcceptedOptionalValueSharesLocatedWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField>(bytes[orderAcceptedOptionalValueSharesLocatedWriterPosition..]) = value.caseStorage.sharesLocated;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueUserRefIdxCase;
            int orderAcceptedOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[orderAcceptedOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueAiqStrategyCase;
            int orderAcceptedOptionalValueAiqStrategyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField>(bytes[orderAcceptedOptionalValueAiqStrategyWriterPosition..]) = value.caseStorage.aiqStrategy;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            orderAcceptedOptionalValueWriterCase = OrderAcceptedOptionalValueAiqGroupIdCase;
            int orderAcceptedOptionalValueAiqGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId>(bytes[orderAcceptedOptionalValueAiqGroupIdWriterPosition..]) = value.caseStorage.aiqGroupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref orderAcceptedOptionalValueWriterCaseRecord))
        {
            int recorded = orderAcceptedOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : orderAcceptedOptionalValueWriterCase;
            if (orderAcceptedOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && orderAcceptedOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            orderAcceptedOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
