using UnityEngine;
using System.Collections;

public class MyAudioSource : MonoBehaviour
{
	public AudioClip audioClip;
	float pan;
	float vol;

	public void SetAudio( AudioClip clip, float relPan, float relVol )
	{
		audioClip = clip; 
		pan = relPan;
		vol = relVol;
	}

	// Use this for initialization
	void Start ()
	{
		if( audioClip )
		{
			audio.volume = vol;
			audio.pan = pan;
			audio.clip = audioClip;
			audio.Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        audio.volume *= CrowdManager.MasterVolume;
		if( audioClip==null || audio==null || !audio.isPlaying )
		{
			Destroy( gameObject );
		}
	}
}
