using System.Runtime.CompilerServices;

namespace Eurex.Eobi
{

    /// <summary>
    ///  Non Disclosed Trade Volume
    /// </summary>

    public struct NonDisclosedTradeVolume
    {
        /// <summary>
        ///  Sentinel null value for Non Disclosed Trade Volume
        /// </summary>
        public const ulong NoValue = 0x8000000000000000;

        /// <summary>
        ///  Maximum value for Non Disclosed Trade Volume
        /// </summary>
        public const ulong Maximum = 922337203685477.5807;

        /// <summary>
        ///  Minimum value for Non Disclosed Trade Volume
        /// </summary>
        public const ulong Minimum = -922337203685477.5807;

        /// <summary>
        ///  Fix Tag for Non Disclosed Trade Volume
        /// </summary>
        public const ushort FixTag = 28873;

        /// <summary>
        ///  Decimal place factor for Non Disclosed Trade Volume
        /// </summary>
        public const ulong Factor = 10000;

        /// <summary>
        ///  Size of Non Disclosed Trade Volume in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Non Disclosed Trade Volume value
        /// </summary>
        public readonly ulong Value
            => Decode();

        /// <summary>
        ///  Does Non Disclosed Trade Volume field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Non Disclosed Trade Volume
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly ulong Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Non Disclosed Trade Volume
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
        ///  Write Non Disclosed Trade Volume
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(ulong value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Non Disclosed Trade Volume to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Non Disclosed Trade Volume as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal ulong Underlying;
    }
}