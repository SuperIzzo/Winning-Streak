using UnityEngine;
using System.Collections;

static public class AchievementInterface
{
    static private AchievementsManager achievementManager = new AchievementsManager();

    public static void LoadAchievements()
    {
        achievementManager.Load();
    }

    //-------------------------------------------------------------
    /// <summary> Gives the player an achievement </summary>
    /// 
    /// <usage> 
    /// AchievementInterface.GiveAchievement<int>(0);
    /// AchievementInterface.GiveAchievement<string>("Getting a-head");
    /// </usage>
    /// 
    /// <param name="id">string or int to specify which achievement
    /// to give to the player. </param>
    //-------------------------------------------------------------
    public static void GiveAchievement<T>(T id)
    {
        achievementManager.UnlockAchievement<T>(id);

    }
}
