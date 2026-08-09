using Iex.IexEquities.DeepPlus.IexTp;

var passed = 0;
var failed = 0;

// Round-trip test for AddOrderMessage
try
{
    var msg = AddOrderMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = AddOrderMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == AddOrderMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = AddOrderMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == AddOrderMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new AddOrderMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new AddOrderMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.Side.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = AddOrderMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.Side.IsRecognized, "Side should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL AddOrderMessage: {ex.Message}");
    failed++;
}

// Round-trip test for ClearBookMessage
try
{
    var msg = ClearBookMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = ClearBookMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == ClearBookMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = ClearBookMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == ClearBookMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new ClearBookMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new ClearBookMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.Reserved1.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL ClearBookMessage: {ex.Message}");
    failed++;
}

// Round-trip test for OperationalHaltStatusMessage
try
{
    var msg = OperationalHaltStatusMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = OperationalHaltStatusMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == OperationalHaltStatusMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = OperationalHaltStatusMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == OperationalHaltStatusMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new OperationalHaltStatusMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new OperationalHaltStatusMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.OperationalHaltStatus.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = OperationalHaltStatusMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.OperationalHaltStatus.IsRecognized, "OperationalHaltStatus should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL OperationalHaltStatusMessage: {ex.Message}");
    failed++;
}

// Round-trip test for OrderDeleteMessage
try
{
    var msg = OrderDeleteMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = OrderDeleteMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == OrderDeleteMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = OrderDeleteMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == OrderDeleteMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new OrderDeleteMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new OrderDeleteMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.Reserved1.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL OrderDeleteMessage: {ex.Message}");
    failed++;
}

// Round-trip test for OrderExecutedMessage
try
{
    var msg = OrderExecutedMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = OrderExecutedMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == OrderExecutedMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = OrderExecutedMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == OrderExecutedMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new OrderExecutedMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new OrderExecutedMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SaleConditionFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL OrderExecutedMessage: {ex.Message}");
    failed++;
}

// Round-trip test for OrderModifyMessage
try
{
    var msg = OrderModifyMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = OrderModifyMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == OrderModifyMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = OrderModifyMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == OrderModifyMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new OrderModifyMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new OrderModifyMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.ModifyFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL OrderModifyMessage: {ex.Message}");
    failed++;
}

// Round-trip test for RetailLiquidityIndicatorMessage
try
{
    var msg = RetailLiquidityIndicatorMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = RetailLiquidityIndicatorMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == RetailLiquidityIndicatorMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = RetailLiquidityIndicatorMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == RetailLiquidityIndicatorMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new RetailLiquidityIndicatorMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new RetailLiquidityIndicatorMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.RetailLiquidityIndicator.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = RetailLiquidityIndicatorMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.RetailLiquidityIndicator.IsRecognized, "RetailLiquidityIndicator should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL RetailLiquidityIndicatorMessage: {ex.Message}");
    failed++;
}

// Round-trip test for SecurityDirectoryMessage
try
{
    var msg = SecurityDirectoryMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = SecurityDirectoryMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == SecurityDirectoryMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = SecurityDirectoryMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == SecurityDirectoryMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new SecurityDirectoryMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new SecurityDirectoryMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SecurityDirectoryFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[29] = 0x03;
    var enumParsed = SecurityDirectoryMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.LuldTier.IsRecognized, "LuldTier should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL SecurityDirectoryMessage: {ex.Message}");
    failed++;
}

// Round-trip test for SecurityEventMessage
try
{
    var msg = SecurityEventMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = SecurityEventMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == SecurityEventMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = SecurityEventMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == SecurityEventMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new SecurityEventMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new SecurityEventMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SecurityEvent.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = SecurityEventMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.SecurityEvent.IsRecognized, "SecurityEvent should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL SecurityEventMessage: {ex.Message}");
    failed++;
}

// Round-trip test for ShortSalePriceTestStatusMessage
try
{
    var msg = ShortSalePriceTestStatusMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = ShortSalePriceTestStatusMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == ShortSalePriceTestStatusMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = ShortSalePriceTestStatusMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == ShortSalePriceTestStatusMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new ShortSalePriceTestStatusMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new ShortSalePriceTestStatusMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.ShortSalePriceTestStatus.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x02;
    var enumParsed = ShortSalePriceTestStatusMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.ShortSalePriceTestStatus.IsRecognized, "ShortSalePriceTestStatus should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL ShortSalePriceTestStatusMessage: {ex.Message}");
    failed++;
}

// Round-trip test for SystemEventMessage
try
{
    var msg = SystemEventMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = SystemEventMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == SystemEventMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = SystemEventMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == SystemEventMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new SystemEventMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new SystemEventMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SystemEvent.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = SystemEventMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.SystemEvent.IsRecognized, "SystemEvent should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL SystemEventMessage: {ex.Message}");
    failed++;
}

// Round-trip test for TradeBreakMessage
try
{
    var msg = TradeBreakMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = TradeBreakMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == TradeBreakMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = TradeBreakMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == TradeBreakMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new TradeBreakMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new TradeBreakMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SaleConditionFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL TradeBreakMessage: {ex.Message}");
    failed++;
}

// Round-trip test for TradeMessage
try
{
    var msg = TradeMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = TradeMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == TradeMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = TradeMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == TradeMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new TradeMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new TradeMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.SaleConditionFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL TradeMessage: {ex.Message}");
    failed++;
}

// Round-trip test for TradingStatusMessage
try
{
    var msg = TradingStatusMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = TradingStatusMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == TradingStatusMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = TradingStatusMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == TradingStatusMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new TradingStatusMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new TradingStatusMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.TradingStatus.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = TradingStatusMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.TradingStatus.IsRecognized, "TradingStatus should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL TradingStatusMessage: {ex.Message}");
    failed++;
}

// Dirty-buffer padding clear test for IextpHeader
try
{
    var header = IextpHeader.Sample();
    var buffer = new byte[header.ByteLength];
    Array.Fill(buffer, (byte)0xAA);
    var written = header.Encode(buffer);
    Assert(written == header.ByteLength, $"Encode wrote {written} bytes, expected {header.ByteLength}");
    Assert(BytesAreZero(buffer, 1, 1), "IextpHeader.Encode must clear Reserved padding bytes at offset 1 length 1 in a dirty buffer");
    var clean = header.ToBytes();
    Assert(clean.AsSpan().SequenceEqual(buffer), "ToBytes is not bytewise stable with dirty-buffer Encode");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL IextpHeader dirty-buffer padding: {ex.Message}");
    failed++;
}

static void Assert(bool condition, string message)
{
    if (!condition)
        throw new InvalidOperationException($"ASSERTION FAILED: {message}");
}

static bool BytesAreZero(ReadOnlySpan<byte> data, int offset, int length)
{
    for (var i = 0; i < length; i++)
        if (data[offset + i] != 0) return false;
    return true;
}

Console.WriteLine($"Round-trip tests: {passed} passed, {failed} failed");
return failed > 0 ? 1 : 0;
