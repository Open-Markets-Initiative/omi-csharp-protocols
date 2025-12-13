using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Low Limit Price: Lower price threshold for the instrument
    /// </summary>

    public struct LowLimitPrice
    {
        /// <summary>
        ///  Fix Tag for Low Limit Price
        /// </summary>
        public const ushort FixTag = 1148;

        /// <summary>
        ///  Sentinel null value for Low Limit Price
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Low Limit Price
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Low Limit Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Low Limit Price value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Low Limit Price field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Low Limit Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Low Limit Price
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
        ///  Write Low Limit Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Low Limit Price to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Low Limit Price as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}