using UnityEngine;
using System.Collections;

public class ReferenceManager : MonoBehaviour {

    private static GameObject Player;
    private static GameObject SoundManager;
    private static GameObject Camera;
	
	void Update () {

        if (!Player)
            Player = GameObject.FindGameObjectWithTag("Player");

        if (!SoundManager)
            SoundManager = GameObject.FindGameObjectWithTag("SoundManager");

        if (!Camera)
        {
            Camera = GameObject.FindGameObjectWithTag("MainCamera");
            Camera = GameObject.Find("Main Camera");
        }
	}

    public static GameObject GetPlayer()
    {
        return Player;
    }

    public static GameObject GetSoundManager()
    {
        return SoundManager;
    }

    public static GameObject GetCamera()
    {
        return Camera;
    }
}
