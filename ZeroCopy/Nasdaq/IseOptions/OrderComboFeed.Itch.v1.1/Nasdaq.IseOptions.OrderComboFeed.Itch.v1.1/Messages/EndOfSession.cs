using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Mold Udp64 end of session
/// </summary>
public static class EndOfSession
{
    /// <summary>
    ///  The End Of Session fields to write.
    /// </summary>
    public readonly struct Value
    {
        /// <summary>
        ///  Creates the value of the End Of Session, with each field that it leaves out absent.
        /// </summary>
        public Value() { }
    }

    /// <summary>
    ///  Writes the End Of Session to the start of destination and fills in each length, count and message type in it.
    /// </summary>
    /// <param name="destination">
    ///  The buffer that receives the bytes.
    /// </param>
    /// <param name="value">
    ///  The fields to write.
    /// </param>
    /// <returns>
    ///  The bytes of the End Of Session.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///  The destination is too short, a value selects no message, or two values disagree.
    /// </exception>
    /// <exception cref="OverflowException">
    ///  A length or a count does not fit its field.
    /// </exception>
    public static global::System.Span<byte> Write(global::System.Span<byte> destination, scoped in Value value)
    {
        var cursor = new global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor();
        return Write(destination, ref cursor, global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.Position.Minimum, in value);
    }

    internal static global::System.Span<byte> Write(global::System.Span<byte> bytes, scoped ref global::Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1.Parser.WriterCursor cursor, int start, scoped in Value value)
    {
        return bytes[start..cursor.Index];
    }
}
