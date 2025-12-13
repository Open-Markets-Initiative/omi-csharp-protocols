using System.Buffers.Binary;
using System.Runtime.CompilerServices;

namespace Ice.iMpact
{

    /// <summary>
    ///  Notification Date Time: Date time of the IPL Hold notification
    /// </summary>

    public struct NotificationDateTime
    {
        /// <summary>
        ///  Size of Notification Date Time in bytes
        /// </summary>
        public const int Size = 8;

        /// <summary>
        ///  Notification Date Time value
        /// </summary>
        public readonly DateTime Value
            => Decode();

        /// <summary>
        ///  Read Notification Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly DateTime Decode()
        {
            var milliseconds = BinaryPrimitives.ReverseEndianness(Underlying);
            return DateTime.UnixEpoch.AddMilliseconds(milliseconds);;
        }

        /// <summary>
        ///  Write Notification Date Time using Milliseconds since Jan 1st, 1970, 00:00:00 GMT
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(long milliseconds)
            => Underlying = BinaryPrimitives.ReverseEndianness(milliseconds);

        /// <summary>
        ///  Write Notification Date Time
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Encode(DateTime timestamp)
            => Encode(timestamp.Millisecond);

        /// <summary>
        ///  Notification Date Time as string
        /// </summary>
        public readonly override string ToString()
            => $"{Value}";

        /// <summary>
        ///  Underlying bytes
        /// </summary>
        internal long Underlying;
    }
