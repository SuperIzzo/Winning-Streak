using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawn : MonoBehaviour {

	public List<GameObject> spawnPoints;


	// Use this for initialization
	void Start () 
	{
		int count = spawnPoints.Count;

		this.transform.position = spawnPoints[Random.Range(0,count)].transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
