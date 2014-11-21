using UnityEngine;
using System.Collections;

public class GoalScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collider.name == "ball")
		{
			SoundManager.TriggerEvent("goal");
		}
	}
}
