using UnityEngine;
using System.Collections;

public class HitNoise : MonoBehaviour {

	public GameObject soundManager;
	public GameObject player;

	float initTimer = 0;
	bool canPlaySound = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(initTimer < 4)
		{
			initTimer += Time.deltaTime;
		}
		else canPlaySound = true;
	}

	void OnCollisionEnter(Collision collision)
	{
		if(!canPlaySound)
			return;

		if(collision.collider.tag == "soundCollision")
		{
			//sound bubble
			if(Vector3.Distance(this.transform.position,player.transform.position) < 30)
			{
				float volume = Vector3.Distance(this.transform.position, player.transform.position) / 10;
				volume /= (volume * volume); 

				if(soundManager == null)
					soundManager = GameObject.FindGameObjectWithTag("SoundManager");

				soundManager.GetComponent<AudioMan>().PlayEffect("OBJECT_HIT2", volume);
			}
		}
	}
}
