using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Alt Min Price Increment: New sub-tick which is only available for order entry when certain conditions are met, tick value which corresponds to the Alt Min Quote Life
    /// </summary>

    public struct AltMinPriceIncrement
    {
        /// <summary>
        ///  Fix Tag for Alt Min Price Increment
        /// </summary>
        public const ushort FixTag = 37739;

        /// <summary>
        ///  Sentinel null value for Alt Min Price Increment
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Alt Min Price Increment
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Alt Min Price Increment in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Alt Min Price Increment value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Alt Min Price Increment field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Alt Min Price Increment
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Alt Min Price Increment
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
        ///  Write Alt Min Price Increment
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Alt Min Price Increment to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Alt Min Price Increment as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}