using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Bid Px
    /// </summary>

    public struct BidPx
    {
        /// <summary>
        ///  Sentinel null value for Bid Px
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Bid Px
        /// </summary>
        public const ulong Maximum = 92233720368.54775807;

        /// <summary>
        ///  Minimum value for Bid Px
        /// </summary>
        public const ulong Minimum = -92233720368.54775807;

        /// <summary>
        ///  Fix Tag for Bid Px
        /// </summary>
        public const ushort FixTag = 132;

        /// <summary>
        ///  Decimal place factor for Bid Px
        /// </summary>
        public const ulong Factor = 100000000;

        /// <summary>
        ///  Size of Bid Px in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Bid Px value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Bid Px field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Bid Px
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Bid Px
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
        ///  Write Bid Px
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Bid Px to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Bid Px as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}