using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public GameObject player;

	int cameraSwing = 75;

	bool inHatPicker = false;
	bool transitioning = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {

		player.GetComponent<PlayerController>().UpdateController();

		if(!transitioning)
		{
			if((player.GetComponent<PlayerController>().TestButton("DPAD-RIGHT") || Input.GetKeyDown(KeyCode.D)) && !inHatPicker)
			{
				Debug.Log("pressed dpad right");
				inHatPicker = true;
				transitioning = true;
			}

			if((player.GetComponent<PlayerController>().TestButton("DPAD-LEFT") || Input.GetKeyDown(KeyCode.A)) && inHatPicker)
			{
				Debug.Log("pressed dpad left");
				inHatPicker = false;
				transitioning = true;
			}
		}
		else
		{	
			//going to hatPicker
			if(inHatPicker)
			{
				Quaternion toRot = Quaternion.identity;

				toRot.eulerAngles = new Vector3(0,73,0);

				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,toRot,Time.deltaTime * 10);

				if(this.transform.eulerAngles.y > 71)
				{
					Debug.Log("ended transition");
					transitioning = false;
				}
			}

			//going to mainmenu
			if(!inHatPicker)
			{
				Quaternion toRot = Quaternion.identity;
				
				toRot.eulerAngles = new Vector3(0,0,0);
				
				this.transform.rotation = Quaternion.Lerp (this.transform.rotation,toRot,Time.deltaTime * 10);
				
				if(this.transform.eulerAngles.y < 1)
				{
					transitioning = false;
				}
			}
		}
	
	}
}
