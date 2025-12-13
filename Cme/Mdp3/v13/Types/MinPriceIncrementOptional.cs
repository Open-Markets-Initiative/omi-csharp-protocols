using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Min Price Increment Optional: Minimum constant tick for the instrument
    /// </summary>

    public struct MinPriceIncrementOptional
    {
        /// <summary>
        ///  Fix Tag for Min Price Increment Optional
        /// </summary>
        public const ushort FixTag = 969;

        /// <summary>
        ///  Sentinel null value for Min Price Increment Optional
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Min Price Increment Optional
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Min Price Increment Optional in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Min Price Increment Optional value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Min Price Increment Optional field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Min Price Increment Optional
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Min Price Increment Optional
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
        ///  Write Min Price Increment Optional
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Min Price Increment Optional to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Min Price Increment Optional as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}