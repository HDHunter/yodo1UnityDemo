/// <summary>
/// Leaderboard works on Apple and Google only
/// </summary>
public class Yodo1U3dLeaderboard
{
    /// <summary>
    /// 打开排行榜
    /// </summary>
    public static void leaderboardsOpen()
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.leaderboardsOpen();
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.LeaderboardsOpen();
#endif
    }

    /// <summary>
    /// 提交分数
    /// </summary>
    /// <param name="scoreId">分数Id</param>
    /// <param name="score">分数值</param>
    public static void updateScore(string scoreId, long score)
    {
#if UNITY_ANDROID
        Yodo1U3dAccountForAndroid.updateScore(scoreId, score);
#elif UNITY_IPHONE
		Yodo1U3dGCManagerForIOS.UpdateScore(scoreId,(int)score);
#endif
    }

    /// <summary>
    /// Highs the score for leaderboard.获取排行榜最高分
    /// </summary>
    /// <returns>The score for leaderboard.</returns>
    /// <param name="identifier">Identifier.</param>
    public static int highScoreForLeaderboard(string identifier)
    {
#if UNITY_ANDROID
        return 0;
#elif UNITY_IPHONE
        return Yodo1U3dGCManagerForIOS.HighScoreForLeaderboard(identifier);
#else
        return 0;
#endif
    }
}