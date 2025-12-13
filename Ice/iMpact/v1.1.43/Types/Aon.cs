using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Aon: Order is All-Or-None
    /// </summary>

    public struct Aon
    {
        /// <summary>
        ///  Size of Aon in bytes
        /// </summary>
        public const int Size = 1;

        /// <summary>
        ///  Aon value
        /// </summary>
        public readonly char Value
            => (char)Byte;

        /// <summary>
        ///  Write Aon
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(char value)
            => Byte = (byte)value;

        /// <summary>
        ///  Aon as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying byte
        /// </summary>
        internal byte Byte;
    }
}