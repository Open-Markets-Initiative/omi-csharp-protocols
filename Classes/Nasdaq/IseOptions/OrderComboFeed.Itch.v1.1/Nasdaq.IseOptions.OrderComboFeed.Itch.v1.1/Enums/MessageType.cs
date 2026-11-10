namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Code identifying this message type
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum MessageType : byte
{
    /// <summary>
    ///  The system event message type is used to signal a market or data feed handler event.
    /// </summary>
    SystemEventMessage = (byte)'S',

    /// <summary>
    ///  Whenever a complex order is added in the system for an underlying, the order is normalized and results in either the creation of a new complex strategy or is added to an existing strategy. A Complex Strategy Message containing the strategy definition will be sent.
    /// </summary>
    ComplexStrategyDirectoryMessage = (byte)'R',

    /// <summary>
    ///  ISE uses this administrative message to indicate the current trading status of a strategy within the ISE Options Market.
    /// </summary>
    StrategyTradingActionMessage = (byte)'H',

    /// <summary>
    ///  The options system uses this administrative message to indicate when an strategy has completed the opening process and is now available for auto execution or when the option has closed and is no longer available for auto execution.
    /// </summary>
    StrategyOpenClosedMessage = (byte)'O',

    /// <summary>
    ///  An Order on Book message is generated for all the following situations whenever an order free from any display restrictions is reported by the matching engine. In all such cases the order size to be displayed is as reported by the matching engine
    /// </summary>
    ComplexStrategyOrderOnBookMessage = (byte)'L',

    /// <summary>
    ///  This message is used to announce strategy auctions. The start of auction is announced, followed by possible updates on the auction, and announcing the end of the auction.
    /// </summary>
    ComplexStrategyAuctionMessage = (byte)'J',
}
