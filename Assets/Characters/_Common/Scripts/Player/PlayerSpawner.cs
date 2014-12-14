using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {

    public GameObject PlayerPrefab;
    public List<GameObject> SpawnPoints;

	// Use this for initialization
	void Start () {

        Vector3 position;
        Quaternion rotation;

        int rand = Random.Range(0, SpawnPoints.Count);

        position = SpawnPoints[rand].transform.position;
        rotation = SpawnPoints[rand].transform.rotation;

        Instantiate(PlayerPrefab, position, rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
