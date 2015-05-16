/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                          Spawner.cs                         </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    17-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A general purpose spawner. </summary>
//-------------------------------------- 
public class Spawner : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Public settings
	//--------------------------------------
	public GameObject prefab;
	public float initialSpawnDelay = 0;
	public float spawnDelay = 0;
	public int spawnAmount = 1;
	public int spawnLimit = 1;
	public Transform[] spawnAreas;
	#endregion


	//--------------------------------------------------------------
	#region Private state
	//--------------------------------------
	private float spawnTimer;
	private int spawnCounter;
	#endregion

	//--------------------------------------------------------------
	/// <summary> Start callback. </summary>
	//--------------------------------------
	void Start ()
	{
		spawnTimer = initialSpawnDelay;
	}

	//--------------------------------------------------------------
	/// <summary> Update callback. </summary>
	//--------------------------------------
	void Update()
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

	//--------------------------------------------------------------
	/// <summary> Spawns a single instance. </summary>
	//--------------------------------------
	void Spawn()
	{
		if( spawnCounter<spawnLimit )
		{
			spawnCounter++;
			Vector3 randomPosition = transform.position;

			Transform randomArea = GetRandomArea();
			randomPosition = GetRandomAreaPoint( randomArea );

			GameObject.Instantiate( prefab, randomPosition, Quaternion.identity );
		}
	}

	//--------------------------------------------------------------
	/// <summary> Returns a random spawn area </summary>
	//--------------------------------------
	Transform GetRandomArea()
	{
		Transform spawnArea = this.transform;

		if( spawnAreas.Length>0 )
		{
			int areaIdx = Random.Range(0,spawnAreas.Length);
			spawnArea = spawnAreas[areaIdx];
		}

		return spawnArea;
	}

	//--------------------------------------------------------------
	/// <summary> Returns a random 3D point on an area. </summary>
	//--------------------------------------
	Vector3 GetRandomAreaPoint( Transform area )
	{
		Vector3 randomPosition = area.position;

		if( area.GetComponent<Collider>() )
		{
			Vector3 min = area.GetComponent<Collider>().bounds.min;
			Vector3 max = area.GetComponent<Collider>().bounds.max;
			
			randomPosition.x = Random.Range( min.x, max.x );
			randomPosition.y = Random.Range( min.y, max.y );
			randomPosition.z = Random.Range( min.z, max.z );
		}

		return randomPosition;
	}
}
