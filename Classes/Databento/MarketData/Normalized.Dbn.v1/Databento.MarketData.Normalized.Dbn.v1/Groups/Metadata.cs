using System.Buffers.Binary;
using System.Diagnostics;
using static Databento.MarketData.Normalized.Dbn.v1.Rules;

namespace Databento.MarketData.Normalized.Dbn.v1;

/// <summary>
///  DBN metadata header, once at the start of a file or stream
/// </summary>
[DebuggerDisplay("{DebuggerDisplay,nq}")]
public sealed class Metadata : IGroupEntry, IEquatable<Metadata>, IFieldOwner
{
    internal const int MagicOffset = 0;

    internal const int MagicLength = 3;

    /// <summary>
    ///  The byte that fills the unused bytes of Magic.
    /// </summary>
    public const byte MagicFill = (byte)0;

    internal const int VersionOffset = MagicOffset + MagicLength;

    internal const int VersionLength = 1;

    internal const int MetadataLengthOffset = VersionOffset + VersionLength;

    internal const int MetadataLengthLength = 4;

    internal const int DatasetOffset = MetadataLengthOffset + MetadataLengthLength;

    internal const int DatasetLength = 16;

    /// <summary>
    ///  The byte that fills the unused bytes of Dataset.
    /// </summary>
    public const byte DatasetFill = (byte)0;

    internal const int SchemaOffset = DatasetOffset + DatasetLength;

    internal const int SchemaLength = 2;

    /// <summary>
    ///  The encoded value of Schema that means no value.
    /// </summary>
    public const Schema SchemaNullValue = (Schema)65535;

    internal const int StartOffset = SchemaOffset + SchemaLength;

    internal const int StartLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="Start"/>.
    /// </summary>
    public const long StartNanosecondsPerUnit = 1;

    internal const int EndOffset = StartOffset + StartLength;

    internal const int EndLength = 8;

    /// <summary>
    ///  The number of nanoseconds in one unit of the wire value of <see cref="End"/>.
    /// </summary>
    public const long EndNanosecondsPerUnit = 1;

    /// <summary>
    ///  The encoded value of End that means no value.
    /// </summary>
    public const ulong EndNullValue = 18446744073709551615UL;

    internal const int LimitOffset = EndOffset + EndLength;

    internal const int LimitLength = 8;

    /// <summary>
    ///  The encoded value of Limit that means no value.
    /// </summary>
    public const ulong LimitNullValue = 0UL;

    internal const int MetadataReserved8Offset = LimitOffset + LimitLength;

    internal const int MetadataReserved8Length = 8;

    internal const int StypeInOffset = MetadataReserved8Offset + MetadataReserved8Length;

    internal const int StypeInLength = 1;

    /// <summary>
    ///  The encoded value of Stype In that means no value.
    /// </summary>
    public const StypeIn StypeInNullValue = (StypeIn)255;

    internal const int StypeOutOffset = StypeInOffset + StypeInLength;

    internal const int StypeOutLength = 1;

    internal const int TsOutOffset = StypeOutOffset + StypeOutLength;

    internal const int TsOutLength = 1;

    internal const int MetadataReservedOffset = TsOutOffset + TsOutLength;

    internal const int MetadataReservedLength = 47;

    internal const int SchemaDefinitionLengthOffset = MetadataReservedOffset + MetadataReservedLength;

    internal const int SchemaDefinitionLengthLength = 4;

    internal const int SchemaDefinitionOffset = SchemaDefinitionLengthOffset + SchemaDefinitionLengthLength;

    internal const int SymbolsCountLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Symbol.
    /// </summary>
    public const byte SymbolListFill = (byte)0;

    internal const int PartialCountLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Partial Symbol.
    /// </summary>
    public const byte PartialSymbolListFill = (byte)0;

    internal const int NotFoundCountLength = 4;

    /// <summary>
    ///  The byte that fills the unused bytes of Not Found Symbol.
    /// </summary>
    public const byte NotFoundSymbolListFill = (byte)0;

    internal const int MappingsCountLength = 4;

    /// <summary>
    ///  The encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public const int MessageByteLength = MagicLength + VersionLength + MetadataLengthLength + DatasetLength + SchemaLength + StartLength + EndLength + LimitLength + MetadataReserved8Length + StypeInLength + StypeOutLength + TsOutLength + MetadataReservedLength + SchemaDefinitionLengthLength + SymbolsCountLength + PartialCountLength + NotFoundCountLength + MappingsCountLength;

    private const int SymbolListEntryByteLength = 22;

    private const int PartialSymbolListEntryByteLength = 22;

    private const int NotFoundSymbolListEntryByteLength = 22;

    private const int SymbolMappingListEntryByteLength = SymbolMapping.MessageByteLength;

    internal string magic = string.Empty;

    /// <summary>
    ///  The DBN magic string
    /// </summary>
    /// <remarks>
    ///  The field is 3 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Magic
    {
        get => this.magic;
        set
        {
            this.magic = value ?? string.Empty;
            Touch();
        }
    }

    internal byte version;

    /// <summary>
    ///  The DBN schema version number
    /// </summary>
    public byte Version
    {
        get => this.version;
        set
        {
            this.version = value;
            Touch();
        }
    }

    internal uint metadataLength;

    /// <summary>
    ///  The length of the metadata after this field, in bytes
    /// </summary>
    public uint MetadataLength => this.metadataLength;

    internal string dataset = string.Empty;

    /// <summary>
    ///  The dataset code
    /// </summary>
    /// <remarks>
    ///  The field is 16 bytes long. The field holds ASCII text. Text that is shorter than the field is padded with null characters at the end. Text longer than the field is cut to fit when the message is encoded. A character that the field cannot hold is replaced with a question mark when the message is encoded.
    /// </remarks>
    public string Dataset
    {
        get => this.dataset;
        set
        {
            this.dataset = value ?? string.Empty;
            Touch();
        }
    }

    internal Schema schema;

    /// <summary>
    ///  The data record schema; no value for a file of mixed schemas
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="SchemaNullValue"/>. Setting <see langword="null"/> writes <see cref="SchemaNullValue"/>, and a value equal to <see cref="SchemaNullValue"/> also reads back as <see langword="null"/>.
    /// </remarks>
    public Schema? Schema
    {
        get => this.schema == SchemaNullValue ? (Schema?)null : this.schema;
        set
        {
            this.schema = value ?? SchemaNullValue;
            Touch();
        }
    }

    internal ulong start;

