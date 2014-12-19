using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public bool enableBalls = true;
    public bool enableEnemies = true;

	public GameObject spawnPlace;

	public GameObject ball;
	public GameObject enemyPrefab;
	public GameObject min, max;

	public int spawnAmount = 0;

	public float addNewPlayerTime = 5;
	public int addAmount = 1;
	float addPlayerTimer = 0;

	public float speedupTimeInterval = 2;
	public float speedupIncrement = 0.1f;
	float speedTimer = 0;

  
	// Use this for initialization
	void Start () 
	{
        if (enableEnemies)
        {
            //random enemy spawning
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 randomPoint = new Vector3(Random.Range(min.transform.position.x, max.transform.position.x),
                                                  Random.Range(min.transform.position.y, max.transform.position.y),
                                                  Random.Range(min.transform.position.z, max.transform.position.z));

                GameObject go = (GameObject)Instantiate(enemyPrefab, randomPoint, new Quaternion(0, Random.Range(0, 359), 0, 0));

                //Random team selection, DOESN'T work well as they just take each other out instantly
                //if (Random.value < 0.5f)
                //{
                //    go.GetComponent<Faction>().faction = FactionID.TEAM_A;
                //}
                //else
                //{
                //    go.GetComponent<Faction>().faction = FactionID.TEAM_B;
                //}
            }
        }

        if (enableBalls)
        {
            //random ball spawning
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 randomPoint = new Vector3(Random.Range(min.transform.position.x, max.transform.position.x),
                                                  Random.Range(min.transform.position.y, max.transform.position.y),
                                                  Random.Range(min.transform.position.z, max.transform.position.z));

                GameObject b = (GameObject)Instantiate(ball, randomPoint, new Quaternion(0, Random.Range(0, 359), 0, 0));
            }
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
		addPlayerTimer += Time.deltaTime;
		speedTimer += Time.deltaTime;

		if(addPlayerTimer > addNewPlayerTime)
		{
			GameObject go = (GameObject)Instantiate(enemyPrefab,spawnPlace.transform.position, Quaternion.identity);

			addPlayerTimer = 0;
		}

		if(speedTimer > speedupTimeInterval)
		{
			if(addNewPlayerTime > 0)
			{
				addNewPlayerTime -= speedupIncrement;
			}
			speedTimer = 0;
		}
	}
}





