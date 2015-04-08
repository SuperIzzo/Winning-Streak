using UnityEngine;
using System.Collections;

//-------------------------------------------------------------
/// <summary> An utility class that has the responsibility of
/// 	      destroying the gameObject when the audio clip
///	      stops playing </summary>
//--------------------------------------
public class DestroyOnAudioSourceCompletion : MonoBehaviour
{
	AudioSource audio;

	void Start()
	{
		audio = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update () 
	{
		if( audio==null || !audio.isPlaying )
		{
			Destroy( gameObject );
		}
	}
}
