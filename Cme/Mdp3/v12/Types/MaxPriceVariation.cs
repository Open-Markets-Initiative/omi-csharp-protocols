using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Max Price Variation: Differential value for price banding
    /// </summary>

    public struct MaxPriceVariation
    {
        /// <summary>
        ///  Fix Tag for Max Price Variation
        /// </summary>
        public const ushort FixTag = 1143;

        /// <summary>
        ///  Sentinel null value for Max Price Variation
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Max Price Variation
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Max Price Variation in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Max Price Variation value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Max Price Variation field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Max Price Variation
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Max Price Variation
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryRead(out long value)
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
        ///  Write Max Price Variation
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Max Price Variation to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Max Price Variation as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}