using System.Numerics;

namespace Iex.IexEquities.Tops.IexTp.v1_66;

/// <summary>
///  Reports the fields that differ between two messages.
/// </summary>
public static class Diffs
{
    private const int WordBits = 64;

    /// <summary>
    ///  Returns the name, the previous text, and the new text of each field that differs.
    /// </summary>
    /// <exception cref="ArgumentException">The two messages are not of the same type.</exception>
    /// <exception cref="ArgumentNullException">A message is <see langword="null"/>.</exception>
    public static IEnumerable<(string Name, string Before, string After)> Report(IMessage before, IMessage after)
    {
        ArgumentNullException.ThrowIfNull(before);
        ArgumentNullException.ThrowIfNull(after);
        if (before.GetType() != after.GetType() || before is not IFieldOwner first || after is not IFieldOwner second)
        {
            throw new ArgumentException("The two messages are not of the same type.", nameof(after));
        }
        return Changed(first, second);
    }

    private static IEnumerable<(string Name, string Before, string After)> Changed(IFieldOwner first, IFieldOwner second)
    {
        var words = first.ChangedAgainst(second);
        for (var word = 0; word < words.Length; word++)
        {
            for (var bits = words[word]; bits != 0; bits &= bits - 1)
            {
                var field = word * WordBits + BitOperations.TrailingZeroCount(bits);
                yield return (first.Descriptors[field].Name, first.TextOf(field), second.TextOf(field));
            }
        }
    }
}
