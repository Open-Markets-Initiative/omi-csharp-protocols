using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Last Qty
    /// </summary>

    public struct LastQty
    {
        /// <summary>
        ///  Sentinel null value for Last Qty
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Last Qty
        /// </summary>
        public const ulong Maximum = 922337203685477.5807;

        /// <summary>
        ///  Minimum value for Last Qty
        /// </summary>
        public const ulong Minimum = -922337203685477.5807;

        /// <summary>
        ///  Fix Tag for Last Qty
        /// </summary>
        public const ushort FixTag = 32;

        /// <summary>
        ///  Decimal place factor for Last Qty
        /// </summary>
        public const ulong Factor = 10000;

        /// <summary>
        ///  Size of Last Qty in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Last Qty value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Last Qty field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Last Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Last Qty
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
        ///  Write Last Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Last Qty to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Last Qty as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}