using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  Branch
/// </summary>
public static class RecordBody
{
    /// <summary>
    ///  Identifies the case of the Record Body that the bytes select.
    /// </summary>
    public enum Case
    {
        /// <summary>
        ///  The bytes select no case of the Record Body.
        /// </summary>
        None = 0,
        /// <summary>
        ///  Market by price implementation with a book depth of 0; equivalent to MBP-0, the record of the trades schema
        /// </summary>
        TradeMsg = 1,
        /// <summary>
        ///  Market by price implementation with a known book depth of 1; the record of the mbp-1 and tbbo schemas
        /// </summary>
        Mbp1Msg = 2,
        /// <summary>
        ///  Market by price implementation with a known book depth of 10
        /// </summary>
        Mbp10Msg = 3,
        /// <summary>
        ///  Open, high, low, close, and volume; the record of the ohlcv schemas
        /// </summary>
        OhlcvMsg = 4,
        /// <summary>
        ///  A trading status update message; the record of the status schema
        /// </summary>
        StatusMsg = 5,
        /// <summary>
        ///  A definition of an instrument; the record of the definition schema
        /// </summary>
        InstrumentDefMsg = 6,
        /// <summary>
        ///  An auction imbalance message; the record of the imbalance schema
        /// </summary>
        ImbalanceMsg = 7,
        /// <summary>
        ///  An error message from the Databento Live Subscription Gateway (LSG)
        /// </summary>
        ErrorMsg = 8,
        /// <summary>
        ///  A symbol mapping message, which maps a symbol of one SType to another
        /// </summary>
        SymbolMappingMsg = 9,
        /// <summary>
        ///  A non-error message from the Databento Live Subscription Gateway (LSG); also used for heartbeating
        /// </summary>
        SystemMsg = 10,
        /// <summary>
        ///  A statistics message; the record of the statistics schema
        /// </summary>
        StatMsg = 11,
        /// <summary>
        ///  A market-by-order (MBO) tick message
        /// </summary>
        MboMsg = 12,
        /// <summary>
        ///  Consolidated market by price implementation with a known book depth of 1; the record of the cmbp-1 and tcbbo schemas
        /// </summary>
        Cmbp1Msg = 13,
        /// <summary>
        ///  Subsampled consolidated market by price with a known book depth of 1; the record of the cbbo-1s and cbbo-1m schemas
        /// </summary>
        CbboMsg = 14,
        /// <summary>
        ///  Subsampled market by price with a known book depth of 1; the record of the bbo-1s and bbo-1m schemas
        /// </summary>
        BboMsg = 15,
    }

