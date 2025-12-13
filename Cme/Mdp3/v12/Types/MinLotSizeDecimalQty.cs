using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Min Lot Size Decimal Qty: Minimum quantity accepted for order entry. If tag 1093-LotType=4, this value is the minimum quantity for order entry expressed in the applicable units, specified in tag 996-UnitOfMeasure, e.g. megawatts
    /// </summary>

    public struct MinLotSizeDecimalQty
    {
        /// <summary>
        ///  Fix Tag for Min Lot Size Decimal Qty
        /// </summary>
        public const ushort FixTag = 1231;

        /// <summary>
        ///  Sentinel null value for Min Lot Size Decimal Qty
        /// </summary>
        public const int NoValue = 2147483647;

        /// <summary>
        ///  Decimal place factor for Min Lot Size Decimal Qty
        /// </summary>
        public const int Factor = 10000;

        /// <summary>
        ///  Size of Min Lot Size Decimal Qty in bytes
        /// </summary>
        public const int Size = 4;

        /// <summary>
        ///  Min Lot Size Decimal Qty value
        /// </summary>
        public readonly int Value
            => Decode();

        /// <summary>
        ///  Does Min Lot Size Decimal Qty field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Min Lot Size Decimal Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Min Lot Size Decimal Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool TryRead(out int value)
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
        ///  Write Min Lot Size Decimal Qty
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(int value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Min Lot Size Decimal Qty to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Min Lot Size Decimal Qty as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal int Underlying;
    }
}