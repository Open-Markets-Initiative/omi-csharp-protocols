using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Md Entry Px
    /// </summary>

    public struct MdEntryPx
    {
        /// <summary>
        ///  Sentinel null value for Md Entry Px
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Md Entry Px
        /// </summary>
        public const ulong Maximum = 92233720368.54775807;

        /// <summary>
        ///  Minimum value for Md Entry Px
        /// </summary>
        public const ulong Minimum = -92233720368.54775807;

        /// <summary>
        ///  Fix Tag for Md Entry Px
        /// </summary>
        public const ushort FixTag = 270;

        /// <summary>
        ///  Decimal place factor for Md Entry Px
        /// </summary>
        public const ulong Factor = 100000000;

        /// <summary>
        ///  Size of Md Entry Px in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Md Entry Px value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Md Entry Px field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Md Entry Px
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Md Entry Px
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryRead(out ulong value)
        {
            if (HasValue)
            {
                value = Decode();
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        ///  Write Md Entry Px
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Md Entry Px to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Md Entry Px as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}