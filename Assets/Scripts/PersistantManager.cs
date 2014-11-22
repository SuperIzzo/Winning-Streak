using UnityEngine;
using System.Collections;

public class PersistantManager : MonoBehaviour {

	//make sure it's persistant
	public static PersistantManager PM;
	void Awake()
	{
		if(!PM)
		{
			PM = this;
			DontDestroyOnLoad(gameObject);
		}
		else Destroy(gameObject);
	}

	void Start()
	{
		//PlayerPrefs.DeleteAll();
		if(PlayerPrefs.GetString("HighScore") == null || PlayerPrefs.GetString("HighScore") == "")
		{
			Debug.Log("resetting score");
		    PlayerPrefs.SetString("HighScore", "0");
		}
	}
}
