using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Min Cab Price: Defines cabinet price for outright options products
    /// </summary>

    public struct MinCabPrice
    {
        /// <summary>
        ///  Fix Tag for Min Cab Price
        /// </summary>
        public const ushort FixTag = 9850;

        /// <summary>
        ///  Sentinel null value for Min Cab Price
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Min Cab Price
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Min Cab Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Min Cab Price value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Min Cab Price field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Min Cab Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Min Cab Price
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
        ///  Write Min Cab Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Min Cab Price to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Min Cab Price as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}