using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {
	//ball spawner too

	public GameObject player;
	public GameObject ball;
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

			GameObject go = (GameObject)Instantiate(enemyPrefab,randomPoint, new Quaternion(0,Random.Range(0,359),0,0));

			go.GetComponent<AIAttack>().player = player;
		}

		for(int i = 0; i < spawnAmount; i++)
		{
			Vector3 randomPoint = new Vector3(Random.Range(min.transform.position.x,max.transform.position.x),
			                                  Random.Range(min.transform.position.y,max.transform.position.y),
			                                  Random.Range(min.transform.position.z,max.transform.position.z));

			GameObject b = (GameObject)Instantiate(ball,randomPoint, new Quaternion(0,Random.Range(0,359),0,0));

			player.GetComponent<ItemControl>().weaponList.Add(b);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