    /// <summary>
    ///  The start time of query range in UNIX epoch nanoseconds
    /// </summary>
    /// <remarks>
    ///  This property has a precision of 100 nanoseconds, and <see cref="StartRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range.
    /// </remarks>
    public DateTime Start
    {
        get => Instant.ToDateTime(this.start, StartNanosecondsPerUnit);
        set
        {
            this.start = Instant.ToRaw<ulong>(value, StartNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue);
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="Start"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong StartRaw
    {
        get => this.start;
        set
        {
            this.start = value;
            Touch();
        }
    }

    internal ulong end;

    /// <summary>
    ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="EndNullValue"/>. Setting <see langword="null"/> writes <see cref="EndNullValue"/>. This property has a precision of 100 nanoseconds, and <see cref="EndRaw"/> holds the exact value. Setting a local time converts it to UTC, and a time of unspecified kind is treated as UTC. A time outside the range of the field is set to the nearest limit of the range. A result equal to <see cref="EndNullValue"/> is moved one step to the nearest other value.
    /// </remarks>
    public DateTime? End
    {
        get => this.end == EndNullValue ? (DateTime?)null : Instant.ToDateTime(this.end, EndNanosecondsPerUnit);
        set
        {
            this.end = value is { } present ? Instant.ToRaw<ulong>(present, EndNanosecondsPerUnit, ulong.MinValue, ulong.MaxValue, EndNullValue) : EndNullValue;
            Touch();
        }
    }

    /// <summary>
    ///  Gets or sets the wire value of <see cref="End"/>, as the unscaled integer that the field holds.
    /// </summary>
    public ulong EndRaw
    {
        get => this.end;
        set
        {
            this.end = value;
            Touch();
        }
    }

    internal ulong limit;

    /// <summary>
    ///  The maximum number of records to return; no value when unlimited
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="LimitNullValue"/>. Setting <see langword="null"/> writes <see cref="LimitNullValue"/>, and a value equal to <see cref="LimitNullValue"/> also reads back as <see langword="null"/>.
    /// </remarks>
    public ulong? Limit
    {
        get => this.limit == LimitNullValue ? (ulong?)null : this.limit;
        set
        {
            this.limit = value ?? LimitNullValue;
            Touch();
        }
    }

    internal StypeIn stypeIn;

    /// <summary>
    ///  The input symbology type
    /// </summary>
    /// <remarks>
    ///  The value is <see langword="null"/> if the field holds <see cref="StypeInNullValue"/>. Setting <see langword="null"/> writes <see cref="StypeInNullValue"/>, and a value equal to <see cref="StypeInNullValue"/> also reads back as <see langword="null"/>.
    /// </remarks>
    public StypeIn? StypeIn
    {
        get => this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn;
        set
        {
            this.stypeIn = value ?? StypeInNullValue;
            Touch();
        }
    }

    internal StypeOut stypeOut;

    /// <summary>
    ///  The output symbology type
    /// </summary>
    public StypeOut StypeOut
    {
        get => this.stypeOut;
        set
        {
            this.stypeOut = value;
            Touch();
        }
    }

    internal TsOut tsOut;

    /// <summary>
    ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
    /// </summary>
    public TsOut TsOut
    {
        get => this.tsOut;
        set
        {
            this.tsOut = value;
            Touch();
        }
    }

    internal uint schemaDefinitionLength;

    /// <summary>
    ///  The length of the schema definition; always 0
    /// </summary>
    public uint SchemaDefinitionLength
    {
        get => this.schemaDefinitionLength;
        set
        {
            this.schemaDefinitionLength = value;
            Touch();
        }
    }

    internal ReadOnlyMemory<byte> schemaDefinition;

    /// <summary>
    ///  The schema definition; always empty
    /// </summary>
    public ReadOnlyMemory<byte> SchemaDefinition
    {
        get => this.schemaDefinition;
        set
        {
            this.schemaDefinition = value.ToArray();
            Touch();
        }
    }

    internal uint symbolsCount;

    /// <summary>
    ///  The number of query input symbols
    /// </summary>
    public uint SymbolsCount
    {
        get => this.symbolsCount;
        set
        {
            this.symbolsCount = value;
            Touch();
        }
    }

    internal SymbolList symbolList = new([]);

    /// <summary>
    ///  One of the query input symbols
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public SymbolList SymbolList
    {
        get => this.symbolList;
        set
        {
            value ??= new([]);
            this.symbolList = new SymbolList(value);
            Touch();
        }
    }

    internal uint partialCount;

    /// <summary>
    ///  The number of symbols that did not resolve for at least one day in the query time range
    /// </summary>
    public uint PartialCount
    {
        get => this.partialCount;
        set
        {
            this.partialCount = value;
            Touch();
        }
    }

    internal PartialSymbolList partialSymbolList = new([]);

    /// <summary>
    ///  One of the symbols that did not resolve for at least one day in the query time range
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public PartialSymbolList PartialSymbolList
    {
        get => this.partialSymbolList;
        set
        {
            value ??= new([]);
            this.partialSymbolList = new PartialSymbolList(value);
            Touch();
        }
    }

    internal uint notFoundCount;

    /// <summary>
    ///  The number of symbols that did not resolve for any day in the query time range
    /// </summary>
    public uint NotFoundCount
    {
        get => this.notFoundCount;
        set
        {
            this.notFoundCount = value;
            Touch();
        }
    }

    internal NotFoundSymbolList notFoundSymbolList = new([]);

    /// <summary>
    ///  One of the symbols that did not resolve for any day in the query time range
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public NotFoundSymbolList NotFoundSymbolList
    {
        get => this.notFoundSymbolList;
        set
        {
            value ??= new([]);
            this.notFoundSymbolList = new NotFoundSymbolList(value);
            Touch();
        }
    }

    internal uint mappingsCount;

    /// <summary>
    ///  The number of symbol mappings
    /// </summary>
    public uint MappingsCount
    {
        get => this.mappingsCount;
        set
        {
            this.mappingsCount = value;
            Touch();
        }
    }

    internal SymbolMappingList symbolMappingList = new([]);

    /// <summary>
    ///  The mappings of one raw symbol
    /// </summary>
    /// <remarks>
    ///  Setting <see langword="null"/> stores the value that Clear stores.
    /// </remarks>
    public SymbolMappingList SymbolMappingList
    {
        get => this.symbolMappingList;
        set
        {
            value ??= new([]);
            this.symbolMappingList.SetOwner(null);
            this.symbolMappingList = new SymbolMappingList(value);
            this.symbolMappingList.SetOwner(this);
            Touch();
        }
    }

    /// <summary>
    ///  Gets the encoded length in bytes, not counting variable-length content or fields that a message can omit.
    /// </summary>
    public int ByteLength => MessageByteLength;

    /// <summary>
    ///  Gets the number of bytes that Encode writes, including variable-length content and each omittable field that is present.
    /// </summary>
    public int EncodedByteLength
    {
        get
        {
            var position = 0;
            position += MagicLength;
            position += VersionLength;
            position += MetadataLengthLength;
            position += DatasetLength;
            position += SchemaLength;
            position += StartLength;
            position += EndLength;
            position += LimitLength;
            position += MetadataReserved8Length;
            position += StypeInLength;
            position += StypeOutLength;
            position += TsOutLength;
            position += MetadataReservedLength;
            position += SchemaDefinitionLengthLength;
            position += this.schemaDefinition.Length;
            position += SymbolsCountLength;
            position += this.symbolList.Count * SymbolListEntryByteLength;
            position += PartialCountLength;
            position += this.partialSymbolList.Count * PartialSymbolListEntryByteLength;
            position += NotFoundCountLength;
            position += this.notFoundSymbolList.Count * NotFoundSymbolListEntryByteLength;
            position += MappingsCountLength;
            foreach (var entry in this.symbolMappingList)
            {
                position += entry.EncodedByteLength;
            }
            position += this.trailer.Length;
            return position;
        }
    }

    /// <summary>
    ///  Gets the result of the most recent parse.
    /// </summary>
    public ParseStatus Status => this.status;

    /// <summary>
    ///  Gets the field where the most recent parse stopped, or <see langword="null"/> if it did not stop at a field.
    /// </summary>
    public FieldView? FailedField => this.failed is { } entry ? new FieldView(this, (int)entry) : null;

    /// <summary>
    ///  Gets the byte offset, relative to the start of the parsed bytes, where the most recent parse stopped.
    /// </summary>
    public int FailedOffset => this.failedOffset;

    /// <summary>
    ///  Gets a value indicating whether every message type and every enumerated value is one that the protocol defines.
    /// </summary>
    public bool IsRecognized => (this.schema == SchemaNullValue || global::System.Enum.IsDefined(this.schema)) && (this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)) && global::System.Enum.IsDefined(this.stypeOut) && global::System.Enum.IsDefined(this.tsOut) && this.symbolMappingList.IsRecognized;

    /// <summary>
    ///  Gets the bytes that were parsed, or no bytes if the instance was not parsed.
    /// </summary>
    public ReadOnlyMemory<byte> Raw => this.raw;

    /// <summary>
    ///  Gets a value indicating whether any value has changed since the most recent parse or clear.
    /// </summary>
    public bool IsModified => this.modified;

    private ParseStatus status;
    private FieldName? failed;
    private int failedOffset;
    private byte[] raw = [];
    private byte[]? cache;
    private bool modified;
    private IFieldOwner? owner;
    private int symbolsCountOffset;
    private int symbolListOffset;
    private int partialCountOffset;
    private int partialSymbolListOffset;
    private int notFoundCountOffset;
    private int notFoundSymbolListOffset;
    private int mappingsCountOffset;
    private int symbolMappingListOffset;
    private int symbolListLength;
    private int partialSymbolListLength;
    private int notFoundSymbolListLength;
    private int symbolMappingListLength;

    private void Touch()
    {
        this.modified = true;
        this.cache = null;
        this.owner?.Touch();
    }

    private byte[] trailer = [];

    /// <summary>
    ///  Gets or sets any extra bytes that follow the entry within its stated length.
    /// </summary>
    /// <remarks>
    ///  Encode writes these bytes after the fields of the entry. Setting this property marks the entry as modified.
    /// </remarks>
    public ReadOnlyMemory<byte> Trailer
    {
        get => this.trailer;
        set
        {
            this.trailer = value.ToArray();
            Touch();
        }
    }

    /// <summary>
    ///  Identifies each field, in wire order.
    /// </summary>
    public enum FieldName
    {
        /// <summary>
        ///  The DBN magic string
        /// </summary>
        Magic,
        /// <summary>
        ///  The DBN schema version number
        /// </summary>
        Version,
        /// <summary>
        ///  The length of the metadata after this field, in bytes
        /// </summary>
        MetadataLength,
        /// <summary>
        ///  The dataset code
        /// </summary>
        Dataset,
        /// <summary>
        ///  The data record schema; no value for a file of mixed schemas
        /// </summary>
        Schema,
        /// <summary>
        ///  The start time of query range in UNIX epoch nanoseconds
        /// </summary>
        Start,
        /// <summary>
        ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
        /// </summary>
        End,
        /// <summary>
        ///  The maximum number of records to return; no value when unlimited
        /// </summary>
        Limit,
        /// <summary>
        ///  Reserved, 8 bytes; the deprecated record count
        /// </summary>
        MetadataReserved8,
        /// <summary>
        ///  The input symbology type
        /// </summary>
        StypeIn,
        /// <summary>
        ///  The output symbology type
        /// </summary>
        StypeOut,
        /// <summary>
        ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
        /// </summary>
        TsOut,
        /// <summary>
        ///  Reserved, 47 bytes
        /// </summary>
        MetadataReserved,
        /// <summary>
        ///  The length of the schema definition; always 0
        /// </summary>
        SchemaDefinitionLength,
        /// <summary>
        ///  The schema definition; always empty
        /// </summary>
        SchemaDefinition,
        /// <summary>
        ///  The number of query input symbols
        /// </summary>
        SymbolsCount,
        /// <summary>
        ///  One of the query input symbols
        /// </summary>
        SymbolList,
        /// <summary>
        ///  The number of symbols that did not resolve for at least one day in the query time range
        /// </summary>
        PartialCount,
        /// <summary>
        ///  One of the symbols that did not resolve for at least one day in the query time range
        /// </summary>
        PartialSymbolList,
        /// <summary>
        ///  The number of symbols that did not resolve for any day in the query time range
        /// </summary>
        NotFoundCount,
        /// <summary>
        ///  One of the symbols that did not resolve for any day in the query time range
        /// </summary>
        NotFoundSymbolList,
        /// <summary>
        ///  The number of symbol mappings
        /// </summary>
        MappingsCount,
        /// <summary>
        ///  The mappings of one raw symbol
        /// </summary>
        SymbolMappingList,
    }

    /// <summary>
    ///  Provides access to the fields of one instance, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public readonly struct FieldSet : IEnumerable<FieldView>
    {
        private readonly Metadata owner;

        internal FieldSet(Metadata owner)
        {
            this.owner = owner;
        }

        /// <summary>
        ///  The DBN magic string
        /// </summary>
        public FieldView Magic => this[FieldName.Magic];

        /// <summary>
        ///  The DBN schema version number
        /// </summary>
        public FieldView Version => this[FieldName.Version];

        /// <summary>
        ///  The length of the metadata after this field, in bytes
        /// </summary>
        public FieldView MetadataLength => this[FieldName.MetadataLength];

        /// <summary>
        ///  The dataset code
        /// </summary>
        public FieldView Dataset => this[FieldName.Dataset];

        /// <summary>
        ///  The data record schema; no value for a file of mixed schemas
        /// </summary>
        public FieldView Schema => this[FieldName.Schema];

        /// <summary>
        ///  The start time of query range in UNIX epoch nanoseconds
        /// </summary>
        public FieldView Start => this[FieldName.Start];

        /// <summary>
        ///  The end time of query range in UNIX epoch nanoseconds; no value when unbounded
        /// </summary>
        public FieldView End => this[FieldName.End];

        /// <summary>
        ///  The maximum number of records to return; no value when unlimited
        /// </summary>
        public FieldView Limit => this[FieldName.Limit];

        /// <summary>
        ///  Reserved, 8 bytes; the deprecated record count
        /// </summary>
        public FieldView MetadataReserved8 => this[FieldName.MetadataReserved8];

        /// <summary>
        ///  The input symbology type
        /// </summary>
        public FieldView StypeIn => this[FieldName.StypeIn];

        /// <summary>
        ///  The output symbology type
        /// </summary>
        public FieldView StypeOut => this[FieldName.StypeOut];

        /// <summary>
        ///  Whether every record carries a ts_out trailer, the live gateway send timestamp
        /// </summary>
        public FieldView TsOut => this[FieldName.TsOut];

        /// <summary>
        ///  Reserved, 47 bytes
        /// </summary>
        public FieldView MetadataReserved => this[FieldName.MetadataReserved];

        /// <summary>
        ///  The length of the schema definition; always 0
        /// </summary>
        public FieldView SchemaDefinitionLength => this[FieldName.SchemaDefinitionLength];

        /// <summary>
        ///  The schema definition; always empty
        /// </summary>
        public FieldView SchemaDefinition => this[FieldName.SchemaDefinition];

        /// <summary>
        ///  The number of query input symbols
        /// </summary>
        public FieldView SymbolsCount => this[FieldName.SymbolsCount];

        /// <summary>
        ///  One of the query input symbols
        /// </summary>
        public FieldView SymbolList => this[FieldName.SymbolList];

        /// <summary>
        ///  The number of symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public FieldView PartialCount => this[FieldName.PartialCount];

        /// <summary>
        ///  One of the symbols that did not resolve for at least one day in the query time range
        /// </summary>
        public FieldView PartialSymbolList => this[FieldName.PartialSymbolList];

        /// <summary>
        ///  The number of symbols that did not resolve for any day in the query time range
        /// </summary>
        public FieldView NotFoundCount => this[FieldName.NotFoundCount];

        /// <summary>
        ///  One of the symbols that did not resolve for any day in the query time range
        /// </summary>
        public FieldView NotFoundSymbolList => this[FieldName.NotFoundSymbolList];

        /// <summary>
        ///  The number of symbol mappings
        /// </summary>
        public FieldView MappingsCount => this[FieldName.MappingsCount];

        /// <summary>
        ///  The mappings of one raw symbol
        /// </summary>
        public FieldView SymbolMappingList => this[FieldName.SymbolMappingList];

        /// <summary>
        ///  Gets the specified field.
        /// </summary>
        public FieldView this[FieldName field] => new(this.owner, (int)field);

        /// <summary>
        ///  Returns an enumerator that iterates through the fields in wire order.
        /// </summary>
        public FieldEnumerator GetEnumerator() => new(this.owner);

        IEnumerator<FieldView> IEnumerable<FieldView>.GetEnumerator() => GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }

    private static readonly FieldDescriptor[] Descriptors =
    [
        new(nameof(FieldName.Magic), FieldKind.String, MagicLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Version), FieldKind.UnsignedInteger, VersionLength) { IsSigned = false },
        new(nameof(FieldName.MetadataLength), FieldKind.UnsignedInteger, MetadataLengthLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.Dataset), FieldKind.String, DatasetLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.Schema), FieldKind.Enum, SchemaLength) { ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = SchemaNullValue },
        new(nameof(FieldName.Start), FieldKind.DateTime, StartLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.End), FieldKind.DateTime, EndLength) { Unit = "Nanoseconds", Epoch = "Unix", ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = EndNullValue },
        new(nameof(FieldName.Limit), FieldKind.UnsignedInteger, LimitLength) { ByteOrder = "Little", IsSigned = false, Nullable = "Value", NullValue = LimitNullValue },
        new(nameof(FieldName.MetadataReserved8), FieldKind.Padding, MetadataReserved8Length),
        new(nameof(FieldName.StypeIn), FieldKind.Enum, StypeInLength) { IsSigned = false, Nullable = "Value", NullValue = StypeInNullValue },
        new(nameof(FieldName.StypeOut), FieldKind.Enum, StypeOutLength) { IsSigned = false },
        new(nameof(FieldName.TsOut), FieldKind.Enum, TsOutLength) { IsSigned = false },
        new(nameof(FieldName.MetadataReserved), FieldKind.Padding, MetadataReservedLength),
        new(nameof(FieldName.SchemaDefinitionLength), FieldKind.UnsignedInteger, SchemaDefinitionLengthLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SchemaDefinition), FieldKind.Data, null),
        new(nameof(FieldName.SymbolsCount), FieldKind.UnsignedInteger, SymbolsCountLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SymbolList), FieldKind.String, SymbolListEntryByteLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.PartialCount), FieldKind.UnsignedInteger, PartialCountLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.PartialSymbolList), FieldKind.String, PartialSymbolListEntryByteLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.NotFoundCount), FieldKind.UnsignedInteger, NotFoundCountLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.NotFoundSymbolList), FieldKind.String, NotFoundSymbolListEntryByteLength) { Fill = "NullCharacters", Justification = "Left" },
        new(nameof(FieldName.MappingsCount), FieldKind.UnsignedInteger, MappingsCountLength) { ByteOrder = "Little", IsSigned = false },
        new(nameof(FieldName.SymbolMappingList), FieldKind.Group, null),
    ];

    /// <summary>
    ///  Gets the fields in wire order, each with its name, offset, length, bytes, text, and protocol attributes.
    /// </summary>
    public FieldSet Fields => new(this);

    IEnumerable<FieldView> IGroupEntry.Fields => new FieldSet(this);

    /// <summary>
    ///  Initializes a new instance of the <see cref="Metadata"/> class with default values.
    /// </summary>
    public Metadata()
    {
        Clear();
    }

    /// <summary>
    ///  Initializes a new instance of the <see cref="Metadata"/> class with the values, bytes, and parse result of another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Metadata(Metadata other) : this()
    {
        CopyFrom(other);
    }

    /// <summary>
    ///  Copies the values, bytes, and parse result from another instance.
    /// </summary>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public void CopyFrom(Metadata other)
    {
        ArgumentNullException.ThrowIfNull(other);
        this.magic = other.magic;
        this.version = other.version;
        this.metadataLength = other.metadataLength;
        this.dataset = other.dataset;
        this.schema = other.schema;
        this.start = other.start;
        this.end = other.end;
        this.limit = other.limit;
        this.stypeIn = other.stypeIn;
        this.stypeOut = other.stypeOut;
        this.tsOut = other.tsOut;
        this.schemaDefinitionLength = other.schemaDefinitionLength;
        this.schemaDefinition = other.schemaDefinition.ToArray();
        this.symbolsCount = other.symbolsCount;
        this.symbolList = new SymbolList(other.symbolList);
        this.partialCount = other.partialCount;
        this.partialSymbolList = new PartialSymbolList(other.partialSymbolList);
        this.notFoundCount = other.notFoundCount;
        this.notFoundSymbolList = new NotFoundSymbolList(other.notFoundSymbolList);
        this.mappingsCount = other.mappingsCount;
        this.symbolMappingList.SetOwner(null);
        this.symbolMappingList = new SymbolMappingList(other.symbolMappingList);
        this.symbolMappingList.SetOwner(this);
        this.symbolsCountOffset = other.symbolsCountOffset;
        this.symbolListOffset = other.symbolListOffset;
        this.partialCountOffset = other.partialCountOffset;
        this.partialSymbolListOffset = other.partialSymbolListOffset;
        this.notFoundCountOffset = other.notFoundCountOffset;
        this.notFoundSymbolListOffset = other.notFoundSymbolListOffset;
        this.mappingsCountOffset = other.mappingsCountOffset;
        this.symbolMappingListOffset = other.symbolMappingListOffset;
        this.symbolListLength = other.symbolListLength;
        this.partialSymbolListLength = other.partialSymbolListLength;
        this.notFoundSymbolListLength = other.notFoundSymbolListLength;
        this.symbolMappingListLength = other.symbolMappingListLength;
        this.trailer = other.trailer;
        this.status = other.status;
        this.failed = other.failed;
        this.failedOffset = other.failedOffset;
        this.raw = other.raw;
        this.cache = other.cache;
        this.modified = other.modified;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Resets all values, the bytes, and the parse result to their defaults.
    /// </summary>
    /// <remarks>
    ///  A field with one fixed value is reset to that value.
    /// </remarks>
    public void Clear()
    {
        this.magic = string.Empty;
        this.version = default;
        this.metadataLength = default;
        this.dataset = string.Empty;
        this.schema = default;
        this.start = default;
        this.end = default;
        this.limit = default;
        this.stypeIn = default;
        this.stypeOut = default;
        this.tsOut = default;
        this.schemaDefinitionLength = default;
        this.schemaDefinition = ReadOnlyMemory<byte>.Empty;
        this.symbolsCount = default;
        this.symbolList = new([]);
        this.partialCount = default;
        this.partialSymbolList = new([]);
        this.notFoundCount = default;
        this.notFoundSymbolList = new([]);
        this.mappingsCount = default;
        this.symbolMappingList.SetOwner(null);
        this.symbolMappingList = new([]);
        this.symbolMappingList.SetOwner(this);
        this.trailer = [];
        this.status = default;
        this.failed = null;
        this.failedOffset = default;
        this.raw = [];
        this.cache = null;
        this.modified = false;
        this.owner?.Touch();
    }

    /// <summary>
    ///  Parses a new instance from the start of the bytes.
    /// </summary>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static Metadata Parse(ReadOnlySpan<byte> data)
    {
        var instance = new Metadata();
        instance.ParseFrom(data);
        return instance;
    }

    /// <summary>
    ///  Parses a new instance starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    /// <returns>The new instance. Check Status to determine whether the parse succeeded.</returns>
    public static Metadata Parse(ReadOnlySpan<byte> data, ref int offset)
    {
        var instance = new Metadata();
        instance.ParseFrom(data, ref offset);
        return instance;
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the first byte.
    /// </summary>
    public void ParseFrom(ReadOnlySpan<byte> data)
    {
        var offset = 0;
        ParseFrom(data, ref offset);
    }

    /// <summary>
    ///  Parses the bytes into this instance, starting at the specified offset.
    /// </summary>
    /// <param name="data">The bytes to parse.</param>
    /// <param name="offset">The offset at which to start. A successful parse advances it past the parsed bytes, and a failed parse does not change it.</param>
    public void ParseFrom(ReadOnlySpan<byte> data, ref int offset)
    {
        Clear();
        if ((uint)offset > (uint)data.Length)
        {
            this.status = ParseStatus.InvalidOffset;
            return;
        }
        var start = offset;
        var cursor = start;
        if (Truncated(data, start, cursor, MagicLength, FieldName.Magic)) return;
        this.magic = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, MagicLength), MagicFill);
        cursor += MagicLength;
        if (Truncated(data, start, cursor, VersionLength, FieldName.Version)) return;
        this.version = data.Slice(cursor, VersionLength)[0];
        cursor += VersionLength;
        if (Truncated(data, start, cursor, MetadataLengthLength, FieldName.MetadataLength)) return;
        this.metadataLength = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MetadataLengthLength));
        cursor += MetadataLengthLength;
        {
            int MetadataSizeRule0End;
            global::System.Int128 MetadataSizeRule0Width;
            uint metadataLengthValue = this.metadataLength;
            int? metadataEnd = null;
            const long MetadataSizeRule0Data2 = 8;
            MetadataSizeRule0Width = checked((checked((global::System.Int128)(metadataLengthValue)) + ((global::System.Int128)(MetadataSizeRule0Data2))));
            if (MetadataSizeRule0Width < RuleEmptyWidth || MetadataSizeRule0Width > (long)data.Length - start) { Fail(ParseStatus.Truncated, FieldName.MetadataLength, MetadataLengthOffset, data[start..]); return; }
            MetadataSizeRule0End = start + (int)MetadataSizeRule0Width;
            metadataEnd = MetadataSizeRule0End;
            if (MetadataSizeRule0End < cursor) { Fail(ParseStatus.Truncated, FieldName.MetadataLength, MetadataLengthOffset, data[start..]); return; }
            if (metadataEnd is int metadataDeclaredEnd)
            {
                data = data[..metadataDeclaredEnd];
            }
        }
        if (Truncated(data, start, cursor, DatasetLength, FieldName.Dataset)) return;
        this.dataset = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, DatasetLength), DatasetFill);
        cursor += DatasetLength;
        if (Truncated(data, start, cursor, SchemaLength, FieldName.Schema)) return;
        this.schema = (Schema)BinaryPrimitives.ReadUInt16LittleEndian(data.Slice(cursor, SchemaLength));
        cursor += SchemaLength;
        if (Truncated(data, start, cursor, StartLength, FieldName.Start)) return;
        this.start = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, StartLength));
        cursor += StartLength;
        if (Truncated(data, start, cursor, EndLength, FieldName.End)) return;
        this.end = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, EndLength));
        cursor += EndLength;
        if (Truncated(data, start, cursor, LimitLength, FieldName.Limit)) return;
        this.limit = BinaryPrimitives.ReadUInt64LittleEndian(data.Slice(cursor, LimitLength));
        cursor += LimitLength;
        if (Truncated(data, start, cursor, MetadataReserved8Length, FieldName.MetadataReserved8)) return;
        cursor += MetadataReserved8Length;
        if (Truncated(data, start, cursor, StypeInLength, FieldName.StypeIn)) return;
        this.stypeIn = (StypeIn)data.Slice(cursor, StypeInLength)[0];
        cursor += StypeInLength;
        if (Truncated(data, start, cursor, StypeOutLength, FieldName.StypeOut)) return;
        this.stypeOut = (StypeOut)data.Slice(cursor, StypeOutLength)[0];
        cursor += StypeOutLength;
        if (Truncated(data, start, cursor, TsOutLength, FieldName.TsOut)) return;
        this.tsOut = (TsOut)data.Slice(cursor, TsOutLength)[0];
        cursor += TsOutLength;
        if (Truncated(data, start, cursor, MetadataReservedLength, FieldName.MetadataReserved)) return;
        cursor += MetadataReservedLength;
        if (Truncated(data, start, cursor, SchemaDefinitionLengthLength, FieldName.SchemaDefinitionLength)) return;
        this.schemaDefinitionLength = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, SchemaDefinitionLengthLength));
        cursor += SchemaDefinitionLengthLength;
        {
            var schemaDefinitionAvailableBytes = data;
            var schemaDefinitionEndRead = false;
            {
                int SchemaDefinitionSizeRule0End;
                global::System.Int128 SchemaDefinitionSizeRule0Width;
                uint schemaDefinitionLengthValue = this.schemaDefinitionLength;
                int? schemaDefinitionEnd = null;
                SchemaDefinitionSizeRule0Width = checked((global::System.Int128)(schemaDefinitionLengthValue));
                if (SchemaDefinitionSizeRule0Width < RuleEmptyWidth || SchemaDefinitionSizeRule0Width > (long)data.Length - cursor) { Fail(ParseStatus.Truncated, FieldName.SchemaDefinition, cursor - start, data[start..]); return; }
                SchemaDefinitionSizeRule0End = cursor + (int)SchemaDefinitionSizeRule0Width;
                schemaDefinitionEnd = SchemaDefinitionSizeRule0End;
                if (schemaDefinitionEnd is int schemaDefinitionDeclaredEnd)
                {
                    data = data[..schemaDefinitionDeclaredEnd];
                    schemaDefinitionEndRead = true;
                }
            }
            this.schemaDefinition = data[cursor..].ToArray();
            cursor = data.Length;
            if (schemaDefinitionEndRead)
            {
                cursor = data.Length;
            }
            data = schemaDefinitionAvailableBytes;
        }
        this.symbolsCountOffset = cursor - start;
        if (Truncated(data, start, cursor, SymbolsCountLength, FieldName.SymbolsCount)) return;
        this.symbolsCount = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, SymbolsCountLength));
        cursor += SymbolsCountLength;
        {
            this.symbolListOffset = cursor - start;
            var groupStart = cursor;
            this.symbolListLength = data.Length - groupStart;
            global::System.UInt128 SymbolCountRule0Entries;
            uint symbolsCountValue = this.symbolsCount;
            var entries = new List<string>();
            SymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(symbolsCountValue)));
            while (SymbolCountRule0Entries > RuleCountNone)
            {
                string entry;
                if (Truncated(data, start, cursor, SymbolListEntryByteLength, FieldName.SymbolList))
                {
                    this.symbolList = new SymbolList(entries, this.status);
                    Fail(this.status, FieldName.SymbolList, groupStart - start, data[start..]);
                    return;
                }
                entry = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, SymbolListEntryByteLength), SymbolListFill);
                cursor += SymbolListEntryByteLength;
                entries.Add(entry);
                SymbolCountRule0Entries--;
            }
            if (SymbolCountRule0Entries > RuleCountNone) { Fail(ParseStatus.BadCount, FieldName.SymbolList, groupStart - start, data[start..]); return; }
            this.symbolList = new SymbolList(entries, ParseStatus.Ok);
            this.symbolListLength = cursor - groupStart;
        }
        this.partialCountOffset = cursor - start;
        if (Truncated(data, start, cursor, PartialCountLength, FieldName.PartialCount)) return;
        this.partialCount = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, PartialCountLength));
        cursor += PartialCountLength;
        {
            this.partialSymbolListOffset = cursor - start;
            var groupStart = cursor;
            this.partialSymbolListLength = data.Length - groupStart;
            global::System.UInt128 PartialSymbolCountRule0Entries;
            uint partialCountValue = this.partialCount;
            var entries = new List<string>();
            PartialSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(partialCountValue)));
            while (PartialSymbolCountRule0Entries > RuleCountNone)
            {
                string entry;
                if (Truncated(data, start, cursor, PartialSymbolListEntryByteLength, FieldName.PartialSymbolList))
                {
                    this.partialSymbolList = new PartialSymbolList(entries, this.status);
                    Fail(this.status, FieldName.PartialSymbolList, groupStart - start, data[start..]);
                    return;
                }
                entry = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, PartialSymbolListEntryByteLength), PartialSymbolListFill);
                cursor += PartialSymbolListEntryByteLength;
                entries.Add(entry);
                PartialSymbolCountRule0Entries--;
            }
            if (PartialSymbolCountRule0Entries > RuleCountNone) { Fail(ParseStatus.BadCount, FieldName.PartialSymbolList, groupStart - start, data[start..]); return; }
            this.partialSymbolList = new PartialSymbolList(entries, ParseStatus.Ok);
            this.partialSymbolListLength = cursor - groupStart;
        }
        this.notFoundCountOffset = cursor - start;
        if (Truncated(data, start, cursor, NotFoundCountLength, FieldName.NotFoundCount)) return;
        this.notFoundCount = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, NotFoundCountLength));
        cursor += NotFoundCountLength;
        {
            this.notFoundSymbolListOffset = cursor - start;
            var groupStart = cursor;
            this.notFoundSymbolListLength = data.Length - groupStart;
            global::System.UInt128 NotFoundSymbolCountRule0Entries;
            uint notFoundCountValue = this.notFoundCount;
            var entries = new List<string>();
            NotFoundSymbolCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(notFoundCountValue)));
            while (NotFoundSymbolCountRule0Entries > RuleCountNone)
            {
                string entry;
                if (Truncated(data, start, cursor, NotFoundSymbolListEntryByteLength, FieldName.NotFoundSymbolList))
                {
                    this.notFoundSymbolList = new NotFoundSymbolList(entries, this.status);
                    Fail(this.status, FieldName.NotFoundSymbolList, groupStart - start, data[start..]);
                    return;
                }
                entry = TextEncoding.Ascii.ReadLeftJustified(data.Slice(cursor, NotFoundSymbolListEntryByteLength), NotFoundSymbolListFill);
                cursor += NotFoundSymbolListEntryByteLength;
                entries.Add(entry);
                NotFoundSymbolCountRule0Entries--;
            }
            if (NotFoundSymbolCountRule0Entries > RuleCountNone) { Fail(ParseStatus.BadCount, FieldName.NotFoundSymbolList, groupStart - start, data[start..]); return; }
            this.notFoundSymbolList = new NotFoundSymbolList(entries, ParseStatus.Ok);
            this.notFoundSymbolListLength = cursor - groupStart;
        }
        this.mappingsCountOffset = cursor - start;
        if (Truncated(data, start, cursor, MappingsCountLength, FieldName.MappingsCount)) return;
        this.mappingsCount = BinaryPrimitives.ReadUInt32LittleEndian(data.Slice(cursor, MappingsCountLength));
        cursor += MappingsCountLength;
        {
            this.symbolMappingListOffset = cursor - start;
            var groupStart = cursor;
            this.symbolMappingListLength = data.Length - groupStart;
            global::System.UInt128 SymbolMappingCountRule0Entries;
            uint mappingsCountValue = this.mappingsCount;
            var entries = new List<SymbolMapping>();
            SymbolMappingCountRule0Entries = CreateRuleCount(checked((global::System.Int128)(mappingsCountValue)));
            while (SymbolMappingCountRule0Entries > RuleCountNone)
            {
                var entry = new SymbolMapping();
                var contract = (IGroupEntry)entry;
                contract.ParseFrom(data, ref cursor);
                if (contract.Status != ParseStatus.Ok)
                {
                    this.symbolMappingList = new SymbolMappingList(entries, contract.Status, entry);
                    this.symbolMappingList.SetOwner(this);
                    Fail(contract.Status, FieldName.SymbolMappingList, groupStart - start, data[start..]);
                    return;
                }
                entries.Add(entry);
                SymbolMappingCountRule0Entries--;
            }
            if (SymbolMappingCountRule0Entries > RuleCountNone) { Fail(ParseStatus.BadCount, FieldName.SymbolMappingList, groupStart - start, data[start..]); return; }
            this.symbolMappingList = new SymbolMappingList(entries, ParseStatus.Ok, null);
            this.symbolMappingList.SetOwner(this);
            this.symbolMappingListLength = cursor - groupStart;
        }
        this.trailer = data[cursor..].ToArray();
        cursor = data.Length;
        this.raw = data[start..cursor].ToArray();
        this.status = ParseStatus.Ok;
        offset = cursor;
    }

    private void Fail(ParseStatus status, FieldName field, int offset, ReadOnlySpan<byte> available)
    {
        this.status = status;
        this.failed = field;
        this.failedOffset = offset;
        this.raw = available.ToArray();
    }

    private bool Truncated(ReadOnlySpan<byte> data, int start, int cursor, int length, FieldName field)
    {
        if (data.Length - cursor >= length) return false;
        Fail(ParseStatus.Truncated, field, cursor - start, data[start..]);
        return true;
    }

    /// <summary>
    ///  Encodes this instance into the span, starting at the specified offset.
    /// </summary>
    /// <param name="data">The span that receives the bytes.</param>
    /// <param name="offset">The offset in the span of the first byte to write.</param>
    /// <returns>The offset immediately after the last byte written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentOutOfRangeException">The value of offset is negative or greater than the length of the span.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data, int offset)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(offset);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(offset, data.Length);
        if (data.Length - offset < EncodedByteLength)
        {
            throw new ArgumentException("The span is too small for the encoded bytes.", nameof(data));
        }
        if (!((IGroupEntry)this).Write(data, offset, out var cursor, out var failure))
        {
            throw new EncodeException(failure);
        }
        return cursor;
    }

    /// <summary>
    ///  Encodes this instance into the start of the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <returns>The number of bytes written.</returns>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    /// <exception cref="ArgumentException">The span is too small for the encoded bytes.</exception>
    public int Encode(Span<byte> data) => Encode(data, 0);

    /// <summary>
    ///  Encodes this instance into a new array.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public byte[] ToBytes()
    {
        var buffer = new byte[EncodedByteLength];
        Encode(buffer);
        return buffer;
    }

    /// <summary>
    ///  Tries to encode the entry into the span.
    /// </summary>
    /// <param name="data">The span that receives the bytes from its first byte.</param>
    /// <param name="written">When this method returns, contains the number of bytes written, or 0 if encoding failed.</param>
    /// <param name="failure">When this method returns <see langword="false"/>, contains the first field whose value cannot be encoded and the reason. It contains <see cref="EncodeStatus.BufferTooSmall"/> if the span is shorter than the encoded bytes. For a field in a group entry or nested message, it also identifies the group or field that holds it.</param>
    /// <returns><see langword="true"/> if the entry was written, or <see langword="false"/> if a value cannot be encoded or the span is too small.</returns>
    /// <remarks>
    ///  If a nested message has a trailer, the reason is <see cref="EncodeStatus.NestedTrailer"/>, and the field is the one holding the message.
    /// </remarks>
    public bool TryEncode(Span<byte> data, out int written, out EncodeFailure failure)
    {
        written = 0;
        if (data.Length < EncodedByteLength)
        {
            failure = new EncodeFailure(EncodeStatus.BufferTooSmall);
            return false;
        }
        if (!((IGroupEntry)this).Write(data, 0, out var cursor, out failure))
        {
            return false;
        }
        written = cursor;
        return true;
    }

    bool IGroupEntry.Write(Span<byte> data, int offset, out int cursor, out EncodeFailure failure)
    {
        cursor = offset;
        var position = offset;
        position += MagicLength;
        position += VersionLength;
        position += MetadataLengthLength;
        position += DatasetLength;
        position += SchemaLength;
        position += StartLength;
        position += EndLength;
        position += LimitLength;
        position += MetadataReserved8Length;
        position += StypeInLength;
        position += StypeOutLength;
        position += TsOutLength;
        position += MetadataReservedLength;
        position += SchemaDefinitionLengthLength;
        var schemaDefinitionMeasureStart = position;
        position += this.schemaDefinition.Length;
        var schemaDefinitionMeasureEnd = position;
        {
            global::System.Int128 measure = schemaDefinitionMeasureEnd - schemaDefinitionMeasureStart;
            global::System.Int128 schemaDefinitionLengthStampedValue;
            schemaDefinitionLengthStampedValue = measure;
            if (schemaDefinitionLengthStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.SchemaDefinitionLength), EncodeStatus.TooLong);
                return false;
            }
            if (schemaDefinitionLengthStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.SchemaDefinitionLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.schemaDefinitionLength = checked((uint)(schemaDefinitionLengthStampedValue));
        }
        {
            global::System.Int128 measure = this.symbolList.Count;
            global::System.Int128 symbolsCountStampedValue;
            symbolsCountStampedValue = measure;
            if (symbolsCountStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.SymbolsCount), EncodeStatus.TooLong);
                return false;
            }
            if (symbolsCountStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.SymbolsCount), EncodeStatus.DoesNotFit);
                return false;
            }
            this.symbolsCount = checked((uint)(symbolsCountStampedValue));
        }
        {
            global::System.Int128 measure = this.partialSymbolList.Count;
            global::System.Int128 partialCountStampedValue;
            partialCountStampedValue = measure;
            if (partialCountStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.PartialCount), EncodeStatus.TooLong);
                return false;
            }
            if (partialCountStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.PartialCount), EncodeStatus.DoesNotFit);
                return false;
            }
            this.partialCount = checked((uint)(partialCountStampedValue));
        }
        {
            global::System.Int128 measure = this.notFoundSymbolList.Count;
            global::System.Int128 notFoundCountStampedValue;
            notFoundCountStampedValue = measure;
            if (notFoundCountStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.NotFoundCount), EncodeStatus.TooLong);
                return false;
            }
            if (notFoundCountStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.NotFoundCount), EncodeStatus.DoesNotFit);
                return false;
            }
            this.notFoundCount = checked((uint)(notFoundCountStampedValue));
        }
        {
            global::System.Int128 measure = this.symbolMappingList.Count;
            global::System.Int128 mappingsCountStampedValue;
            mappingsCountStampedValue = measure;
            if (mappingsCountStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.MappingsCount), EncodeStatus.TooLong);
                return false;
            }
            if (mappingsCountStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.MappingsCount), EncodeStatus.DoesNotFit);
                return false;
            }
            this.mappingsCount = checked((uint)(mappingsCountStampedValue));
        }
        {
            const long MetadataSizeRule0Data2 = 8;
            global::System.Int128 measure = EncodedByteLength;
            global::System.Int128 metadataLengthStampedValue;
            try
            {
                metadataLengthStampedValue = checked((measure - checked((((global::System.Int128)(RuleNumberZero)) + ((global::System.Int128)(MetadataSizeRule0Data2))))));
            }
            catch (global::System.OverflowException)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.MetadataLength), EncodeStatus.RuleMismatch);
                return false;
            }
            if (metadataLengthStampedValue > uint.MaxValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.MetadataLength), EncodeStatus.TooLong);
                return false;
            }
            if (metadataLengthStampedValue < uint.MinValue)
            {
                failure = new EncodeFailure(new FieldView(this, (int)FieldName.MetadataLength), EncodeStatus.DoesNotFit);
                return false;
            }
            this.metadataLength = checked((uint)(metadataLengthStampedValue));
        }
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, MagicLength), TextEncoding.Ascii.Fit(this.magic, MagicLength), MagicFill);
        cursor += MagicLength;
        data.Slice(cursor, VersionLength)[0] = unchecked((byte)(this.version));
        cursor += VersionLength;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MetadataLengthLength), this.metadataLength);
        cursor += MetadataLengthLength;
        TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, DatasetLength), TextEncoding.Ascii.Fit(this.dataset, DatasetLength), DatasetFill);
        cursor += DatasetLength;
        BinaryPrimitives.WriteUInt16LittleEndian(data.Slice(cursor, SchemaLength), unchecked((ushort)(this.schema)));
        cursor += SchemaLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, StartLength), this.start);
        cursor += StartLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, EndLength), this.end);
        cursor += EndLength;
        BinaryPrimitives.WriteUInt64LittleEndian(data.Slice(cursor, LimitLength), this.limit);
        cursor += LimitLength;
        {
            if (this.raw.Length >= MetadataReserved8Offset + MetadataReserved8Length)
            {
                this.raw.AsSpan(MetadataReserved8Offset, MetadataReserved8Length).CopyTo(data.Slice(cursor, MetadataReserved8Length));
            }
            else
            {
                data.Slice(cursor, MetadataReserved8Length).Clear();
            }
            cursor += MetadataReserved8Length;
        }
        data.Slice(cursor, StypeInLength)[0] = unchecked((byte)(this.stypeIn));
        cursor += StypeInLength;
        data.Slice(cursor, StypeOutLength)[0] = unchecked((byte)(this.stypeOut));
        cursor += StypeOutLength;
        data.Slice(cursor, TsOutLength)[0] = unchecked((byte)(this.tsOut));
        cursor += TsOutLength;
        {
            if (this.raw.Length >= MetadataReservedOffset + MetadataReservedLength)
            {
                this.raw.AsSpan(MetadataReservedOffset, MetadataReservedLength).CopyTo(data.Slice(cursor, MetadataReservedLength));
            }
            else
            {
                data.Slice(cursor, MetadataReservedLength).Clear();
            }
            cursor += MetadataReservedLength;
        }
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, SchemaDefinitionLengthLength), this.schemaDefinitionLength);
        cursor += SchemaDefinitionLengthLength;
        this.schemaDefinition.Span.CopyTo(data.Slice(cursor, this.schemaDefinition.Length));
        cursor += this.schemaDefinition.Length;
        this.symbolsCountOffset = cursor - offset;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, SymbolsCountLength), this.symbolsCount);
        cursor += SymbolsCountLength;
        {
            this.symbolListOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.symbolList.Count; index++)
            {
                TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, SymbolListEntryByteLength), TextEncoding.Ascii.Fit(this.symbolList[index], SymbolListEntryByteLength), SymbolListFill);
                cursor += SymbolListEntryByteLength;
            }
            this.symbolListLength = cursor - groupStart;
        }
        this.partialCountOffset = cursor - offset;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, PartialCountLength), this.partialCount);
        cursor += PartialCountLength;
        {
            this.partialSymbolListOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.partialSymbolList.Count; index++)
            {
                TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, PartialSymbolListEntryByteLength), TextEncoding.Ascii.Fit(this.partialSymbolList[index], PartialSymbolListEntryByteLength), PartialSymbolListFill);
                cursor += PartialSymbolListEntryByteLength;
            }
            this.partialSymbolListLength = cursor - groupStart;
        }
        this.notFoundCountOffset = cursor - offset;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, NotFoundCountLength), this.notFoundCount);
        cursor += NotFoundCountLength;
        {
            this.notFoundSymbolListOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.notFoundSymbolList.Count; index++)
            {
                TextEncoding.Ascii.WriteLeftJustified(data.Slice(cursor, NotFoundSymbolListEntryByteLength), TextEncoding.Ascii.Fit(this.notFoundSymbolList[index], NotFoundSymbolListEntryByteLength), NotFoundSymbolListFill);
                cursor += NotFoundSymbolListEntryByteLength;
            }
            this.notFoundSymbolListLength = cursor - groupStart;
        }
        this.mappingsCountOffset = cursor - offset;
        BinaryPrimitives.WriteUInt32LittleEndian(data.Slice(cursor, MappingsCountLength), this.mappingsCount);
        cursor += MappingsCountLength;
        {
            this.symbolMappingListOffset = cursor - offset;
            var groupStart = cursor;
            for (var index = 0; index < this.symbolMappingList.Count; index++)
            {
                if (!((IGroupEntry)this.symbolMappingList[index]).Write(data, cursor, out cursor, out var memberFailure))
                {
                    failure = memberFailure.Within(new FieldView(this, (int)FieldName.SymbolMappingList), index);
                    return false;
                }
            }
            this.symbolMappingListLength = cursor - groupStart;
        }
        this.trailer.CopyTo(data[cursor..]);
        cursor += this.trailer.Length;
        failure = default;
        return true;
    }

    bool IFieldOwner.TryRaw(out ReadOnlyMemory<byte> raw)
    {
        if (this.status == ParseStatus.None || this.modified)
        {
            if (this.cache is null)
            {
                var buffer = new byte[EncodedByteLength];
                if (!((IGroupEntry)this).Write(buffer, 0, out _, out _))
                {
                    raw = ReadOnlyMemory<byte>.Empty;
                    return false;
                }
                this.cache = buffer;
            }
            raw = this.cache;
            return true;
        }
        raw = this.raw;
        return true;
    }

    /// <summary>
    ///  Checks every field against the values that the protocol specification allows.
    /// </summary>
    /// <returns>The fields whose values are not allowed, each with the reason, or an empty list if every value is allowed.</returns>
    /// <remarks>
    ///  Validate checks the current values. After a parse, check Status first, because a field that the parse did not reach holds its cleared value. A new or cleared instance holds zero in a field that can be null, until the field is set.
    /// </remarks>
    public List<ValidationFailure> Validate()
    {
        var failures = new List<ValidationFailure>();
        if (!global::System.Text.Ascii.IsValid(this.magic))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Magic), ValidationStatus.NonAsciiText));
        }
        if (!global::System.Text.Ascii.IsValid(this.dataset))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Dataset), ValidationStatus.NonAsciiText));
        }
        if (!(this.schema == SchemaNullValue || global::System.Enum.IsDefined(this.schema)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.Schema), ValidationStatus.UndeclaredValue));
        }
        if (!(this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeIn), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.stypeOut))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.StypeOut), ValidationStatus.UndeclaredValue));
        }
        if (!global::System.Enum.IsDefined(this.tsOut))
        {
            failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.TsOut), ValidationStatus.UndeclaredValue));
        }
        for (var index = 0; index < this.symbolList.Count; index++)
        {
            if (!global::System.Text.Ascii.IsValid(this.symbolList[index]))
            {
                failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.SymbolList), ValidationStatus.NonAsciiText));
            }
        }
        for (var index = 0; index < this.partialSymbolList.Count; index++)
        {
            if (!global::System.Text.Ascii.IsValid(this.partialSymbolList[index]))
            {
                failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.PartialSymbolList), ValidationStatus.NonAsciiText));
            }
        }
        for (var index = 0; index < this.notFoundSymbolList.Count; index++)
        {
            if (!global::System.Text.Ascii.IsValid(this.notFoundSymbolList[index]))
            {
                failures.Add(new ValidationFailure(new FieldView(this, (int)FieldName.NotFoundSymbolList), ValidationStatus.NonAsciiText));
            }
        }
        for (var index = 0; index < this.symbolMappingList.Count; index++)
        {
            foreach (var failure in ((IGroupEntry)this.symbolMappingList[index]).Validate())
            {
                failures.Add(failure.Within(new FieldView(this, (int)FieldName.SymbolMappingList), index));
            }
        }
        return failures;
    }

    /// <summary>
    ///  Determines whether another instance has the same values.
    /// </summary>
    public bool Equals(Metadata? other)
    {
        if (other is null)
        {
            return false;
        }
        if (this.magic != other.magic) return false;
        if (this.version != other.version) return false;
        if (this.dataset != other.dataset) return false;
        if (this.schema != other.schema) return false;
        if (this.start != other.start) return false;
        if (this.end != other.end) return false;
        if (this.limit != other.limit) return false;
        if (this.stypeIn != other.stypeIn) return false;
        if (this.stypeOut != other.stypeOut) return false;
        if (this.tsOut != other.tsOut) return false;
        if (!this.schemaDefinition.Span.SequenceEqual(other.schemaDefinition.Span)) return false;
        if (!this.symbolList.Equals(other.symbolList)) return false;
        if (!this.partialSymbolList.Equals(other.partialSymbolList)) return false;
        if (!this.notFoundSymbolList.Equals(other.notFoundSymbolList)) return false;
        if (!this.symbolMappingList.Equals(other.symbolMappingList)) return false;
        return true;
    }

    /// <summary>
    ///  Determines whether the specified object is an instance of the same type with the same values.
    /// </summary>
    public override bool Equals(object? obj) => obj is Metadata other && Equals(other);

    /// <summary>
    ///  Returns a hash code computed from the values.
    /// </summary>
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(this.magic);
        hash.Add(this.version);
        hash.Add(this.dataset);
        hash.Add(this.schema);
        hash.Add(this.start);
        hash.Add(this.end);
        hash.Add(this.limit);
        hash.Add(this.stypeIn);
        hash.Add(this.stypeOut);
        hash.Add(this.tsOut);
        hash.AddBytes(this.schemaDefinition.Span);
        hash.Add(this.symbolList);
        hash.Add(this.partialSymbolList);
        hash.Add(this.notFoundSymbolList);
        hash.Add(this.symbolMappingList);
        return hash.ToHashCode();
    }

    /// <summary>
    ///  Determines whether the specified bytes equal the result of <see cref="ToBytes"/>.
    /// </summary>
    /// <exception cref="EncodeException">A value does not fit its field.</exception>
    public bool BytesEqual(ReadOnlySpan<byte> data) => ToBytes().AsSpan().SequenceEqual(data);

    /// <summary>
    ///  Identifies the fields that differ between two instances.
    /// </summary>
    public readonly struct Changes : IEquatable<Changes>, IEnumerable<FieldName>
    {
        internal const int WordShift = 6;
        internal const int WordBitMask = 63;
        private const int WordCount = 1;
        private const int FieldCount = 23;

        [global::System.Runtime.CompilerServices.InlineArray(WordCount)]
        private struct Words
        {
            private ulong word;
        }

        private readonly Words words;

        private Changes(Words words)
        {
            this.words = words;
        }

        /// <summary>
        ///  Gets a value with no fields marked.
        /// </summary>
        public static Changes None => default;

        /// <summary>
        ///  Gets a value indicating whether no fields are marked.
        /// </summary>
        public bool IsNone => ((ReadOnlySpan<ulong>)this.words).IndexOfAnyExcept(0UL) < 0;

        internal ulong[] ToWords() => ((ReadOnlySpan<ulong>)this.words).ToArray();

        internal Changes With(int bit)
        {
            var word = bit >> WordShift;
            if ((uint)word >= WordCount) return this;
            var words = this.words;
            words[word] |= 1UL << (bit & WordBitMask);
            return new(words);
        }

        internal Changes With(FieldName field) => With((int)field);

        internal bool Has(int bit)
        {
            var word = bit >> WordShift;
            return (uint)word < WordCount && (this.words[word] & (1UL << (bit & WordBitMask))) != 0;
        }

        /// <summary>
        ///  Determines whether the specified field is marked.
        /// </summary>
        public bool Has(FieldName field) => Has((int)field);

        /// <summary>
        ///  Determines whether another value marks the same fields.
        /// </summary>
        public bool Equals(Changes other) => ((ReadOnlySpan<ulong>)this.words).SequenceEqual((ReadOnlySpan<ulong>)other.words);

        /// <summary>
        ///  Determines whether the specified object is a value that marks the same fields.
        /// </summary>
        public override bool Equals(object? obj) => obj is Changes other && Equals(other);

        /// <summary>
        ///  Returns a hash code computed from the marked fields.
        /// </summary>
        public override int GetHashCode()
        {
            var hc = new HashCode();
            foreach (var word in this.words)
            {
                hc.Add(word);
            }
            return hc.ToHashCode();
        }

        /// <summary>
        ///  Determines whether two values mark the same fields.
        /// </summary>
        public static bool operator ==(Changes a, Changes b) => a.Equals(b);

        /// <summary>
        ///  Determines whether two values mark different fields.
        /// </summary>
        public static bool operator !=(Changes a, Changes b) => !a.Equals(b);

        /// <summary>
        ///  Returns an enumerator that iterates through the marked fields in field order.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        IEnumerator<FieldName> IEnumerable<FieldName>.GetEnumerator() => GetEnumerator();

        global::System.Collections.IEnumerator global::System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

        /// <summary>
        ///  Enumerates the marked fields of a value.
        /// </summary>
        public struct Enumerator : IEnumerator<FieldName>
        {
            private const int BeforeFirst = -1;
            private readonly Changes changes;
            private int field;

            internal Enumerator(Changes changes)
            {
                this.changes = changes;
                this.field = BeforeFirst;
            }

            /// <summary>
            ///  Gets the current marked field.
            /// </summary>
            public readonly FieldName Current => (FieldName)this.field;

            readonly object global::System.Collections.IEnumerator.Current => Current;

            /// <summary>
            ///  Advances the enumerator to the next marked field.
            /// </summary>
            public bool MoveNext()
            {
                while (++this.field < FieldCount)
                {
                    if (this.changes.Has(this.field)) return true;
                }
                return false;
            }

            /// <summary>
            ///  Sets the enumerator to its initial position, before the first field.
            /// </summary>
            public void Reset() => this.field = BeforeFirst;

            /// <summary>
            ///  Releases the enumerator.
            /// </summary>
            public readonly void Dispose() { }
        }

        /// <summary>
        ///  Returns the names of the marked fields, or None if no fields are marked.
        /// </summary>
        public override string ToString()
        {
            var names = new List<string>();
            if (Has(FieldName.Magic)) names.Add(nameof(FieldName.Magic));
            if (Has(FieldName.Version)) names.Add(nameof(FieldName.Version));
            if (Has(FieldName.MetadataLength)) names.Add(nameof(FieldName.MetadataLength));
            if (Has(FieldName.Dataset)) names.Add(nameof(FieldName.Dataset));
            if (Has(FieldName.Schema)) names.Add(nameof(FieldName.Schema));
            if (Has(FieldName.Start)) names.Add(nameof(FieldName.Start));
            if (Has(FieldName.End)) names.Add(nameof(FieldName.End));
            if (Has(FieldName.Limit)) names.Add(nameof(FieldName.Limit));
            if (Has(FieldName.MetadataReserved8)) names.Add(nameof(FieldName.MetadataReserved8));
            if (Has(FieldName.StypeIn)) names.Add(nameof(FieldName.StypeIn));
            if (Has(FieldName.StypeOut)) names.Add(nameof(FieldName.StypeOut));
            if (Has(FieldName.TsOut)) names.Add(nameof(FieldName.TsOut));
            if (Has(FieldName.MetadataReserved)) names.Add(nameof(FieldName.MetadataReserved));
            if (Has(FieldName.SchemaDefinitionLength)) names.Add(nameof(FieldName.SchemaDefinitionLength));
            if (Has(FieldName.SchemaDefinition)) names.Add(nameof(FieldName.SchemaDefinition));
            if (Has(FieldName.SymbolsCount)) names.Add(nameof(FieldName.SymbolsCount));
            if (Has(FieldName.SymbolList)) names.Add(nameof(FieldName.SymbolList));
            if (Has(FieldName.PartialCount)) names.Add(nameof(FieldName.PartialCount));
            if (Has(FieldName.PartialSymbolList)) names.Add(nameof(FieldName.PartialSymbolList));
            if (Has(FieldName.NotFoundCount)) names.Add(nameof(FieldName.NotFoundCount));
            if (Has(FieldName.NotFoundSymbolList)) names.Add(nameof(FieldName.NotFoundSymbolList));
            if (Has(FieldName.MappingsCount)) names.Add(nameof(FieldName.MappingsCount));
            if (Has(FieldName.SymbolMappingList)) names.Add(nameof(FieldName.SymbolMappingList));
            return names.Count == 0 ? "None" : string.Join(", ", names);
        }
    }

    /// <summary>
    ///  Compares this instance with another and identifies the fields that differ.
    /// </summary>
    /// <returns>The fields that differ, or None if there are no differences.</returns>
    /// <exception cref="ArgumentNullException">The other instance is <see langword="null"/>.</exception>
    public Changes Diff(Metadata other)
    {
        ArgumentNullException.ThrowIfNull(other);
        var diff = Changes.None;
        if (this.magic != other.magic) diff = diff.With(FieldName.Magic);
        if (this.version != other.version) diff = diff.With(FieldName.Version);
        if (this.dataset != other.dataset) diff = diff.With(FieldName.Dataset);
        if (this.schema != other.schema) diff = diff.With(FieldName.Schema);
        if (this.start != other.start) diff = diff.With(FieldName.Start);
        if (this.end != other.end) diff = diff.With(FieldName.End);
        if (this.limit != other.limit) diff = diff.With(FieldName.Limit);
        if (this.stypeIn != other.stypeIn) diff = diff.With(FieldName.StypeIn);
        if (this.stypeOut != other.stypeOut) diff = diff.With(FieldName.StypeOut);
        if (this.tsOut != other.tsOut) diff = diff.With(FieldName.TsOut);
        if (!this.schemaDefinition.Span.SequenceEqual(other.schemaDefinition.Span)) diff = diff.With(FieldName.SchemaDefinition);
        if (!this.symbolList.Equals(other.symbolList)) diff = diff.With(FieldName.SymbolList);
        if (!this.partialSymbolList.Equals(other.partialSymbolList)) diff = diff.With(FieldName.PartialSymbolList);
        if (!this.notFoundSymbolList.Equals(other.notFoundSymbolList)) diff = diff.With(FieldName.NotFoundSymbolList);
        if (!this.symbolMappingList.Equals(other.symbolMappingList)) diff = diff.With(FieldName.SymbolMappingList);
        return diff;
    }

    private void CopyField(FieldName field, Metadata from)
    {
        switch (field)
        {
            case FieldName.Magic:
                this.magic = from.magic;
                break;
            case FieldName.Version:
                this.version = from.version;
                break;
            case FieldName.MetadataLength:
                this.metadataLength = from.metadataLength;
                break;
            case FieldName.Dataset:
                this.dataset = from.dataset;
                break;
            case FieldName.Schema:
                this.schema = from.schema;
                break;
            case FieldName.Start:
                this.start = from.start;
                break;
            case FieldName.End:
                this.end = from.end;
                break;
            case FieldName.Limit:
                this.limit = from.limit;
                break;
            case FieldName.StypeIn:
                this.stypeIn = from.stypeIn;
                break;
            case FieldName.StypeOut:
                this.stypeOut = from.stypeOut;
                break;
            case FieldName.TsOut:
                this.tsOut = from.tsOut;
                break;
            case FieldName.SchemaDefinitionLength:
                this.schemaDefinitionLength = from.schemaDefinitionLength;
                break;
            case FieldName.SchemaDefinition:
                this.schemaDefinition = from.schemaDefinition.ToArray();
                break;
            case FieldName.SymbolsCount:
                this.symbolsCount = from.symbolsCount;
                break;
            case FieldName.SymbolList:
                this.symbolList = new SymbolList(from.symbolList);
                break;
            case FieldName.PartialCount:
                this.partialCount = from.partialCount;
                break;
            case FieldName.PartialSymbolList:
                this.partialSymbolList = new PartialSymbolList(from.partialSymbolList);
                break;
            case FieldName.NotFoundCount:
                this.notFoundCount = from.notFoundCount;
                break;
            case FieldName.NotFoundSymbolList:
                this.notFoundSymbolList = new NotFoundSymbolList(from.notFoundSymbolList);
                break;
            case FieldName.MappingsCount:
                this.mappingsCount = from.mappingsCount;
                break;
            case FieldName.SymbolMappingList:
                this.symbolMappingList.SetOwner(null);
                this.symbolMappingList = new SymbolMappingList(from.symbolMappingList);
                this.symbolMappingList.SetOwner(this);
                break;
            default:
                return;
        }
        Touch();
    }

    /// <summary>
    ///  Returns the values formatted on a single line.
    /// </summary>
    public override string ToString() => $"Metadata {{ Magic: {(Text.Of(this.magic))}, Version: {(Text.Of(this.version))}, MetadataLength: {(Text.Of(this.metadataLength))}, Dataset: {(Text.Of(this.dataset))}, Schema: {(Text.Enum(this.schema == SchemaNullValue ? (Schema?)null : this.schema))}, Start: {(Text.Instant(this.start, StartNanosecondsPerUnit))}, End: {(Text.Instant(this.end, EndNanosecondsPerUnit, EndNullValue))}, Limit: {(Text.Of(this.limit == LimitNullValue ? (ulong?)null : this.limit))}, StypeIn: {(Text.Enum(this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn))}, StypeOut: {(Text.Enum(this.stypeOut))}, TsOut: {(Text.Enum(this.tsOut))}, SchemaDefinitionLength: {(Text.Of(this.schemaDefinitionLength))}, SchemaDefinition: {(Text.Bytes(this.schemaDefinition))}, SymbolsCount: {(Text.Of(this.symbolsCount))}, SymbolList: {(Text.Entries(global::System.Linq.Enumerable.Range(0, this.symbolList.Count).Select(index => Text.Of(this.symbolList[index]))))}, PartialCount: {(Text.Of(this.partialCount))}, PartialSymbolList: {(Text.Entries(global::System.Linq.Enumerable.Range(0, this.partialSymbolList.Count).Select(index => Text.Of(this.partialSymbolList[index]))))}, NotFoundCount: {(Text.Of(this.notFoundCount))}, NotFoundSymbolList: {(Text.Entries(global::System.Linq.Enumerable.Range(0, this.notFoundSymbolList.Count).Select(index => Text.Of(this.notFoundSymbolList[index]))))}, MappingsCount: {(Text.Of(this.mappingsCount))}, SymbolMappingList: {(Text.Entries(this.symbolMappingList))} }}{(this.trailer.Length == 0 ? string.Empty : $" Trailer: {(Text.Of(this.trailer.Length))} bytes")}";

    private string DebuggerDisplay => $"[{(Text.Of(this.magic))}] [{(Text.Of(this.version))}] [{(Text.Of(this.metadataLength))}]";

    IFieldOwner? IFieldOwner.Owner
    {
        get => this.owner;
        set => this.owner = value;
    }

    FieldDescriptor[] IFieldOwner.Descriptors => Descriptors;

    void IFieldOwner.Touch() => Touch();

    string IFieldOwner.TextOf(int field) => (FieldName)field switch
    {
        FieldName.Magic => Text.Of(this.magic),
        FieldName.Version => Text.Of(this.version),
        FieldName.MetadataLength => Text.Of(this.metadataLength),
        FieldName.Dataset => Text.Of(this.dataset),
        FieldName.Schema => Text.Enum(this.schema == SchemaNullValue ? (Schema?)null : this.schema),
        FieldName.Start => Text.Instant(this.start, StartNanosecondsPerUnit),
        FieldName.End => Text.Instant(this.end, EndNanosecondsPerUnit, EndNullValue),
        FieldName.Limit => Text.Of(this.limit == LimitNullValue ? (ulong?)null : this.limit),
        FieldName.StypeIn => Text.Enum(this.stypeIn == StypeInNullValue ? (StypeIn?)null : this.stypeIn),
        FieldName.StypeOut => Text.Enum(this.stypeOut),
        FieldName.TsOut => Text.Enum(this.tsOut),
        FieldName.SchemaDefinitionLength => Text.Of(this.schemaDefinitionLength),
        FieldName.SchemaDefinition => Text.Bytes(this.schemaDefinition),
        FieldName.SymbolsCount => Text.Of(this.symbolsCount),
        FieldName.SymbolList => Text.Entries(global::System.Linq.Enumerable.Range(0, this.symbolList.Count).Select(index => Text.Of(this.symbolList[index]))),
        FieldName.PartialCount => Text.Of(this.partialCount),
        FieldName.PartialSymbolList => Text.Entries(global::System.Linq.Enumerable.Range(0, this.partialSymbolList.Count).Select(index => Text.Of(this.partialSymbolList[index]))),
        FieldName.NotFoundCount => Text.Of(this.notFoundCount),
        FieldName.NotFoundSymbolList => Text.Entries(global::System.Linq.Enumerable.Range(0, this.notFoundSymbolList.Count).Select(index => Text.Of(this.notFoundSymbolList[index]))),
        FieldName.MappingsCount => Text.Of(this.mappingsCount),
        FieldName.SymbolMappingList => Text.Entries(this.symbolMappingList),
        _ => string.Empty,
    };

    bool IFieldOwner.IsRecognized(int field) => (FieldName)field switch
    {
        FieldName.Schema => (this.schema == SchemaNullValue || global::System.Enum.IsDefined(this.schema)),
        FieldName.StypeIn => (this.stypeIn == StypeInNullValue || global::System.Enum.IsDefined(this.stypeIn)),
        FieldName.StypeOut => global::System.Enum.IsDefined(this.stypeOut),
        FieldName.TsOut => global::System.Enum.IsDefined(this.tsOut),
        FieldName.SymbolMappingList => this.symbolMappingList.IsRecognized,
        _ => true,
    };

    bool IFieldOwner.IsPresent(int field) => (FieldName)field switch
    {
        FieldName.Schema => this.schema != SchemaNullValue,
        FieldName.End => this.end != EndNullValue,
        FieldName.Limit => this.limit != LimitNullValue,
        FieldName.StypeIn => this.stypeIn != StypeInNullValue,
        FieldName.SymbolList => this.symbolList.Count != 0,
        FieldName.PartialSymbolList => this.partialSymbolList.Count != 0,
        FieldName.NotFoundSymbolList => this.notFoundSymbolList.Count != 0,
        FieldName.SymbolMappingList => this.symbolMappingList.Count != 0,
        _ => true,
    };

    bool IFieldOwner.IsParsed(int field) => this.status == ParseStatus.Ok || field < (int?)this.failed;

    bool IFieldOwner.IsReached(int field) => this.modified || this.status is ParseStatus.None or ParseStatus.Ok || field <= (int?)this.failed;

    bool IFieldOwner.HasParseLayout(int field) => (FieldName)field switch
    {
        FieldName.SymbolsCount => true,
        FieldName.SymbolList => true,
        FieldName.PartialCount => true,
        FieldName.PartialSymbolList => true,
        FieldName.NotFoundCount => true,
        FieldName.NotFoundSymbolList => true,
        FieldName.MappingsCount => true,
        FieldName.SymbolMappingList => true,
        _ => false,
    };

    int IFieldOwner.OffsetOf(int field) => (FieldName)field switch
    {
        FieldName.Magic => MagicOffset,
        FieldName.Version => VersionOffset,
        FieldName.MetadataLength => MetadataLengthOffset,
        FieldName.Dataset => DatasetOffset,
        FieldName.Schema => SchemaOffset,
        FieldName.Start => StartOffset,
        FieldName.End => EndOffset,
        FieldName.Limit => LimitOffset,
        FieldName.MetadataReserved8 => MetadataReserved8Offset,
        FieldName.StypeIn => StypeInOffset,
        FieldName.StypeOut => StypeOutOffset,
        FieldName.TsOut => TsOutOffset,
        FieldName.MetadataReserved => MetadataReservedOffset,
        FieldName.SchemaDefinitionLength => SchemaDefinitionLengthOffset,
        FieldName.SchemaDefinition => SchemaDefinitionOffset,
        FieldName.SymbolsCount => this.symbolsCountOffset,
        FieldName.SymbolList => this.symbolListOffset,
        FieldName.PartialCount => this.partialCountOffset,
        FieldName.PartialSymbolList => this.partialSymbolListOffset,
        FieldName.NotFoundCount => this.notFoundCountOffset,
        FieldName.NotFoundSymbolList => this.notFoundSymbolListOffset,
        FieldName.MappingsCount => this.mappingsCountOffset,
        FieldName.SymbolMappingList => this.symbolMappingListOffset,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    int IFieldOwner.LengthOf(int field) => (FieldName)field switch
    {
        FieldName.Magic => MagicLength,
        FieldName.Version => VersionLength,
        FieldName.MetadataLength => MetadataLengthLength,
        FieldName.Dataset => DatasetLength,
        FieldName.Schema => SchemaLength,
        FieldName.Start => StartLength,
        FieldName.End => EndLength,
        FieldName.Limit => LimitLength,
        FieldName.MetadataReserved8 => MetadataReserved8Length,
        FieldName.StypeIn => StypeInLength,
        FieldName.StypeOut => StypeOutLength,
        FieldName.TsOut => TsOutLength,
        FieldName.MetadataReserved => MetadataReservedLength,
        FieldName.SchemaDefinitionLength => SchemaDefinitionLengthLength,
        FieldName.SchemaDefinition => this.schemaDefinition.Length,
        FieldName.SymbolsCount => SymbolsCountLength,
        FieldName.SymbolList => this.symbolListLength,
        FieldName.PartialCount => PartialCountLength,
        FieldName.PartialSymbolList => this.partialSymbolListLength,
        FieldName.NotFoundCount => NotFoundCountLength,
        FieldName.NotFoundSymbolList => this.notFoundSymbolListLength,
        FieldName.MappingsCount => MappingsCountLength,
        FieldName.SymbolMappingList => this.symbolMappingListLength,
        _ => throw new ArgumentOutOfRangeException(nameof(field)),
    };

    IReadOnlyList<IGroupEntry>? IFieldOwner.EntriesOf(int field) => (FieldName)field switch
    {
        FieldName.SymbolMappingList => this.symbolMappingList,
        _ => null,
    };

    IMessage? IFieldOwner.MessageOf(int field) => null;

    ulong[] IFieldOwner.ChangedAgainst(IFieldOwner other) => other is Metadata typed ? Diff(typed).ToWords() : throw new ArgumentException("The other instance is not of the same type.", nameof(other));

    void IFieldOwner.CopyField(int field, IFieldOwner from) => CopyField((FieldName)field, from as Metadata ?? throw new ArgumentException("The other instance is not of the same type.", nameof(from)));
}
