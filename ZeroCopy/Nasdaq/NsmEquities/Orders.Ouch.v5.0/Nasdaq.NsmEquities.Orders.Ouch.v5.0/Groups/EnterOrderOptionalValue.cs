using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class EnterOrderOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Enter Order Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Enter Order Optional Value.
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
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        ExpireTime = 12,
        /// <summary>
        ///  TradeNow
        /// </summary>
        TradeNow = 13,
        /// <summary>
        ///  HandleInst
        /// </summary>
        HandleInst = 14,
        /// <summary>
        ///  Customer Group ID – identifies specific entity
        /// </summary>
        GroupId = 15,
        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        SharesLocated = 16,
        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        LocateBroker = 17,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 18,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy = 19,
        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        AiqGroupId = 20,
    }

    /// <summary>
    ///  Reads the Enter Order Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte enterOrderOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte enterOrderOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.enterOrderOptionalFieldValue = enterOrderOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case)Parser.Valid(Parser.SelectEnterOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.enterOrderOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Enter Order Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Enter Order Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureEnterOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.enterOrderOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RandomReserves>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<HandleInstField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SharesLocatedField>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly LocateBroker LocateBroker
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<LocateBroker>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AiqGroupId>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Enter Order Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case @case;
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
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime expireTime;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField tradeNow;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField handleInst;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId groupId;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField sharesLocated;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker locateBroker;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated;
            }
        }

        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker LocateBroker
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.locateBroker = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Firm.
        /// </summary>
        public const byte FirmEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.Firm;

        /// <summary>
        ///  The value in the bytes that selects the Min Qty.
        /// </summary>
        public const byte MinQtyEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.MinQty;

        /// <summary>
        ///  The value in the bytes that selects the Customer Type.
        /// </summary>
        public const byte CustomerTypeEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.CustomerType;

        /// <summary>
        ///  The value in the bytes that selects the Max Floor.
        /// </summary>
        public const byte MaxFloorEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.MaxFloor;

        /// <summary>
        ///  The value in the bytes that selects the Price Type.
        /// </summary>
        public const byte PriceTypeEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.PriceType;

        /// <summary>
        ///  The value in the bytes that selects the Peg Offset.
        /// </summary>
        public const byte PegOffsetEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.PegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price.
        /// </summary>
        public const byte DiscretionPriceEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.DiscretionPrice;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price Type.
        /// </summary>
        public const byte DiscretionPriceTypeEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.DiscretionPriceType;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Peg Offset.
        /// </summary>
        public const byte DiscretionPegOffsetEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.DiscretionPegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Post Only.
        /// </summary>
        public const byte PostOnlyEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.PostOnly;

        /// <summary>
        ///  The value in the bytes that selects the Random Reserves.
        /// </summary>
        public const byte RandomReservesEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.RandomReserves;

        /// <summary>
        ///  The value in the bytes that selects the Expire Time.
        /// </summary>
        public const byte ExpireTimeEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.ExpireTime;

        /// <summary>
        ///  The value in the bytes that selects the Trade Now.
        /// </summary>
        public const byte TradeNowEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.TradeNow;

        /// <summary>
        ///  The value in the bytes that selects the Handle Inst.
        /// </summary>
        public const byte HandleInstEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.HandleInst;

        /// <summary>
        ///  The value in the bytes that selects the Group Id.
        /// </summary>
        public const byte GroupIdEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.GroupId;

        /// <summary>
        ///  The value in the bytes that selects the Shares Located.
        /// </summary>
        public const byte SharesLocatedEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.SharesLocated;

        /// <summary>
        ///  The value in the bytes that selects the Locate Broker.
        /// </summary>
        public const byte LocateBrokerEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.LocateBroker;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Strategy.
        /// </summary>
        public const byte AiqStrategyEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.AiqStrategy;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Group Id.
        /// </summary>
        public const byte AiqGroupIdEnterOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalField.AiqGroupId;

        /// <summary>
        ///  Creates the value of the Enter Order Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int EnterOrderOptionalValueFirmCase = 1;
    private const int EnterOrderOptionalValueMinQtyCase = 2;
    private const int CustomerTypeCase = 3;
    private const int EnterOrderOptionalValueMaxFloorCase = 4;
    private const int EnterOrderOptionalValuePriceTypeCase = 5;
    private const int EnterOrderOptionalValuePegOffsetCase = 6;
    private const int EnterOrderOptionalValueDiscretionPriceCase = 7;
    private const int EnterOrderOptionalValueDiscretionPriceTypeCase = 8;
    private const int EnterOrderOptionalValueDiscretionPegOffsetCase = 9;
    private const int EnterOrderOptionalValuePostOnlyCase = 10;
    private const int EnterOrderOptionalValueRandomReservesCase = 11;
    private const int EnterOrderOptionalValueExpireTimeCase = 12;
    private const int EnterOrderOptionalValueTradeNowCase = 13;
    private const int EnterOrderOptionalValueHandleInstCase = 14;
    private const int EnterOrderOptionalValueGroupIdCase = 15;
    private const int EnterOrderOptionalValueSharesLocatedCase = 16;
    private const int EnterOrderOptionalValueLocateBrokerCase = 17;
    private const int EnterOrderOptionalValueUserRefIdxCase = 18;
    private const int EnterOrderOptionalValueAiqStrategyCase = 19;
    private const int EnterOrderOptionalValueAiqGroupIdCase = 20;

    /// <summary>
    ///  Writes the Enter Order Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Enter Order Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int enterOrderOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int enterOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueFirmCase;
            int enterOrderOptionalValueFirmWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm>(bytes[enterOrderOptionalValueFirmWriterPosition..]) = value.caseStorage.firm;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueMinQtyCase;
            int enterOrderOptionalValueMinQtyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty>(bytes[enterOrderOptionalValueMinQtyWriterPosition..]) = value.caseStorage.minQty;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCustomerType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = CustomerTypeCase;
            int customerTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CustomerTypeField>(bytes[customerTypeWriterPosition..]) = value.caseStorage.customerType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeCustomerType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueMaxFloorCase;
            int enterOrderOptionalValueMaxFloorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor>(bytes[enterOrderOptionalValueMaxFloorWriterPosition..]) = value.caseStorage.maxFloor;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValuePriceTypeCase;
            int enterOrderOptionalValuePriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField>(bytes[enterOrderOptionalValuePriceTypeWriterPosition..]) = value.caseStorage.priceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValuePegOffsetCase;
            int enterOrderOptionalValuePegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset>(bytes[enterOrderOptionalValuePegOffsetWriterPosition..]) = value.caseStorage.pegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueDiscretionPriceCase;
            int enterOrderOptionalValueDiscretionPriceWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice>(bytes[enterOrderOptionalValueDiscretionPriceWriterPosition..]) = value.caseStorage.discretionPrice;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueDiscretionPriceTypeCase;
            int enterOrderOptionalValueDiscretionPriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField>(bytes[enterOrderOptionalValueDiscretionPriceTypeWriterPosition..]) = value.caseStorage.discretionPriceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueDiscretionPegOffsetCase;
            int enterOrderOptionalValueDiscretionPegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset>(bytes[enterOrderOptionalValueDiscretionPegOffsetWriterPosition..]) = value.caseStorage.discretionPegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValuePostOnlyCase;
            int enterOrderOptionalValuePostOnlyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField>(bytes[enterOrderOptionalValuePostOnlyWriterPosition..]) = value.caseStorage.postOnly;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueRandomReservesCase;
            int enterOrderOptionalValueRandomReservesWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves>(bytes[enterOrderOptionalValueRandomReservesWriterPosition..]) = value.caseStorage.randomReserves;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueExpireTimeCase;
            int enterOrderOptionalValueExpireTimeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime>(bytes[enterOrderOptionalValueExpireTimeWriterPosition..]) = value.caseStorage.expireTime;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueTradeNowCase;
            int enterOrderOptionalValueTradeNowWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField>(bytes[enterOrderOptionalValueTradeNowWriterPosition..]) = value.caseStorage.tradeNow;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueHandleInstCase;
            int enterOrderOptionalValueHandleInstWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField>(bytes[enterOrderOptionalValueHandleInstWriterPosition..]) = value.caseStorage.handleInst;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueGroupIdCase;
            int enterOrderOptionalValueGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.GroupId>(bytes[enterOrderOptionalValueGroupIdWriterPosition..]) = value.caseStorage.groupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeGroupId;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueSharesLocatedCase;
            int enterOrderOptionalValueSharesLocatedWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField>(bytes[enterOrderOptionalValueSharesLocatedWriterPosition..]) = value.caseStorage.sharesLocated;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueLocateBrokerCase;
            int enterOrderOptionalValueLocateBrokerWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker>(bytes[enterOrderOptionalValueLocateBrokerWriterPosition..]) = value.caseStorage.locateBroker;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueUserRefIdxCase;
            int enterOrderOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[enterOrderOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueAiqStrategyCase;
            int enterOrderOptionalValueAiqStrategyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField>(bytes[enterOrderOptionalValueAiqStrategyWriterPosition..]) = value.caseStorage.aiqStrategy;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            enterOrderOptionalValueWriterCase = EnterOrderOptionalValueAiqGroupIdCase;
            int enterOrderOptionalValueAiqGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId>(bytes[enterOrderOptionalValueAiqGroupIdWriterPosition..]) = value.caseStorage.aiqGroupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref enterOrderOptionalValueWriterCaseRecord))
        {
            int recorded = enterOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : enterOrderOptionalValueWriterCase;
            if (enterOrderOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && enterOrderOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            enterOrderOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
