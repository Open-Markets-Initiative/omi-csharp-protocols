using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0;

/// <summary>
///  Branch
/// </summary>
public static class ReplacedMessageOptionalValue
{
    /// <summary>
    ///  Identifies the case of the Replaced Message Optional Value that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Replaced Message Optional Value.
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
        ///  Represents the portion of your order that you wish to have displayed.
        /// </summary>
        MaxFloor = 3,
        /// <summary>
        ///  PriceType
        /// </summary>
        PriceType = 4,
        /// <summary>
        ///  PostOnly
        /// </summary>
        PostOnly = 5,
        /// <summary>
        ///  Seconds to live. Must be less than 86400 (number of seconds in a day)
        /// </summary>
        ExpireTime = 6,
        /// <summary>
        ///  TradeNow
        /// </summary>
        TradeNow = 7,
        /// <summary>
        ///  HandleInst
        /// </summary>
        HandleInst = 8,
        /// <summary>
        ///  BBO Weight Indicator
        /// </summary>
        BboWeightIndicator = 9,
        /// <summary>
        ///  User Reference Index – identifies the channel within the given port
        /// </summary>
        UserRefIdx = 10,
        /// <summary>
        ///  AIQ strategy used on the cancelled order.
        /// </summary>
        AiqStrategy = 11,
        /// <summary>
        ///  Specified to enable self match prevention at a more granular level within a given matching level
        /// </summary>
        AiqGroupId = 12,
    }

    /// <summary>
    ///  Reads the Replaced Message Optional Value in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte replacedMessageOptionalFieldValue;
        private readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte replacedMessageOptionalFieldValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.replacedMessageOptionalFieldValue = replacedMessageOptionalFieldValue;
            this.selectedCase = (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case)Parser.Valid(Parser.SelectReplacedMessageOptionalValue(this.bytes, this.start, this.end, this.start, this.replacedMessageOptionalFieldValue));
        }

        /// <summary>
        ///  Gets the case of the Replaced Message Optional Value that the bytes select.
        /// </summary>
        public global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Replaced Message Optional Value.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureReplacedMessageOptionalValue(this.bytes, this.start, this.end, this.start, this.replacedMessageOptionalFieldValue))];

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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BboWeightIndicatorField>(this.bytes[this.start..this.end]);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy) throw new global::System.InvalidOperationException(Parser.OtherCase);
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
                if ((int)this.Case != (int)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<AiqGroupId>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Replaced Message Optional Value fields to write.
    /// </summary>
    public readonly struct Value
    {
        internal readonly global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case @case;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm firm;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty minQty;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor maxFloor;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField priceType;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField postOnly;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime expireTime;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField tradeNow;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField handleInst;
            [FieldOffset(CaseStart)] internal global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BboWeightIndicatorField bboWeightIndicator;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy;
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
                this.@case = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Firm.
        /// </summary>
        public const byte FirmReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.Firm;

        /// <summary>
        ///  The value in the bytes that selects the Min Qty.
        /// </summary>
        public const byte MinQtyReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.MinQty;

        /// <summary>
        ///  The value in the bytes that selects the Max Floor.
        /// </summary>
        public const byte MaxFloorReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.MaxFloor;

        /// <summary>
        ///  The value in the bytes that selects the Price Type.
        /// </summary>
        public const byte PriceTypeReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.PriceType;

        /// <summary>
        ///  The value in the bytes that selects the Post Only.
        /// </summary>
        public const byte PostOnlyReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.PostOnly;

        /// <summary>
        ///  The value in the bytes that selects the Expire Time.
        /// </summary>
        public const byte ExpireTimeReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.ExpireTime;

        /// <summary>
        ///  The value in the bytes that selects the Trade Now.
        /// </summary>
        public const byte TradeNowReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.TradeNow;

        /// <summary>
        ///  The value in the bytes that selects the Handle Inst.
        /// </summary>
        public const byte HandleInstReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.HandleInst;

        /// <summary>
        ///  The value in the bytes that selects the Bbo Weight Indicator.
        /// </summary>
        public const byte BboWeightIndicatorReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.BboWeightIndicator;

        /// <summary>
        ///  The value in the bytes that selects the User Ref Idx.
        /// </summary>
        public const byte UserRefIdxReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.UserRefIdx;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Strategy.
        /// </summary>
        public const byte AiqStrategyReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.AiqStrategy;

        /// <summary>
        ///  The value in the bytes that selects the Aiq Group Id.
        /// </summary>
        public const byte AiqGroupIdReplacedMessageOptionalField = (byte)global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalField.AiqGroupId;

        /// <summary>
        ///  Creates the value of the Replaced Message Optional Value, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int ReplacedMessageOptionalValueFirmCase = 1;
    private const int ReplacedMessageOptionalValueMinQtyCase = 2;
    private const int ReplacedMessageOptionalValueMaxFloorCase = 3;
    private const int ReplacedMessageOptionalValuePriceTypeCase = 4;
    private const int ReplacedMessageOptionalValuePostOnlyCase = 5;
    private const int ReplacedMessageOptionalValueExpireTimeCase = 6;
    private const int ReplacedMessageOptionalValueTradeNowCase = 7;
    private const int ReplacedMessageOptionalValueHandleInstCase = 8;
    private const int ReplacedMessageOptionalValueBboWeightIndicatorCase = 9;
    private const int ReplacedMessageOptionalValueUserRefIdxCase = 10;
    private const int ReplacedMessageOptionalValueAiqStrategyCase = 11;
    private const int ReplacedMessageOptionalValueAiqGroupIdCase = 12;

    /// <summary>
    ///  Writes the Replaced Message Optional Value to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Replaced Message Optional Value.
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

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterCursor cursor, int start, scoped ref int replacedMessageOptionalValueWriterCaseRecord, scoped in Value value)
    {
        int replacedMessageOptionalValueWriterCase = global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent;
        if (value.@case is not (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy or global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId)) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueFirmCase;
            int replacedMessageOptionalValueFirmWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Firm>(bytes[replacedMessageOptionalValueFirmWriterPosition..]) = value.caseStorage.firm;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeFirm;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueMinQtyCase;
            int replacedMessageOptionalValueMinQtyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MinQty>(bytes[replacedMessageOptionalValueMinQtyWriterPosition..]) = value.caseStorage.minQty;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMinQty;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueMaxFloorCase;
            int replacedMessageOptionalValueMaxFloorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MaxFloor>(bytes[replacedMessageOptionalValueMaxFloorWriterPosition..]) = value.caseStorage.maxFloor;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeMaxFloor;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValuePriceTypeCase;
            int replacedMessageOptionalValuePriceTypeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PriceTypeField>(bytes[replacedMessageOptionalValuePriceTypeWriterPosition..]) = value.caseStorage.priceType;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePriceType;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValuePostOnlyCase;
            int replacedMessageOptionalValuePostOnlyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.PostOnlyField>(bytes[replacedMessageOptionalValuePostOnlyWriterPosition..]) = value.caseStorage.postOnly;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizePostOnly;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueExpireTimeCase;
            int replacedMessageOptionalValueExpireTimeWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ExpireTime>(bytes[replacedMessageOptionalValueExpireTimeWriterPosition..]) = value.caseStorage.expireTime;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeExpireTime;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueTradeNowCase;
            int replacedMessageOptionalValueTradeNowWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.TradeNowField>(bytes[replacedMessageOptionalValueTradeNowWriterPosition..]) = value.caseStorage.tradeNow;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeTradeNow;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueHandleInstCase;
            int replacedMessageOptionalValueHandleInstWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.HandleInstField>(bytes[replacedMessageOptionalValueHandleInstWriterPosition..]) = value.caseStorage.handleInst;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeHandleInst;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeBboWeightIndicator > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueBboWeightIndicatorCase;
            int replacedMessageOptionalValueBboWeightIndicatorWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BboWeightIndicatorField>(bytes[replacedMessageOptionalValueBboWeightIndicatorWriterPosition..]) = value.caseStorage.bboWeightIndicator;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeBboWeightIndicator;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueUserRefIdxCase;
            int replacedMessageOptionalValueUserRefIdxWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UserRefIdx>(bytes[replacedMessageOptionalValueUserRefIdxWriterPosition..]) = value.caseStorage.userRefIdx;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeUserRefIdx;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueAiqStrategyCase;
            int replacedMessageOptionalValueAiqStrategyWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqStrategyField>(bytes[replacedMessageOptionalValueAiqStrategyWriterPosition..]) = value.caseStorage.aiqStrategy;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqStrategy;
        }
        if (value.@case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId)
        {
            if (global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId > bytes.Length - cursor.Index) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterNoRoom, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterDestination);
            replacedMessageOptionalValueWriterCase = ReplacedMessageOptionalValueAiqGroupIdCase;
            int replacedMessageOptionalValueAiqGroupIdWriterPosition = cursor.Index;
            global::System.Runtime.InteropServices.MemoryMarshal.AsRef<global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqGroupId>(bytes[replacedMessageOptionalValueAiqGroupIdWriterPosition..]) = value.caseStorage.aiqGroupId;
            cursor.Index += global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.SizeAiqGroupId;
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref replacedMessageOptionalValueWriterCaseRecord))
        {
            int recorded = replacedMessageOptionalValueWriterCase == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent ? global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.NoCase : replacedMessageOptionalValueWriterCase;
            if (replacedMessageOptionalValueWriterCaseRecord != global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.Position.Absent && replacedMessageOptionalValueWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterRuleDisagrees, global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.Parser.WriterValue);
            replacedMessageOptionalValueWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
