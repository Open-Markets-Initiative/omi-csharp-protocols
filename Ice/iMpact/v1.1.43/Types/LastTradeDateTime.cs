using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Last Trade Date Time: time of last trade
    /// </summary>

    public struct LastTradeDateTime
    {
        /// <summary>
        ///  Size of Last Trade Date Time in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Last Trade Date Time value
        /// </summary>
        public readonly DateTime Value
            => Decode();

        /// <summary>
        ///  Read Last Trade Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DateTime Decode()
        {
            var milliseconds = BinaryPrimitives.ReverseEndianness(Underlying);
            return DateTime.UnixEpoch.AddMilliseconds(milliseconds);;
        }

        /// <summary>
        ///  Write Last Trade Date Time using Milliseconds since Jan 1st, 1970, 00:00:00 GMT
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long milliseconds)
            => Underlying = BinaryPrimitives.ReverseEndianness(milliseconds);

        /// <summary>
        ///  Write Last Trade Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(DateTime timestamp)
            => Encode(timestamp.Millisecond);

        /// <summary>
        ///  Last Trade Date Time as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
