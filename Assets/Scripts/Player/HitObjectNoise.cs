using UnityEngine;
using System.Collections;

public class HitObjectNoise : MonoBehaviour {

	public GameObject soundManager;

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

			if(collision.collider.name != "Field" && collision.collider.name != "grass" && collision.collider.name != "Marking")
			soundManager.GetComponent<AudioMan>().PlayEffect("OBJECT_HIT2", 1);
		}
	}
}
