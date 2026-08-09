namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Controls display when calling <c>ToFormattedString</c> on generated message and field types.
///  Use <c>default(PrintOptions)</c> for top-level indented output, <see cref="Identifier"/> for compact
///  one-line debugger display, or increment <see cref="IndentDepth"/> when descending into nested structures.
/// </summary>
public readonly record struct PrintOptions
{
    // Precomputed indent strings for depths 0..16. Each entry is IndentDepth * 2 spaces.
    // Realistic call-site depth is 0..4; the table makes Prefix allocation-free for all common cases.
    private const int InlineMaxDepth = 16;
    private static readonly string[] prefixes =
    [
        "",
        "  ",
        "    ",
        "      ",
        "        ",
        "          ",
        "            ",
        "              ",
        "                ",
        "                  ",
        "                    ",
        "                      ",
        "                        ",
        "                          ",
        "                            ",
        "                              ",
        "                                ",
    ];

    /// <summary>
    ///  The current indent depth. Zero at the top level; increment by one for each nesting level.
    /// </summary>
    public int IndentDepth { get; init; }

    /// <summary>
    ///  When true, requests a compact one-line summary suitable for debugger display,
    ///  exception messages, or ID-tracking output. Default is false (full indented output).
    /// </summary>
    public bool IsIdentifier { get; init; }

    /// <summary>
    ///  When true, requests a compact one-line summary including all fields: <c>TypeName { Field1: val1, Field2: val2, ... }</c>
    ///  (in contrast to <see cref="IsIdentifier"/>, which includes only key fields).
    ///  Default is false (full indented output).
    /// </summary>
    public bool IsCompact { get; init; }

    /// <summary>
    ///  A <see cref="PrintOptions"/> instance configured for compact identifier output.
    /// </summary>
    public static PrintOptions Identifier { get; } = new() { IsIdentifier = true };

    /// <summary>
    ///  A <see cref="PrintOptions"/> instance configured for compact one-line output.
    /// </summary>
    public static PrintOptions Compact { get; } = new() { IsCompact = true };

    /// <summary>
    ///  Returns the indent string for the current depth (two spaces per level, hardcoded).
    /// </summary>
    public string Prefix => IndentDepth <= InlineMaxDepth
        ? prefixes[IndentDepth]
        : new string(' ', IndentDepth * 2);
}
