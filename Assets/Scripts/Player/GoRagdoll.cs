using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoRagdoll : MonoBehaviour {

	public List<GameObject> ragdollParts;
	public GameObject player;
	

	public void KillPlayer()
	{
		//if(this.tag == "Player")
			Time.timeScale = 0.2f;

		this.GetComponent<Animator>().enabled = false;

		if( player )
			player.GetComponent<PlayerController>().canMove = false;
		
		foreach(GameObject go in ragdollParts)
		{
			go.GetComponent<Rigidbody>().isKinematic = false;
		}
	}

	public void RevivePlayer()
	{
		this.GetComponent<Animator>().enabled = true;
		
		if( player )
			player.GetComponent<PlayerController>().canMove = true;
		
		foreach(GameObject go in ragdollParts)
		{
			go.GetComponent<Rigidbody>().isKinematic = true;
		}
	}
}
