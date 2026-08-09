namespace Iex.IexEquities.Deep.IexTp;

/// <summary>
///  Routes a message type code to its parsed IMessage, or null for unknown types.
/// </summary>
public static class Dispatch
{
    /// <summary>
    ///  Routes a message type code to its parsed IMessage from a pre-sliced payload span.
    /// </summary>
    public static IMessage? Parse(char type, ReadOnlySpan<byte> payload)
    {
        const char SystemEventMessageCode = 'S';
        const char SecurityDirectoryMessageCode = 'D';
        const char TradingStatusMessageCode = 'H';
        const char RetailLiquidityIndicatorMessageCode = 'I';
        const char OperationalHaltStatusMessageCode = 'O';
        const char ShortSalePriceTestStatusMessageCode = 'P';
        const char SecurityEventMessageCode = 'E';
        const char PriceLevelBuyUpdateMessageCode = '8';
        const char PriceLevelSellUpdateMessageCode = '5';
        const char TradeReportMessageCode = 'T';
        const char OfficialPriceMessageCode = 'X';
        const char TradeBreakMessageCode = 'B';
        const char AuctionInformationMessageCode = 'A';

        switch (type)
        {
            case SystemEventMessageCode: return SystemEventMessage.Parse(payload);
            case SecurityDirectoryMessageCode: return SecurityDirectoryMessage.Parse(payload);
            case TradingStatusMessageCode: return TradingStatusMessage.Parse(payload);
            case RetailLiquidityIndicatorMessageCode: return RetailLiquidityIndicatorMessage.Parse(payload);
            case OperationalHaltStatusMessageCode: return OperationalHaltStatusMessage.Parse(payload);
            case ShortSalePriceTestStatusMessageCode: return ShortSalePriceTestStatusMessage.Parse(payload);
            case SecurityEventMessageCode: return SecurityEventMessage.Parse(payload);
            case PriceLevelBuyUpdateMessageCode: return PriceLevelBuyUpdateMessage.Parse(payload);
            case PriceLevelSellUpdateMessageCode: return PriceLevelSellUpdateMessage.Parse(payload);
            case TradeReportMessageCode: return TradeReportMessage.Parse(payload);
            case OfficialPriceMessageCode: return OfficialPriceMessage.Parse(payload);
            case TradeBreakMessageCode: return TradeBreakMessage.Parse(payload);
            case AuctionInformationMessageCode: return AuctionInformationMessage.Parse(payload);
            default: return null;
        }
    }

    /// <summary>
    ///  True if the type code maps to a modelled message with no body (e.g. an SBE heartbeat template), for which Parse intentionally returns null. Lets size-walk callers skip these silently while still flagging genuinely-unknown codes.
    /// </summary>
    public static bool IsKnownEmptyType(char type) => false;
}
