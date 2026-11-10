namespace Iex.IexEquities.DeepPlus.IexTp.v1_01;

internal interface IFieldOwner
{
    bool TryRaw(out ReadOnlyMemory<byte> raw);
    IFieldOwner? Owner { get; set; }
    FieldDescriptor[] Descriptors { get; }
    void Touch();
    string TextOf(int field);
    bool IsRecognized(int field);
    bool IsPresent(int field);
    bool IsParsed(int field);
    bool IsReached(int field);
    bool HasParseLayout(int field);
    int OffsetOf(int field);
    int LengthOf(int field);
    IReadOnlyList<IGroupEntry>? EntriesOf(int field);
    IMessage? MessageOf(int field);
    IReadOnlyList<IMessage>? MessagesOf(int field) => null;
    ulong[] ChangedAgainst(IFieldOwner other);
    void CopyField(int field, IFieldOwner from);
    ReadOnlyMemory<byte> CodeBytes(int field) => ReadOnlyMemory<byte>.Empty;
}
