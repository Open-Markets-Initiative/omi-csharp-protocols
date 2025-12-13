using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Status: For Endex Spot markets
    /// </summary>

    public struct Status
    {
        /// <summary>
        ///  Size of Status in bytes
        /// </summary>
        public const int Size = 1;

        /// <summary>
        ///  Status value
        /// </summary>
        public readonly char Value
            => (char)Byte;

        /// <summary>
        ///  Write Status
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(char value)
            => Byte = (byte)value;

        /// <summary>
        ///  Status as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying byte
        /// </summary>
        internal byte Byte;
    }
}