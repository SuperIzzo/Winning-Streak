using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawn : MonoBehaviour {

	GameObject player;
	public List<GameObject> spawnPoints;


	// Use this for initialization
	void Start () 
	{
		player = this.gameObject;

		int count = spawnPoints.Count;

		player.transform.position = spawnPoints[Random.Range(0,count)].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
