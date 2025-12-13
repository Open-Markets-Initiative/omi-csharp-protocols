using System.Runtime.CompilerServices;

namespace Cme.Mdp3
{

    /// <summary>
    ///  Leg Option Delta: Delta used to calculate the quantity of futures used to cover the option or option strategy
    /// </summary>

    public struct LegOptionDelta
    {
        /// <summary>
        ///  Fix Tag for Leg Option Delta
        /// </summary>
        public const ushort FixTag = 1017;

        /// <summary>
        ///  Sentinel null value for Leg Option Delta
        /// </summary>
        public const int NoValue = 2147483647;

        /// <summary>
        ///  Decimal place factor for Leg Option Delta
        /// </summary>
        public const int Factor = 10000;

        /// <summary>
        ///  Size of Leg Option Delta in bytes
        /// </summary>
        public const int Size = 4;

        /// <summary>
        ///  Leg Option Delta value
        /// </summary>
        public readonly int Value
            => Decode();

        /// <summary>
        ///  Does Leg Option Delta field contain a value?
        /// </summary>
        public readonly bool HasValue
            => Underlying != NoValue;

        /// <summary>
        ///  Read Leg Option Delta
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly int Decode()
            => Underlying / Factor;

        /// <summary>
        ///  Try Read Leg Option Delta
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
        ///  Write Leg Option Delta
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(int value)
            => Underlying = value * Factor;

        /// <summary>
        ///  Set Leg Option Delta to unused
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Reset()
            => Encode(NoValue);

        /// <summary>
        ///  Leg Option Delta as string
        /// </summary>
        public readonly override string ToString()
            => TryRead(out var value) ? $"{value}" : "Not Applicable";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal int Underlying;
    }
}