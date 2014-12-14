using UnityEngine;
using System.Collections;

public class HitNoise : MonoBehaviour
{
	public AudioClip hitSound;
	public float collsionPowerRate = 5;

	float soundTimer = 5.0f;

	void Update()
	{
		if( soundTimer>0 )
			soundTimer -= Time.deltaTime;
	}

	void OnCollisionEnter(Collision collision)
	{
		if( audio && hitSound && soundTimer<=0 )
		{
			float collisionPower = collision.relativeVelocity.magnitude;
			if( collisionPower > 2 )
			{
				audio.clip = hitSound;
				audio.volume = Mathf.Clamp01( collisionPower/collsionPowerRate );
				audio.Play();
			}
		}
	}
}
