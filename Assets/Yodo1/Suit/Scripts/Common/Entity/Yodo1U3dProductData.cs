/// <summary>
/// Yodo1 u3d product data.
/// </summary>
public class Yodo1U3dProductData
{
    public enum ProductTypeEnum
    {
        /// <summary>
        /// The non consumables(不可消耗).
        /// </summary>
        NonConsumables = 0,

        /// <summary>
        /// The consumables(可消耗).
        /// </summary>
        Consumables = 1,

        /// <summary>
        /// The auto subscription(自动订阅), Only works on iOS.
        /// </summary>
        Auto_Subscription = 2,

        /// <summary>
        /// The none auto subscription(非自动订阅), Only works on iOS.
        /// </summary>
        None_Auto_Subscription = 3
    }

    private string orderId;
    private string productId;
    private string marketId;
    private string priceDisplay;
    private double price;
    private string currency;
    private string productName;
    private string description;
    private int coin;
    private ProductTypeEnum productType;
    private string periodUnit;

    /// <summary>
    /// A string that uniquely identifies a payment transaction.
    /// </summary>
    public string OrderId
    {
        get { return orderId; }

        set { orderId = value; }
    }

    /// <summary>
    /// The product identifier will be used by game.
    /// </summary>
    public string ProductId
    {
        get { return productId; }

        set { productId = value; }
    }

    /// <summary>
    /// The product identifier of market store.
    /// </summary>
    public string MarketId
    {
        get { return marketId; }

        set { marketId = value; }
    }

    /// <summary>
    /// The locale used to format the price of the product.
    /// </summary>
    public string PriceDisplay
    {
        get { return priceDisplay; }

        set { priceDisplay = value; }
    }

    /// <summary>
    /// The cost of the product in the local currency.
    /// </summary>
    public double Price
    {
        get { return price; }

        set { price = value; }
    }

    /// <summary>
    /// The currency.
    /// </summary>
    public string Currency
    {
        get { return currency; }

        set { currency = value; }
    }

    /// <summary>
    /// The name of the product.
    /// </summary>
    public string ProductName
    {
        get { return productName; }

        set { productName = value; }
    }

    /// <summary>
    /// A description of the product.
    /// </summary>
    public string Description
    {
        get { return description; }

        set { description = value; }
    }

    /// <summary>
    /// The purchase is equivalent to the game currency value.
    /// </summary>
    /// <value></value>
    public int Coin
    {
        get { return coin; }

        set { coin = value; }
    }

    /// <summary>
    /// The type of the product.
    /// </summary>
    public ProductTypeEnum ProductType
    {
        get { return productType; }
        set { productType = value; }
    }

    /// <summary>
    /// The product type is subscription, values representing the duration of an interval, from a day up to a year(Weekly, monthly, yearly, every 3 months). Only works on iOS.
    /// </summary>
    public string PeriodUnit
    {
        get { return periodUnit; }

        set { periodUnit = value; }
    }
}