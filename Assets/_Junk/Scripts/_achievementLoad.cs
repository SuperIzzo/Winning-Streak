using UnityEngine;
using System.Collections;

public class _achievementLoad : MonoBehaviour {


	void Start () 
    {
        //this would be contained in the social platform stuff?
        AchievementInterface.LoadAchievements();

        AchievementInterface.GiveAchievement<int>(0);
        AchievementInterface.GiveAchievement<string>("test");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
