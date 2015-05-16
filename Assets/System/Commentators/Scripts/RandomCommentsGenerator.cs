/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                  RandomCommentsGenerator.cs                 </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    26-Mar-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

public class RandomCommentsGenerator : MonoBehaviour
{
	public float minSilence = 5.0f;
	public float maxSilence = 10.0f;

	private float silenceTime = 0;

	// Update is called once per frame
	void Update ()
	{
		if( Time.unscaledTime - Commentator.timeSinceLastComment > silenceTime )
		{
			silenceTime = Random.Range(minSilence, maxSilence);
			Commentator.Comment( CommentatorEvent.RANDOM );
		}
	}
}
