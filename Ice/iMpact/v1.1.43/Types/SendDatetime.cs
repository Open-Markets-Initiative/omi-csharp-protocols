using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Send Datetime: Field is the timestamp of when the message block is sent, the number of milliseconds since Jan 1st, 1970, 00:00:00 GMT
    /// </summary>

    public struct SendDatetime
    {
        /// <summary>
        ///  Size of Send Datetime in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Send Datetime value
        /// </summary>
        public readonly DateTime Value
            => Decode();

        /// <summary>
        ///  Read Send Datetime
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DateTime Decode()
        {
            var milliseconds = BinaryPrimitives.ReverseEndianness(Underlying);
            return DateTime.UnixEpoch.AddMilliseconds(milliseconds);;
        }

        /// <summary>
        ///  Write Send Datetime using Milliseconds since Jan 1st, 1970, 00:00:00 GMT
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long milliseconds)
            => Underlying = BinaryPrimitives.ReverseEndianness(milliseconds);

        /// <summary>
        ///  Write Send Datetime
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(DateTime timestamp)
            => Encode(timestamp.Millisecond);

        /// <summary>
        ///  Send Datetime as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
