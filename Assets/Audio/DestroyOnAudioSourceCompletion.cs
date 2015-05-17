/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>              DestroyOnAudioSourceCompletion.cs              </file> * 
 *                                                                            * 
 * <copyright>                                                                * 
 *   Copyright (C) 2015  Roaring Snail Limited - All Rights Reserved          * 
 *                                                                            * 
 *   Unauthorized copying of this file, via any medium is strictly prohibited * 
 *   Proprietary and confidential                                             * 
 *                                                               </copyright> * 
 *                                                                            * 
 * <author>  Hristoz Stefanov                                       </author> * 
 * <date>    08-Apr-2015                                              </date> * 
|*                                                                            *|
\** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **/
using UnityEngine;
using System.Collections;

//-------------------------------------------------------------
/// <summary> An utility class that has the responsibility of
/// 	      destroying the gameObject when the audio clip
///	      stops playing </summary>
//--------------------------------------
public class DestroyOnAudioSourceCompletion : MonoBehaviour
{
	AudioSource _audio;

	void Start()
	{
		_audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		if( _audio == null || !_audio.isPlaying )
		{
			Destroy( gameObject );
		}
	}
}
