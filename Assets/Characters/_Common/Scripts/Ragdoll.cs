using UnityEngine;
using System.Collections;

using System.Collections.Generic;	

//--------------------------------------------------------------
/// <summary> Ragdoll character "animator". </summary>
/// <description> Puts a rigged character into a ragdoll mode
/// disabling the character animator, and back.
/// </description>
//--------------------------------------
public class Ragdoll : MonoBehaviour
{
	public Animator animator;
	public bool automaticPartsList = true;
	public List<Rigidbody> parts;

	// Internal property state
	private bool _activated = false;

	//--------------------------------------------------------------
	/// <summary> Gets or sets a value indicating whether this 
	/// <see cref="Ragdoll"/> is activated. </summary>
	/// <value><c>true</c> if activated; otherwise, <c>false</c>.</value>
	//--------------------------------------
	public bool activated 
	{
		get{ return _activated; }
		set
		{
			if( _activated != value )
			{
				_activated = value;
				if( _activated )
					ActivateRagdoll();
				else
					DeactivateRagdoll();
			}
		}
	}

	//--------------------------------------------------------------
	/// <summary> Activates the ragdoll and disables other 
	/// character animations. </summary>
	//--------------------------------------
	void ActivateRagdoll()
	{
		if( animator )
			animator.enabled = false;

		if( automaticPartsList )
			UpdateParts();

		foreach(Rigidbody part in parts)
		{
            if (part)
            {
                part.isKinematic = false;

                // Jake: stops the infamous helicopter arms 
				// Izzo: this will work for now, might be a problem if we change the rig
                if (part.name == "lShldr" || part.name == "rShldr" || part.name == "lForeArm" || part.name == "rForeArm")
                {
                    part.freezeRotation = true;
                }            
            }
		}
	}
	
	//--------------------------------------------------------------
	/// <summary> Deactivates the ragdoll and re-enables other 
	/// character animations. </summary>
	//--------------------------------------
	void DeactivateRagdoll () 
	{
		if( animator )
			animator.enabled = true;

		if( automaticPartsList )
			UpdateParts();

		foreach(Rigidbody part in parts)
		{
			if( part )
            {
				part.isKinematic = true;
                part.freezeRotation = false;
            }
		}
	}

	//--------------------------------------------------------------
	/// <summary> Automatically updates the ragdoll parts list </summary>
	//--------------------------------------
	void UpdateParts()
	{
		parts = new List<Rigidbody>( GetComponentsInChildren<Rigidbody>() );
	}
}
