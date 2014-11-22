using UnityEngine;
using System.Collections;

public class HatPosition : MonoBehaviour {

	public GameObject toFollow;
	

	// Use this for initialization
	void Start () {

		this.transform.position = toFollow.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//this.transform.localScale = originalScale;
		//this.transform.rotation = originalRotation;
		this.transform.position = toFollow.transform.position;


	}
}
