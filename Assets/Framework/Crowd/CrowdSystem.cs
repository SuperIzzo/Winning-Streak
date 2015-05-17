/** -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=- **\
|*                                                                            *|
 * <file>                        CrowdSystem.cs                       </file> * 
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
using System.Collections.Generic;

//--------------------------------------------------------------
/// <summary>	Crowd system.
/// 		The thing that deals with the crowd. </summary>
//--------------------------------------
public class CrowdSystem : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Inspector settings
	//--------------------------------------
	[SerializeField] AudioClip          _ambience = null;
	[SerializeField] List<AudioClip>    _soloDetailSounds = null;
	[SerializeField] List<AudioClip>    _massDetailSounds = null;

	[SerializeField] private float _soloDetailRate = 0.3f;
	[SerializeField] private float _massDetailRate = 0.2f;
	[SerializeField] private float _hype;
	[SerializeField] private float _hypeDecay = 0.01f;
	[SerializeField] private float _minHype = 0.2f;
	[SerializeField] private float _maxHype = 1.0f;
	#endregion


	//--------------------------------------------------------------
	#region Private settings
	//--------------------------------------
	private float _gradualHype;
	private float _gradualHypeTransition = 0.1f;
	#endregion


	//--------------------------------------------------------------
	/// <summary> Gets or sets the crowd's hype level. </summary>
	/// <value>The hype.</value>
	//--------------------------------------
	public float hype
	{ 
		get{ return _hype; }
		set{ _hype = Mathf.Clamp( value, _minHype, _maxHype ); }
	}

	//--------------------------------------------------------------
	/// <summary> Initializes this instance. </summary>
	//--------------------------------------
	void Start () 
	{
		AudioSource audio = GetComponent<AudioSource>();
		if( audio )
		{
			audio.clip = _ambience;
			audio.loop = true;
			audio.Play();
		}

		_gradualHype = hype;

		StartCoroutine( SoloDetail() );
		StartCoroutine( MassDetail() );
	}

	//--------------------------------------------------------------
	/// <summary> Updates the instance. </summary>
	//--------------------------------------
	void Update ()
	{
		hype -= _hypeDecay * Time.deltaTime;

		_gradualHype = 	hype 		 * _gradualHypeTransition +
						_gradualHype * (1 - _gradualHypeTransition);

		GetComponent<AudioSource>().volume = _gradualHype;
	}

	//--------------------------------------------------------------
	/// <summary> Mass detail sound effect production. </summary>
	/// <description>
	/// 	Mass detail sound effects are such that play multiple
	/// sounds at once - like horns, people, whistles. This is a
	/// continuos couroutine (worker) and generates sounds
	/// periodically based on hype and chance.
	/// </description>
	//--------------------------------------
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
				int random = Random.Range(0, _massDetailSounds.Count);
				AudioClip clip = _massDetailSounds[ random ];
				
				AudioUtils.PlayOneShot( this, clip, pan, vol );
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Solos the detail. </summary>
	/// <description>
	/// 	Solo detail sound effects are indivudual effects. This 
	/// is a continuos couroutine (worker) and generates sounds
	/// periodically based on hype and chance.
	/// </description>
	//--------------------------------------
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
				int random = Random.Range(0, _massDetailSounds.Count);
				AudioClip clip = _soloDetailSounds[ random ];
				
				AudioUtils.PlayOneShot( this, clip, pan, vol );
			}
		}
	}
}

//--------------------------------------------------------------
/// <summary> Crowd. </summary>
//--------------------------------------
public static class Crowd
{
	//-------------------------------------------------------------
	/// <summary> This is the currently active
	///           Crowd. </summary>
	/// <value>The active crowd.</value>
	//--------------------------------------
	public static CrowdSystem Active
	{
		set{ _active = value; }
		get
		{
			if( _active == null )
			{
				_active = GameObject.FindObjectOfType<CrowdSystem>();
			}

			return _active;
		}
	}
	private static CrowdSystem _active;

	//--------------------------------------------------------------
	/// <summary> Gets or sets the crowd hype. </summary>
	/// <value>The hype.</value>
	//--------------------------------------
	public static float hype
	{
		get
		{
			CheckActive();
			return Active.hype;
		}
		set
		{
			CheckActive();
			Active.hype = value;
		}
	}

	//-------------------------------------------------------------
	/// <summary> Tests if the Active instance is valid </summary>
	//--------------------------------------
	private static void CheckActive()
	{
		if( Active == null )
			throw new MissingReferenceException( 
			    "Crowd.Active is not set to a valid " +
	                    "instance.\nTry adding a CrowdSystem " +
	                    "component to the scene.");
	}
}
