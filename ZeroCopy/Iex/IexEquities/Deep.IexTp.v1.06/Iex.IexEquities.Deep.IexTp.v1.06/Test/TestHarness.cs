namespace Iex.IexEquities.Deep.IexTp.Testing;

/// <summary>
///  A single protocol message extracted from a transport frame
/// </summary>
public readonly struct ProtocolMessage<TDisc> where TDisc : notnull
{
    public readonly TDisc Type;
    public readonly ReadOnlyMemory<byte> Payload;

    public ProtocolMessage(TDisc type, ReadOnlyMemory<byte> payload)
    {
        Type = type;
        Payload = payload;
    }
}

/// <summary>
///  Delegate for printing a decoded protocol message
/// </summary>
public delegate void MessagePrinter<TDisc>(TDisc type, ReadOnlySpan<byte> payload, int seq);

/// <summary>
///  Shared test harness for pcap-based protocol testing
/// </summary>
public static class TestHarness
{
    public static int Run<TDisc>(string[] args, string usage,
        Func<string, IEnumerable<ProtocolMessage<TDisc>>> readMessages,
        MessagePrinter<TDisc> printMessage) where TDisc : notnull
    {
        if (args.Length < 1)
        {
            Console.WriteLine($"Usage: {usage} <pcap-file>");
            return 1;
        }

        var path = args[0];

        if (!File.Exists(path))
        {
            Console.WriteLine($"File not found: {path}");
            return 1;
        }

        Console.WriteLine($"Reading: {path}");
        Console.WriteLine();

        var counts = new Dictionary<TDisc, int>();
        var total = 0;
        var errors = 0;

        foreach (var message in readMessages(path))
        {
            total++;
            counts[message.Type] = counts.GetValueOrDefault(message.Type) + 1;

            try
            {
                printMessage(message.Type, message.Payload.Span, total);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{total}] ERROR parsing type '{message.Type}': {ex.Message}");
                errors++;
            }
        }

        Console.WriteLine();
        Console.WriteLine("=== Summary ===");
        Console.WriteLine($"Messages: {total}");
        Console.WriteLine($"Errors:   {errors}");
        Console.WriteLine();
        Console.WriteLine("Messages by type:");
        foreach (var (type, count) in counts.OrderByDescending(x => x.Value))
            Console.WriteLine($"  '{type}': {count}");

        return errors > 0 ? 1 : 0;
    }

    public static string FormatTs(long nanoseconds)
        => TimeSpan.FromMicroseconds(nanoseconds / 1000.0).ToString(@"hh\:mm\:ss\.ffffff");

    public static string FormatTs(ulong nanoseconds)
        => TimeSpan.FromMicroseconds(nanoseconds / 1000.0).ToString(@"hh\:mm\:ss\.ffffff");

    public static string FormatTs(DateTime timestamp)
        => timestamp.ToString(@"HH\:mm\:ss\.ffffff");
}