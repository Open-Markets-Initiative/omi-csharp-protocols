using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Prev Display Qty
    /// </summary>

    public struct PrevDisplayQty
    {
        /// <summary>
        ///  Sentinel null value for Prev Display Qty
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Prev Display Qty
        /// </summary>
        public const ulong Maximum = 922337203685477.5807;

        /// <summary>
        ///  Minimum value for Prev Display Qty
        /// </summary>
        public const ulong Minimum = -922337203685477.5807;

        /// <summary>
        ///  Fix Tag for Prev Display Qty
        /// </summary>
        public const ushort FixTag = 28867;

        /// <summary>
        ///  Decimal place factor for Prev Display Qty
        /// </summary>
        public const ulong Factor = 10000;

        /// <summary>
        ///  Size of Prev Display Qty in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Prev Display Qty value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Prev Display Qty field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Prev Display Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Prev Display Qty
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
        ///  Write Prev Display Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Prev Display Qty to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Prev Display Qty as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}