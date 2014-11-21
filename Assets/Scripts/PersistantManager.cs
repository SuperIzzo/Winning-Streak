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
		for(int i = 0; i < 5; i++)
		{
			if(PlayerPrefs.GetString("Score" + i) == "" ||
			   PlayerPrefs.GetString("Score"  + i) == null)
			{
				Debug.Log("resetting");
				PlayerPrefs.SetString("Score" + i, "0");
			}
		}


	}
}
