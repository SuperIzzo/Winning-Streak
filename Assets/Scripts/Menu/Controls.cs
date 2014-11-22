using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public GameObject player;

	public AudioClip toHats;
	public AudioClip fromHats;
	AudioSource sound;

	int cameraSwing = 75;

	bool inHatPicker = false;
	bool transitioning = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		sound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		player.GetComponent<PlayerController>().UpdateController();

		if(!transitioning)
		{
			if((player.GetComponent<PlayerController>().TestButton("DPAD-RIGHT") || Input.GetKeyDown(KeyCode.D)) && !inHatPicker)
			{
				sound.PlayOneShot(toHats,1);
				inHatPicker = true;
				transitioning = true;
			}

			if((player.GetComponent<PlayerController>().TestButton("DPAD-LEFT") || Input.GetKeyDown(KeyCode.A)) && inHatPicker)
			{
				sound.PlayOneShot(fromHats,1);
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
