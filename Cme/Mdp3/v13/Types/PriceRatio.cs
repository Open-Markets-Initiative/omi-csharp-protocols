using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Price Ratio: Used for price calculation in spread and leg pricing
    /// </summary>

    public struct PriceRatio
    {
        /// <summary>
        ///  Fix Tag for Price Ratio
        /// </summary>
        public const ushort FixTag = 5770;

        /// <summary>
        ///  Sentinel null value for Price Ratio
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Price Ratio
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Price Ratio in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Price Ratio value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Price Ratio field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Price Ratio
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Price Ratio
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
        ///  Write Price Ratio
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Price Ratio to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Price Ratio as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}