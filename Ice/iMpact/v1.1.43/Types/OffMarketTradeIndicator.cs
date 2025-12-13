using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Off Market Trade Indicator: Legacy field that supports all single character trade types on ICE. The new 3-character “OffMarketTradeType” field replaces this field. In the future (no earlier than 2015), ICE anticipates the introduction of 3 character trade types that will only be available in the new field. Trade types that are longer than a single character will be represented with “#” in this field.  Only for off market trade. The first character is ‘ ‘ when it is a regular trade.
    /// </summary>

    public struct OffMarketTradeIndicator
    {
        /// <summary>
        ///  Size of Off Market Trade Indicator in bytes
        /// </summary>
        public const int Size = 1;

        /// <summary>
        ///  Off Market Trade Indicator value
        /// </summary>
        public readonly char Value
            => (char)Byte;

        /// <summary>
        ///  Write Off Market Trade Indicator
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(char value)
            => Byte = (byte)value;

        /// <summary>
        ///  Off Market Trade Indicator as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying byte
        /// </summary>
        internal byte Byte;
    }
}