using UnityEngine;
using System.Collections;

// TODO: Inerit from Grabbable and move logic common to both throwable and usable to there
public class ThrowableObject : MonoBehaviour
{
	public float knockOutPower = 1.0f;
	private Transform slot;
	private BaseCharacterController thrower;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		if( slot && transform.parent )
		{
			transform.localPosition = Vector3.Lerp( transform.localPosition, Vector3.zero, 0.2f );
		}
	}
	
	public void OnGrabbed( BaseCharacterController character, Transform propSlot )
	{
		thrower = character;
		slot = propSlot;

		if( rigidbody )
			rigidbody.isKinematic = true;

		if( collider )
			collider.enabled = false;
		
		// Get addopted by the hand/slot
		// We keep the world position, as we'll animate the grabbing
		transform.SetParent( slot, true );
	}

	public void OnThrown( BaseCharacterController character, Vector3 force )
	{
		//Debug.Break();
		
		if( collider )
			collider.enabled = true;

		if( rigidbody )
		{
			rigidbody.isKinematic = false;
			rigidbody.AddForce( force, ForceMode.Impulse );
		}

		// Unlink
		transform.SetParent( null, true );
		slot = null;
	}

	void OnCollisionEnter( Collision collision )
	{
		BaseCharacterController character =  collision.collider.GetComponent<BaseCharacterController>();
		if( character && character!=thrower )
		{
			Debug.Log( "Relative vel: " + collision.relativeVelocity.magnitude );
			if( collision.relativeVelocity.magnitude > knockOutPower 
			   	&& rigidbody.velocity.magnitude		 > knockOutPower)
			{
				character.KnockDown();
			}
		}
	}
}
