using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoRagdoll : MonoBehaviour {

	public List<GameObject> ragdollParts;
	public GameObject player;
	public GameObject pixelateGO;
	

	public void KillPlayer()
	{
		this.GetComponent<Animator>().enabled = false;
		pixelateGO.SetActive(false);
		player.GetComponent<PlayerController>().canMove = false;
		
		foreach(GameObject go in ragdollParts)
		{
			go.GetComponent<Rigidbody>().isKinematic = false;
		}
	}
}
