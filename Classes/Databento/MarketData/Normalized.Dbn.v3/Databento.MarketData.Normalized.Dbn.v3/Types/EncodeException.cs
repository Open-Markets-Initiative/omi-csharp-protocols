namespace Databento.MarketData.Normalized.Dbn.v3;

/// <summary>
///  The exception that is thrown when encoding fails for a reason other than a span that is too small.
/// </summary>
public sealed class EncodeException : Exception
{
    internal EncodeException(EncodeFailure failure)
        : base(failure.Descriptor is { } descriptor
        ? $"The value of {descriptor.Name}{Place(failure)} does not fit its field: {failure.Reason}."
        : $"Encoding failed{Place(failure)}: {failure.Reason}.")
    {
        Failure = failure;
    }

    /// <summary>
    ///  Gets why encoding failed, as TryEncode reports it for the instance.
    /// </summary>
    public EncodeFailure Failure { get; }

    private static string Place(EncodeFailure failure)
    {
        var location = failure.Parent is not { } parent ? string.Empty
            : failure.EntryIndex is { } entry ? $" in entry {entry} of {parent.Name}"
            : $" in {parent.Name}";
        var message = failure.MessageIndex is { } index ? $" in message {index}" : string.Empty;
        return $"{location}{message}";
    }
}
