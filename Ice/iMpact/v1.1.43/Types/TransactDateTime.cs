using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Transact Date Time: Deal date time
    /// </summary>

    public struct TransactDateTime
    {
        /// <summary>
        ///  Size of Transact Date Time in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Transact Date Time value
        /// </summary>
        public readonly DateTime Value
            => Decode();

        /// <summary>
        ///  Read Transact Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DateTime Decode()
        {
            var milliseconds = BinaryPrimitives.ReverseEndianness(Underlying);
            return DateTime.UnixEpoch.AddMilliseconds(milliseconds);;
        }

        /// <summary>
        ///  Write Transact Date Time using Milliseconds since Jan 1st, 1970, 00:00:00 GMT
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long milliseconds)
            => Underlying = BinaryPrimitives.ReverseEndianness(milliseconds);

        /// <summary>
        ///  Write Transact Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(DateTime timestamp)
            => Encode(timestamp.Millisecond);

        /// <summary>
        ///  Transact Date Time as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
