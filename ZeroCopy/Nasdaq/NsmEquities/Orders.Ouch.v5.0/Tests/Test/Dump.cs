namespace Nasdaq.NsmEquities.Orders.Ouch.v5_0.Test;

/// <summary>
///  Writes the fields of messages, headers, and packets to the console, one line for each field.
/// </summary>
internal static class Dump
{
    /// <summary>
    ///  The indent of the fields under the element that holds them.
    /// </summary>
    internal const string Indent = "  ";

    /// <summary>
    ///  The text between the name of a field and its value.
    /// </summary>
    internal const string Separator = " = ";

    /// <summary>
    ///  Writes a name on its own line.
    /// </summary>
    internal static void Line(string indent, string name) => Console.WriteLine($"{indent}{name}");

    /// <summary>
    ///  Writes a name and its value on one line.
    /// </summary>
    internal static void Line(string indent, string name, object? value) => Console.WriteLine($"{indent}{name}{Separator}{value}");

    internal static void Print(EnableOrderEntryResponseOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(EnableOrderEntryResponseAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseAppendage.Reader.EnableOrderEntryResponseOptionalField), reader.EnableOrderEntryResponseOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseAppendage.Reader.EnableOrderEntryResponseOptionalValue));
        Print(reader.EnableOrderEntryResponseOptionalValue, indent + Indent);
        if (reader.EnableOrderEntryResponseOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(EnableOrderEntryResponseMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.EnableOrderEntryResponseAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryResponseMessage.Reader.EnableOrderEntryResponseAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(DisableOrderEntryResponseOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(DisableOrderEntryResponseAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Reader.DisableOrderEntryResponseOptionalField), reader.DisableOrderEntryResponseOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseAppendage.Reader.DisableOrderEntryResponseOptionalValue));
        Print(reader.DisableOrderEntryResponseOptionalValue, indent + Indent);
        if (reader.DisableOrderEntryResponseOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(DisableOrderEntryResponseMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.DisableOrderEntryResponseAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryResponseMessage.Reader.DisableOrderEntryResponseAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(MassCancelResponseOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.Side)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Reader.Side), reader.Side);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.GroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Reader.GroupId), reader.GroupId);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(MassCancelResponseAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseAppendage.Reader.MassCancelResponseOptionalField), reader.MassCancelResponseOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseAppendage.Reader.MassCancelResponseOptionalValue));
        Print(reader.MassCancelResponseOptionalValue, indent + Indent);
        if (reader.MassCancelResponseOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(MassCancelResponseMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.Symbol), reader.Symbol);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.MassCancelResponseAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelResponseMessage.Reader.MassCancelResponseAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(AccountQueryResponseOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(AccountQueryResponseAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Reader.AccountQueryResponseOptionalField), reader.AccountQueryResponseOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseAppendage.Reader.AccountQueryResponseOptionalValue));
        Print(reader.AccountQueryResponseOptionalValue, indent + Indent);
        if (reader.AccountQueryResponseOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(AccountQueryResponseMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.NextUserRefNum), reader.NextUserRefNum);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.AccountQueryResponseAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryResponseMessage.Reader.AccountQueryResponseAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(OrderRestatedOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayQuantity)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Reader.DisplayQuantity), reader.DisplayQuantity);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.DisplayPrice)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Reader.DisplayPrice), reader.DisplayPrice);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.SecondaryOrdRefNum)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Reader.SecondaryOrdRefNum), reader.SecondaryOrdRefNum);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(OrderRestatedAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedAppendage.Reader.OrderRestatedOptionalField), reader.OrderRestatedOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedAppendage.Reader.OrderRestatedOptionalValue));
        Print(reader.OrderRestatedOptionalValue, indent + Indent);
        if (reader.OrderRestatedOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(OrderRestatedMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.OrderRestatedReason), reader.OrderRestatedReason);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.OrderRestatedAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderRestatedMessage.Reader.OrderRestatedAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(OrderModifiedOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.SharesLocated)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Reader.SharesLocated), reader.SharesLocated);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.LocateBroker)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Reader.LocateBroker), reader.LocateBroker);
        }
    }

    internal static void Print(OrderModifiedAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedAppendage.Reader.OrderModifiedOptionalField), reader.OrderModifiedOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedAppendage.Reader.OrderModifiedOptionalValue));
        Print(reader.OrderModifiedOptionalValue, indent + Indent);
        if (reader.OrderModifiedOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(OrderModifiedMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.Quantity), reader.Quantity);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.OrderModifiedAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderModifiedMessage.Reader.OrderModifiedAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(OrderPriorityUpdateOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(OrderPriorityUpdateAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Reader.OrderPriorityUpdateOptionalField), reader.OrderPriorityUpdateOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateAppendage.Reader.OrderPriorityUpdateOptionalValue));
        Print(reader.OrderPriorityUpdateOptionalValue, indent + Indent);
        if (reader.OrderPriorityUpdateOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(OrderPriorityUpdateMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.Display), reader.Display);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.OrderPriorityUpdateAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderPriorityUpdateMessage.Reader.OrderPriorityUpdateAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(CancelRejectOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(CancelRejectAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Reader.CancelRejectOptionalField), reader.CancelRejectOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectAppendage.Reader.CancelRejectOptionalValue));
        Print(reader.CancelRejectOptionalValue, indent + Indent);
        if (reader.CancelRejectOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(CancelRejectMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.UserRefNum), reader.UserRefNum);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.CancelRejectAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelRejectMessage.Reader.CancelRejectAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(CancelPendingOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(CancelPendingAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Reader.CancelPendingOptionalField), reader.CancelPendingOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingAppendage.Reader.CancelPendingOptionalValue));
        Print(reader.CancelPendingOptionalValue, indent + Indent);
        if (reader.CancelPendingOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(CancelPendingMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.UserRefNum), reader.UserRefNum);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.CancelPendingAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelPendingMessage.Reader.CancelPendingAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(RejectedOrderOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(RejectedOrderAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Reader.RejectedOrderOptionalField), reader.RejectedOrderOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderAppendage.Reader.RejectedOrderOptionalValue));
        Print(reader.RejectedOrderOptionalValue, indent + Indent);
        if (reader.RejectedOrderOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(RejectedOrderMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.RejectedOrderReason), reader.RejectedOrderReason);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.Clordid), reader.Clordid);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.RejectedOrderAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.RejectedOrderMessage.Reader.RejectedOrderAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(BrokenTradeOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(BrokenTradeAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Reader.BrokenTradeOptionalField), reader.BrokenTradeOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeAppendage.Reader.BrokenTradeOptionalValue));
        Print(reader.BrokenTradeOptionalValue, indent + Indent);
        if (reader.BrokenTradeOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(BrokenTradeMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.MatchNumber), reader.MatchNumber);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.BrokenTradeReason), reader.BrokenTradeReason);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.Clordid), reader.Clordid);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.BrokenTradeAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.BrokenTradeMessage.Reader.BrokenTradeAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(OrderExecutedOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(OrderExecutedAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Reader.OrderExecutedOptionalField), reader.OrderExecutedOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedAppendage.Reader.OrderExecutedOptionalValue));
        Print(reader.OrderExecutedOptionalValue, indent + Indent);
        if (reader.OrderExecutedOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(OrderExecutedMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.LiquidityFlag), reader.LiquidityFlag);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.MatchNumber), reader.MatchNumber);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.OrderExecutedAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderExecutedMessage.Reader.OrderExecutedAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(AiqCanceledOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(AiqCanceledAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Reader.AiqCanceledOptionalField), reader.AiqCanceledOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledAppendage.Reader.AiqCanceledOptionalValue));
        Print(reader.AiqCanceledOptionalValue, indent + Indent);
        if (reader.AiqCanceledOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(AiqCanceledMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.DecrementShares), reader.DecrementShares);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.OrderCancelReason), reader.OrderCancelReason);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.QuantityPreventedFromTrading), reader.QuantityPreventedFromTrading);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.ExecutionPrice), reader.ExecutionPrice);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.LiquidityFlag), reader.LiquidityFlag);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AiqStrategy), reader.AiqStrategy);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.AiqCanceledAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AiqCanceledMessage.Reader.AiqCanceledAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(CanceledMessageOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(CanceledMessageAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageAppendage.Reader.CanceledMessageOptionalField), reader.CanceledMessageOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageAppendage.Reader.CanceledMessageOptionalValue));
        Print(reader.CanceledMessageOptionalValue, indent + Indent);
        if (reader.CanceledMessageOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessageOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(CanceledMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.CancelOrderReason), reader.CancelOrderReason);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.CanceledMessageAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CanceledMessage.Reader.CanceledMessageAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(ReplacedMessageOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.Firm)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.Firm), reader.Firm);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MinQty)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.MinQty), reader.MinQty);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.MaxFloor)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.MaxFloor), reader.MaxFloor);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.PriceType), reader.PriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.PostOnly)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.PostOnly), reader.PostOnly);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.ExpireTime)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.ExpireTime), reader.ExpireTime);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.TradeNow)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.TradeNow), reader.TradeNow);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.HandleInst)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.HandleInst), reader.HandleInst);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.BboWeightIndicator)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.BboWeightIndicator), reader.BboWeightIndicator);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqStrategy)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.AiqStrategy), reader.AiqStrategy);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.AiqGroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Reader.AiqGroupId), reader.AiqGroupId);
        }
    }

    internal static void Print(ReplacedMessageAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Reader.ReplacedMessageOptionalField), reader.ReplacedMessageOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageAppendage.Reader.ReplacedMessageOptionalValue));
        Print(reader.ReplacedMessageOptionalValue, indent + Indent);
        if (reader.ReplacedMessageOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessageOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ReplacedMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrigUserRefNum), reader.OrigUserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Symbol), reader.Symbol);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.TimeInForce), reader.TimeInForce);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Display), reader.Display);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Capacity), reader.Capacity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.CrossType), reader.CrossType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.OrderState), reader.OrderState);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.Clordid), reader.Clordid);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.ReplacedMessageAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplacedMessage.Reader.ReplacedMessageAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(OrderAcceptedOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Firm)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.Firm), reader.Firm);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MinQty)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.MinQty), reader.MinQty);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.CustomerType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.CustomerType), reader.CustomerType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.MaxFloor)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.MaxFloor), reader.MaxFloor);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.PriceType), reader.PriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.PegOffset), reader.PegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPrice)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.DiscretionPrice), reader.DiscretionPrice);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.DiscretionPriceType), reader.DiscretionPriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.DiscretionPegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.DiscretionPegOffset), reader.DiscretionPegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.PostOnly)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.PostOnly), reader.PostOnly);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.RandomReserves)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.RandomReserves), reader.RandomReserves);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.Route)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.Route), reader.Route);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.ExpireTime)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.ExpireTime), reader.ExpireTime);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.TradeNow)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.TradeNow), reader.TradeNow);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.HandleInst)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.HandleInst), reader.HandleInst);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.BboWeightIndicator)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.BboWeightIndicator), reader.BboWeightIndicator);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.GroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.GroupId), reader.GroupId);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.SharesLocated)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.SharesLocated), reader.SharesLocated);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqStrategy)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.AiqStrategy), reader.AiqStrategy);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.AiqGroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Reader.AiqGroupId), reader.AiqGroupId);
        }
    }

    internal static void Print(OrderAcceptedAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedAppendage.Reader.OrderAcceptedOptionalField), reader.OrderAcceptedOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedAppendage.Reader.OrderAcceptedOptionalValue));
        Print(reader.OrderAcceptedOptionalValue, indent + Indent);
        if (reader.OrderAcceptedOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(OrderAcceptedMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Timestamp), reader.Timestamp);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Symbol), reader.Symbol);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.TimeInForce), reader.TimeInForce);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Display), reader.Display);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderReferenceNumber), reader.OrderReferenceNumber);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Capacity), reader.Capacity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.CrossType), reader.CrossType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderState), reader.OrderState);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.Clordid), reader.Clordid);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.OrderAcceptedAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.OrderAcceptedMessage.Reader.OrderAcceptedAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(SequencedMessage.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.SystemEventMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.SystemEventMessage));
            Print(reader.SystemEventMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderAcceptedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.OrderAcceptedMessage));
            Print(reader.OrderAcceptedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.ReplacedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.ReplacedMessage));
            Print(reader.ReplacedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CanceledMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.CanceledMessage));
            Print(reader.CanceledMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AiqCanceledMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.AiqCanceledMessage));
            Print(reader.AiqCanceledMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderExecutedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.OrderExecutedMessage));
            Print(reader.OrderExecutedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.BrokenTradeMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.BrokenTradeMessage));
            Print(reader.BrokenTradeMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.RejectedOrderMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.RejectedOrderMessage));
            Print(reader.RejectedOrderMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelPendingMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.CancelPendingMessage));
            Print(reader.CancelPendingMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.CancelRejectMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.CancelRejectMessage));
            Print(reader.CancelRejectMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderPriorityUpdateMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.OrderPriorityUpdateMessage));
            Print(reader.OrderPriorityUpdateMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderModifiedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.OrderModifiedMessage));
            Print(reader.OrderModifiedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.OrderRestatedMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.OrderRestatedMessage));
            Print(reader.OrderRestatedMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.AccountQueryResponseMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.AccountQueryResponseMessage));
            Print(reader.AccountQueryResponseMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.MassCancelResponseMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.MassCancelResponseMessage));
            Print(reader.MassCancelResponseMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.DisableOrderEntryResponseMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.DisableOrderEntryResponseMessage));
            Print(reader.DisableOrderEntryResponseMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.EnableOrderEntryResponseMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Reader.EnableOrderEntryResponseMessage));
            Print(reader.EnableOrderEntryResponseMessage, indent + Indent);
        }
    }

    internal static void Print(SequencedDataPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader.SequencedMessageType), reader.SequencedMessageType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedDataPacket.Reader.SequencedMessage));
        Print(reader.SequencedMessage, indent + Indent);
        if (reader.SequencedMessage.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.SequencedMessage.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(DebugPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DebugPacket.Reader.DebugText), Convert.ToHexString(reader.DebugText));
    }

    internal static void Print(ServerPayload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.DebugPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Reader.DebugPacket));
            Print(reader.DebugPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginAcceptedPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Reader.LoginAcceptedPacket));
            Print(reader.LoginAcceptedPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.LoginRejectedPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Reader.LoginRejectedPacket));
            Print(reader.LoginRejectedPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.SequencedDataPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Reader.SequencedDataPacket));
            Print(reader.SequencedDataPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.ServerHeartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.ServerHeartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.EndOfSession)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.EndOfSession));
        }
    }

    internal static void Print(ServerSoupBinTcpPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerSoupBinTcpPacket.Reader.ServerPacketHeader));
        Print(reader.ServerPacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerSoupBinTcpPacket.Reader.ServerPayload));
        Print(reader.ServerPayload, indent + Indent);
        if (reader.ServerPayload.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPayload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ServerPacket.Reader reader, string indent)
    {
        foreach (var entry in reader.ServerSoupBinTcpPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ServerPacket.Reader.ServerSoupBinTcpPacket));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(EnableOrderEntryRequestOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(EnableOrderEntryRequestAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Reader.EnableOrderEntryRequestOptionalField), reader.EnableOrderEntryRequestOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestAppendage.Reader.EnableOrderEntryRequestOptionalValue));
        Print(reader.EnableOrderEntryRequestOptionalValue, indent + Indent);
        if (reader.EnableOrderEntryRequestOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(EnableOrderEntryRequestMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.EnableOrderEntryRequestAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnableOrderEntryRequestMessage.Reader.EnableOrderEntryRequestAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(DisableOrderEntryRequestOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(DisableOrderEntryRequestAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestAppendage.Reader.DisableOrderEntryRequestOptionalField), reader.DisableOrderEntryRequestOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestAppendage.Reader.DisableOrderEntryRequestOptionalValue));
        Print(reader.DisableOrderEntryRequestOptionalValue, indent + Indent);
        if (reader.DisableOrderEntryRequestOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(DisableOrderEntryRequestMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.DisableOrderEntryRequestAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.DisableOrderEntryRequestMessage.Reader.DisableOrderEntryRequestAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(MassCancelRequestOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.Side)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Reader.Side), reader.Side);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.GroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Reader.GroupId), reader.GroupId);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(MassCancelRequestAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Reader.MassCancelRequestOptionalField), reader.MassCancelRequestOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestAppendage.Reader.MassCancelRequestOptionalValue));
        Print(reader.MassCancelRequestOptionalValue, indent + Indent);
        if (reader.MassCancelRequestOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(MassCancelRequestMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.Firm), reader.Firm);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.Symbol), reader.Symbol);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.MassCancelRequestAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.MassCancelRequestMessage.Reader.MassCancelRequestAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(AccountQueryOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(AccountQueryAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Reader.AccountQueryOptionalField), reader.AccountQueryOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryAppendage.Reader.AccountQueryOptionalValue));
        Print(reader.AccountQueryOptionalValue, indent + Indent);
        if (reader.AccountQueryOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(AccountQueryMessage.Reader reader, string indent)
    {
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.AccountQueryAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.AccountQueryMessage.Reader.AccountQueryAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(ModifyOrderOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.SharesLocated)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Reader.SharesLocated), reader.SharesLocated);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.LocateBroker)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Reader.LocateBroker), reader.LocateBroker);
        }
    }

    internal static void Print(ModifyOrderAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Reader.ModifyOrderOptionalField), reader.ModifyOrderOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderAppendage.Reader.ModifyOrderOptionalValue));
        Print(reader.ModifyOrderOptionalValue, indent + Indent);
        if (reader.ModifyOrderOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ModifyOrderMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.Quantity), reader.Quantity);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.ModifyOrderAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ModifyOrderMessage.Reader.ModifyOrderAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(CancelOrderOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
    }

    internal static void Print(CancelOrderAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderAppendage.Reader.CancelOrderOptionalField), reader.CancelOrderOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderAppendage.Reader.CancelOrderOptionalValue));
        Print(reader.CancelOrderOptionalValue, indent + Indent);
        if (reader.CancelOrderOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(CancelOrderMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.Quantity), reader.Quantity);
        if (reader.AppendageLength is { } appendageLengthValue)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.AppendageLength), appendageLengthValue);
        }
        foreach (var entry in reader.CancelOrderAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.CancelOrderMessage.Reader.CancelOrderAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(ReplaceOrderOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MinQty)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.MinQty), reader.MinQty);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.MaxFloor)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.MaxFloor), reader.MaxFloor);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.PriceType), reader.PriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PostOnly)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.PostOnly), reader.PostOnly);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.ExpireTime)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.ExpireTime), reader.ExpireTime);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.TradeNow)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.TradeNow), reader.TradeNow);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.HandleInst)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.HandleInst), reader.HandleInst);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.RandomReserves)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.RandomReserves), reader.RandomReserves);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.PegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.PegOffset), reader.PegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPrice)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.DiscretionPrice), reader.DiscretionPrice);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.DiscretionPriceType), reader.DiscretionPriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.DiscretionPegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.DiscretionPegOffset), reader.DiscretionPegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.SharesLocated)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.SharesLocated), reader.SharesLocated);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.LocateBroker)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.LocateBroker), reader.LocateBroker);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.Side)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.Side), reader.Side);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqStrategy)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.AiqStrategy), reader.AiqStrategy);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.AiqGroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Reader.AiqGroupId), reader.AiqGroupId);
        }
    }

    internal static void Print(ReplaceOrderAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Reader.ReplaceOrderOptionalField), reader.ReplaceOrderOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderAppendage.Reader.ReplaceOrderOptionalValue));
        Print(reader.ReplaceOrderOptionalValue, indent + Indent);
        if (reader.ReplaceOrderOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ReplaceOrderMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.OrigUserRefNum), reader.OrigUserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.TimeInForce), reader.TimeInForce);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Display), reader.Display);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.Clordid), reader.Clordid);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.ReplaceOrderAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ReplaceOrderMessage.Reader.ReplaceOrderAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(EnterOrderOptionalValue.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.Firm)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.Firm), reader.Firm);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MinQty)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.MinQty), reader.MinQty);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.CustomerType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.CustomerType), reader.CustomerType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.MaxFloor)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.MaxFloor), reader.MaxFloor);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.PriceType), reader.PriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.PegOffset), reader.PegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPrice)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.DiscretionPrice), reader.DiscretionPrice);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPriceType)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.DiscretionPriceType), reader.DiscretionPriceType);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.DiscretionPegOffset)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.DiscretionPegOffset), reader.DiscretionPegOffset);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.PostOnly)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.PostOnly), reader.PostOnly);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.RandomReserves)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.RandomReserves), reader.RandomReserves);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.ExpireTime)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.ExpireTime), reader.ExpireTime);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.TradeNow)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.TradeNow), reader.TradeNow);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.HandleInst)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.HandleInst), reader.HandleInst);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.GroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.GroupId), reader.GroupId);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.SharesLocated)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.SharesLocated), reader.SharesLocated);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.LocateBroker)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.LocateBroker), reader.LocateBroker);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.UserRefIdx)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.UserRefIdx), reader.UserRefIdx);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqStrategy)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.AiqStrategy), reader.AiqStrategy);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.AiqGroupId)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Reader.AiqGroupId), reader.AiqGroupId);
        }
    }

    internal static void Print(EnterOrderAppendage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Reader.OptionalFieldLength), reader.OptionalFieldLength);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Reader.EnterOrderOptionalField), reader.EnterOrderOptionalField);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderAppendage.Reader.EnterOrderOptionalValue));
        Print(reader.EnterOrderOptionalValue, indent + Indent);
        if (reader.EnterOrderOptionalValue.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderOptionalValue.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(EnterOrderMessage.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.UserRefNum), reader.UserRefNum);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Side), reader.Side);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Quantity), reader.Quantity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Symbol), reader.Symbol);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Price), reader.Price);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.TimeInForce), reader.TimeInForce);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Display), reader.Display);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Capacity), reader.Capacity);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.InterMarketSweepEligibility), reader.InterMarketSweepEligibility);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.CrossType), reader.CrossType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.Clordid), reader.Clordid);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.AppendageLength), reader.AppendageLength);
        foreach (var entry in reader.EnterOrderAppendage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.EnterOrderMessage.Reader.EnterOrderAppendage));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(UnsequencedMessage.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnterOrderMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.EnterOrderMessage));
            Print(reader.EnterOrderMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ReplaceOrderMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.ReplaceOrderMessage));
            Print(reader.ReplaceOrderMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.CancelOrderMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.CancelOrderMessage));
            Print(reader.CancelOrderMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.ModifyOrderMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.ModifyOrderMessage));
            Print(reader.ModifyOrderMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.AccountQueryMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.AccountQueryMessage));
            Print(reader.AccountQueryMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.MassCancelRequestMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.MassCancelRequestMessage));
            Print(reader.MassCancelRequestMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.DisableOrderEntryRequestMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.DisableOrderEntryRequestMessage));
            Print(reader.DisableOrderEntryRequestMessage, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.EnableOrderEntryRequestMessage)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Reader.EnableOrderEntryRequestMessage));
            Print(reader.EnableOrderEntryRequestMessage, indent + Indent);
        }
    }

    internal static void Print(UnsequencedDataPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader.UnsequencedMessageType), reader.UnsequencedMessageType);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedDataPacket.Reader.UnsequencedMessage));
        Print(reader.UnsequencedMessage, indent + Indent);
        if (reader.UnsequencedMessage.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.UnsequencedMessage.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ClientPayload.Reader reader, string indent)
    {
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.DebugPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Reader.DebugPacket));
            Print(reader.DebugPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LoginRequestPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Reader.LoginRequestPacket));
            Print(reader.LoginRequestPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.UnsequencedDataPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Reader.UnsequencedDataPacket));
            Print(reader.UnsequencedDataPacket, indent + Indent);
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.ClientHeartbeat)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.ClientHeartbeat));
        }
        if (reader.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LogoutRequest)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.LogoutRequest));
        }
    }

    internal static void Print(ClientSoupBinTcpPacket.Reader reader, string indent)
    {
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientSoupBinTcpPacket.Reader.ClientPacketHeader));
        Print(reader.ClientPacketHeader, indent + Indent);
        Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientSoupBinTcpPacket.Reader.ClientPayload));
        Print(reader.ClientPayload, indent + Indent);
        if (reader.ClientPayload.Case == global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.None)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPayload.Case.None), Convert.ToHexString(reader.Bytes));
        }
    }

    internal static void Print(ClientPacket.Reader reader, string indent)
    {
        foreach (var entry in reader.ClientSoupBinTcpPacket)
        {
            Line(indent, nameof(global::Nasdaq.NsmEquities.Orders.Ouch.v5_0.ClientPacket.Reader.ClientSoupBinTcpPacket));
            Print(entry, indent + Indent);
        }
    }

    internal static void Print(in EnableOrderEntryResponseAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(EnableOrderEntryResponseAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(EnableOrderEntryResponseAppendage.Layout.EnableOrderEntryResponseOptionalField), layout.EnableOrderEntryResponseOptionalField);
    }

    internal static void Print(in EnableOrderEntryResponseMessage.Layout layout, string indent)
    {
        Line(indent, nameof(EnableOrderEntryResponseMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(EnableOrderEntryResponseMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(EnableOrderEntryResponseMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(EnableOrderEntryResponseMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in DisableOrderEntryResponseAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(DisableOrderEntryResponseAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(DisableOrderEntryResponseAppendage.Layout.DisableOrderEntryResponseOptionalField), layout.DisableOrderEntryResponseOptionalField);
    }

    internal static void Print(in DisableOrderEntryResponseMessage.Layout layout, string indent)
    {
        Line(indent, nameof(DisableOrderEntryResponseMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(DisableOrderEntryResponseMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(DisableOrderEntryResponseMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(DisableOrderEntryResponseMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in MassCancelResponseAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(MassCancelResponseAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(MassCancelResponseAppendage.Layout.MassCancelResponseOptionalField), layout.MassCancelResponseOptionalField);
    }

    internal static void Print(in MassCancelResponseMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MassCancelResponseMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(MassCancelResponseMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(MassCancelResponseMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(MassCancelResponseMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(MassCancelResponseMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in AccountQueryResponseAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(AccountQueryResponseAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(AccountQueryResponseAppendage.Layout.AccountQueryResponseOptionalField), layout.AccountQueryResponseOptionalField);
    }

    internal static void Print(in AccountQueryResponseMessage.Layout layout, string indent)
    {
        Line(indent, nameof(AccountQueryResponseMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(AccountQueryResponseMessage.Layout.NextUserRefNum), layout.NextUserRefNum);
    }

    internal static void Print(in OrderRestatedAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderRestatedAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(OrderRestatedAppendage.Layout.OrderRestatedOptionalField), layout.OrderRestatedOptionalField);
    }

    internal static void Print(in OrderRestatedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderRestatedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderRestatedMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(OrderRestatedMessage.Layout.OrderRestatedReason), layout.OrderRestatedReason);
        Line(indent, nameof(OrderRestatedMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in OrderModifiedAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderModifiedAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(OrderModifiedAppendage.Layout.OrderModifiedOptionalField), layout.OrderModifiedOptionalField);
    }

    internal static void Print(in OrderModifiedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderModifiedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderModifiedMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(OrderModifiedMessage.Layout.Side), layout.Side);
        Line(indent, nameof(OrderModifiedMessage.Layout.Quantity), layout.Quantity);
    }

    internal static void Print(in OrderPriorityUpdateAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderPriorityUpdateAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(OrderPriorityUpdateAppendage.Layout.OrderPriorityUpdateOptionalField), layout.OrderPriorityUpdateOptionalField);
    }

    internal static void Print(in OrderPriorityUpdateMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderPriorityUpdateMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderPriorityUpdateMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(OrderPriorityUpdateMessage.Layout.Price), layout.Price);
        Line(indent, nameof(OrderPriorityUpdateMessage.Layout.Display), layout.Display);
        Line(indent, nameof(OrderPriorityUpdateMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
    }

    internal static void Print(in CancelRejectAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelRejectAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(CancelRejectAppendage.Layout.CancelRejectOptionalField), layout.CancelRejectOptionalField);
    }

    internal static void Print(in CancelRejectMessage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelRejectMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(CancelRejectMessage.Layout.UserRefNum), layout.UserRefNum);
    }

    internal static void Print(in CancelPendingAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelPendingAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(CancelPendingAppendage.Layout.CancelPendingOptionalField), layout.CancelPendingOptionalField);
    }

    internal static void Print(in CancelPendingMessage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelPendingMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(CancelPendingMessage.Layout.UserRefNum), layout.UserRefNum);
    }

    internal static void Print(in RejectedOrderAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(RejectedOrderAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(RejectedOrderAppendage.Layout.RejectedOrderOptionalField), layout.RejectedOrderOptionalField);
    }

    internal static void Print(in RejectedOrderMessage.Layout layout, string indent)
    {
        Line(indent, nameof(RejectedOrderMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(RejectedOrderMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(RejectedOrderMessage.Layout.RejectedOrderReason), layout.RejectedOrderReason);
        Line(indent, nameof(RejectedOrderMessage.Layout.Clordid), layout.Clordid);
    }

    internal static void Print(in BrokenTradeAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(BrokenTradeAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(BrokenTradeAppendage.Layout.BrokenTradeOptionalField), layout.BrokenTradeOptionalField);
    }

    internal static void Print(in BrokenTradeMessage.Layout layout, string indent)
    {
        Line(indent, nameof(BrokenTradeMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(BrokenTradeMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(BrokenTradeMessage.Layout.MatchNumber), layout.MatchNumber);
        Line(indent, nameof(BrokenTradeMessage.Layout.BrokenTradeReason), layout.BrokenTradeReason);
        Line(indent, nameof(BrokenTradeMessage.Layout.Clordid), layout.Clordid);
    }

    internal static void Print(in OrderExecutedAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderExecutedAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(OrderExecutedAppendage.Layout.OrderExecutedOptionalField), layout.OrderExecutedOptionalField);
    }

    internal static void Print(in OrderExecutedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderExecutedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderExecutedMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(OrderExecutedMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(OrderExecutedMessage.Layout.Price), layout.Price);
        Line(indent, nameof(OrderExecutedMessage.Layout.LiquidityFlag), layout.LiquidityFlag);
        Line(indent, nameof(OrderExecutedMessage.Layout.MatchNumber), layout.MatchNumber);
        Line(indent, nameof(OrderExecutedMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in AiqCanceledAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(AiqCanceledAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(AiqCanceledAppendage.Layout.AiqCanceledOptionalField), layout.AiqCanceledOptionalField);
    }

    internal static void Print(in AiqCanceledMessage.Layout layout, string indent)
    {
        Line(indent, nameof(AiqCanceledMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(AiqCanceledMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(AiqCanceledMessage.Layout.DecrementShares), layout.DecrementShares);
        Line(indent, nameof(AiqCanceledMessage.Layout.OrderCancelReason), layout.OrderCancelReason);
        Line(indent, nameof(AiqCanceledMessage.Layout.QuantityPreventedFromTrading), layout.QuantityPreventedFromTrading);
        Line(indent, nameof(AiqCanceledMessage.Layout.ExecutionPrice), layout.ExecutionPrice);
        Line(indent, nameof(AiqCanceledMessage.Layout.LiquidityFlag), layout.LiquidityFlag);
        Line(indent, nameof(AiqCanceledMessage.Layout.AiqStrategy), layout.AiqStrategy);
    }

    internal static void Print(in CanceledMessageAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(CanceledMessageAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(CanceledMessageAppendage.Layout.CanceledMessageOptionalField), layout.CanceledMessageOptionalField);
    }

    internal static void Print(in CanceledMessage.Layout layout, string indent)
    {
        Line(indent, nameof(CanceledMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(CanceledMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(CanceledMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(CanceledMessage.Layout.CancelOrderReason), layout.CancelOrderReason);
    }

    internal static void Print(in ReplacedMessageAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(ReplacedMessageAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(ReplacedMessageAppendage.Layout.ReplacedMessageOptionalField), layout.ReplacedMessageOptionalField);
    }

    internal static void Print(in ReplacedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ReplacedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(ReplacedMessage.Layout.OrigUserRefNum), layout.OrigUserRefNum);
        Line(indent, nameof(ReplacedMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(ReplacedMessage.Layout.Side), layout.Side);
        Line(indent, nameof(ReplacedMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(ReplacedMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(ReplacedMessage.Layout.Price), layout.Price);
        Line(indent, nameof(ReplacedMessage.Layout.TimeInForce), layout.TimeInForce);
        Line(indent, nameof(ReplacedMessage.Layout.Display), layout.Display);
        Line(indent, nameof(ReplacedMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(ReplacedMessage.Layout.Capacity), layout.Capacity);
        Line(indent, nameof(ReplacedMessage.Layout.InterMarketSweepEligibility), layout.InterMarketSweepEligibility);
        Line(indent, nameof(ReplacedMessage.Layout.CrossType), layout.CrossType);
        Line(indent, nameof(ReplacedMessage.Layout.OrderState), layout.OrderState);
        Line(indent, nameof(ReplacedMessage.Layout.Clordid), layout.Clordid);
        Line(indent, nameof(ReplacedMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in OrderAcceptedAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderAcceptedAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(OrderAcceptedAppendage.Layout.OrderAcceptedOptionalField), layout.OrderAcceptedOptionalField);
    }

    internal static void Print(in OrderAcceptedMessage.Layout layout, string indent)
    {
        Line(indent, nameof(OrderAcceptedMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(OrderAcceptedMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Side), layout.Side);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Price), layout.Price);
        Line(indent, nameof(OrderAcceptedMessage.Layout.TimeInForce), layout.TimeInForce);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Display), layout.Display);
        Line(indent, nameof(OrderAcceptedMessage.Layout.OrderReferenceNumber), layout.OrderReferenceNumber);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Capacity), layout.Capacity);
        Line(indent, nameof(OrderAcceptedMessage.Layout.InterMarketSweepEligibility), layout.InterMarketSweepEligibility);
        Line(indent, nameof(OrderAcceptedMessage.Layout.CrossType), layout.CrossType);
        Line(indent, nameof(OrderAcceptedMessage.Layout.OrderState), layout.OrderState);
        Line(indent, nameof(OrderAcceptedMessage.Layout.Clordid), layout.Clordid);
        Line(indent, nameof(OrderAcceptedMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in SystemEventMessage.Layout layout, string indent)
    {
        Line(indent, nameof(SystemEventMessage.Layout.Timestamp), layout.Timestamp);
        Line(indent, nameof(SystemEventMessage.Layout.EventCode), layout.EventCode);
    }

    internal static void Print(in SequencedDataPacket.Layout layout, string indent)
    {
        Line(indent, nameof(SequencedDataPacket.Layout.SequencedMessageType), layout.SequencedMessageType);
    }

    internal static void Print(in LoginRejectedPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginRejectedPacket.Layout.RejectReasonCode), layout.RejectReasonCode);
    }

    internal static void Print(in LoginAcceptedPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginAcceptedPacket.Layout.AcceptedSession), layout.AcceptedSession);
        Line(indent, nameof(LoginAcceptedPacket.Layout.AcceptedSequenceNumber), layout.AcceptedSequenceNumber);
    }

    internal static void Print(in ServerPacketHeader.Layout layout, string indent)
    {
        Line(indent, nameof(ServerPacketHeader.Layout.PacketLength), layout.PacketLength);
        Line(indent, nameof(ServerPacketHeader.Layout.ServerPacketType), layout.ServerPacketType);
    }

    internal static void Print(in ServerSoupBinTcpPacket.Layout layout, string indent)
    {
        Line(indent, nameof(ServerSoupBinTcpPacket.Layout.ServerPacketHeader));
        Print(layout.ServerPacketHeader, indent + Indent);
    }

    internal static void Print(in EnableOrderEntryRequestAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(EnableOrderEntryRequestAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(EnableOrderEntryRequestAppendage.Layout.EnableOrderEntryRequestOptionalField), layout.EnableOrderEntryRequestOptionalField);
    }

    internal static void Print(in EnableOrderEntryRequestMessage.Layout layout, string indent)
    {
        Line(indent, nameof(EnableOrderEntryRequestMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(EnableOrderEntryRequestMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(EnableOrderEntryRequestMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in DisableOrderEntryRequestAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(DisableOrderEntryRequestAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(DisableOrderEntryRequestAppendage.Layout.DisableOrderEntryRequestOptionalField), layout.DisableOrderEntryRequestOptionalField);
    }

    internal static void Print(in DisableOrderEntryRequestMessage.Layout layout, string indent)
    {
        Line(indent, nameof(DisableOrderEntryRequestMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(DisableOrderEntryRequestMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(DisableOrderEntryRequestMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in MassCancelRequestAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(MassCancelRequestAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(MassCancelRequestAppendage.Layout.MassCancelRequestOptionalField), layout.MassCancelRequestOptionalField);
    }

    internal static void Print(in MassCancelRequestMessage.Layout layout, string indent)
    {
        Line(indent, nameof(MassCancelRequestMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(MassCancelRequestMessage.Layout.Firm), layout.Firm);
        Line(indent, nameof(MassCancelRequestMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(MassCancelRequestMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in AccountQueryAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(AccountQueryAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(AccountQueryAppendage.Layout.AccountQueryOptionalField), layout.AccountQueryOptionalField);
    }

    internal static void Print(in ModifyOrderAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(ModifyOrderAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(ModifyOrderAppendage.Layout.ModifyOrderOptionalField), layout.ModifyOrderOptionalField);
    }

    internal static void Print(in ModifyOrderMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ModifyOrderMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(ModifyOrderMessage.Layout.Side), layout.Side);
        Line(indent, nameof(ModifyOrderMessage.Layout.Quantity), layout.Quantity);
    }

    internal static void Print(in CancelOrderAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelOrderAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(CancelOrderAppendage.Layout.CancelOrderOptionalField), layout.CancelOrderOptionalField);
    }

    internal static void Print(in CancelOrderMessage.Layout layout, string indent)
    {
        Line(indent, nameof(CancelOrderMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(CancelOrderMessage.Layout.Quantity), layout.Quantity);
    }

    internal static void Print(in ReplaceOrderAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(ReplaceOrderAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(ReplaceOrderAppendage.Layout.ReplaceOrderOptionalField), layout.ReplaceOrderOptionalField);
    }

    internal static void Print(in ReplaceOrderMessage.Layout layout, string indent)
    {
        Line(indent, nameof(ReplaceOrderMessage.Layout.OrigUserRefNum), layout.OrigUserRefNum);
        Line(indent, nameof(ReplaceOrderMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(ReplaceOrderMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(ReplaceOrderMessage.Layout.Price), layout.Price);
        Line(indent, nameof(ReplaceOrderMessage.Layout.TimeInForce), layout.TimeInForce);
        Line(indent, nameof(ReplaceOrderMessage.Layout.Display), layout.Display);
        Line(indent, nameof(ReplaceOrderMessage.Layout.InterMarketSweepEligibility), layout.InterMarketSweepEligibility);
        Line(indent, nameof(ReplaceOrderMessage.Layout.Clordid), layout.Clordid);
        Line(indent, nameof(ReplaceOrderMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in EnterOrderAppendage.Layout layout, string indent)
    {
        Line(indent, nameof(EnterOrderAppendage.Layout.OptionalFieldLength), layout.OptionalFieldLength);
        Line(indent, nameof(EnterOrderAppendage.Layout.EnterOrderOptionalField), layout.EnterOrderOptionalField);
    }

    internal static void Print(in EnterOrderMessage.Layout layout, string indent)
    {
        Line(indent, nameof(EnterOrderMessage.Layout.UserRefNum), layout.UserRefNum);
        Line(indent, nameof(EnterOrderMessage.Layout.Side), layout.Side);
        Line(indent, nameof(EnterOrderMessage.Layout.Quantity), layout.Quantity);
        Line(indent, nameof(EnterOrderMessage.Layout.Symbol), layout.Symbol);
        Line(indent, nameof(EnterOrderMessage.Layout.Price), layout.Price);
        Line(indent, nameof(EnterOrderMessage.Layout.TimeInForce), layout.TimeInForce);
        Line(indent, nameof(EnterOrderMessage.Layout.Display), layout.Display);
        Line(indent, nameof(EnterOrderMessage.Layout.Capacity), layout.Capacity);
        Line(indent, nameof(EnterOrderMessage.Layout.InterMarketSweepEligibility), layout.InterMarketSweepEligibility);
        Line(indent, nameof(EnterOrderMessage.Layout.CrossType), layout.CrossType);
        Line(indent, nameof(EnterOrderMessage.Layout.Clordid), layout.Clordid);
        Line(indent, nameof(EnterOrderMessage.Layout.AppendageLength), layout.AppendageLength);
    }

    internal static void Print(in UnsequencedDataPacket.Layout layout, string indent)
    {
        Line(indent, nameof(UnsequencedDataPacket.Layout.UnsequencedMessageType), layout.UnsequencedMessageType);
    }

    internal static void Print(in LoginRequestPacket.Layout layout, string indent)
    {
        Line(indent, nameof(LoginRequestPacket.Layout.Username), layout.Username);
        Line(indent, nameof(LoginRequestPacket.Layout.Password), layout.Password);
        Line(indent, nameof(LoginRequestPacket.Layout.RequestedSession), layout.RequestedSession);
        Line(indent, nameof(LoginRequestPacket.Layout.RequestedSequenceNumber), layout.RequestedSequenceNumber);
    }

    internal static void Print(in ClientPacketHeader.Layout layout, string indent)
    {
        Line(indent, nameof(ClientPacketHeader.Layout.PacketLength), layout.PacketLength);
        Line(indent, nameof(ClientPacketHeader.Layout.ClientPacketType), layout.ClientPacketType);
    }

    internal static void Print(in ClientSoupBinTcpPacket.Layout layout, string indent)
    {
        Line(indent, nameof(ClientSoupBinTcpPacket.Layout.ClientPacketHeader));
        Print(layout.ClientPacketHeader, indent + Indent);
    }
}
