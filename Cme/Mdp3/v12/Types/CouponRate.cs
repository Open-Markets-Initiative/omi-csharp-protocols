using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Coupon Rate: The rate of interest that, when multiplied by the principal, par value, or face value of a bond, provides the currency amount of the periodic interest payment
    /// </summary>

    public struct CouponRate
    {
        /// <summary>
        ///  Fix Tag for Coupon Rate
        /// </summary>
        public const ushort FixTag = 223;

        /// <summary>
        ///  Sentinel null value for Coupon Rate
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Coupon Rate
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Coupon Rate in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Coupon Rate value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Coupon Rate field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Coupon Rate
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Coupon Rate
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
        ///  Write Coupon Rate
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Coupon Rate to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Coupon Rate as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}