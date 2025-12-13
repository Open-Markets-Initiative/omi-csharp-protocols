using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Strike Price
    /// </summary>

    public struct StrikePrice
    {
        /// <summary>
        ///  Sentinel null value for Strike Price
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Strike Price
        /// </summary>
        public const ulong Maximum = 92233720368.54775807;

        /// <summary>
        ///  Minimum value for Strike Price
        /// </summary>
        public const ulong Minimum = -92233720368.54775807;

        /// <summary>
        ///  Fix Tag for Strike Price
        /// </summary>
        public const ushort FixTag = 202;

        /// <summary>
        ///  Decimal place factor for Strike Price
        /// </summary>
        public const ulong Factor = 100000000;

        /// <summary>
        ///  Size of Strike Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Strike Price value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Strike Price field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Strike Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Strike Price
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
        ///  Write Strike Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Strike Price to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Strike Price as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}