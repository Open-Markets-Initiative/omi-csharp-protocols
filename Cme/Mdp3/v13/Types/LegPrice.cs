using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Leg Price: Price for the future leg of a UDS Covered instrument
    /// </summary>

    public struct LegPrice
    {
        /// <summary>
        ///  Fix Tag for Leg Price
        /// </summary>
        public const ushort FixTag = 566;

        /// <summary>
        ///  Sentinel null value for Leg Price
        /// </summary>
        public const long NoValue = 9223372036854775807;

        /// <summary>
        ///  Decimal place factor for Leg Price
        /// </summary>
        public const long Factor = 1000000000;

        /// <summary>
        ///  Size of Leg Price in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Leg Price value
        /// </summary>
        public readonly long Value
            => Decode();

        /// <summary>
        ///  Does Leg Price field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Leg Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly long Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Leg Price
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
        ///  Write Leg Price
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Leg Price to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Leg Price as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
}