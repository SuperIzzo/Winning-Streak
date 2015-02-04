using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
	public GameObject prefab;
	public float initialSpawnDelay = 0;
	public float spawnDelay = 0;
	public int spawnAmount = 1;
	public int spawnLimit = 1;
	public Transform[] spawnAreas;

	private float spawnTimer;
	private int spawnCounter;

	// Use this for initialization
	void Start ()
	{
		spawnTimer = initialSpawnDelay;
	}
	
	// Update is called once per frame
	void Update ()
	{
		spawnTimer -= Time.deltaTime;

		if( spawnTimer<=0 )
		{
			spawnTimer = spawnDelay;

			for( int i=0; i<spawnAmount; i++ )
			{
				Spawn();
			}
		}
	}

	void Spawn()
	{
		if( spawnCounter<spawnLimit )
		{
			spawnCounter++;
			Vector3 randomPosition = transform.position;

			if( spawnAreas.Length>0 )
			{
				int areaIdx = Random.Range(0,spawnAreas.Length);
				Transform area = spawnAreas[areaIdx];

				if( area.collider )
				{
					Vector3 min = area.collider.bounds.min;
					Vector3 max = area.collider.bounds.max;

					randomPosition.x = Random.Range( min.x, max.x );
					randomPosition.y = Random.Range( min.y, max.y );
					randomPosition.z = Random.Range( min.z, max.z );
				}
				else
				{
					randomPosition = area.position;
				}
			}

			GameObject.Instantiate( prefab, randomPosition, Quaternion.identity );
		}
	}
}
