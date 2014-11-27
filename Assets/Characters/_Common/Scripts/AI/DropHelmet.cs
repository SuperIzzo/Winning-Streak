﻿using UnityEngine;
using System.Collections;

public class DropHelmet : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Drop()
	{
//		if(Random.value < 0.1f)
//		{
//			Debug.Log ("dropped");
//			this.rigidbody.isKinematic = false;
//			this.transform.parent = null;
//			player.GetComponent<ItemControl>().weaponList.Add(gameObject);
//			//Destroy (this.GetComponent<DropHelmet>());
//		}
//		else 
//		{
			if(player)
				player.GetComponent<ItemControl>().weaponList.Add(gameObject);

			this.rigidbody.isKinematic = false;
			this.transform.parent = null;
			//Destroy (gameObject);
			return;
//		}
	}
}
