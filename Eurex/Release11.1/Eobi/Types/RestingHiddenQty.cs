using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Resting Hidden Qty
    /// </summary>

    public struct RestingHiddenQty
    {
        /// <summary>
        ///  Sentinel null value for Resting Hidden Qty
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Resting Hidden Qty
        /// </summary>
        public const ulong Maximum = 922337203685477.5807;

        /// <summary>
        ///  Minimum value for Resting Hidden Qty
        /// </summary>
        public const ulong Minimum = -922337203685477.5807;

        /// <summary>
        ///  Fix Tag for Resting Hidden Qty
        /// </summary>
        public const ushort FixTag = 28868;

        /// <summary>
        ///  Decimal place factor for Resting Hidden Qty
        /// </summary>
        public const ulong Factor = 10000;

        /// <summary>
        ///  Size of Resting Hidden Qty in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Resting Hidden Qty value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Resting Hidden Qty field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Resting Hidden Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Resting Hidden Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryRead(out ulong value)
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
        ///  Write Resting Hidden Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Resting Hidden Qty to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Resting Hidden Qty as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}