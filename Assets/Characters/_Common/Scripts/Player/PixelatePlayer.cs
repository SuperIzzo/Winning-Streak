using UnityEngine;
using System.Collections;

public class PixelatePlayer : MonoBehaviour {

	GameObject camera;
	public GameObject target;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");

		if( target == null )
			target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = target.transform.position;
		this.transform.LookAt(camera.transform.position);
	}
}
