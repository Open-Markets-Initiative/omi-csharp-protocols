using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  High Limit Price: Upper price threshold for the instrument
    /// </summary>

    public struct HighLimitPrice
    {
        /// <summary>
        ///  Fix Tag for High Limit Price
        /// </summary>
        public const ushort FixTag = 1149;

        /// <summary>
        ///  Sentinel null value for High Limit Price
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for High Limit Price
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of High Limit Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  High Limit Price value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does High Limit Price field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read High Limit Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read High Limit Price
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
        ///  Write High Limit Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set High Limit Price to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  High Limit Price as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}