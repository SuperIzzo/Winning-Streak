using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoRagdoll : MonoBehaviour {

	public List<GameObject> ragdollParts;
	public GameObject player;

	// Use this for initialization
	void Start () {
//		foreach(GameObject go in ragdollParts)
//		{
//			go.GetComponent<Rigidbody>().isKinematic = true;
//		}
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.GetComponent<Animator>().enabled = false;
			player.GetComponent<PlayerController>().canMove = false;

			foreach(GameObject go in ragdollParts)
			{
				go.GetComponent<Rigidbody>().isKinematic = false;
			}
		}
	}

	public void KillPlayer()
	{
		this.GetComponent<Animator>().enabled = false;
		player.GetComponent<PlayerController>().canMove = false;
		
		foreach(GameObject go in ragdollParts)
		{
			go.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