    /// <summary>
    ///  Reads the Record Body in place.
    /// </summary>
    public readonly ref struct Reader
    {
        private readonly ReadOnlySpan<byte> bytes;
        private readonly int start;
        private readonly int end;
        private readonly byte rtypeValue;
        private readonly global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case selectedCase;

        internal Reader(ReadOnlySpan<byte> bytes, int start, int end, byte rtypeValue)
        {
            this.bytes = bytes;
            this.start = start;
            this.end = end;
            this.rtypeValue = rtypeValue;
            this.selectedCase = (global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case)Parser.Valid(Parser.SelectRecordBody(this.bytes, this.start, this.end, this.start, this.rtypeValue));
        }

        /// <summary>
        ///  Gets the case of the Record Body that the bytes select.
        /// </summary>
        public global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case Case => this.selectedCase;

        /// <summary>
        ///  Gets the bytes of the Record Body.
        /// </summary>
        public ReadOnlySpan<byte> Bytes => this.bytes[this.start..Parser.Valid(Parser.MeasureRecordBody(this.bytes, this.start, this.end, this.start, this.rtypeValue))];

        /// <summary>
        ///  Market by price implementation with a book depth of 0; equivalent to MBP-0, the record of the trades schema
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly TradeMsg.Layout TradeMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<TradeMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Market by price implementation with a known book depth of 1; the record of the mbp-1 and tbbo schemas
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly Mbp1Msg.Layout Mbp1Msg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Mbp1Msg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Market by price implementation with a known book depth of 10
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public Mbp10Msg.Reader Mbp10Msg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                int mbp10MsgReaderPosition = this.start;
                return new(this.bytes, mbp10MsgReaderPosition, this.end);
            }
        }

        /// <summary>
        ///  Open, high, low, close, and volume; the record of the ohlcv schemas
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly OhlcvMsg.Layout OhlcvMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<OhlcvMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A trading status update message; the record of the status schema
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StatusMsg.Layout StatusMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StatusMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A definition of an instrument; the record of the definition schema
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly InstrumentDefMsg.Layout InstrumentDefMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<InstrumentDefMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  An auction imbalance message; the record of the imbalance schema
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ImbalanceMsg.Layout ImbalanceMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ImbalanceMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  An error message from the Databento Live Subscription Gateway (LSG)
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly ErrorMsg.Layout ErrorMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<ErrorMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A symbol mapping message, which maps a symbol of one SType to another
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SymbolMappingMsg.Layout SymbolMappingMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SymbolMappingMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A non-error message from the Databento Live Subscription Gateway (LSG); also used for heartbeating
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly SystemMsg.Layout SystemMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<SystemMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A statistics message; the record of the statistics schema
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly StatMsg.Layout StatMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<StatMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  A market-by-order (MBO) tick message
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly MboMsg.Layout MboMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<MboMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Consolidated market by price implementation with a known book depth of 1; the record of the cmbp-1 and tcbbo schemas
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly Cmbp1Msg.Layout Cmbp1Msg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<Cmbp1Msg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Subsampled consolidated market by price with a known book depth of 1; the record of the cbbo-1s and cbbo-1m schemas
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly CbboMsg.Layout CbboMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<CbboMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }

        /// <summary>
        ///  Subsampled market by price with a known book depth of 1; the record of the bbo-1s and bbo-1m schemas
        /// </summary>
        /// <exception cref="global::System.InvalidOperationException">
        ///  The bytes select another case.
        /// </exception>
        public ref readonly BboMsg.Layout BboMsg
        {
            get
            {
                if ((int)this.Case != (int)global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg) throw new global::System.InvalidOperationException(Parser.OtherCase);
                return ref global::System.Runtime.InteropServices.MemoryMarshal.AsRef<BboMsg.Layout>(this.bytes[this.start..this.end]);
            }
        }
    }

    /// <summary>
    ///  The Record Body fields to write.
    /// </summary>
    public readonly ref struct Value
    {
        internal readonly global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case @case;
        internal readonly global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Value mbp10Msg;
        internal readonly CaseStorage caseStorage;

        [StructLayout(LayoutKind.Explicit)]
        internal struct CaseStorage
        {
            const int CaseStart = 0;

            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Value tradeMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Value mbp1Msg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Value ohlcvMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Value statusMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Value instrumentDefMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Value imbalanceMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Value errorMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Value symbolMappingMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Value systemMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Value statMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Value mboMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Value cmbp1Msg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Value cbboMsg;
            [FieldOffset(CaseStart)] internal global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Value bboMsg;
        }

        /// <summary>
        ///  Market by price implementation with a book depth of 0; equivalent to MBP-0, the record of the trades schema
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Value TradeMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.tradeMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg;
            }
        }

        /// <summary>
        ///  Market by price implementation with a known book depth of 1; the record of the mbp-1 and tbbo schemas
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Value Mbp1Msg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.mbp1Msg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg;
            }
        }

        /// <summary>
        ///  Market by price implementation with a known book depth of 10
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Value Mbp10Msg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.mbp10Msg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg;
            }
        }

        /// <summary>
        ///  Open, high, low, close, and volume; the record of the ohlcv schemas
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Value OhlcvMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.ohlcvMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg;
            }
        }

        /// <summary>
        ///  A trading status update message; the record of the status schema
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Value StatusMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.statusMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg;
            }
        }

        /// <summary>
        ///  A definition of an instrument; the record of the definition schema
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Value InstrumentDefMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.instrumentDefMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg;
            }
        }

        /// <summary>
        ///  An auction imbalance message; the record of the imbalance schema
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Value ImbalanceMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.imbalanceMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg;
            }
        }

        /// <summary>
        ///  An error message from the Databento Live Subscription Gateway (LSG)
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Value ErrorMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.errorMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg;
            }
        }

        /// <summary>
        ///  A symbol mapping message, which maps a symbol of one SType to another
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Value SymbolMappingMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.symbolMappingMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg;
            }
        }

        /// <summary>
        ///  A non-error message from the Databento Live Subscription Gateway (LSG); also used for heartbeating
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Value SystemMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.systemMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg;
            }
        }

        /// <summary>
        ///  A statistics message; the record of the statistics schema
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Value StatMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.statMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg;
            }
        }

        /// <summary>
        ///  A market-by-order (MBO) tick message
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Value MboMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.mboMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg;
            }
        }

        /// <summary>
        ///  Consolidated market by price implementation with a known book depth of 1; the record of the cmbp-1 and tcbbo schemas
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Value Cmbp1Msg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.cmbp1Msg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg;
            }
        }

        /// <summary>
        ///  Subsampled consolidated market by price with a known book depth of 1; the record of the cbbo-1s and cbbo-1m schemas
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Value CbboMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.cbboMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg;
            }
        }

        /// <summary>
        ///  Subsampled market by price with a known book depth of 1; the record of the bbo-1s and bbo-1m schemas
        /// </summary>
        /// <exception cref="ArgumentException">
        ///  The value already has a case.
        /// </exception>
        public global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Value BboMsg
        {
            init
            {
                if (this.@case != default) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterTwoCases, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
                this.caseStorage.bboMsg = value;
                this.@case = global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg;
            }
        }

        /// <summary>
        ///  The value in the bytes that selects the Trade Msg.
        /// </summary>
        public const byte TradeMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.MarketByPrice0;

        /// <summary>
        ///  The value in the bytes that selects the Mbp 1 Msg.
        /// </summary>
        public const byte Mbp1MsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.MarketByPrice1;

        /// <summary>
        ///  The value in the bytes that selects the Mbp 10 Msg.
        /// </summary>
        public const byte Mbp10MsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.MarketByPrice10;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.OhlcvDeprecated;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype2 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Ohlcv1Second;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype3 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Ohlcv1Minute;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype4 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Ohlcv1Hour;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype5 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Ohlcv1Day;

        /// <summary>
        ///  The value in the bytes that selects the Ohlcv Msg.
        /// </summary>
        public const byte OhlcvMsgRtype6 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.OhlcvEndOfDay;

        /// <summary>
        ///  The value in the bytes that selects the Status Msg.
        /// </summary>
        public const byte StatusMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Status;

        /// <summary>
        ///  The value in the bytes that selects the Instrument Def Msg.
        /// </summary>
        public const byte InstrumentDefMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.InstrumentDefinition;

        /// <summary>
        ///  The value in the bytes that selects the Imbalance Msg.
        /// </summary>
        public const byte ImbalanceMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Imbalance;

        /// <summary>
        ///  The value in the bytes that selects the Error Msg.
        /// </summary>
        public const byte ErrorMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Error;

        /// <summary>
        ///  The value in the bytes that selects the Symbol Mapping Msg.
        /// </summary>
        public const byte SymbolMappingMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.SymbolMapping;

        /// <summary>
        ///  The value in the bytes that selects the System Msg.
        /// </summary>
        public const byte SystemMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.System;

        /// <summary>
        ///  The value in the bytes that selects the Stat Msg.
        /// </summary>
        public const byte StatMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Statistics;

        /// <summary>
        ///  The value in the bytes that selects the Mbo Msg.
        /// </summary>
        public const byte MboMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.MarketByOrder;

        /// <summary>
        ///  The value in the bytes that selects the Cmbp 1 Msg.
        /// </summary>
        public const byte Cmbp1MsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.ConsolidatedMarketByPrice1;

        /// <summary>
        ///  The value in the bytes that selects the Cmbp 1 Msg.
        /// </summary>
        public const byte Cmbp1MsgRtype2 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.TradeWithConsolidatedBbo;

        /// <summary>
        ///  The value in the bytes that selects the Cbbo Msg.
        /// </summary>
        public const byte CbboMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.ConsolidatedBbo1Second;

        /// <summary>
        ///  The value in the bytes that selects the Cbbo Msg.
        /// </summary>
        public const byte CbboMsgRtype2 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.ConsolidatedBbo1Minute;

        /// <summary>
        ///  The value in the bytes that selects the Bbo Msg.
        /// </summary>
        public const byte BboMsgRtype = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Bbo1Second;

        /// <summary>
        ///  The value in the bytes that selects the Bbo Msg.
        /// </summary>
        public const byte BboMsgRtype2 = (byte)global::Databento.MarketData.Normalized.Dbn.v1.Rtype.Bbo1Minute;

        /// <summary>
        ///  Creates the value of the Record Body, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    private const int TradeMsgCase = 1;
    private const int Mbp1MsgCase = 2;
    private const int Mbp10MsgCase = 3;
    private const int OhlcvMsgCase = 4;
    private const int StatusMsgCase = 5;
    private const int InstrumentDefMsgCase = 6;
    private const int ImbalanceMsgCase = 7;
    private const int ErrorMsgCase = 8;
    private const int SymbolMappingMsgCase = 9;
    private const int SystemMsgCase = 10;
    private const int StatMsgCase = 11;
    private const int MboMsgCase = 12;
    private const int Cmbp1MsgCase = 13;
    private const int CbboMsgCase = 14;
    private const int BboMsgCase = 15;

    /// <summary>
    ///  Writes the Record Body to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the Record Body.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Minimum, ref global::System.Runtime.CompilerServices.Unsafe.NullRef<int>(), in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterCursor cursor, int start, scoped ref int recordBodyWriterCaseRecord, scoped in Value value)
    {
        int recordBodyWriterCase = global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent;
        if (value.@case is not (global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg or global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg)) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterNoCase, nameof(value));
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.TradeMsg)
        {
            recordBodyWriterCase = TradeMsgCase;
            int tradeMsgWriting = cursor.Index;
            var tradeMsgWriteValue = value.caseStorage.tradeMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.TradeMsg.Write(bytes, ref cursor, tradeMsgWriting, in tradeMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp1Msg)
        {
            recordBodyWriterCase = Mbp1MsgCase;
            int mbp1MsgWriting = cursor.Index;
            var mbp1MsgWriteValue = value.caseStorage.mbp1Msg;
            global::Databento.MarketData.Normalized.Dbn.v1.Mbp1Msg.Write(bytes, ref cursor, mbp1MsgWriting, in mbp1MsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Mbp10Msg)
        {
            recordBodyWriterCase = Mbp10MsgCase;
            int mbp10MsgWriting = cursor.Index;
            var mbp10MsgWriteValue = value.mbp10Msg;
            global::Databento.MarketData.Normalized.Dbn.v1.Mbp10Msg.Write(bytes, ref cursor, mbp10MsgWriting, in mbp10MsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.OhlcvMsg)
        {
            recordBodyWriterCase = OhlcvMsgCase;
            int ohlcvMsgWriting = cursor.Index;
            var ohlcvMsgWriteValue = value.caseStorage.ohlcvMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.OhlcvMsg.Write(bytes, ref cursor, ohlcvMsgWriting, in ohlcvMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatusMsg)
        {
            recordBodyWriterCase = StatusMsgCase;
            int statusMsgWriting = cursor.Index;
            var statusMsgWriteValue = value.caseStorage.statusMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.StatusMsg.Write(bytes, ref cursor, statusMsgWriting, in statusMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.InstrumentDefMsg)
        {
            recordBodyWriterCase = InstrumentDefMsgCase;
            int instrumentDefMsgWriting = cursor.Index;
            var instrumentDefMsgWriteValue = value.caseStorage.instrumentDefMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.InstrumentDefMsg.Write(bytes, ref cursor, instrumentDefMsgWriting, in instrumentDefMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ImbalanceMsg)
        {
            recordBodyWriterCase = ImbalanceMsgCase;
            int imbalanceMsgWriting = cursor.Index;
            var imbalanceMsgWriteValue = value.caseStorage.imbalanceMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.ImbalanceMsg.Write(bytes, ref cursor, imbalanceMsgWriting, in imbalanceMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.ErrorMsg)
        {
            recordBodyWriterCase = ErrorMsgCase;
            int errorMsgWriting = cursor.Index;
            var errorMsgWriteValue = value.caseStorage.errorMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.ErrorMsg.Write(bytes, ref cursor, errorMsgWriting, in errorMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SymbolMappingMsg)
        {
            recordBodyWriterCase = SymbolMappingMsgCase;
            int symbolMappingMsgWriting = cursor.Index;
            var symbolMappingMsgWriteValue = value.caseStorage.symbolMappingMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.SymbolMappingMsg.Write(bytes, ref cursor, symbolMappingMsgWriting, in symbolMappingMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.SystemMsg)
        {
            recordBodyWriterCase = SystemMsgCase;
            int systemMsgWriting = cursor.Index;
            var systemMsgWriteValue = value.caseStorage.systemMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.SystemMsg.Write(bytes, ref cursor, systemMsgWriting, in systemMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.StatMsg)
        {
            recordBodyWriterCase = StatMsgCase;
            int statMsgWriting = cursor.Index;
            var statMsgWriteValue = value.caseStorage.statMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.StatMsg.Write(bytes, ref cursor, statMsgWriting, in statMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.MboMsg)
        {
            recordBodyWriterCase = MboMsgCase;
            int mboMsgWriting = cursor.Index;
            var mboMsgWriteValue = value.caseStorage.mboMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.MboMsg.Write(bytes, ref cursor, mboMsgWriting, in mboMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.Cmbp1Msg)
        {
            recordBodyWriterCase = Cmbp1MsgCase;
            int cmbp1MsgWriting = cursor.Index;
            var cmbp1MsgWriteValue = value.caseStorage.cmbp1Msg;
            global::Databento.MarketData.Normalized.Dbn.v1.Cmbp1Msg.Write(bytes, ref cursor, cmbp1MsgWriting, in cmbp1MsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.CbboMsg)
        {
            recordBodyWriterCase = CbboMsgCase;
            int cbboMsgWriting = cursor.Index;
            var cbboMsgWriteValue = value.caseStorage.cbboMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.CbboMsg.Write(bytes, ref cursor, cbboMsgWriting, in cbboMsgWriteValue);
        }
        if (value.@case == global::Databento.MarketData.Normalized.Dbn.v1.RecordBody.Case.BboMsg)
        {
            recordBodyWriterCase = BboMsgCase;
            int bboMsgWriting = cursor.Index;
            var bboMsgWriteValue = value.caseStorage.bboMsg;
            global::Databento.MarketData.Normalized.Dbn.v1.BboMsg.Write(bytes, ref cursor, bboMsgWriting, in bboMsgWriteValue);
        }
        if (!global::System.Runtime.CompilerServices.Unsafe.IsNullRef(ref recordBodyWriterCaseRecord))
        {
            int recorded = recordBodyWriterCase == global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent ? global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.NoCase : recordBodyWriterCase;
            if (recordBodyWriterCaseRecord != global::Databento.MarketData.Normalized.Dbn.v1.Parser.Position.Absent && recordBodyWriterCaseRecord != recorded) throw new global::System.ArgumentException(global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterRuleDisagrees, global::Databento.MarketData.Normalized.Dbn.v1.Parser.WriterValue);
            recordBodyWriterCaseRecord = recorded;
        }
        return bytes[start..cursor.Index];
    }
}
