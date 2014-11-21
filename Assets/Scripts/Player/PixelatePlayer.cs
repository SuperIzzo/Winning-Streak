using UnityEngine;
using System.Collections;

public class PixelatePlayer : MonoBehaviour {

	GameObject camera;
	GameObject player;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = player.transform.position - new Vector3(0,-0.4f,0.25f);
		this.transform.LookAt(camera.transform.position);
	}
}
