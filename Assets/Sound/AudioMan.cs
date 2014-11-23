using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioMan : MonoBehaviour
{
	public MyAudioSource audioSourceType;

	public AudioClip ambience;
	public AudioClip introduction;
	public List<AudioClip> soloDetailSounds;
	public List<AudioClip> massDetailSounds;

	public List<AudioClip> celebrateSounds;
	public List<AudioClip> whistleSounds;
	public List<AudioClip> tackledSounds;

	public List<AudioClip> effectSounds;

	public List<AudioClip> tackleDodgeSounds;
	float dodgeSpeechCooldown = 10;
	float dodgeSpeechTimer = 5;

	public List<AudioClip> randomSpeechSounds;

	public List<AudioClip> changeHatSounds;
	int lastHatSound;

	public List<AudioClip> commentatorSpeechSounds;

	public float hype;
	public float hypeDecay = 0.01f;
	public float minHype = 0.2f;
	public float maxHype = 1.0f;

	bool inIntroduction = true;
	bool introSoundPlaying = false;

	bool buildingHype = false;
	float increment = 0;
	float count = 0;
	float interval = 0;

	// Use this for initialization
	void Start () 
	{
		audio.clip = ambience;
		audio.loop = true;
		audio.Play();

		lastHatSound = Random.Range(0, changeHatSounds.Count);
	}

	bool playedCredits = false;
	
	// Update is called once per frame
	void Update ()
	{
		if(Application.loadedLevelName == "menu")
		{
			if(!introSoundPlaying)
			{
				float pan = 0.5f - Random.value * 1f;
				float vol = Mathf.Min(1, Random.value * hype + 0.3f); 
				
				CreateSound( introduction, pan, vol );

				introSoundPlaying = true;
			}

			minHype = 0.1f;
		}
		else minHype = 0.2f;

		if(Application.loadedLevelName == "credits" && !playedCredits)
		{
			PlayCelebrate();
			playedCredits = true;
		}

		dodgeSpeechTimer += Time.deltaTime;

		// Hype stuff, the crowd hype dies gradually
		if(!buildingHype)
			hype -= hypeDecay;

 		hype = Mathf.Clamp( hype, minHype, maxHype );
		audio.volume = hype;

		float hypeChance = hype * 0.01f; // 1%, 60 frames per second, hype * 60% per second

		if( Random.value < hypeChance )
		{
			if( Random.value < 0.2f )
			{
				float pan = 0.3f - Random.value * 0.6f;
				float vol = Mathf.Min(0.3f, Random.value * hype + 0.1f); 
				int random = Random.Range(0, massDetailSounds.Count - 1);
				AudioClip clip = massDetailSounds[ random ];

				CreateSound( clip, pan, vol );
			}

			if( Random.value < 0.3f )
			{
				float pan = 0.5f - Random.value * 1f;
				float vol = Mathf.Min(1, Random.value * hype + 0.3f); 
				int random = Random.Range(0, massDetailSounds.Count - 1);
				AudioClip clip = soloDetailSounds[ random ];
				
				CreateSound( clip, pan, vol );
			}

			if( Random.value < 0.1f )
			{
				float pan = 0.3f - Random.value * 0.6f;
				float vol = Mathf.Min(1, Random.value * hype + 0.1f); 
				int random = Random.Range(0, whistleSounds.Count - 1);
				AudioClip clip = whistleSounds[ random ];
				
				CreateSound( clip, pan, vol );
			}
		}
	}

	public void PlayEffect(string effect, float volume)
	{

		float pan = 0.3f - Random.value * 0.6f;
		float vol = volume; 
		
		int random = 0;

		for(int i = 0; i < effectSounds.Count; i++)
		{
			if(effectSounds[i].name == effect)
			{
				AudioClip clip = effectSounds[i];
				CreateSound( clip, pan, vol );

				return;
			}
		}
	}

	public void PlaySpeech(string effect)
	{
		
		float pan = 0.3f - Random.value * 0.6f;
		float vol = 1; 
		
		int random = 0;
		
		for(int i = 0; i < commentatorSpeechSounds.Count; i++)
		{
			if(commentatorSpeechSounds[i].name == effect)
			{
				AudioClip clip = commentatorSpeechSounds[i];
				CreateSound( clip, pan, vol );
				
				return;
			}
		}
	}

	public void PlayChangeHat()
	{
		float pan = 0.3f - Random.value * 0.6f;
		float vol = 1; 

		int random = 0;

		do
		{
			random = Random.Range(0, changeHatSounds.Count - 1);
		}while(random == lastHatSound);

		lastHatSound = random;

		AudioClip clip = changeHatSounds[ random ];
		
		CreateSound( clip, pan, vol );
	}

	public void PlayCelebrate()
	{
		BuildHype(0.02f, 30, 0.03f);

		float pan = 0.3f - Random.value * 0.6f;
		float vol = Mathf.Min(1, Random.value * hype + 0.5f); 
		int random = Random.Range(0, celebrateSounds.Count - 1);
		AudioClip clip = celebrateSounds[ random ];
		
		CreateSound( clip, pan, vol ); 
	}

	public void PlayTackleDodge()
	{
		if(dodgeSpeechTimer < dodgeSpeechCooldown)
			return;

		BuildHype(0.02f, 15, 0.01f);
		
		float pan = 0.3f - Random.value * 0.6f;
		float vol = Mathf.Min(1, Random.value * hype + 0.5f); 
		int random = Random.Range(0, tackleDodgeSounds.Count - 1);
		AudioClip clip = tackleDodgeSounds[ random ];
		
		CreateSound( clip, pan, vol );

		dodgeSpeechTimer = 0;
	}

	public void PlayTackled()
	{
		BuildHype(0.02f, 15, 0.01f);
		
		float pan = 0.3f - Random.value * 0.6f;
		float vol = Mathf.Min(1, Random.value * hype + 0.5f); 
		int random = Random.Range(0, tackledSounds.Count - 1);
		AudioClip clip = tackledSounds[ random ];
		
		CreateSound( clip, pan, vol );
	}

	public void BuildHype(float incre, float cnt, float inte)
	{
		increment = incre;
		count = cnt;
		interval = inte;

		StartCoroutine("ChangeTheHype");
	}

	public void LowerHype(float incre, float cnt, float inte)
	{
		increment = -incre;
		count = cnt;
		interval = inte;
		
		StartCoroutine("ChangeTheHype");
	}

	//used to raise and hold the hype to the maximum
	public void KeepHypeUp()
	{
		hype += Time.deltaTime / 10;
	}

	public float GetHype()
	{
		return hype;
	}

	IEnumerator ChangeTheHype()
	{
		buildingHype = true;
		float timer = 0;

		while(timer < count)
		{
			timer++;

			hype += increment;

			yield return new WaitForSeconds(interval);
		}

		buildingHype = false;
	}

//	void OnGUI()
//	{
//		if(GUI.Button(new Rect(100,100,200,50),"CHEER"))
//			PlayCelebrate();
//
//		if(GUI.Button(new Rect(100,160,200,50),"HYPE UP"))
//			BuildHype(0.01f, 20, 0.1f);
//
//		if(GUI.Button(new Rect(100,240,200,50),"HYPE DOWN"))
//			LowerHype(0.01f, 20, 0.1f);
//	}


	void CreateSound( AudioClip clip, float relPan, float relVolume )
	{
		//MyAudioSource source = (MyAudioSource)Instantiate( audioSourceType );
		GameObject go = new GameObject();

		go.AddComponent<MyAudioSource>();
		go.AddComponent<AudioSource>();
		go.GetComponent<MyAudioSource>().SetAudio(clip,relPan,relVolume);

		//source.SetAudio( clip, relPan, relVolume );
	}
}
