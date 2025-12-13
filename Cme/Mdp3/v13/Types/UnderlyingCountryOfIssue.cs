using System.Runtime.CompilerServices;

namespace Cme.Mdp3;

/// <summary>
///  Underlying Country Of Issue: Underlying Security's CountryOfIssue. See CountryOfIssue (470) field for description
/// </summary>

public unsafe struct UnderlyingCountryOfIssue
{
    /// <summary>
    ///  Fix Tag for Underlying Country Of Issue
    /// </summary>
    public const ushort FixTag = 592;

    /// <summary>
    ///  Size of Underlying Country Of Issue underlying field in bytes
    /// </summary>
    public const int Size = 2;

    /// <summary>
    ///  Length of Underlying Country Of Issue field
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            if (Bytes[0] == 0) { return 0; }
            if (Bytes[1] == 0) { return 1; }

            return 2;
        }
    }

    /// <summary>
    ///  Underlying Country Of Issue value
    /// </summary>
    public readonly string Value
        => Decode(this);

    /// <summary>
    ///  Does Underlying Country Of Issue field contain a value?
    /// </summary>
    public bool HasValue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get { return Bytes[0] != 0; }
    }

    /// <summary>
    ///  Read Underlying Country Of Issue
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string Decode(UnderlyingCountryOfIssue value)
        => new string((sbyte*)value.Bytes, 0, value.Length);

    /// <summary>
    ///  Try Read Underlying Country Of Issue
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryRead(out string value)
    {
        if (HasValue)
        {
            value = Decode(this);
            return true;
        }

        value = string.Empty;
        return false;
    }

    /// <summary>
    ///  Encode Underlying Country Of Issue
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Encode(string value)
    {
        var length = value.Length;
        Bytes[0] = length > 0 ? (byte)value[0] : (byte)0;
        Bytes[1] = length > 1 ? (byte)value[1] : (byte)0;
    }

    /// <summary>
    ///  Underlying Country Of Issue as string
    /// </summary>
    public readonly override string ToString()
        => Value;

    /// <summary>
    ///  Underlying bytes
    /// </summary>
    internal fixed byte Bytes[Size];
}
