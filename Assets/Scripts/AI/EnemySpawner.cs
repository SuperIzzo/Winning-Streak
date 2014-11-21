using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject player;
	public GameObject enemyPrefab;
	public int spawnAmount = 0;

	public GameObject min, max;

	// Use this for initialization
	void Start () 
	{
		for(int i = 0; i < spawnAmount; i++)
		{
			Vector3 randomPoint = new Vector3(Random.Range(min.transform.position.x,max.transform.position.x),
			                                  Random.Range(min.transform.position.y,max.transform.position.y),
			                                  Random.Range(min.transform.position.z,max.transform.position.z));

			Instantiate(enemyPrefab,randomPoint, new Quaternion(0,Random.Range(0,359),0,0));
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
