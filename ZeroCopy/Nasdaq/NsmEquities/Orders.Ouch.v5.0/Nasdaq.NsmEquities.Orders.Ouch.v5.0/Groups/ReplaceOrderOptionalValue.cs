using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class ReplaceOrderOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Replace Order Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Replace Order Optional Value.
        /// </summary>
        None = 0,
        /// <summary>
        ///  MinQty must be a round lot.
        /// </summary>
        MinQty = 1,
        /// <summary>
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        MaxFloor = 2,
        /// <summary>
        ///  PriceType
        /// </summary>
        PriceType = 3,
        /// <summary>
        ///  PostOnly
        /// </summary>
        PostOnly = 4,
        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        ExpireTime = 5,
        /// <summary>
        ///  TradeNow
        /// </summary>
        TradeNow = 6,
        /// <summary>
        ///  HandleInst
        /// </summary>
        HandleInst = 7,
        /// <summary>
        ///  Shares to do random reserve with.
        /// </summary>
        RandomReserves = 8,
        /// <summary>
        ///  Offset amount for the pegged value.
        /// </summary>
        PegOffset = 9,
        /// <summary>
        ///  Discretion price for Discretionary Order.
        /// </summary>
        DiscretionPrice = 10,
        /// <summary>
        ///  DiscretionPriceType
        /// </summary>
        DiscretionPriceType = 11,
        /// <summary>
        ///  Offset amount for the pegged value of the Discretionary Price.
        /// </summary>
        DiscretionPegOffset = 12,
        /// <summary>
        ///  Shares located for short sale order
        /// </summary>
        SharesLocated = 13,
        /// <summary>
        ///  Broker code from which the locate has been acquired for short sale orders
        /// </summary>
        LocateBroker = 14,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 15,
        /// <summary>
        ///  Side
        /// </summary>
        Side = 16,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy = 17,
        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        AiqGroupId = 18,
    }

    /// <summary>
    ///  Reads the Replace Order Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte replaceOrderOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte replaceOrderOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.replaceOrderOptionalFieldValue = replaceOrderOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case)Parser.Valid(Parser.SelectReplaceOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.replaceOrderOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Replace Order Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Replace Order Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureReplaceOrderOptionalValue(this.bytes, this.start, this.end, this.start, this.replaceOrderOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MinQty>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PriceTypeField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<PostOnlyField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<HandleInstField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<RandomReserves>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<DiscretionPegOffset>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<UserRefIdx>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Side
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SideField Side
        {
            get
            {
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SideField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AiqGroupId>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Replace Order Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty minQty;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor maxFloor;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField priceType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField postOnly;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime expireTime;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField tradeNow;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField handleInst;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves randomReserves;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset pegOffset;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice discretionPrice;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField discretionPriceType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset discretionPegOffset;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField sharesLocated;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker locateBroker;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx userRefIdx;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField side;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField aiqStrategy;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId aiqGroupId;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx;
            }
        }

        /// <summary>
        ///  Side
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField Side
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterTwoCases, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
                this.caseStorage.side = value;
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Min Qty.
        /// </summary>
        public const byte MinQtyReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.MinQty;

        /// <summary>
        ///  The value in the bytes that selects the Max Floor.
        /// </summary>
        public const byte MaxFloorReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.MaxFloor;

        /// <summary>
        ///  The value in the bytes that selects the Price Type.
        /// </summary>
        public const byte PriceTypeReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.PriceType;

        /// <summary>
        ///  The value in the bytes that selects the Post Only.
        /// </summary>
        public const byte PostOnlyReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.PostOnly;

        /// <summary>
        ///  The value in the bytes that selects the Expire Time.
        /// </summary>
        public const byte ExpireTimeReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.ExpireTime;

        /// <summary>
        ///  The value in the bytes that selects the Trade Now.
        /// </summary>
        public const byte TradeNowReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.TradeNow;

        /// <summary>
        ///  The value in the bytes that selects the Handle Inst.
        /// </summary>
        public const byte HandleInstReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.HandleInst;

        /// <summary>
        ///  The value in the bytes that selects the Random Reserves.
        /// </summary>
        public const byte RandomReservesReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.RandomReserves;

        /// <summary>
        ///  The value in the bytes that selects the Peg Offset.
        /// </summary>
        public const byte PegOffsetReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.PegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price.
        /// </summary>
        public const byte DiscretionPriceReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.DiscretionPrice;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Price Type.
        /// </summary>
        public const byte DiscretionPriceTypeReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.DiscretionPriceType;

        /// <summary>
        ///  The value in the bytes that selects the Discretion Peg Offset.
        /// </summary>
        public const byte DiscretionPegOffsetReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.DiscretionPegOffset;

        /// <summary>
        ///  The value in the bytes that selects the Shares Located.
        /// </summary>
        public const byte SharesLocatedReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.SharesLocated;

        /// <summary>
        ///  The value in the bytes that selects the Locate Broker.
        /// </summary>
        public const byte LocateBrokerReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.LocateBroker;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Side.
        /// </summary>
        public const byte SideReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.Side;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Strategy.
        /// </summary>
        public const byte AiqStrategyReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.AiqStrategy;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Group Id.
        /// </summary>
        public const byte AiqGroupIdReplaceOrderOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalField.AiqGroupId;

        /// <summary>
        ///  Creates the value of the Replace Order Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int ReplaceOrderOptionalValueMinQtyCase = 1;
    private const int ReplaceOrderOptionalValueMaxFloorCase = 2;
    private const int ReplaceOrderOptionalValuePriceTypeCase = 3;
    private const int ReplaceOrderOptionalValuePostOnlyCase = 4;
    private const int ReplaceOrderOptionalValueExpireTimeCase = 5;
    private const int ReplaceOrderOptionalValueTradeNowCase = 6;
    private const int ReplaceOrderOptionalValueHandleInstCase = 7;
    private const int ReplaceOrderOptionalValueRandomReservesCase = 8;
    private const int ReplaceOrderOptionalValuePegOffsetCase = 9;
    private const int ReplaceOrderOptionalValueDiscretionPriceCase = 10;
    private const int ReplaceOrderOptionalValueDiscretionPriceTypeCase = 11;
    private const int ReplaceOrderOptionalValueDiscretionPegOffsetCase = 12;
    private const int ReplaceOrderOptionalValueSharesLocatedCase = 13;
    private const int ReplaceOrderOptionalValueLocateBrokerCase = 14;
    private const int ReplaceOrderOptionalValueUserRefIdxCase = 15;
    private const int ReplaceOrderOptionalValueSideCase = 16;
    private const int ReplaceOrderOptionalValueAiqStrategyCase = 17;
    private const int ReplaceOrderOptionalValueAiqGroupIdCase = 18;

    /// <summary>
    ///  Writes the Replace Order Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replace Order Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int replaceOrderOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int replaceOrderOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueMinQtyCase;
            int replaceOrderOptionalValueMinQtyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty>(bytes[replaceOrderOptionalValueMinQtyWriterPosition..]) = value.caseStorage.minQty;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueMaxFloorCase;
            int replaceOrderOptionalValueMaxFloorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor>(bytes[replaceOrderOptionalValueMaxFloorWriterPosition..]) = value.caseStorage.maxFloor;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValuePriceTypeCase;
            int replaceOrderOptionalValuePriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField>(bytes[replaceOrderOptionalValuePriceTypeWriterPosition..]) = value.caseStorage.priceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValuePostOnlyCase;
            int replaceOrderOptionalValuePostOnlyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField>(bytes[replaceOrderOptionalValuePostOnlyWriterPosition..]) = value.caseStorage.postOnly;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueExpireTimeCase;
            int replaceOrderOptionalValueExpireTimeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime>(bytes[replaceOrderOptionalValueExpireTimeWriterPosition..]) = value.caseStorage.expireTime;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueTradeNowCase;
            int replaceOrderOptionalValueTradeNowWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField>(bytes[replaceOrderOptionalValueTradeNowWriterPosition..]) = value.caseStorage.tradeNow;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueHandleInstCase;
            int replaceOrderOptionalValueHandleInstWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField>(bytes[replaceOrderOptionalValueHandleInstWriterPosition..]) = value.caseStorage.handleInst;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueRandomReservesCase;
            int replaceOrderOptionalValueRandomReservesWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RandomReserves>(bytes[replaceOrderOptionalValueRandomReservesWriterPosition..]) = value.caseStorage.randomReserves;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeRandomReserves;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValuePegOffsetCase;
            int replaceOrderOptionalValuePegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PegOffset>(bytes[replaceOrderOptionalValuePegOffsetWriterPosition..]) = value.caseStorage.pegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueDiscretionPriceCase;
            int replaceOrderOptionalValueDiscretionPriceWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPrice>(bytes[replaceOrderOptionalValueDiscretionPriceWriterPosition..]) = value.caseStorage.discretionPrice;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPrice;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueDiscretionPriceTypeCase;
            int replaceOrderOptionalValueDiscretionPriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPriceTypeField>(bytes[replaceOrderOptionalValueDiscretionPriceTypeWriterPosition..]) = value.caseStorage.discretionPriceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueDiscretionPegOffsetCase;
            int replaceOrderOptionalValueDiscretionPegOffsetWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DiscretionPegOffset>(bytes[replaceOrderOptionalValueDiscretionPegOffsetWriterPosition..]) = value.caseStorage.discretionPegOffset;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeDiscretionPegOffset;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueSharesLocatedCase;
            int replaceOrderOptionalValueSharesLocatedWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SharesLocatedField>(bytes[replaceOrderOptionalValueSharesLocatedWriterPosition..]) = value.caseStorage.sharesLocated;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSharesLocated;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueLocateBrokerCase;
            int replaceOrderOptionalValueLocateBrokerWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.LocateBroker>(bytes[replaceOrderOptionalValueLocateBrokerWriterPosition..]) = value.caseStorage.locateBroker;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeLocateBroker;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueUserRefIdxCase;
            int replaceOrderOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[replaceOrderOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueSideCase;
            int replaceOrderOptionalValueSideWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SideField>(bytes[replaceOrderOptionalValueSideWriterPosition..]) = value.caseStorage.side;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeSide;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueAiqStrategyCase;
            int replaceOrderOptionalValueAiqStrategyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField>(bytes[replaceOrderOptionalValueAiqStrategyWriterPosition..]) = value.caseStorage.aiqStrategy;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replaceOrderOptionalValueWriterCase = ReplaceOrderOptionalValueAiqGroupIdCase;
            int replaceOrderOptionalValueAiqGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId>(bytes[replaceOrderOptionalValueAiqGroupIdWriterPosition..]) = value.caseStorage.aiqGroupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref replaceOrderOptionalValueWriterCaseRecord))
        {
            int recorded = replaceOrderOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : replaceOrderOptionalValueWriterCase;
            if (replaceOrderOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && replaceOrderOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            replaceOrderOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
