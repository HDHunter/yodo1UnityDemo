using System;

namespace Yodo1.Suit
{
    [Serializable]
    public class SettingItem
    {
        /// <summary>
        /// The name.
        /// </summary>
        public string Name;

        /// <summary>
        /// The index of the ad.
        /// </summary>
        public long Index;

        /// <summary>
        /// The URL.
        /// </summary>
        public string Url;

        /// <summary>
        /// The enable.
        /// </summary>
        public bool Enable;

        /// <summary>
        /// The selected.
        /// </summary>
        public bool Selected;
    }
}