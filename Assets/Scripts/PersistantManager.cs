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
}
