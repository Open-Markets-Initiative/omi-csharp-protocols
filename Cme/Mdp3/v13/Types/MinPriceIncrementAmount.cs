using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Min Price Increment Amount: Monetary value equivalent to the minimum price fluctuation
    /// </summary>

    public struct MinPriceIncrementAmount
    {
        /// <summary>
        ///  Fix Tag for Min Price Increment Amount
        /// </summary>
        public const ushort FixTag = 1146;

        /// <summary>
        ///  Sentinel null value for Min Price Increment Amount
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Min Price Increment Amount
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Min Price Increment Amount in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Min Price Increment Amount value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Min Price Increment Amount field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Min Price Increment Amount
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Min Price Increment Amount
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
        ///  Write Min Price Increment Amount
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Min Price Increment Amount to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Min Price Increment Amount as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}