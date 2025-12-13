using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Alt Price Increment Constraint: Minimum price offset better than the best Standard Tick order for an order to be allowed into the market
    /// </summary>

    public struct AltPriceIncrementConstraint
    {
        /// <summary>
        ///  Fix Tag for Alt Price Increment Constraint
        /// </summary>
        public const ushort FixTag = 37742;

        /// <summary>
        ///  Sentinel null value for Alt Price Increment Constraint
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Alt Price Increment Constraint
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Alt Price Increment Constraint in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Alt Price Increment Constraint value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Alt Price Increment Constraint field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Alt Price Increment Constraint
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Alt Price Increment Constraint
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
        ///  Write Alt Price Increment Constraint
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Alt Price Increment Constraint to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Alt Price Increment Constraint as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}