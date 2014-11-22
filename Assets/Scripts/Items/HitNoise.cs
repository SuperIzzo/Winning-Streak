using UnityEngine;
using System.Collections;

public class HitNoise : MonoBehaviour {

	public GameObject soundManager;
	public GameObject player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision collision)
	{
		if(collision.collider.tag == "soundCollision")
		{
			float volume = Vector3.Distance(this.transform.position, player.transform.position) / 10;
			volume /= (volume * volume) - volume / 10; 

			if(soundManager == null)
				soundManager = GameObject.FindGameObjectWithTag("SoundManager");

			soundManager.GetComponent<AudioMan>().PlayEffect("OBJECT_HIT2", volume);
		}
	}
}
