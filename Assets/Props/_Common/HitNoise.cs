using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> Hit noise generator component. </summary>
//--------------------------------------
[AddComponentMenu("Audio/Hit Noise",100)]
public class HitNoise : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Public settings
	//--------------------------------------
	public AudioClip hitSound;
	public float collsionPowerRate = 5;
	#endregion


	//--------------------------------------------------------------
	#region Private state
	//--------------------------------------
	float soundTimer = 5.0f;
	#endregion


	//--------------------------------------------------------------
	/// <summary> Update this instance. </summary>
	//--------------------------------------
	void Update()
	{
		if( soundTimer>0 )
			soundTimer -= Time.deltaTime;
	}

	//--------------------------------------------------------------
	/// <summary> Raises the collision enter event. </summary>
	/// <param name="collision">Collision.</param>
	//--------------------------------------
	void OnCollisionEnter(Collision collision)
	{
		if( GetComponent<AudioSource>() && hitSound && soundTimer<=0 )
		{
			float collisionPower = collision.relativeVelocity.magnitude;
			if( collisionPower > 2 )
			{
				GetComponent<AudioSource>().clip = hitSound;
				GetComponent<AudioSource>().volume = Mathf.Clamp01( collisionPower/collsionPowerRate );
				GetComponent<AudioSource>().Play();
			}
		}
	}
}
