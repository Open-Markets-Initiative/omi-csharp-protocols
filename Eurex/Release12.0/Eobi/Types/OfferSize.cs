using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Offer Size
    /// </summary>

    public struct OfferSize
    {
        /// <summary>
        ///  Sentinel null value for Offer Size
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Offer Size
        /// </summary>
        public const ulong Maximum = 922337203685477.5807;

        /// <summary>
        ///  Minimum value for Offer Size
        /// </summary>
        public const ulong Minimum = -922337203685477.5807;

        /// <summary>
        ///  Fix Tag for Offer Size
        /// </summary>
        public const ushort FixTag = 135;

        /// <summary>
        ///  Decimal place factor for Offer Size
        /// </summary>
        public const ulong Factor = 10000;

        /// <summary>
        ///  Size of Offer Size in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Offer Size value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Offer Size field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Offer Size
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Offer Size
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
        ///  Write Offer Size
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Offer Size to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Offer Size as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}