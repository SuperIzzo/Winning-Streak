using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CrowdManager : MonoBehaviour
{
	public MyAudioSource audioSourceType;

	public AudioClip _ambience;
	public AudioClip _introduction;
	public List<AudioClip> _soloDetailSounds;
	public List<AudioClip> _massDetailSounds;

	// Inspector properties
	public float _soloDetailRate = 0.3f;
	public float _massDetailRate = 0.2f;
	public float _hype;
	public float _hypeDecay = 0.01f;
	public float _minHype = 0.2f;
	public float _maxHype = 1.0f;
	
	private float _gradualHype;
	private float _gradualHypeTransition = 0.1f;

    public static float MasterVolume = 1;

	public float hype
	{ 
		get{ return _hype; }
		set{ _hype = Mathf.Clamp( value, _minHype, _maxHype ); }
	}

	// Use this for initialization
	void Start () 
	{
		audio.clip = _ambience;
		audio.loop = true;
		audio.Play();

		_gradualHype = hype;

		StartCoroutine( SoloDetail() );
		StartCoroutine( MassDetail() );
	}

	void Update ()
	{
		hype -= _hypeDecay * Time.deltaTime;

		_gradualHype = 	hype 		 * _gradualHypeTransition +
						_gradualHype * (1 - _gradualHypeTransition);

		audio.volume = _gradualHype;
        audio.volume *= MasterVolume;
	}

	IEnumerator MassDetail()
	{
		while( true )
		{
			// Once a second
			yield return new WaitForSeconds(1);

			if( Random.value < _massDetailRate * _gradualHype )
			{
				float pan = 0.3f - Random.value * 0.6f;
				float vol = Mathf.Min(0.3f, Random.value * _gradualHype + 0.1f);
                vol *= MasterVolume;
				int random = Random.Range(0, _massDetailSounds.Count);
				AudioClip clip = _massDetailSounds[ random ];
				
				CreateSound( clip, pan, vol );
			}
		}
	}

	IEnumerator SoloDetail()
	{
		while( true )
		{
			// Once a second
			yield return new WaitForSeconds(1);
			
			if( Random.value < _soloDetailRate * _gradualHype )
			{
				float pan = 0.5f - Random.value * 1f;
				float vol = Mathf.Min(1, Random.value * _gradualHype + 0.3f);
                vol *= MasterVolume;
				int random = Random.Range(0, _massDetailSounds.Count);
				AudioClip clip = _soloDetailSounds[ random ];
				
				CreateSound( clip, pan, vol );
			}
		}
	}

	void CreateSound( AudioClip clip, float relPan, float relVolume )
	{
		//MyAudioSource source = (MyAudioSource)Instantiate( audioSourceType );
		GameObject go = new GameObject();

		// Parent the sound effect
		go.transform.parent = transform;

		go.AddComponent<MyAudioSource>();
		go.AddComponent<AudioSource	>();
		go.GetComponent<MyAudioSource>().SetAudio(clip,relPan,relVolume);

		//source.SetAudio( clip, relPan, relVolume );
	}
}
