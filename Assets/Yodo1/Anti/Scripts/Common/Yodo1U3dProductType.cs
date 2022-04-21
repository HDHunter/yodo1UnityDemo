namespace Yodo1.AntiAddiction
{
    public enum Yodo1U3dProductType
    {
        /// <summary>
        /// The non consumables(不可消耗).
        /// </summary>
        NonConsumables,

        /// <summary>
        /// The consumables(可消耗).
        /// </summary>
        Consumables,

        /// <summary>
        /// The auto subscription(自动订阅), Only works on iOS.
        /// </summary>
        AutoSubscription,

        /// <summary>
        /// The none auto subscription(非自动订阅), Only works on iOS.
        /// </summary>
        NoneAutoSubscription
    }
}