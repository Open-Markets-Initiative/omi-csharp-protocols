namespace Nasdaq.IseOptions.OrderComboFeed.Itch.v1_1;

/// <summary>
///  Order Capacity value
/// </summary>
/// <remarks>
///  Each value is the ASCII character that represents it on the wire.
/// </remarks>
public enum OrderCapacity : byte
{
    /// <summary>
    ///  Customer
    /// </summary>
    Customer = (byte)'C',

    /// <summary>
    ///  Customer Professional
    /// </summary>
    CustomerProfessional = (byte)'D',

    /// <summary>
    ///  Firm
    /// </summary>
    Firm = (byte)'F',

    /// <summary>
    ///  Broker/Dealer - Customer
    /// </summary>
    BrokerDealerCustomer = (byte)'B',

    /// <summary>
    ///  Broker/Dealer - Firm
    /// </summary>
    BrokerDealerFirm = (byte)'K',

    /// <summary>
    ///  Proprietary
    /// </summary>
    Proprietary = (byte)'E',

    /// <summary>
    ///  Away Market Maker
    /// </summary>
    AwayMarketMaker = (byte)'N',

    /// <summary>
    ///  Market Maker
    /// </summary>
    MarketMaker = (byte)'M',
}
