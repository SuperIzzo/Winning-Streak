/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                   RandomCharacterShouts.cs                  </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    14-Feb-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class RandomCharacterShouts : MonoBehaviour
{
	public AudioClip[] clips;
	public float minTime;
	public float maxTime;

	private BaseCharacterController controller;

	// Use this for initialization
	void Start ()
	{
		controller = GetComponent<BaseCharacterController>();
		StartCoroutine( PlayAudio() );
	}
	
	// Update is called once per frame
	IEnumerator PlayAudio ()
	{
		while( true )
		{
			for( float timer = Random.Range( minTime, maxTime ); timer>0; timer-=Time.deltaTime )
			{
				yield return 0;
			}

			// Play sounds only of the character's not dead
			if( controller==null || !controller.isKnockedDown )
			{
				GetComponent<AudioSource>().clip = clips[ Random.Range(0, clips.Length) ];
				GetComponent<AudioSource>().Play();
			}
		}
	}
}
