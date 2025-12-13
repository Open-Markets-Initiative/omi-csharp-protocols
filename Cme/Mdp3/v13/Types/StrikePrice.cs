using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Strike Price: Strike Price for an option instrument
    /// </summary>

    public struct StrikePrice
    {
        /// <summary>
        ///  Fix Tag for Strike Price
        /// </summary>
        public const ushort FixTag = 202;

        /// <summary>
        ///  Sentinel null value for Strike Price
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Strike Price
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Strike Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Strike Price value
        /// </summary>
        public readonly long Value
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
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Strike Price
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
        ///  Write Strike Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
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
        internal long Underlying;
    }
}