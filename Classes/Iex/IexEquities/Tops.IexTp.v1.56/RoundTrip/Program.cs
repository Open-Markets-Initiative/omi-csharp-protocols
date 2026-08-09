using Iex.IexEquities.Tops.IexTp;

var passed = 0;
var failed = 0;

// Round-trip test for AuctionInformationMessage
try
{
    var msg = AuctionInformationMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = AuctionInformationMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == AuctionInformationMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = AuctionInformationMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == AuctionInformationMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new AuctionInformationMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new AuctionInformationMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.AuctionType.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = AuctionInformationMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.AuctionType.IsRecognized, "AuctionType should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL AuctionInformationMessage: {ex.Message}");
    failed++;
}

// Round-trip test for OfficialPriceMessage
try
{
    var msg = OfficialPriceMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = OfficialPriceMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == OfficialPriceMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = OfficialPriceMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == OfficialPriceMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new OfficialPriceMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new OfficialPriceMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.PriceType.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    var enumBytes = msg.ToBytes();
    enumBytes[0] = 0x00;
    var enumParsed = OfficialPriceMessage.Parse(enumBytes);
    Assert(enumParsed.IsDecoded, "Unrecognized enum byte should not stop structural decode");
    Assert(!enumParsed.PriceType.IsRecognized, "PriceType should report IsRecognized=false for an unrecognized byte");
    Assert(!enumParsed.IsRecognized, "Message should report IsRecognized=false when an enum field is unrecognized");
    Assert(!enumParsed.IsValid, "Message should be invalid when an enum field is unrecognized");
    Assert(enumParsed.FailedAt is null, "Unrecognized enum byte should not set FailedAt");
    Assert(enumParsed.Timestamp.IsDecoded, "Parsing should continue to Timestamp after an unrecognized enum byte");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL OfficialPriceMessage: {ex.Message}");
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

// Round-trip test for QuoteUpdateMessage
try
{
    var msg = QuoteUpdateMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = QuoteUpdateMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == QuoteUpdateMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = QuoteUpdateMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == QuoteUpdateMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new QuoteUpdateMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new QuoteUpdateMessage();
    resetAfterSuccess.ParseFrom(buffer);
    Assert(resetAfterSuccess.IsValid, "Initial valid ParseFrom should be valid");
    resetAfterSuccess.ParseFrom(buffer.AsSpan(0, 0));
    Assert(!resetAfterSuccess.IsValid, "Short-buffer ParseFrom after success should be invalid");
    Assert(resetAfterSuccess.FailedAt is not null, "Short-buffer ParseFrom after success should set FailedAt");
    Assert(!resetAfterSuccess.QuoteUpdateFlags.IsDecoded, "ParseFrom should clear stale field decode state before reparsing");
    passed++;
}
catch (Exception ex)
{
    Console.Error.WriteLine($"FAIL QuoteUpdateMessage: {ex.Message}");
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

// Round-trip test for TradeReportMessage
try
{
    var msg = TradeReportMessage.Sample();
    var bufLen = msg.ByteLength;
    var buffer = new byte[bufLen];
    Array.Fill(buffer, (byte)0xAA);
    var written = msg.Encode(buffer);
    Assert(written == bufLen, $"Encode wrote {written} bytes, expected {bufLen}");
    var parsed = TradeReportMessage.Parse(buffer);
    Assert(parsed.IsValid, $"IsValid=false; failed at {parsed.FailedAt}");
    var diff = msg.Diff(parsed);
    Assert(diff == TradeReportMessage.Changes.None, $"Diff: {diff}");
    var buffer2 = new byte[bufLen];
    Array.Fill(buffer2, (byte)0xAA);
    var written2 = parsed.Encode(buffer2);
    Assert(written2 == bufLen, $"Re-encode wrote {written2} bytes, expected {bufLen}");
    Assert(buffer.AsSpan(0, bufLen).SequenceEqual(buffer2), "Encode is not bytewise stable");
    var bytes = msg.ToBytes();
    Assert(bytes.Length == bufLen, $"ToBytes length {bytes.Length}, expected {bufLen}");
    var parsedFromBytes = TradeReportMessage.Parse(bytes);
    Assert(parsedFromBytes.IsValid, $"ToBytes parse IsValid=false; failed at {parsedFromBytes.FailedAt}");
    var toBytesDiff = msg.Diff(parsedFromBytes);
    Assert(toBytesDiff == TradeReportMessage.Changes.None, $"ToBytes diff: {toBytesDiff}");
    Assert(bytes.AsSpan().SequenceEqual(buffer.AsSpan(0, bufLen)), "ToBytes is not bytewise stable with Encode");
    var resetAfterFailure = new TradeReportMessage();
    resetAfterFailure.ParseFrom(buffer.AsSpan(0, Math.Max(0, bufLen - 1)));
    Assert(!resetAfterFailure.IsValid, "Short-buffer ParseFrom should be invalid");
    Assert(resetAfterFailure.FailedAt is not null, "Short-buffer ParseFrom should set FailedAt");
    resetAfterFailure.ParseFrom(buffer);
    Assert(resetAfterFailure.IsValid, $"Valid ParseFrom after failure should clear FailedAt and become valid; failed at {resetAfterFailure.FailedAt}");
    Assert(resetAfterFailure.FailedAt is null, "Successful ParseFrom should clear FailedAt");
    var resetAfterSuccess = new TradeReportMessage();
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
    Console.Error.WriteLine($"FAIL TradeReportMessage: {ex.Message}");
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
