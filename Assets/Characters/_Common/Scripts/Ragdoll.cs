using UnityEngine;
using System.Collections;

using System.Collections.Generic;	

public class Ragdoll : MonoBehaviour
{
	public Animator animator;
	public List<Rigidbody> parts;

	private bool _activated = false; 
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


	// Use this for initialization
	void ActivateRagdoll()
	{
		if( animator )
			animator.enabled = false;

		foreach(Rigidbody part in parts)
		{
            if (part)
            {
                part.isKinematic = false;

                //stops the infamous helicopter arms 
                if (part.name == "lShldr" || part.name == "rShldr" || part.name == "lForeArm" || part.name == "rForeArm")
                {
                    part.freezeRotation = true;
                }

                
            }
		}
	}
	
	// Update is called once per frame
	void DeactivateRagdoll () 
	{
		if( animator )
			animator.enabled = true;

		foreach(Rigidbody part in parts)
		{
			if( part )
            {
				part.isKinematic = true;
                part.freezeRotation = false;
            }
		}
	}
}
