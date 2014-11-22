using UnityEngine;
using System.Collections;

public class HatPosition : MonoBehaviour {

	public GameObject toFollow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		this.transform.position = toFollow.transform.position;
	}
}
