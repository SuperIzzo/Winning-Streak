using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioMan : MonoBehaviour
{
	public MyAudioSource audioSourceType;

	public AudioClip ambience;
	public List<AudioClip> soloDetailSounds;
	public List<AudioClip> massDetailSounds;

	public float hype;
	public float hypeDecay = 0.01f;
	public float minHype = 0.2f;
	public float maxHype = 1.0f;


	// Use this for initialization
	void Start () 
	{
		audio.clip = ambience;
		audio.Play();
		//ambience.
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Hype stuff, the crowd hype dies gradually
		hype *= hypeDecay;
 		hype = Mathf.Clamp( hype, minHype, maxHype );
		audio.volume = hype;

		float hypeChance = hype * 0.1f; // 10% is a lot

		if( Random.value < hypeChance )
		{
			if( Random.value > 0.6f )
			{
				float pan = 0.3f - Random.value * 0.6f;
				float vol = Mathf.Min(1, Random.value * hype + 0.3f); 

				AudioClip clip = massDetailSounds[ Random.Range(0, massDetailSounds.Count) ];

				CreateSound( clip, pan, vol );
			}
			else
			{
				float pan = 0.5f - Random.value * 1f;
				float vol = Mathf.Min(1, Random.value * hype + 0.3f); 
				
				AudioClip clip = soloDetailSounds[ Random.Range(0, massDetailSounds.Count) ];
				
				CreateSound( clip, pan, vol );
			}
		}
	
	}


	void CreateSound( AudioClip clip, float relPan, float relVolume )
	{
		MyAudioSource source = (MyAudioSource) Instantiate( audioSourceType );

		source.SetAudio( clip, relPan, relVolume );
	}
}
