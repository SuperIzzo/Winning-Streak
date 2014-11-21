using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	GameObject player;

	int cameraSwing = 75;

	bool inHatPicker = false;
	bool transitioning = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		if(!transitioning)
		{
			if(player.GetComponent<PlayerController>().TestButton("DPAD-RIGHT") && !inHatPicker)
			{
				inHatPicker = true;
				transitioning = true;
			}
		}
		else
		{	
			//going to hatPicker
			if(inHatPicker)
			{
				//this.transform.rotation
			}
		}
	
	}
}
