using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Yodo1.AntiAddiction.Common
{
    public static class Yodo1U3dEventCode
    {
        /// <summary>
        /// Anti addiction initialization callback event(防沉迷初始化回调事件).
        /// </summary>
        public const int RESULT_TYPE_INIT = 8001;

        /// <summary>
        /// Time remaining to notify callback events(剩余时间通知回调事件).
        /// </summary>
        public const int RESULT_TYPE_TIME_LIMIT = 8002;

        /// <summary>
        /// Real name authentication callback event(实名认证回调事件).
        /// </summary>
        public const int RESULT_TYPE_CERTIFICATION = 8003;

        /// <summary>
        /// Whether to restrict consumption callback events(是否限制消费回调事件).
        /// </summary>
        public const int RESULT_TYPE_VERIFY_PURCHASE = 8004;

        /// <summary>
        /// Player has disconnected callback events(玩家从防沉迷掉线时回调事件).
        /// </summary>
        public const int RESULT_TYPE_PLAYER_DISCONNECTED = 8005;

        /// <summary>
        /// Players go online or offline callback events(玩家上下线行为回调事件).
        /// </summary>
        public const int RESULT_TYPE_BEHAVIOR_RESULT = 8006;
    }
}
