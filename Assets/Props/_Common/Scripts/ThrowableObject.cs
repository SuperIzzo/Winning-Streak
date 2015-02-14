using UnityEngine;
using System.Collections;

//--------------------------------------------------------------
/// <summary> A base class for all throwable objects. </summary>
// TODO: Inerit from Grabbable and move logic common to both throwable and usable to there
//--------------------------------------
public class ThrowableObject : MonoBehaviour
{
	//--------------------------------------------------------------
	#region Public settings
	//--------------------------------------
	public float knockOutPower = 5.0f;
	public bool  isThrown = false;
	public BaseCharacterController owner {get; private set;}
	#endregion


	//--------------------------------------------------------------
	#region Priavate references
	//--------------------------------------
	private Transform slot;
	#endregion


	//--------------------------------------------------------------
	/// <summary> Update is called once per frame </summary>
	//--------------------------------------
	void Update ()
	{
		if( slot && transform.parent )
		{
			transform.localPosition = Vector3.Lerp( transform.localPosition, Vector3.zero, 0.2f );
		}
	}

	//--------------------------------------------------------------
	/// <summary> Called when the throwable is grabbed. </summary>
	/// <param name="character">The grabbing character.</param>
	/// <param name="propSlot">The slot for the prop.</param>
	//--------------------------------------
	public void OnGrabbed( BaseCharacterController character, Transform propSlot )
	{
		owner = character;
		slot = propSlot;

		if( rigidbody )
			rigidbody.isKinematic = true;

		if( collider )
			collider.isTrigger = true;
		
		// Get addopted by the hand/slot
		// We keep the world position, as we'll animate the grabbing
		transform.SetParent( slot, true );
	}

	//--------------------------------------------------------------
	/// <summary> Called when the prop is thrown. </summary>
	/// <param name="character">The throwing character.</param>
	/// <param name="force">The force at which the object is thrown.</param>
	//--------------------------------------
	public void OnThrown( BaseCharacterController character, Vector3 force )
	{
        if (collider)
			collider.isTrigger = false;

		if( rigidbody )
		{
			rigidbody.isKinematic = false;
			rigidbody.AddForce( force , ForceMode.Impulse );
		}

        // Unlink, note we keep it in world space
        transform.SetParent(null, true);
        slot = null;
		isThrown = true;
	}

	//--------------------------------------------------------------
	/// <summary> Raises the collision enter event. </summary>
	/// <param name="collision">Collision data.</param>
	//--------------------------------------
	void OnCollisionEnter( Collision collision )
	{
		if( isThrown )
		{
			var character =  collision.collider.GetComponentInChildren<BaseCharacterController>();

			if( character )
			{
				if( character!=owner )
				{
					isThrown = false;
					owner = null;

					if( collision.relativeVelocity.magnitude > knockOutPower 
					   	&& rigidbody.velocity.magnitude		 > knockOutPower)
					{
		                Debug.Log("Relative vel: " + collision.relativeVelocity.magnitude);
						character.KnockDown();
					}
				}
			}
			else
			{
				isThrown = true;
				owner = null;
			}
		}
	}
}
