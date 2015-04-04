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
			GetComponent<AudioSource>().volume = vol;
			GetComponent<AudioSource>().panStereo = pan;
			GetComponent<AudioSource>().clip = audioClip;
			GetComponent<AudioSource>().Play();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
        GetComponent<AudioSource>().volume *= CrowdManager.MasterVolume;
		if( audioClip==null || GetComponent<AudioSource>()==null || !GetComponent<AudioSource>().isPlaying )
		{
			Destroy( gameObject );
		}
	}
}
