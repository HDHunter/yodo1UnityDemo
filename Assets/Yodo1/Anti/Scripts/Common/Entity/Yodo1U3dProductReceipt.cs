using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yodo1.AntiAddiction
{
    public class Yodo1U3dProductReceipt
    {
        private string _productId;
        private Yodo1U3dProductType _productType;
        private double _price;
        private string _currency;
        private string _orderId;


        private const string JSON_PRODUCT_ID_KEY = "itemCode";
        private const string JSON_PRODUCT_TYPE_KEY = "itemType";
        private const string JSON_PRODUCT_PRICE_KEY = "money";
        private const string JSON_PRODUCT_CURRENCY_KEY = "currency";

        private const string JSON_PRODUCT_ORDER_ID_KEY = "orderId";
        //region spendDate

        /// <summary>
        /// The product identifier will be used by game.
        /// </summary>
        public string ProductId
        {
            get { return _productId; }
        }

        /// <summary>
        /// The type of the product.
        /// </summary>
        public Yodo1U3dProductType ProductType
        {
            get { return _productType; }
        }

        /// <summary>
        /// The cost of the product in the local currency.
        /// </summary>
        public double Price
        {
            get { return _price; }
        }

        /// <summary>
        /// The currency.
        /// </summary>
        public string Currency
        {
            get { return _currency; }
        }

        /// <summary>
        /// A string that uniquely identifies a payment transaction.
        /// </summary>
        public string OrderId
        {
            get { return _orderId; }
        }

        /// <summary>
        /// Serialize to JSON string
        /// </summary>
        /// <returns></returns>
        public string ToJsonString()
        {
            Dictionary<string, object> jsonDict = new Dictionary<string, object>();
            jsonDict[JSON_PRODUCT_ID_KEY] = ProductId;
            jsonDict[JSON_PRODUCT_TYPE_KEY] = (int) ProductType;
            jsonDict[JSON_PRODUCT_CURRENCY_KEY] = Currency;
            jsonDict[JSON_PRODUCT_PRICE_KEY] = Price;
            jsonDict[JSON_PRODUCT_ORDER_ID_KEY] = OrderId;
            return JSONObject.Serialize(jsonDict);
        }

        /// <summary>
        /// Initialization form param
        /// </summary>
        /// <param name="productId">The product identifier will be used by game.</param>
        /// <param name="productType">The type of the product.</param>
        /// <param name="price">The cost of the product in the local currency.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="orderId">A string that uniquely identifies a payment transaction.</param>
        /// <returns></returns>
        private bool Init(string productId, Yodo1U3dProductType productType, double price, string currency,
            string orderId)
        {
            _productId = productId;
            _productType = productType;
            _price = price;
            _currency = currency;
            _orderId = orderId;
            return true;
        }

        /// <summary>
        /// Initialization form json string
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        private bool InitFromJsonString(string jsonString)
        {
            object jsonObj = JSONObject.Deserialize(jsonString);
            if (jsonObj == null)
            {
                return false;
            }

            Dictionary<string, object> jsonDict = jsonObj as Dictionary<string, object>;

            if (jsonDict == null)
            {
                return false;
            }

            if (jsonDict.ContainsKey(JSON_PRODUCT_ID_KEY))
            {
                _productId = jsonDict[JSON_PRODUCT_ID_KEY].ToString();
            }

            if (jsonDict.ContainsKey(JSON_PRODUCT_TYPE_KEY))
            {
                System.Enum.TryParse<Yodo1U3dProductType>(jsonDict[JSON_PRODUCT_TYPE_KEY].ToString(), out _productType);
            }

            if (jsonDict.ContainsKey(JSON_PRODUCT_CURRENCY_KEY))
            {
                _currency = jsonDict[JSON_PRODUCT_CURRENCY_KEY].ToString();
            }

            if (jsonDict.ContainsKey(JSON_PRODUCT_PRICE_KEY))
            {
                double.TryParse(jsonDict[JSON_PRODUCT_PRICE_KEY].ToString(), System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out _price);
            }

            if (jsonDict.ContainsKey(JSON_PRODUCT_ORDER_ID_KEY))
            {
                _orderId = jsonDict[JSON_PRODUCT_ORDER_ID_KEY].ToString();
            }

            return true;
        }


        /// <summary>
        /// Create Yodo1U3dProductReceipt object form param
        /// </summary>
        /// <param name="productId">The product identifier will be used by game.</param>
        /// <param name="productType">The type of the product.</param>
        /// <param name="price">The cost of the product in the local currency.</param>
        /// <param name="currency">The currency.</param>
        /// <param name="orderId">A string that uniquely identifies a payment transaction.</param>
        /// <returns></returns>
        public static Yodo1U3dProductReceipt Create(string productId, Yodo1U3dProductType productType, double price,
            string currency, string orderId)
        {
            Yodo1U3dProductReceipt productReceipt = new Yodo1U3dProductReceipt();
            if (productReceipt.Init(productId, productType, price, currency, orderId) == false)
            {
                return null;
            }

            return productReceipt;
        }

        /// <summary>
        /// Create Yodo1U3dProductReceipt object form json string
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns></returns>
        public static Yodo1U3dProductReceipt CreateFormJsonString(string jsonString)
        {
            Yodo1U3dProductReceipt productReceipt = new Yodo1U3dProductReceipt();
            if (productReceipt.InitFromJsonString(jsonString) == false)
            {
                return null;
            }

            return productReceipt;
        }
    }
}